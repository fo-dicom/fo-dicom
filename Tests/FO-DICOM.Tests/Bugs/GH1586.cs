using System;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Imaging)]
    public class GH1586
    {
        [Fact]
        public void RenderingPixelDataWhereEachFragmentIsAFrameAndLastFragmentIs0Bytes_ShouldWork()
        {
            // Arrange
            var dicomFile = new DicomFile();
            var metaInfo = dicomFile.FileMetaInfo;
            var dataset = dicomFile.Dataset;

            metaInfo.TransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            dataset.AddOrUpdate(DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID());
            dataset.AddOrUpdate(DicomTag.Rows, (ushort) 2);
            dataset.AddOrUpdate(DicomTag.Columns, (ushort) 2);
            dataset.AddOrUpdate(DicomTag.BitsAllocated, (ushort) 8);
            dataset.AddOrUpdate(DicomTag.HighBit, (ushort) 7);
            dataset.AddOrUpdate(DicomTag.BitsStored, (ushort) 8);
            dataset.AddOrUpdate(DicomTag.PixelRepresentation, (ushort) 0);
            dataset.AddOrUpdate(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value);
            dataset.AddOrUpdate(DicomTag.SamplesPerPixel, (ushort) 1);
            dataset.AddOrUpdate(DicomTag.NumberOfFrames, 2);

            var pixelData = new DicomOtherByteFragment(DicomTag.PixelData);
            pixelData.Fragments.Add(new MemoryByteBuffer(new byte[] { 255,0,255,0 }));
            pixelData.Fragments.Add(new MemoryByteBuffer(new byte[] { 0,255,0,255 }));
            pixelData.Fragments.Add(EmptyBuffer.Value);
            dataset.AddOrUpdate(pixelData);

            var dicomImage = new DicomImage(dataset);

            // Act
            var exception = Record.Exception(() =>
            {
                using var image = dicomImage.RenderImage();
            });

            // Assert
            Assert.Null(exception);
        }
    }
}
