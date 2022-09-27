using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1442
    {
        [Fact]
        public void ModalitySequenceLUT_ShouldBeNull()
        {
            // Arrange
            var testFile = DicomFile.Open("./Test Data/GH1442.dcm");

            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(testFile.Dataset);

            // Assert
            Assert.Null(grayScaleRenderOptions.ModalityLUTSequence);
        }
    }
}
