using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1653
    {

        [Fact]
        public void DirectOtherWord()
        {
            // Arrange
            var inputFile = new FileInfo(TestData.Resolve("GH645.dcm"));
            using var fs = File.OpenRead(inputFile.FullName);
            using var ms = new MemoryStream();

            fs.CopyTo(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var streamByteDicomFile = DicomFile.Open(ms);

            var referencePixelData = streamByteDicomFile.Dataset.GetDicomItem<DicomOtherWord>(DicomTag.PixelData);
            var pixelData = DicomPixelData.Create(streamByteDicomFile.Dataset);
            var numberOfFrames = streamByteDicomFile.Dataset.GetSingleValue<int>(DicomTag.NumberOfFrames);
            var referenceBytes = Enumerable.Range(0, numberOfFrames).Select(x =>
                {
                    var offset = (long)pixelData.UncompressedFrameSize * x;
                    IByteBuffer buffer = new RangeByteBuffer(referencePixelData.Buffer, offset, pixelData.UncompressedFrameSize);
                    return buffer.Data;
                })
                .ToArray();

            // Act
            var streamBytePixelData = (streamByteDicomFile.Dataset.Clone()).GetDicomItem<DicomOtherWord>(DicomTag.PixelData);

            Parallel.For(0, 500, i =>
            {
                var index = i % numberOfFrames;
                var offset = (long)pixelData.UncompressedFrameSize * index;
                IByteBuffer frame = new RangeByteBuffer(streamBytePixelData.Buffer, offset, pixelData.UncompressedFrameSize);
                Assert.Equal(referenceBytes[index], frame.Data);
            });
        }

    }
}
