using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1339
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task DicomFileWithMissingSequenceDelimitationItemAtTheEnd(bool async)
        {
            // Arrange
            var file = "./Test Data/GH1339.dcm";

            // Act
            DicomFile dicomFile;
            if(async)
            {
                dicomFile = await DicomFile.OpenAsync(file);
            }
            else
            {
                // ReSharper disable MethodHasAsyncOverload
                dicomFile = DicomFile.Open(file);
                // ReSharper restore MethodHasAsyncOverload
            }
            var pixelData = DicomPixelData.Create(dicomFile.Dataset);
            var frame = pixelData.GetFrame(0);

            // Assert
            Assert.NotNull(frame);
        }
    }
}
