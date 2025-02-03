
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1876
    {
        [Fact]
        public void LoadPixelDataOfFileWithUnclearTaglength()
        {
            // arrange
            var dicomFile = DicomFile.Open(TestData.Resolve("GH1876.dcm"));

            // Act
            var dicomImage = new DicomImage(dicomFile.Dataset);

            // Assert
            Assert.NotNull(dicomImage);
        }
    }
}
