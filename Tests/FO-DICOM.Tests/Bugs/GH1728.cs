using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1728
    {
        [Fact]
        public void RenderingDicomFileWithInvalidOverlayData_ShouldNotCrash()
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/GH1728.dcm");
            var dicomImage = new DicomImage(dicomFile.Dataset);

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
