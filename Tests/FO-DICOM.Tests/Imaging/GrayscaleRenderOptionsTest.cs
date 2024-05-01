// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection(TestCollections.WithTranscoder)]
    public class GrayscaleRenderOptionsTest
    {
        #region Unit tests

        [FactForNetCore]
        public void ColorMap_Monochrome2ImageOptions_ReturnsMonochrome2ColorMap()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset, 0);
            Assert.Same(ColorTable.Monochrome2, options.ColorMap);
        }

        [FactForNetCore]
        public void ColorMap_Setter_ReturnsSetColorMap()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset, 0);
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

        [Fact]
        public void FromDataset_WindowCenterWidth_Monochrome()
        {
            var dataset = new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME2"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, 1),
                new DicomUnsignedShort(DicomTag.BitsStored, 1),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 0));

            var actual = GrayscaleRenderOptions.FromDataset(dataset, 0);

            Assert.Equal(1, actual.WindowWidth);
            Assert.Equal(1, actual.WindowCenter);
            Assert.Equal("LINEAR", actual.VOILUTFunction);
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
            var actual = GrayscaleRenderOptions.FromDataset(dataset, 0);

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
            var actual = GrayscaleRenderOptions.FromDataset(dataset, 0);

            Assert.Equal(expected.WindowWidth, actual.WindowWidth);
            Assert.Equal(expected.WindowCenter, actual.WindowCenter);
            Assert.Equal(expectedWindowWidth, actual.WindowWidth);
            Assert.Equal(expectedWindowCenter, actual.WindowCenter);
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

        [Fact]
        public void OptionsHaveNoSequence_IfNoSequenceIsInDataset()
        {
            var dataset = ValidDataset();

            foreach (var optionFactory in OptionsFactories())
            {
                var options = optionFactory(dataset);
                Assert.Null(options.VOILUTSequence);
                Assert.Null(options.ModalityLUT);
            }
        }

        [Fact]
        public void OptionsHaveNoSequence_IfEmptySequenceIsInDataset()
        {
            var dataset = ValidDataset();
            dataset.Add(new DicomSequence(DicomTag.VOILUTSequence));
            dataset.Add(new DicomSequence(DicomTag.ModalityLUTSequence));

            foreach (var optionFactory in OptionsFactories())
            {
                var options = optionFactory(dataset);
                Assert.Null(options.VOILUTSequence);
                Assert.Null(options.ModalityLUT);
            }
        }

        [Fact]
        public void OptionsHaveSequence_IfNonEmptySequenceIsInDataset()
        {
            var dataset = ValidDataset();
            var voiLutSequence = new DicomSequence(DicomTag.VOILUTSequence);
            voiLutSequence.Items.Add(new DicomDataset());
            dataset.Add(voiLutSequence);
            var modalityLutSequence = new DicomSequence(DicomTag.ModalityLUTSequence);
            modalityLutSequence.Items.Add(ValidModalityLutSequenceItem());
            dataset.Add(modalityLutSequence);

            foreach (var optionFactory in OptionsFactories())
            {
                var options = optionFactory(dataset);
                Assert.Equal(voiLutSequence, options.VOILUTSequence);
                Assert.NotNull(options.ModalityLUT);
            }
        }

        private DicomDataset ValidModalityLutSequenceItem()
        {
            ushort zeroUS = 0;
            ushort oneUS = 1;
            return new DicomDataset()
            {
                { DicomTag.LUTDescriptor, oneUS, zeroUS, zeroUS },
                { DicomTag.LUTData, zeroUS, zeroUS, zeroUS }
            };
        }

        private DicomDataset ValidDataset()
        {
            var testValues = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };
            return new DicomDataset(
                new DicomCodeString(DicomTag.PhotometricInterpretation, "MONOCHROME1"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, 8),
                new DicomUnsignedShort(DicomTag.BitsStored, 8),
                new DicomUnsignedShort(DicomTag.HighBit, 7),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 0),
                new DicomUnsignedShort(DicomTag.Rows, 2),
                new DicomUnsignedShort(DicomTag.Columns, 4),
                new DicomDecimalString(DicomTag.WindowWidth, 100),
                new DicomDecimalString(DicomTag.WindowCenter, 50),
                new DicomUnsignedShort(DicomTag.SmallestImagePixelValue, 0),
                new DicomUnsignedShort(DicomTag.LargestImagePixelValue, 128),
                new DicomOtherByte(DicomTag.PixelData, testValues)
            );
        }

        private List<Func<DicomDataset, GrayscaleRenderOptions>> OptionsFactories()
        {
            return new List<Func<DicomDataset, GrayscaleRenderOptions>>
            {
                d => GrayscaleRenderOptions.FromDataset(d, 0),
                GrayscaleRenderOptions.FromBitRange,
                GrayscaleRenderOptions.FromMinMax,
                d => GrayscaleRenderOptions.FromWindowLevel(d, 0),
                GrayscaleRenderOptions.FromImagePixelValueTags,
                dataset => GrayscaleRenderOptions.FromHistogram(dataset),
            };
        }

        #endregion
    }
}
