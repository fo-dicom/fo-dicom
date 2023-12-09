// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Reader;
using FellowOakDicom.Tests.Helpers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.IO.Reader
{

    [Collection(TestCollections.General)]
    public class DicomFileReaderTest
    {

        #region Unit tests

        [Fact]
        public void ReadLargeFileFromStream()
        {
            DicomFile dcm = null;
            string filename = TestData.Resolve("GH355.dcm");
            if (File.Exists(filename))
            {
                byte[] buff = File.ReadAllBytes(filename);
                using var stream = new MemoryStream(buff);
                dcm = DicomFile.Open(stream, FileReadOption.ReadAll);
            }

            // the file shall be completely read in memory, so writing it should be possible even if the stream has been colsed
            string tmpFile = Path.GetTempFileName();
            dcm.Save(tmpFile);
            Assert.True(File.Exists(tmpFile));
            Assert.True(new FileInfo(tmpFile).Length > 0);
            IOHelper.DeleteIfExists(tmpFile);
        }


        [Fact]
        public void ReadLargeFileFromStream_Error()
        {
            DicomFile dcm = null;
            string filename = TestData.Resolve("GH355.dcm");
            if (File.Exists(filename))
            {
                byte[] buff = File.ReadAllBytes(filename);
                using var stream = new MemoryStream(buff);
                dcm = DicomFile.Open(stream, FileReadOption.ReadLargeOnDemand);
            }

            // this will save the image without pixels and generate an ObjectDisposedException
            string tmpFile = Path.GetTempFileName();
            Assert.Throws<DicomIoException>(() => dcm.Save(tmpFile));
            IOHelper.DeleteIfExists(tmpFile);
        }


        [Theory]
        [InlineData("GH355.dcm")]
        [InlineData("10200904.dcm")]
        [InlineData("CR-MONO1-10-chest")]
        [InlineData("TestPattern_RGB.dcm")]
        public void ReadLargeFileFromStream_WithoutPixeldata(string filename)
        {
            filename = TestData.Resolve(filename);
            DicomFile dcm = null;
            if (File.Exists(filename))
            {
                byte[] buff = File.ReadAllBytes(filename);
                using var stream = new MemoryStream(buff);
                dcm = DicomFile.Open(stream, FileReadOption.SkipLargeTags);
            }

            // verify that the pixel data are not loaded from stream
            Assert.False(dcm.Dataset.Contains(DicomTag.PixelData));
        }

        [Fact]
        public void ReadingSkipLargeTags_GH893()
        {
            DicomFile dcm = null;
            string filename = TestData.Resolve("genFile.dcm");
            if (File.Exists(filename))
            {
                byte[] buff = File.ReadAllBytes(filename);
                using var stream = new MemoryStream(buff);
                dcm = DicomFile.Open(stream, FileReadOption.SkipLargeTags);
            }
            Assert.NotNull(dcm);
        }


        [Fact]
        public void Read_ValidSource_ReturnsSuccess()
        {
            using var stream = File.OpenRead(TestData.Resolve("CT1_J2KI"));
            var source = new StreamByteSource(stream);
            var reader = new DicomFileReader();

            var fileMetaInfo = new DicomFileMetaInformation();
            var dataset = new DicomDataset();

            const DicomReaderResult expected = DicomReaderResult.Success;
            var actual = reader.Read(
                source,
                new DicomDatasetReaderObserver(fileMetaInfo),
                new DicomDatasetReaderObserver(dataset));

            Assert.Equal(expected, actual);

            var modality = dataset.GetString(DicomTag.Modality);
            Assert.Equal("CT", modality);
        }


        [Fact]
        public void Read_CompressedImage_RecognizeTransferSyntax()
        {
            using var stream = File.OpenRead(TestData.Resolve("CT1_J2KI"));
            var source = new StreamByteSource(stream);
            var reader = new DicomFileReader();

            var fileMetaInfo = new DicomFileMetaInformation();
            var dataset = new DicomDataset();

            reader.Read(
                source,
                new DicomDatasetReaderObserver(fileMetaInfo),
                new DicomDatasetReaderObserver(dataset));

            var expected = DicomTransferSyntax.JPEG2000Lossy;
            var actual = reader.Syntax;
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task ReadAsync_CompressedImage_RecognizeTransferSyntax()
        {
            using var stream = File.OpenRead(TestData.Resolve("CT1_J2KI"));
            var source = new StreamByteSource(stream);
            var reader = new DicomFileReader();

            var fileMetaInfo = new DicomFileMetaInformation();
            var dataset = new DicomDataset();

            await
                reader.ReadAsync(
                    source,
                    new DicomDatasetReaderObserver(fileMetaInfo),
                    new DicomDatasetReaderObserver(dataset));

            var expected = DicomTransferSyntax.JPEG2000Lossy;
            var actual = reader.Syntax;
            Assert.Equal(expected, actual);
        }


        #endregion

    }
}
