// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System.IO;
    using System.Threading.Tasks;

    using Xunit;

    [Collection("General")]
    public class DicomFileReaderTest
    {
        #region Unit tests

        [Fact]
        public void Read_ValidSource_ReturnsSuccess()
        {
            using (var stream = File.OpenRead(@".\Test Data\CT1_J2KI"))
            {
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

                var modality = dataset.Get<string>(DicomTag.Modality);
                Assert.Equal("CT", modality);
            }
        }

        [Fact]
        public void Read_CompressedImage_RecognizeTransferSyntax()
        {
            using (var stream = File.OpenRead(@".\Test Data\CT1_J2KI"))
            {
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
        }

        [Fact]
        public async Task ReadAsync_CompressedImage_RecognizeTransferSyntax()
        {
            using (var stream = File.OpenRead(@".\Test Data\CT1_J2KI"))
            {
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
        }

        #endregion
    }
}
