using System.IO;
using FellowOakDicom.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1264
    {
        [Fact]
        public void OpenDicomFileFromStream()
        {
            // Arrange
            var inputFile = TestData.Resolve("TestPattern_RGB.dcm");
            var outputFile = new FileInfo("./GH1264.dcm");

            outputFile.Delete();

            using var fs = File.OpenRead(inputFile);
            using var ms = new MemoryStream();

            fs.CopyTo(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var dicomFileFromPath = DicomFile.Open(inputFile);
            var dicomFileFromStream = DicomFile.Open(ms);

            // Act
            dicomFileFromStream.Save(outputFile.FullName);
            var reopenedDicomFile = DicomFile.Open(outputFile.FullName);

            // Assert
            var jsonDicomConverter = new JsonDicomConverter(false, false);
            var dicomFileFromPathJson = JsonConvert.SerializeObject(dicomFileFromPath.Dataset, jsonDicomConverter);
            var dicomFileFromStreamJson = JsonConvert.SerializeObject(dicomFileFromStream.Dataset, jsonDicomConverter);
            var actual = JsonConvert.SerializeObject(reopenedDicomFile.Dataset, jsonDicomConverter);

            Assert.Equal(dicomFileFromPathJson, dicomFileFromStreamJson);
            Assert.Equal(actual, dicomFileFromStreamJson);
        }
    }
}
