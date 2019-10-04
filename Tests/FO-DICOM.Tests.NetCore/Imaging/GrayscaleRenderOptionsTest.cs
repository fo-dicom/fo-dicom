// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection("General")]
    public class GrayscaleRenderOptionsTest
    {
        #region Unit tests

        [Fact]
        public void ColorMap_Monochrome2ImageOptions_ReturnsMonochrome2ColorMap()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            Assert.Same(ColorTable.Monochrome2, options.ColorMap);
        }

        [Fact]
        public void ColorMap_Setter_ReturnsSetColorMap()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            options.ColorMap = ColorTable.Monochrome1;
            Assert.Same(ColorTable.Monochrome1, options.ColorMap);
        }

        [Theory]
        [InlineData((ushort)16, (ushort)12, (ushort)0, 1.0, 0.0, 500.0, 20.0, "LINEAR")]
        public void FromWindowLevel_ValidInput_CorrectOutput(
            ushort bitsAllocated,
            ushort bitsStored,
            ushort pixelRepresentation,
            double rescaleSlope,
            double rescaleIntercept,
            double windowWidth,
            double windowCenter,
            string voiLutFunction)
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, bitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, bitsStored),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, pixelRepresentation),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)rescaleSlope),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)rescaleIntercept),
                new DicomDecimalString(DicomTag.WindowWidth, (decimal)windowWidth),
                new DicomDecimalString(DicomTag.WindowCenter, (decimal)windowCenter),
                new DicomCodeString(DicomTag.VOILUTFunction, voiLutFunction));

            var actual = GrayscaleRenderOptions.FromWindowLevel(dataset);

            Assert.Equal(windowWidth, actual.WindowWidth);
            Assert.Equal(windowCenter, actual.WindowCenter);
        }

        [Theory]
        [InlineData((ushort)16, (ushort)12, (ushort)0, 1.0, 0.0, 500.0, 20.0, "LINEAR")]
        public void FromDataset_WindowCenterWidth_ReturnsSameAsFromWindowLevel(
            ushort bitsAllocated,
            ushort bitsStored,
            ushort pixelRepresentation,
            double rescaleSlope,
            double rescaleIntercept,
            double windowWidth,
            double windowCenter,
            string voiLutFunction)
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, bitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, bitsStored),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, pixelRepresentation),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)rescaleSlope),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)rescaleIntercept),
                new DicomDecimalString(DicomTag.WindowWidth, (decimal)windowWidth),
                new DicomDecimalString(DicomTag.WindowCenter, (decimal)windowCenter),
                new DicomCodeString(DicomTag.VOILUTFunction, voiLutFunction));

            var expected = GrayscaleRenderOptions.FromWindowLevel(dataset);
            var actual = GrayscaleRenderOptions.FromDataset(dataset);

            Assert.Equal(expected.WindowWidth, actual.WindowWidth);
            Assert.Equal(expected.WindowCenter, actual.WindowCenter);
        }

        [Theory]
        [InlineData((ushort)16, (ushort)12, 1.0, 0.0, (short)-150, (short)1050, "LINEAR", 1200.0, 450.0)]
        public void FromImagePixelValueTags_ValidSignedInput_CorrectOutput(
            ushort bitsAllocated,
            ushort bitsStored,
            double rescaleSlope,
            double rescaleIntercept,
            short smallestImagePixelValue,
            short largestImagePixelValue,
            string voiLutFunction,
            double expectedWindowWidth,
            double expectedWindowCenter)
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, bitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, bitsStored),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 1),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)rescaleSlope),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)rescaleIntercept),
                new DicomSignedShort(DicomTag.SmallestImagePixelValue, smallestImagePixelValue),
                new DicomSignedShort(DicomTag.LargestImagePixelValue, largestImagePixelValue),
                new DicomCodeString(DicomTag.VOILUTFunction, voiLutFunction));

            var actual = GrayscaleRenderOptions.FromImagePixelValueTags(dataset);

            Assert.Equal(expectedWindowWidth, actual.WindowWidth);
            Assert.Equal(expectedWindowCenter, actual.WindowCenter);
        }

        [Theory]
        [InlineData((ushort)16, (ushort)12, 1.0, 0.0, (short)-150, (short)1050, "LINEAR", 1200.0, 450.0)]
        public void FromDataset_PixelLimits_ReturnsSameAsFromImagePixelValueTags(
            ushort bitsAllocated,
            ushort bitsStored,
            double rescaleSlope,
            double rescaleIntercept,
            short smallestImagePixelValue,
            short largestImagePixelValue,
            string voiLutFunction,
            double expectedWindowWidth,
            double expectedWindowCenter)
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, bitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, bitsStored),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 1),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)rescaleSlope),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)rescaleIntercept),
                new DicomSignedShort(DicomTag.SmallestImagePixelValue, smallestImagePixelValue),
                new DicomSignedShort(DicomTag.LargestImagePixelValue, largestImagePixelValue),
                new DicomCodeString(DicomTag.VOILUTFunction, voiLutFunction));

            var expected = GrayscaleRenderOptions.FromImagePixelValueTags(dataset);
            var actual = GrayscaleRenderOptions.FromDataset(dataset);

            Assert.Equal(expected.WindowWidth, actual.WindowWidth);
            Assert.Equal(expected.WindowCenter, actual.WindowCenter);
        }

        [Theory]
        [InlineData((ushort)16, (ushort)12, 1.0, 0.0, (ushort)150, (ushort)1050, "LINEAR", 900.0, 600.0)]
        public void FromImagePixelValueTags_ValidUnsignedInput_CorrectOutput(
            ushort bitsAllocated,
            ushort bitsStored,
            double rescaleSlope,
            double rescaleIntercept,
            ushort smallestImagePixelValue,
            ushort largestImagePixelValue,
            string voiLutFunction,
            double expectedWindowWidth,
            double expectedWindowCenter)
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, bitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, bitsStored),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 0),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)rescaleSlope),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)rescaleIntercept),
                new DicomUnsignedShort(DicomTag.SmallestImagePixelValue, smallestImagePixelValue),
                new DicomUnsignedShort(DicomTag.LargestImagePixelValue, largestImagePixelValue),
                new DicomCodeString(DicomTag.VOILUTFunction, voiLutFunction));

            var actual = GrayscaleRenderOptions.FromImagePixelValueTags(dataset);

            Assert.Equal(expectedWindowWidth, actual.WindowWidth);
            Assert.Equal(expectedWindowCenter, actual.WindowCenter);
        }

        [Fact]
        public void FromImagePixelValueTags_SmallestGreaterThanLargest_Throws()
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, 8),
                new DicomUnsignedShort(DicomTag.BitsStored, 8),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 0),
                new DicomDecimalString(DicomTag.RescaleSlope, (decimal)1),
                new DicomDecimalString(DicomTag.RescaleIntercept, (decimal)0),
                new DicomUnsignedShort(DicomTag.SmallestImagePixelValue, 180),
                new DicomUnsignedShort(DicomTag.LargestImagePixelValue, 90),
                new DicomCodeString(DicomTag.VOILUTFunction, "LINEAR"));

            Assert.Throws<DicomImagingException>(() => GrayscaleRenderOptions.FromImagePixelValueTags(dataset));
        }

        #endregion
    }
}
