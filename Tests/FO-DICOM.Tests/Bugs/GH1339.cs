using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1339
    {
        [Fact]
        public void DicomFileWithMissingSequenceDelimitationItemAtTheEnd()
        {
            // Arrange
            var file = "./Test Data/GH1339.dcm";

            // Act
            var dicomFile = DicomFile.Open(file);
            var pixelData = DicomPixelData.Create(dicomFile.Dataset);
            var frame = pixelData.GetFrame(0);

            // Assert
            Assert.NotNull(frame);
        }
    }
}
