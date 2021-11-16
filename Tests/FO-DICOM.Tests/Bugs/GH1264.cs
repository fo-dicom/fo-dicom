using System.IO;
using System.Linq;
using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Writer;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1264
    {
        [Fact]
        public void OpenDicomFileFromStream()
        {
            // Arrange
            var inputFile = TestData.Resolve("10200904.dcm");
            var outputFile = new FileInfo("./GH1264.dcm");

            outputFile.Delete();

            using var fs = File.OpenRead(inputFile);
            using var ms = new MemoryStream();

            fs.CopyTo(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var fileByteDicomFile = DicomFile.Open(inputFile, FileReadOption.ReadAll);
            var streamByteDicomFile = DicomFile.Open(ms, FileReadOption.ReadAll);

            // Act
            streamByteDicomFile.Save(outputFile.FullName);

            var reopenedDicomFile = DicomFile.Open(outputFile.FullName, FileReadOption.ReadAll);

            // Assert
            var fileBytePixelData = DicomPixelData.Create(fileByteDicomFile.Dataset);
            var streamBytePixelData = DicomPixelData.Create(streamByteDicomFile.Dataset);
            var reopenedPixelData = DicomPixelData.Create(reopenedDicomFile.Dataset);

            Assert.Equal(79, fileBytePixelData.NumberOfFrames);
            Assert.Equal(79, streamBytePixelData.NumberOfFrames);
            Assert.Equal(79, reopenedPixelData.NumberOfFrames);

            var fileByteFrame1 = fileBytePixelData.GetFrame(0);
            var streamByteFrame1 = streamBytePixelData.GetFrame(0);
            var reopenedFrame1 = reopenedPixelData.GetFrame(0);

            Assert.Equal(fileByteFrame1.Size, streamByteFrame1.Size);
            Assert.Equal(fileByteFrame1.Size, reopenedFrame1.Size);
        }
    }
}
