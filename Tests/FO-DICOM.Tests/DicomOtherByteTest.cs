// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomOtherByteTest
    {
        #region Unit tests

        [Fact]
        public void Get_Short_ReturnsCorrectValue()
        {
            Get_SingleItem_ReturnsCorrectValue(63, (short)0x7f7e);
        }

        [Fact]
        public void Get_ShortArray_ReturnsCorrectValue()
        {
            Get_Array_ReturnsCorrectValue(31, (short)0x3f3e, 128);
        }

        [Fact]
        public void Get_UShort_ReturnsCorrectValue()
        {
            Get_SingleItem_ReturnsCorrectValue(63, (ushort)0x7f7e);
        }

        [Fact]
        public void Get_UShortArray_ReturnsCorrectValue()
        {
            Get_Array_ReturnsCorrectValue(31, (ushort)0x3f3e, 128);
        }

        [Fact]
        public void Get_Byte_ReturnsCorrectValue()
        {
            Get_SingleItem_ReturnsCorrectValue(63, (byte)0x3f);
        }

        [Fact]
        public void Get_ByteArray_ReturnsCorrectValue()
        {
            Get_Array_ReturnsCorrectValue(31, (byte)0x1f, 256);
        }

        [Fact]
        public void Get_UInt_ReturnsCorrectValue()
        {
            Get_SingleItem_ReturnsCorrectValue(63, (uint)0xfffefdfc);
        }

        [Fact]
        public void Get_UIntArray_ReturnsCorrectValue()
        {
            Get_Array_ReturnsCorrectValue(31, (uint)0x7f7e7d7c, 64);
        }

        [Fact]
        public void Get_Double_ReturnsCorrectValue()
        {
            var doubles = new double[1];
            Buffer.BlockCopy(new byte[] { 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f }, 0, doubles, 0, 8);
            Get_SingleItem_ReturnsCorrectValue(7, doubles[0]);
        }

        [Fact]
        public void Get_DoubleArray_ReturnsCorrectValue()
        {
            var doubles = new double[1];
            Buffer.BlockCopy(new byte[] { 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f }, 0, doubles, 0, 8);
            Get_Array_ReturnsCorrectValue(15, doubles[0], 32);
        }

        #endregion

        #region Helper methods

        internal static void Get_SingleItem_ReturnsCorrectValue<T>(int index, T expected)
        {
            var element = new DicomOtherByte(
                DicomTag.PixelData,
                Enumerable.Range(0, 256).Select(i => (byte)i).ToArray());
            var actual = element.Get<T>(index);
            Assert.Equal(expected, actual);
        }

        internal static void Get_Array_ReturnsCorrectValue<T>(int index, T expected, int expectedLength)
        {
            var element = new DicomOtherByte(
                DicomTag.PixelData,
                Enumerable.Range(0, 256).Select(i => (byte)i).ToArray());
            var actual = element.Get<T[]>();

            Assert.Equal(expectedLength, actual.Length);
            Assert.Equal(expected, actual[index]);
        }

        #endregion
    }
}
