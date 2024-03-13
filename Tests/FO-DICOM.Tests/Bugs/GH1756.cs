using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1756
    {
        [Theory]
        [InlineData("NaN", "NaN")]
        [InlineData("100", "NaN")]
        [InlineData("NaN", "100")]
        [InlineData("0.99", "100")]
        [InlineData("0", "100")]
        [InlineData("-1", "100")]
        public void GrayScaleRenderOptionsFromInvalidWindowCenterOrWidthShouldFallbackCorrectly(string windowWidth, string windowCenter)
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/CT-MONO2-16-ankle");
            
            dicomFile.Dataset.ValidateItems = false;
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowWidth, windowWidth);
            dicomFile.Dataset.AddOrUpdate(DicomTag.WindowCenter, windowCenter);
            
            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset, 0);

            // Assert
            Assert.Equal(1032, grayScaleRenderOptions.WindowCenter);
            Assert.Equal(4048, grayScaleRenderOptions.WindowWidth);
        }
        
        [Theory]
        [InlineData("NaN", "NaN")]
        [InlineData("100", "NaN")]
        [InlineData("NaN", "100")]
        [InlineData("0.99", "100")]
        [InlineData("0", "100")]
        [InlineData("-1", "100")]
        public void GrayScaleRenderOptionsFromInvalidWindowCenterOrWidthInFunctionalGroupShouldFallbackCorrectly(string windowWidth, string windowCenter)
        {
            // Arrange
            var dicomFile = DicomFile.Open("./Test Data/CT-MONO2-16-ankle");
            
            dicomFile.Dataset.ValidateItems = false;
            dicomFile.Dataset.Remove(DicomTag.WindowWidth);
            dicomFile.Dataset.Remove(DicomTag.WindowCenter);
            var referencedImageSequence = new DicomDataset { ValidateItems = false };
            referencedImageSequence.AddOrUpdate(DicomTag.WindowWidth, windowWidth); 
            referencedImageSequence.AddOrUpdate(DicomTag.WindowCenter, windowCenter); 
            var sharedFunctionalGroups = new DicomDataset { ValidateItems = false };
            sharedFunctionalGroups.AddOrUpdate(new DicomSequence(DicomTag.ReferencedImageSequence, referencedImageSequence));
            dicomFile.Dataset.AddOrUpdate(new DicomSequence(DicomTag.SharedFunctionalGroupsSequence, sharedFunctionalGroups));
            
            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset, 0);

            // Assert
            Assert.Equal(1032, grayScaleRenderOptions.WindowCenter);
            Assert.Equal(4048, grayScaleRenderOptions.WindowWidth);
        }
    }
}
