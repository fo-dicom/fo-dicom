// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using System;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Render
{

    [Collection(TestCollections.Imaging)]
    public class PixelDataTest
    {
        #region Unit tests

        [Fact()]
        public void DicomPixelData_TestDefaultWindowing()
        {
            // an image, that contains several values in 0028,1050 and 0028,1051
            var df = DicomFile.Open(TestData.Resolve("IM-0001-0001-0001.dcm"));
            var img = new DicomImage(df.Dataset);
            img.RenderImage(0);
            Assert.Equal(df.Dataset.GetValue<double>(DicomTag.WindowWidth, 0), img.WindowWidth);
            Assert.Equal(df.Dataset.GetValue<double>(DicomTag.WindowCenter, 0), img.WindowCenter);

            // an image, that contains one windowing-setting
            df = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));
            img = new DicomImage(df.Dataset);
            img.RenderImage(0);
            Assert.Equal(df.Dataset.GetSingleValue<double>(DicomTag.WindowWidth), img.WindowWidth);
            Assert.Equal(df.Dataset.GetSingleValue<double>(DicomTag.WindowCenter), img.WindowCenter);

            // an image with no windowing-setting
            df = DicomFile.Open(TestData.Resolve("GH227.dcm"));
            img = new DicomImage(df.Dataset);
            img.RenderImage(0);
            Assert.Equal(255, img.WindowWidth);
            Assert.Equal(127.5, img.WindowCenter);
        }


        [Fact]
        public void DicomPixelData_CreateSignedGrayscale32_12BitMaskWorks()
        {
            ushort bitsAllocated = 32;
            ushort bitsStored = 12;
            ushort highBit = 11;
            ushort pixelRepresentation = 1; // signed

            int mask = (1 << (highBit + 1)) - 1;

            var origData = new int[] { -2048, -1, 0, 1, 0x7ff };

            // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
            // output is unchanged:
            var equivalentData = origData.Select(x => x & mask).ToArray();
            var equivalentData2 = origData.Select(x => x | ~mask).ToArray();

            var pixelData = CreatePixelData_(
                equivalentData,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);
            var pixelData2 = CreatePixelData_(
                equivalentData2,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);

            Assert.IsType<GrayscalePixelDataS32>(pixelData);
            Assert.IsType<GrayscalePixelDataS32>(pixelData2);
            var grayscaleData = (GrayscalePixelDataS32)pixelData;
            var grayscaleData2 = (GrayscalePixelDataS32)pixelData2;
            Assert.Equal(origData.Length, grayscaleData.Data.Length);
            Assert.Equal(origData.Length, grayscaleData2.Data.Length);
            Assert.Equal(origData, grayscaleData.Data);
            Assert.Equal(origData, grayscaleData2.Data);
        }

        [Fact]
        public void DicomPixelData_CreateSignedGrayscale16_12BitMaskWorks()
        {
            // Example 1 from Figure D-3 in PS3.5 Chapter D:
            ushort bitsAllocated = 16;
            ushort bitsStored = 12;
            ushort highBit = 11;
            ushort pixelRepresentation = 1; // signed

            var mask = (short)((1 << (highBit + 1)) - 1);

            var origData = new short[] { -2048, -1, 0, 1, 0x7ff };

            // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
            // output is unchanged:
            var equivalentData = origData.Select(x => (short)(x & mask)).ToArray();
            var equivalentData2 = origData.Select(x => (short)(x | (short)~mask)).ToArray();

            var pixelData = CreatePixelData_(
                equivalentData,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);
            var pixelData2 = CreatePixelData_(
                equivalentData2,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);

            Assert.IsType<GrayscalePixelDataS16>(pixelData);
            Assert.IsType<GrayscalePixelDataS16>(pixelData2);
            var grayscaleData = (GrayscalePixelDataS16)pixelData;
            var grayscaleData2 = (GrayscalePixelDataS16)pixelData2;
            Assert.Equal(origData.Length, grayscaleData.Data.Length);
            Assert.Equal(origData.Length, grayscaleData2.Data.Length);
            Assert.Equal(origData, grayscaleData.Data);
            Assert.Equal(origData, grayscaleData2.Data);
        }

        [Fact]
        public void DicomPixelData_CreateUnsignedGrayscale16_12BitMaskWorks()
        {
            // Example 1 from Figure D-3 in PS3.5 Chapter D:
            const ushort bitsAllocated = 16;
            const ushort bitsStored = 12;
            const ushort highBit = 11;
            const ushort pixelRepresentation = 0; // unsigned

            var mask = (ushort)((1 << (highBit + 1)) - 1);

            var origData = new ushort[] { 0, 1, 2047, 2048, 2049, 4095 };

            // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
            // output is unchanged:
            var equivalentData = origData.Select(x => (ushort)(x & mask)).ToArray();
            var equivalentData2 = origData.Select(x => (ushort)(x | ~mask)).ToArray();

            var pixelData = CreatePixelData_(
                equivalentData,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);
            var pixelData2 = CreatePixelData_(
                equivalentData2,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);

            Assert.IsType<GrayscalePixelDataU16>(pixelData);
            Assert.IsType<GrayscalePixelDataU16>(pixelData2);
            var grayscaleData = (GrayscalePixelDataU16)pixelData;
            var grayscaleData2 = (GrayscalePixelDataU16)pixelData2;
            Assert.Equal(origData.Length, grayscaleData.Data.Length);
            Assert.Equal(origData.Length, grayscaleData2.Data.Length);
            Assert.Equal(origData, grayscaleData.Data);
            Assert.Equal(origData, grayscaleData2.Data);
        }

        [Fact]
        public void DicomPixelData_CreateUnsignedGrayscale32_12BitMaskWorks()
        {
            const ushort bitsAllocated = 32;
            const ushort bitsStored = 12;
            const ushort highBit = 11;
            const ushort pixelRepresentation = 0; // unsigned

            uint mask = (1 << (highBit + 1)) - 1;

            var origData = new uint[] { 0, 1, 2047, 2048, 2049, 4095 };

            // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
            // output is unchanged:
            var equivalentData = origData.Select(x => x & mask).ToArray();
            var equivalentData2 = origData.Select(x => x | ~mask).ToArray();

            var pixelData = CreatePixelData_(
                equivalentData,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);
            var pixelData2 = CreatePixelData_(
                equivalentData2,
                bitsAllocated,
                bitsStored,
                highBit,
                pixelRepresentation,
                BitConverter.GetBytes);

            Assert.IsType<GrayscalePixelDataU32>(pixelData);
            Assert.IsType<GrayscalePixelDataU32>(pixelData2);
            var grayscaleData = (GrayscalePixelDataU32)pixelData;
            var grayscaleData2 = (GrayscalePixelDataU32)pixelData2;
            Assert.Equal(origData.Length, grayscaleData.Data.Length);
            Assert.Equal(origData.Length, grayscaleData2.Data.Length);
            Assert.Equal(origData, grayscaleData.Data);
            Assert.Equal(origData, grayscaleData2.Data);
        }

        [Theory]
        [InlineData(100, 10, 5)]
        public void GetMinMax_GrayscalePixelDataU8_IgnoresPadding(byte max, byte min, byte padding)
        {
            var pixelData = new GrayscalePixelDataU8(2, 2, new MemoryByteBuffer(new[] { max, padding, min, padding }));
            var minmax = pixelData.GetMinMax(padding);
            Assert.Equal(max, minmax.Maximum);
            Assert.Equal(min, minmax.Minimum);
        }

        [Theory]
        [InlineData(10000, 1000, 500)]
        public void GetMinMax_GrayscalePixelDataU16_IgnoresPadding(ushort max, ushort min, ushort padding)
        {
            var pixelData = new GrayscalePixelDataU16(
                2,
                2,
                new BitDepth(16, 16, 15, false),
                new MemoryByteBuffer(new[] { max, padding, min, padding }.SelectMany(BitConverter.GetBytes).ToArray()));
            var minmax = pixelData.GetMinMax(padding);
            Assert.Equal(max, minmax.Maximum);
            Assert.Equal(min, minmax.Minimum);
        }

        [Theory]
        [InlineData(10000, 0, -500)]
        public void GetMinMax_GrayscalePixelDataS16_IgnoresPadding(short max, short min, short padding)
        {
            var pixelData = new GrayscalePixelDataS16(
                2,
                2,
                new BitDepth(16, 16, 15, true),
                new MemoryByteBuffer(new[] { max, padding, min, padding }.SelectMany(BitConverter.GetBytes).ToArray()));
            var minmax = pixelData.GetMinMax(padding);
            Assert.Equal(max, minmax.Maximum);
            Assert.Equal(min, minmax.Minimum);
        }

        [Theory]
        [InlineData(10000, 1000, 500)]
        public void GetMinMax_GrayscalePixelDataU32_IgnoresPadding(uint max, uint min, uint padding)
        {
            var pixelData = new GrayscalePixelDataU32(
                2,
                2,
                new BitDepth(32, 32, 31, false),
                new MemoryByteBuffer(new[] { max, padding, min, padding }.SelectMany(BitConverter.GetBytes).ToArray()));
            var minmax = pixelData.GetMinMax((int)padding);
            Assert.Equal(max, minmax.Maximum);
            Assert.Equal(min, minmax.Minimum);
        }

        [Theory]
        [InlineData(10000, 0, -500)]
        public void GetMinMax_GrayscalePixelDataS32_IgnoresPadding(int max, int min, int padding)
        {
            var pixelData = new GrayscalePixelDataS32(
                2,
                2,
                new BitDepth(32, 32, 31, true),
                new MemoryByteBuffer(new[] { max, padding, min, padding }.SelectMany(BitConverter.GetBytes).ToArray()));
            var minmax = pixelData.GetMinMax(padding);
            Assert.Equal(max, minmax.Maximum);
            Assert.Equal(min, minmax.Minimum);
        }

        #endregion

        #region Support methods

        private static IPixelData CreatePixelData_<T>(
            T[] data,
            ushort bitsAllocated,
            ushort bitsStored,
            ushort highBit,
            ushort pixelRepresentation,
            Func<T, byte[]> getBytes)
        {
            return
                PixelDataFactory.Create(
                    DicomPixelData.Create(
                        new DicomDataset
                            {
                                {
                                    DicomTag.PhotometricInterpretation,
                                    PhotometricInterpretation.Monochrome1.Value
                                },
                                { DicomTag.BitsAllocated, bitsAllocated },
                                { DicomTag.BitsStored, bitsStored },
                                { DicomTag.HighBit, highBit },
                                { DicomTag.PixelData, data.SelectMany(getBytes).ToArray() },
                                { DicomTag.PixelRepresentation, pixelRepresentation },
                                { DicomTag.Columns, (ushort)1 },
                                { DicomTag.Rows, (ushort)data.Length }
                            }),
                    0);
        }

        #endregion
    }
}
