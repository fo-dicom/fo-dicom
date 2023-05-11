using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Render
{
    [Collection("ImageSharp")]
    public class OverlayGraphicTest
    {
        [Fact]
        public void RenderOutOfBoundsOrigin()
        {
            // Arrange
            var dicomFile = DicomFile.Open("Test Data/OutOfBoundsOverlay.dcm");
            var expectedColor = Color32.White;

            // Act
            var dicomImage = new DicomImage(dicomFile.Dataset);
            var image = dicomImage.RenderImage(0);

            // Assert
            var actualColor = image.GetPixel(25, 25);
            Assert.Equal(expectedColor, actualColor);

        }
    }
}
