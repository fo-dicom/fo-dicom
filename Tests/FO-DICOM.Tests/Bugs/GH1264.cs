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
            var inputFile = TestData.Resolve("test_720.dcm");
            var outputFile = new FileInfo("./GH1264.dcm");

            outputFile.Delete();

            using var fs = File.OpenRead(inputFile);
            using var ms = new MemoryStream();

            fs.CopyTo(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var expectedDicomFile = DicomFile.Open(ms);

            // Act
            expectedDicomFile.Save(outputFile.FullName);
            var reopenedDicomFile = DicomFile.Open(outputFile.FullName);

            // Assert
            var jsonDicomConverter = new JsonDicomConverter(false, false);
            var expected = JsonConvert.SerializeObject(expectedDicomFile, jsonDicomConverter);
            var actual = JsonConvert.SerializeObject(reopenedDicomFile, jsonDicomConverter);

            Assert.Equal(expected, actual);
        }
    }
}
