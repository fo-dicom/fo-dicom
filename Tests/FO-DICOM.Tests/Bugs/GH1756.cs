using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1756
    {
        [Fact]
        public void GrayScaleRenderOptionsFromWindowCenterAndWidthEqualToNaNShouldFallbackCorrectly()
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/CT-MONO2-16-ankle");
            
            // Set value of <dicomTagName> to NaN
            dicomFile.Dataset.ValidateItems = false;
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowWidth, "NaN");
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowCenter, "NaN");
            
            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset, 0);

            // Assert
            Assert.Equal(1032, grayScaleRenderOptions.WindowCenter);
            Assert.Equal(4048, grayScaleRenderOptions.WindowWidth);
        }
        
        [Fact]
        public void GrayScaleRenderOptionsFromWindowWidthEqualToNaNShouldFallbackCorrectly()
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/CT-MONO2-16-ankle");
            
            // Set value of <dicomTagName> to NaN
            dicomFile.Dataset.ValidateItems = false;
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowWidth, "NaN");
            
            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset, 0);

            // Assert
            Assert.Equal(1032, grayScaleRenderOptions.WindowCenter);
            Assert.Equal(4048, grayScaleRenderOptions.WindowWidth);
        }
        
        [Fact]
        public void GrayScaleRenderOptionsFromWindowCenterEqualToNaNShouldFallbackCorrectly()
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/CT-MONO2-16-ankle");
            
            // Set value of <dicomTagName> to NaN
            dicomFile.Dataset.ValidateItems = false;
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowCenter, "NaN");
            
            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset, 0);

            // Assert
            Assert.Equal(1032, grayScaleRenderOptions.WindowCenter);
            Assert.Equal(4048, grayScaleRenderOptions.WindowWidth);
        }
    }
}
