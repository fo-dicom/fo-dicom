using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1986
    {
        [Fact]
        public void GrayscaleRenderOptions_FromMinMax_WithModalityLut_ShouldIgnoreRescaleSlopeIntercept()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;

            var result = GrayscaleRenderOptions.FromMinMax(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(511.5, result.WindowCenter);
            Assert.Equal(1023, result.WindowWidth);
        }
    }
}