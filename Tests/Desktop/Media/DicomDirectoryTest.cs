// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;

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

        [Fact]
        public void AddFile_AnonymizedSeries_AllFilesAddedToSameStudySeriesNode()
        {
            var dicomFiles = GetDicomFilesFromWebZip(
                "https://www.creatis.insa-lyon.fr/~jpr/PUBLIC/gdcm/gdcmSampleData/Philips_Medical_Images/mr711-mr712/abd1.zip");

            // Anonymize all files
            var anonymizer = new DicomAnonymizer();
            foreach (var dicomFile in dicomFiles)
            {
                anonymizer.AnonymizeInPlace(dicomFile);
            }

            // Create DICOM directory
            var dicomDir = new DicomDirectory();
            foreach (var dicomFile in dicomFiles)
            {
                dicomDir.AddFile(dicomFile);
            }

            var imageNodes = dicomDir.RootDirectoryRecord.LowerLevelDirectoryRecord.LowerLevelDirectoryRecord
                .LowerLevelDirectoryRecordCollection;
            Assert.Equal(dicomFiles.Count, imageNodes.Count());
        }

        private static IList<DicomFile> GetDicomFilesFromWebZip(string url)
        {
            var dicomFiles = new List<DicomFile>();

            using (var webClient = new WebClient())
            {
                var bytes = webClient.DownloadData(url);

                using (var stream = new MemoryStream(bytes))
                using (var zipper = new ZipArchive(stream))
                {
                    foreach (var entry in zipper.Entries)
                    {
                        try
                        {
                            using (var entryStream = entry.Open())
                            using (var duplicate = new MemoryStream())
                            {
                                entryStream.CopyTo(duplicate);
                                duplicate.Seek(0, SeekOrigin.Begin);
                                var dicomFile = DicomFile.Open(duplicate);
                                dicomFiles.Add(dicomFile);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return dicomFiles;
        }

        #endregion
    }
}
