// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Media
{
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
        public void BeginOpen_DicomDirFile_Succeeds()
        {
            var dir = _DicomDirectory.EndOpen(_DicomDirectory.BeginOpen(@".\Test Data\DICOMDIR", null, null));

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

        #endregion
    }
}