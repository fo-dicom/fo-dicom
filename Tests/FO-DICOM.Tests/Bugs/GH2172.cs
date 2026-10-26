// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using System;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    /// <summary>
    /// Regression tests for GitHub issue #2172: defects found while replacing a hand-written DICOM-to-RGB interpreter with
    /// <see cref="DicomImage.RenderImage"/>. Every dataset is synthetic and every expected value is
    /// derived by hand from the DICOM standard, so the tests do not depend on sample files.
    ///
    /// The tests are grouped in four areas:
    ///   1. YBR_FULL_422 / YBR_PARTIAL_422 chroma subsampling
    ///   2. Planar configuration (0028,0006)
    ///   3. Signed pixel data (0028,0103)
    ///   4. VOI window/level and Modality LUT rescale
    /// </summary>
    [Collection(TestCollections.Imaging)]
    public class GH2172
    {

        #region Helpers

        private static DicomDataset CreateColorDataset(
            PhotometricInterpretation photometricInterpretation,
            int width,
            int height,
            byte[] frame,
            PlanarConfiguration planarConfiguration = PlanarConfiguration.Interleaved)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { DicomTag.PhotometricInterpretation, photometricInterpretation.Value },
                { DicomTag.SamplesPerPixel, (ushort)3 },
                { DicomTag.PlanarConfiguration, (ushort)planarConfiguration },
                { DicomTag.Rows, (ushort)height },
                { DicomTag.Columns, (ushort)width },
                { DicomTag.BitsAllocated, (ushort)8 },
                { DicomTag.BitsStored, (ushort)8 },
                { DicomTag.HighBit, (ushort)7 },
                { DicomTag.PixelRepresentation, (ushort)PixelRepresentation.Unsigned }
            };

            var pixelData = DicomPixelData.CreateNew(dataset);
            pixelData.AddFrame(new MemoryByteBuffer(frame));
            return dataset;
        }

        private static DicomDataset CreateGrayscaleDataset(
            int width,
            int height,
            ushort bitsAllocated,
            ushort bitsStored,
            ushort highBit,
            PixelRepresentation pixelRepresentation,
            byte[] frame)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value },
                { DicomTag.SamplesPerPixel, (ushort)1 },
                { DicomTag.Rows, (ushort)height },
                { DicomTag.Columns, (ushort)width },
                { DicomTag.BitsAllocated, bitsAllocated },
                { DicomTag.BitsStored, bitsStored },
                { DicomTag.HighBit, highBit },
                { DicomTag.PixelRepresentation, (ushort)pixelRepresentation }
            };

            var pixelData = DicomPixelData.CreateNew(dataset);
            pixelData.AddFrame(new MemoryByteBuffer(frame));
            return dataset;
        }

        /// <summary>
        /// Builds a 4:2:2 subsampled frame: Y for every pixel, Cb/Cr for every pixel pair.
        /// Byte layout per pair is Y1 Y2 Cb Cr, i.e. 2 bytes per pixel.
        /// </summary>
        private static byte[] CreateSubsampled422Frame(int width, int height, byte luminance)
        {
            var frame = new byte[width * height * 2];
            for (var i = 0; i < frame.Length; i += 4)
            {
                frame[i + 0] = luminance;
                frame[i + 1] = luminance;
                frame[i + 2] = 128; // neutral Cb
                frame[i + 3] = 128; // neutral Cr
            }

            return frame;
        }

        /// <summary>
        /// Blue channel of the first rendered pixel. <see cref="RawImage"/> renders BGRA32.
        /// </summary>
        private static byte RenderedGray(DicomDataset dataset, int x, int y)
        {
            var image = new DicomImage(dataset);
            var bytes = image.RenderImage().AsBytes();
            return bytes[(y * image.Width + x) * 4];
        }

        #endregion

        #region 1. YBR 4:2:2 chroma subsampling

        /// <summary>
        /// Control: uncompressed YBR_FULL_422 is correctly reported as 2 bytes per pixel.
        /// </summary>
        [Fact]
        public void UncompressedFrameSize_YbrFull422_AccountsForChromaSubsampling()
        {
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrFull422, 4, 2, CreateSubsampled422Frame(4, 2, 100));
            var pixelData = DicomPixelData.CreateFromDataset(dataset);

            Assert.Equal(4 * 2 * 2, pixelData.UncompressedFrameSize);
        }

        /// <summary>
        /// YBR_PARTIAL_422 is 4:2:2 subsampled exactly like YBR_FULL_422 (PS3.3 C.7.6.3.1.2), so an
        /// uncompressed frame occupies 2 bytes per pixel, not 3. fo-dicom only special-cases
        /// YBR_FULL_422 in <see cref="DicomPixelData.UncompressedFrameSize"/>.
        /// </summary>
        [Fact]
        public void UncompressedFrameSize_YbrPartial422_AccountsForChromaSubsampling()
        {
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrPartial422, 4, 2, CreateSubsampled422Frame(4, 2, 100));
            var pixelData = DicomPixelData.CreateFromDataset(dataset);

            Assert.Equal(4 * 2 * 2, pixelData.UncompressedFrameSize);
        }

        /// <summary>
        /// Control: a YBR_FULL_422 frame with neutral chroma (Cb = Cr = 128) must decode to a neutral
        /// gray whose R = G = B = Y.
        /// </summary>
        [Fact]
        public void PixelDataFactory_YbrFull422NeutralChroma_ProducesNeutralGray()
        {
            const byte luminance = 100;
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrFull422, 4, 2, CreateSubsampled422Frame(4, 2, luminance));

            var pixels = (ColorPixelData24)PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            for (var i = 0; i < 4 * 2 * 3; i++)
            {
                Assert.Equal(luminance, pixels.Data[i]);
            }
        }

        /// <summary>
        /// A YBR_PARTIAL_422 frame with neutral chroma must decode to a neutral gray. For YBR_PARTIAL
        /// the luminance is range-limited, so Y = 100 maps to 1.1644 * (100 - 16) + 0.5 = 98.
        /// </summary>
        [Fact]
        public void PixelDataFactory_YbrPartial422NeutralChroma_ProducesNeutralGray()
        {
            const byte expectedGray = 98;
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrPartial422, 4, 2, CreateSubsampled422Frame(4, 2, 100));

            var pixels = (ColorPixelData24)PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(4 * 2 * 3, pixels.Data.Length);
            for (var i = 0; i < 4 * 2 * 3; i++)
            {
                Assert.Equal(expectedGray, pixels.Data[i]);
            }
        }

        /// <summary>
        /// Control: planar configuration is meaningless for chroma-subsampled data, and fo-dicom
        /// correctly rejects it for YBR_FULL_422 (issue #1049).
        /// </summary>
        [Fact]
        public void PixelDataFactory_YbrFull422Planar_IsRejected()
        {
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrFull422,
                4,
                2,
                CreateSubsampled422Frame(4, 2, 100),
                PlanarConfiguration.Planar);

            Assert.Throws<DicomImagingException>(() => PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0));
        }

        /// <summary>
        /// The same guard is missing for YBR_PARTIAL_422: the frame is silently pushed through
        /// PlanarToInterleaved24, which assumes 3 bytes per pixel, and the caller gets garbled color
        /// instead of an error.
        /// </summary>
        [Fact]
        public void PixelDataFactory_YbrPartial422Planar_IsRejected()
        {
            var dataset = CreateColorDataset(
                PhotometricInterpretation.YbrPartial422,
                4,
                2,
                CreateSubsampled422Frame(4, 2, 100),
                PlanarConfiguration.Planar);

            Assert.Throws<DicomImagingException>(() => PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0));
        }

        #endregion

        #region 2. Planar configuration

        /// <summary>
        /// Control: an RGB frame stored color-by-plane must be de-planarized before rendering.
        /// </summary>
        [Fact]
        public void PixelDataFactory_RgbPlanar_IsDeplanarized()
        {
            // 2x1 image: R plane, then G plane, then B plane.
            var frame = new byte[] { 10, 20, 30, 40, 50, 60 };
            var dataset = CreateColorDataset(
                PhotometricInterpretation.Rgb, 2, 1, frame, PlanarConfiguration.Planar);

            var pixels = (ColorPixelData24)PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(new byte[] { 10, 30, 50, 20, 40, 60 }, pixels.Data);
        }

        /// <summary>
        /// Control: planar and interleaved encodings of the same image must render identically.
        /// </summary>
        [Fact]
        public void DicomImage_RgbPlanar_RendersSameAsInterleaved()
        {
            var planar = CreateColorDataset(
                PhotometricInterpretation.Rgb, 2, 1,
                new byte[] { 10, 20, 30, 40, 50, 60 }, PlanarConfiguration.Planar);
            var interleaved = CreateColorDataset(
                PhotometricInterpretation.Rgb, 2, 1,
                new byte[] { 10, 30, 50, 20, 40, 60 });

            Assert.Equal(
                new DicomImage(interleaved).RenderImage().AsBytes(),
                new DicomImage(planar).RenderImage().AsBytes());
        }

        /// <summary>
        /// <see cref="BitDepth.FromDataset"/> recomputes High Bit from Bits Stored instead of reading
        /// High Bit (0028,0102). For a 12-in-16 unsigned image it reports High Bit 12, which is not
        /// even a legal value (High Bit must be less than Bits Stored for unsigned data), and it
        /// silently ignores datasets whose High Bit differs from the default.
        /// </summary>
        [Fact]
        public void BitDepth_FromDataset_PreservesHighBitFromDataset()
        {
            var dataset = CreateGrayscaleDataset(
                2, 1, 16, 12, 11, PixelRepresentation.Unsigned, new byte[4]);

            var bitDepth = BitDepth.FromDataset(dataset);

            Assert.Equal(11, bitDepth.HighBit);
        }

        #endregion

        #region 3. Signed pixel data

        /// <summary>
        /// 8-bit signed grayscale is dispatched to <c>GrayscalePixelDataU8</c> purely on
        /// Bits Allocated / Bits Stored / High Bit, ignoring Pixel Representation (0028,0103).
        /// The stored value -128 (0x80) is therefore reported as +128.
        /// </summary>
        [Fact]
        public void PixelDataFactory_Signed8BitGrayscale_PreservesNegativeValues()
        {
            // Stored values: -128, -1, 0, 127.
            var frame = new byte[] { 0x80, 0xFF, 0x00, 0x7F };
            var dataset = CreateGrayscaleDataset(
                4, 1, 8, 8, 7, PixelRepresentation.Signed, frame);

            var pixels = PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(-128d, pixels.GetPixel(0, 0));
            Assert.Equal(-1d, pixels.GetPixel(1, 0));
            Assert.Equal(0d, pixels.GetPixel(2, 0));
            Assert.Equal(127d, pixels.GetPixel(3, 0));
        }

        /// <summary>
        /// Consequence of the above at the rendering level: with a window covering the full signed
        /// 8-bit range the most negative pixel must render black and the most positive white.
        /// Because the data is read as unsigned, the darkest pixel renders as the brightest.
        /// </summary>
        [Fact]
        public void DicomImage_Signed8BitGrayscale_RendersNegativeValuesDark()
        {
            var frame = new byte[] { 0x80, 0xFF, 0x00, 0x7F };
            var dataset = CreateGrayscaleDataset(
                4, 1, 8, 8, 7, PixelRepresentation.Signed, frame);
            dataset.AddOrUpdate(DicomTag.WindowCenter, "0");
            dataset.AddOrUpdate(DicomTag.WindowWidth, "256");

            var darkest = RenderedGray(dataset, 0, 0);
            var brightest = RenderedGray(dataset, 3, 0);

            Assert.True(
                darkest < brightest,
                $"Stored -128 rendered as {darkest} and stored 127 rendered as {brightest}; " +
                "signed pixel data is being interpreted as unsigned.");
        }

        /// <summary>
        /// Control: 16-bit signed grayscale is dispatched correctly and sign-extended.
        /// </summary>
        [Fact]
        public void PixelDataFactory_Signed16BitGrayscale_PreservesNegativeValues()
        {
            // Stored values (little endian): -2048, 2047.
            var frame = new byte[] { 0x00, 0xF8, 0xFF, 0x07 };
            var dataset = CreateGrayscaleDataset(
                2, 1, 16, 12, 11, PixelRepresentation.Signed, frame);

            var pixels = PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(-2048d, pixels.GetPixel(0, 0));
            Assert.Equal(2047d, pixels.GetPixel(1, 0));
        }

        /// <summary>
        /// Bits outside Bits Stored must be discarded relative to the in-memory container, not Bits
        /// Allocated. Samples narrower than 16 bits are unpacked into a 16-bit container, so masking
        /// against Bits Allocated left the unused high bit set.
        /// </summary>
        [Fact]
        public void PixelDataFactory_Unsigned7In8BitGrayscale_DiscardsUnusedHighBit()
        {
            // Bit 7 is not part of the stored value and must be ignored.
            var frame = new byte[] { 0xFF, 0x80 };
            var dataset = CreateGrayscaleDataset(
                2, 1, 8, 7, 6, PixelRepresentation.Unsigned, frame);

            var pixels = PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(127d, pixels.GetPixel(0, 0));
            Assert.Equal(0d, pixels.GetPixel(1, 0));
        }

        /// <summary>
        /// Signed counterpart of the above: a 7-in-8 signed sample must be sign-extended from High Bit 6.
        /// </summary>
        [Fact]
        public void PixelDataFactory_Signed7In8BitGrayscale_SignExtendsFromHighBit()
        {
            // 0x40 has bit 6 (the sign bit) set: -64. Bit 7 of 0xBF is unused: 0x3F = 63.
            var frame = new byte[] { 0x40, 0xBF };
            var dataset = CreateGrayscaleDataset(
                2, 1, 8, 7, 6, PixelRepresentation.Signed, frame);

            var pixels = PixelDataFactory.Create(DicomPixelData.CreateFromDataset(dataset), 0);

            Assert.Equal(-64d, pixels.GetPixel(0, 0));
            Assert.Equal(63d, pixels.GetPixel(1, 0));
        }

        #endregion

        #region 4. VOI window/level and Modality LUT rescale

        /// <summary>
        /// Control: the Modality LUT (rescale slope/intercept) must be applied before the VOI LUT.
        /// Stored 1024 with intercept -1024 is 0 HU, which sits at the centre of the window and must
        /// therefore render as mid gray rather than white.
        /// </summary>
        [Fact]
        public void DicomImage_RescaleIntercept_IsAppliedBeforeWindowing()
        {
            var frame = new byte[] { 0x00, 0x04 }; // stored value 1024
            var dataset = CreateGrayscaleDataset(
                1, 1, 16, 12, 11, PixelRepresentation.Unsigned, frame);
            dataset.AddOrUpdate(DicomTag.RescaleSlope, "1");
            dataset.AddOrUpdate(DicomTag.RescaleIntercept, "-1024");
            dataset.AddOrUpdate(DicomTag.WindowCenter, "0");
            dataset.AddOrUpdate(DicomTag.WindowWidth, "400");

            var rendered = RenderedGray(dataset, 0, 0);

            Assert.InRange(rendered, 120, 135);
        }

        /// <summary>
        /// A negative Rescale Slope is legal (PS3.3 C.11.1.1.2) and inverts the relationship between
        /// stored value and output value. <c>ModalityRescaleLUT</c> computes its minimum output value
        /// from the minimum stored value and its maximum from the maximum stored value, which are
        /// reversed when the slope is negative.
        /// </summary>
        [Fact]
        public void ModalityRescaleLUT_NegativeSlope_ReportsOrderedOutputRange()
        {
            var dataset = CreateGrayscaleDataset(
                1, 1, 16, 12, 11, PixelRepresentation.Unsigned, new byte[2]);
            dataset.AddOrUpdate(DicomTag.RescaleSlope, "-1");
            dataset.AddOrUpdate(DicomTag.RescaleIntercept, "0");
            dataset.AddOrUpdate(DicomTag.WindowCenter, "0");
            dataset.AddOrUpdate(DicomTag.WindowWidth, "400");

            var options = GrayscaleRenderOptions.FromDataset(dataset, 0);
            var lut = new FellowOakDicom.Imaging.LUT.ModalityRescaleLUT(options);

            Assert.True(
                lut.MinimumOutputValue <= lut.MaximumOutputValue,
                $"Modality LUT reports minimum {lut.MinimumOutputValue} > maximum {lut.MaximumOutputValue}.");
        }

        /// <summary>
        /// Every <see cref="GrayscaleRenderOptions"/> factory must enable a VOI LUT Sequence present in the
        /// dataset. <see cref="GrayscaleRenderOptions.FromBitRange"/> and
        /// <see cref="GrayscaleRenderOptions.FromHistogram"/> previously stored the sequence without enabling it.
        /// </summary>
        [Theory]
        [InlineData("FromDataset")]
        [InlineData("FromWindowLevel")]
        [InlineData("FromImagePixelValueTags")]
        [InlineData("FromMinMax")]
        [InlineData("FromBitRange")]
        [InlineData("FromHistogram")]
        public void GrayscaleRenderOptions_AllFactories_EnableVOILUTSequence(string factory)
        {
            var dataset = CreateGrayscaleDataset(
                2, 1, 16, 12, 11, PixelRepresentation.Unsigned, new byte[] { 0x00, 0x00, 0x03, 0x00 });
            dataset.AddOrUpdate(DicomTag.WindowCenter, "2");
            dataset.AddOrUpdate(DicomTag.WindowWidth, "4");
            dataset.AddOrUpdate(DicomTag.SmallestImagePixelValue, (ushort)0);
            dataset.AddOrUpdate(DicomTag.LargestImagePixelValue, (ushort)3);
            dataset.Add(new DicomSequence(
                DicomTag.VOILUTSequence,
                new DicomDataset
                {
                    { DicomTag.LUTDescriptor, (ushort)4, (ushort)0, (ushort)16 },
                    { DicomTag.LUTData, (ushort)0, (ushort)100, (ushort)200, (ushort)300 }
                }));

            var options = factory switch
            {
                "FromDataset" => GrayscaleRenderOptions.FromDataset(dataset, 0),
                "FromWindowLevel" => GrayscaleRenderOptions.FromWindowLevel(dataset),
                "FromImagePixelValueTags" => GrayscaleRenderOptions.FromImagePixelValueTags(dataset),
                "FromMinMax" => GrayscaleRenderOptions.FromMinMax(dataset),
                "FromBitRange" => GrayscaleRenderOptions.FromBitRange(dataset),
                "FromHistogram" => GrayscaleRenderOptions.FromHistogram(dataset),
                _ => throw new ArgumentOutOfRangeException(nameof(factory))
            };

            Assert.NotNull(options.VOILUTSequence);
            Assert.True(options.UseVOILUT);
        }

        /// <summary>
        /// Setting <see cref="DicomImage.UseVOILUT"/> to false must fall back to window/level
        /// rendering. The VOI LUT Sequence is added to the pipeline unconditionally, so it keeps being
        /// applied and the flag has no effect.
        /// </summary>
        [Fact]
        public void DicomImage_UseVOILUTFalse_IgnoresVOILUTSequence()
        {
            var frame = new byte[] { 0x02, 0x00 }; // stored value 2
            var withSequence = CreateGrayscaleDataset(
                1, 1, 16, 12, 11, PixelRepresentation.Unsigned, frame);
            withSequence.AddOrUpdate(DicomTag.WindowCenter, "2");
            withSequence.AddOrUpdate(DicomTag.WindowWidth, "4");
            withSequence.Add(new DicomSequence(
                DicomTag.VOILUTSequence,
                new DicomDataset
                {
                    { DicomTag.LUTDescriptor, (ushort)4, (ushort)0, (ushort)16 },
                    { DicomTag.LUTExplanation, "inverting lut" },
                    { DicomTag.LUTData, (ushort)65535, (ushort)43690, (ushort)21845, (ushort)0 }
                }));

            var withoutSequence = CreateGrayscaleDataset(
                1, 1, 16, 12, 11, PixelRepresentation.Unsigned, frame);
            withoutSequence.AddOrUpdate(DicomTag.WindowCenter, "2");
            withoutSequence.AddOrUpdate(DicomTag.WindowWidth, "4");

            var imageWithSequence = new DicomImage(withSequence) { UseVOILUT = false };
            var expected = new DicomImage(withoutSequence).RenderImage().AsBytes();
            var actual = imageWithSequence.RenderImage().AsBytes();

            Assert.Equal(expected, actual);
        }

        #endregion

    }
}
