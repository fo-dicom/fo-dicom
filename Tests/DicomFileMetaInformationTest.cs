// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.IO;
    using System.Threading.Tasks;

    using Xunit;

    public class DicomFileMetaInformationTest
    {
        #region Unit tests

        [Fact]
        public void ImplementationVersionName_GetterWhenAttributeIncluded_ReturnsValue()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));

            var exception = Record.Exception(() => { Assert.NotNull(metaInfo.ImplementationVersionName); });
            Assert.Null(exception);
        }

        [Fact]
        public void ImplementationVersionName_GetterWhenAttributeMissing_ReturnsNull()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));
            metaInfo.Remove(DicomTag.ImplementationVersionName);

            var exception = Record.Exception(() => { Assert.Null(metaInfo.ImplementationVersionName); });
            Assert.Null(exception);
        }

        [Fact]
        public void SourceApplicationEntityTitle_GetterWhenAttributeIncluded_ReturnsValue()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));

            var exception = Record.Exception(() => { Assert.NotNull(metaInfo.SourceApplicationEntityTitle); });
            Assert.Null(exception);
        }

        [Fact]
        public void SourceApplicationEntityTitle_GetterWhenAttributeMissing_ReturnsNull()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));
            metaInfo.Remove(DicomTag.SourceApplicationEntityTitle);

            var exception = Record.Exception(() => { Assert.Null(metaInfo.SourceApplicationEntityTitle); });
            Assert.Null(exception);
        }

        [Fact]
        public void Constructor_FromFileMetaInformation_ShouldNotThrow()
        {
            var metaInfo = new DicomFileMetaInformation();
            var exception = Record.Exception(() => new DicomFileMetaInformation(metaInfo));
            Assert.Null(exception);
        }

        [Fact]
        public void DicomFileOpen_StreamedObject_NoMetaInfo()
        {
            var file = DicomFile.Open(@".\Test Data\CR-MONO1-10-chest");
            var metaInfo = file.FileMetaInfo;
            Assert.Null(metaInfo);
        }

        [Fact]
        public void DicomFileSave_StreamedObject_ShouldContainMetaInfo()
        {
            var tempName = Path.GetTempFileName();
            DicomFile.Open(@".\Test Data\CR-MONO1-10-chest").Save(tempName);

            using (var stream = File.OpenRead(tempName))
            {
                var file = DicomFile.Open(stream);
                var metaInfo = file.FileMetaInfo;
                Assert.NotNull(metaInfo);
            }
        }

        [Fact]
        public async Task DicomFileSaveAsync_Part10File_PersistentMetaInfo()
        {
            DicomFileMetaInformation expected;
            var tempName = Path.GetTempFileName();

            using (var stream = File.OpenWrite(tempName))
            {
                var input = await DicomFile.OpenAsync(@".\Test Data\CT-MONO2-16-ankle").ConfigureAwait(false);
                expected = input.FileMetaInfo;
                await input.SaveAsync(stream).ConfigureAwait(false);
            }

            var output = DicomFile.Open(tempName);
            var actual = output.FileMetaInfo;

            Assert.Equal(expected.ImplementationClassUID.UID, actual.ImplementationClassUID.UID);
            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(expected.TransferSyntax.UID.UID, actual.TransferSyntax.UID.UID);
        }

        [Fact]
        public async Task DicomFileSave_Part10File_UpdatedMetaInfo()
        {
            var tempName = Path.GetTempFileName();

            var input = DicomFile.Open(@".\Test Data\CT-MONO2-16-ankle");
            var expected = input.FileMetaInfo;
            await input.SaveAsync(tempName).ConfigureAwait(false);

            using (var stream = File.OpenRead(tempName))
            {
                var output = await DicomFile.OpenAsync(stream).ConfigureAwait(false);
                var actual = output.FileMetaInfo;

                Assert.NotEqual(expected.ImplementationVersionName, actual.ImplementationVersionName);
                Assert.NotEqual(expected.SourceApplicationEntityTitle, actual.SourceApplicationEntityTitle);
            }
        }

        [Fact]
        public void DicomFileOpen_Part10File_MetaInfoUnmodified()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
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
