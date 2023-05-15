using System;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection("WithTranscoder")]
    public class GH1586
    {
        [Fact]
        public async Task RenderingPixelDataWhereLastFragmentIs0Bytes()
        {
            // Arrange
            var dicomFile = await DicomFile.OpenAsync(@"./Test Data/multiframe.dcm");
            var dataset = dicomFile.Dataset;
            var pixelData = dataset.GetDicomItem<DicomOtherByteFragment>(DicomTag.PixelData);
            pixelData.Add(EmptyBuffer.Value);
            pixelData.OffsetTable.Clear();
            dataset.AddOrUpdate(pixelData);

            var dicomImage = new DicomImage(dataset);

            // Act
            var exception = Record.Exception(() =>
            {
                using var image = dicomImage.RenderImage();
            });

            // Assert
            Assert.Null(exception);
        }
    }
}
