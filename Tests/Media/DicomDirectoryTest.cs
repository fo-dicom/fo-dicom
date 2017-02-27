// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Media
{
    using System.IO;
    using System.Threading.Tasks;

    using Xunit;

    [Collection("General")]
    public class DicomDirectoryTest
    {
        #region Unit tests

        [Fact]
        public void Open_DicomDirFile_Succeeds()
        {
            var dir = DicomDirectory.Open(@".\Test Data\DICOMDIR");

            var expected = DicomUID.MediaStorageDirectoryStorage.UID;
            var actual = dir.FileMetaInfo.MediaStorageSOPClassUID.UID;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OpenAsync_DicomDirFile_Succeeds()
        {
            var dir = await DicomDirectory.OpenAsync(@".\Test Data\DICOMDIR");

            var expected = DicomUID.MediaStorageDirectoryStorage.UID;
            var actual = dir.FileMetaInfo.MediaStorageSOPClassUID.UID;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Open_MediaStorageSOPInstanceUID_ShouldBeConsistent()
        {
            var dir = DicomDirectory.Open(@".\Test Data\DICOMDIR");
            var expected = dir.FileMetaInfo.Get<DicomUID>(DicomTag.MediaStorageSOPInstanceUID).UID;
            var actual = dir.MediaStorageSOPInstanceUID.UID;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Open_DicomDirStream_Succeeds()
        {
            using (var stream = File.OpenRead(@".\Test Data\DICOMDIR"))
            {
                DicomDirectory dir = DicomDirectory.Open(stream);

                var expected = DicomUID.MediaStorageDirectoryStorage.UID;
                var actual = dir.FileMetaInfo.MediaStorageSOPClassUID.UID;
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task OpenAsync_DicomDirStream_Succeeds()
        {
            using (var stream = File.OpenRead(@".\Test Data\DICOMDIR"))
            {
                DicomDirectory dir = await DicomDirectory.OpenAsync(stream);

                var expected = DicomUID.MediaStorageDirectoryStorage.UID;
                var actual = dir.FileMetaInfo.MediaStorageSOPClassUID.UID;
                Assert.Equal(expected, actual);
            }
        }

        #endregion
    }
}
