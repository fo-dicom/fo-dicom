// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Tests.Helpers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH265
    {
        #region Unit tests

        [Fact]
        public void DicomFileOpen_StreamedObject_NoMetaInfo()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));
            Assert.Equal(DicomFileFormat.DICOM3NoFileMetaInfo, file.Format);
            Assert.Empty(file.FileMetaInfo);
        }

        [Fact]
        public void DicomFileSave_StreamedObject_ShouldContainMetaInfo()
        {
            var tempName = Path.GetTempFileName();
            DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest")).Save(tempName);

            using (var stream = File.OpenRead(tempName))
            {
                var file = DicomFile.Open(stream);
                Assert.Equal(DicomFileFormat.DICOM3, file.Format);
                Assert.True(file.FileMetaInfo.Contains(DicomTag.MediaStorageSOPClassUID));
                Assert.True(file.FileMetaInfo.Contains(DicomTag.MediaStorageSOPInstanceUID));
            }
            IOHelper.DeleteIfExists(tempName);
        }

        [Fact]
        public async Task DicomFileSaveAsync_Part10File_PersistentMetaInfo()
        {
            DicomFileMetaInformation expected;
            var tempName = Path.GetTempFileName();

            using (var stream = File.OpenWrite(tempName))
            {
                var input = await DicomFile.OpenAsync(TestData.Resolve("CT-MONO2-16-ankle"));
                expected = input.FileMetaInfo;
                await input.SaveAsync(stream);
            }

            var output = DicomFile.Open(tempName);
            var actual = output.FileMetaInfo;

            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(expected.TransferSyntax.UID.UID, actual.TransferSyntax.UID.UID);

            IOHelper.DeleteIfExists(tempName);
        }

        [Fact]
        public async Task DicomFileSave_Part10File_UpdatedMetaInfo()
        {
            var tempName = Path.GetTempFileName();

            var input = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            var expected = input.FileMetaInfo;
            await input.SaveAsync(tempName);

            using (var stream = File.OpenRead(tempName))
            {
                var output = await DicomFile.OpenAsync(stream);
                var actual = output.FileMetaInfo;

                Assert.NotEqual(expected.ImplementationClassUID.UID, actual.ImplementationClassUID.UID);
                Assert.NotEqual(expected.ImplementationVersionName, actual.ImplementationVersionName);
                Assert.Equal(expected.SourceApplicationEntityTitle, actual.SourceApplicationEntityTitle);
            }
            IOHelper.DeleteIfExists(tempName);
        }

        [Fact]
        public void DicomFileOpen_Part10File_MetaInfoUnmodified()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var metaInfo = file.FileMetaInfo;

            Assert.Equal(new byte[] { 0, 1 }, metaInfo.Version);
            Assert.Equal("1.2.840.10008.5.1.4.1.1.2", metaInfo.MediaStorageSOPClassUID.UID);
            Assert.Equal("1.3.6.1.4.1.5962.1.1.1.1.3.20040826185059.5457", metaInfo.MediaStorageSOPInstanceUID.UID);
            Assert.Equal("1.2.840.10008.1.2.4.91", metaInfo.TransferSyntax.UID.UID);
            Assert.Equal("1.3.6.1.4.1.5962.2", metaInfo.ImplementationClassUID.UID);
            Assert.Equal("DCTOOL100", metaInfo.ImplementationVersionName);
            Assert.Equal("CLUNIE1", metaInfo.SourceApplicationEntityTitle);
        }

        #endregion
    }
}
