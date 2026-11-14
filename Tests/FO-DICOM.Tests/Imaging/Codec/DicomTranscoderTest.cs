// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO.Buffer;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Codec
{

    [Collection(TestCollections.WithTranscoder)]
    public class DicomTranscoderTest : IClassFixture<GlobalFixture>
    {

        #region Unit tests

        [Theory]
        [MemberData(nameof(TransferSyntaxesNames))]
        public void GetCodec_KnownTransferSyntax_ShouldReturnCodecObject(DicomTransferSyntax transferSyntax, string expected)
        {
            var transcoderManager = Setup.ServiceProvider.GetRequiredService<ITranscoderManager>();
            var codec = transcoderManager.GetCodec(transferSyntax);
            var actual = codec.Name;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExtractOverlays_EncapsulatedMultiframe_OnlyDecodesFirstFrame()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                { DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value },
                { DicomTag.Rows, (ushort)2 },
                { DicomTag.Columns, (ushort)2 },
                { DicomTag.BitsAllocated, (ushort)16 },
                { DicomTag.BitsStored, (ushort)12 },
                { DicomTag.HighBit, (ushort)11 },
                { DicomTag.PixelRepresentation, (ushort)0 },
                { DicomTag.SamplesPerPixel, (ushort)1 }
            };

            var frameBytes = new byte[8];
            // Pixel (0,0) = 0x1000 (bit 12 set for embedded overlay)
            frameBytes[0] = 0x00;
            frameBytes[1] = 0x10;

            var pixelData = DicomPixelData.CreateNew(dataset);
            pixelData.AddFrame(new MemoryByteBuffer(frameBytes));
            pixelData.AddFrame(new MemoryByteBuffer(frameBytes));

            var rleDataset = dataset.Clone(DicomTransferSyntax.RLELossless);
            var rlePixelData = DicomPixelData.CreateFromDataset(rleDataset);
            // Replace frame 1 with invalid RLE bytes; decoding frame 1 would fail.
            var corruptedRle = new DicomDataset(DicomTransferSyntax.RLELossless);
            corruptedRle.Add(rleDataset.Where(x => x.Tag != DicomTag.PixelData));
            var corruptedPixelData = DicomPixelData.CreateNew(corruptedRle);
            corruptedPixelData.AddFrame(rlePixelData.GetFrame(0));
            corruptedPixelData.AddFrame(new MemoryByteBuffer(new byte[] { 0x00 }));

            corruptedRle.AddOrUpdate(new DicomUnsignedShort(new DicomTag(0x6000, 0x0010), 2));
            corruptedRle.AddOrUpdate(new DicomUnsignedShort(new DicomTag(0x6000, 0x0011), 2));
            corruptedRle.AddOrUpdate(new DicomCodeString(new DicomTag(0x6000, 0x0040), "G"));
            corruptedRle.AddOrUpdate(new DicomSignedShort(new DicomTag(0x6000, 0x0050), 1, 1));
            corruptedRle.AddOrUpdate(new DicomUnsignedShort(new DicomTag(0x6000, 0x0100), 1));
            corruptedRle.AddOrUpdate(new DicomUnsignedShort(new DicomTag(0x6000, 0x0102), 12));

            var extracted = DicomTranscoder.ExtractOverlays(corruptedRle);

            Assert.True(extracted.Contains(new DicomTag(0x6000, 0x3000)));
            Assert.Equal(2, DicomPixelData.CreateFromDataset(extracted).NumberOfFrames);

            var overlay = DicomOverlayData.FromDataset(extracted)[0];
            var values = overlay.GetOverlayDataS32(0, 1);
            Assert.Equal(new[] { 1, 0, 0, 0 }, values);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> TransferSyntaxesNames
        {
            get
            {
                yield return new object[] { DicomTransferSyntax.JPEGProcess1, DicomUID.JPEGBaseline8Bit.Name };
                yield return new object[] { DicomTransferSyntax.RLELossless, DicomUID.RLELossless.Name };
                yield return new object[] { DicomTransferSyntax.JPEGLSNearLossless, DicomUID.JPEGLSNearLossless.Name };
                yield return new object[] { DicomTransferSyntax.JPEG2000Lossless, DicomUID.JPEG2000Lossless.Name };
            }
        }

        #endregion
    }
}
