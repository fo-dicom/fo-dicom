// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;
using Xunit;

namespace FellowOakDicom.Tests.IO
{
    [Collection(TestCollections.General)]
    public class EndianTest
    {
        [Fact]
        public void SwapBytes2_EvenCount_SwapsAllPairs()
        {
            var bytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
            Endian.SwapBytes2(bytes, 4);
            Assert.Equal(new byte[] { 0x34, 0x12, 0x78, 0x56 }, bytes);
        }

        [Fact]
        public void SwapBytes2_OddCount_LeavesTrailingByteUntouched()
        {
            var bytes = new byte[] { 0x12, 0x34, 0x56 };
            Endian.SwapBytes2(bytes, 3);
            // First pair swapped, last byte preserved.
            Assert.Equal(new byte[] { 0x34, 0x12, 0x56 }, bytes);
        }

        [Fact]
        public void SwapBytes2_CountLessThanArray_LeavesTrailingBytesUntouched()
        {
            var bytes = new byte[] { 0x12, 0x34, 0xAA, 0xBB };
            Endian.SwapBytes2(bytes, 2);
            Assert.Equal(new byte[] { 0x34, 0x12, 0xAA, 0xBB }, bytes);
        }

        [Fact]
        public void SwapBytes2_Span_SwapsOnlyWithinSlice()
        {
            var bytes = new byte[] { 0xAA, 0x12, 0x34, 0x56, 0x78, 0xBB };
            Endian.SwapBytes2(bytes.AsSpan(1, 4));
            Assert.Equal(new byte[] { 0xAA, 0x34, 0x12, 0x78, 0x56, 0xBB }, bytes);
        }

        [Fact]
        public void SwapBytes4_AlignedCount_SwapsAllGroups()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x11, 0x22, 0x33, 0x44 };
            Endian.SwapBytes4(bytes, 8);
            Assert.Equal(new byte[] { 0x04, 0x03, 0x02, 0x01, 0x44, 0x33, 0x22, 0x11 }, bytes);
        }

        [Fact]
        public void SwapBytes4_NonAlignedCount_LeavesTrailingBytesUntouched()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };
            Endian.SwapBytes4(bytes, 6);
            // First quad swapped, remaining 2 bytes preserved.
            Assert.Equal(new byte[] { 0x04, 0x03, 0x02, 0x01, 0x05, 0x06 }, bytes);
        }

        [Fact]
        public void SwapBytes4_Span_SwapsOnlyWithinSlice()
        {
            var bytes = new byte[] { 0xAA, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0xBB };
            Endian.SwapBytes4(bytes.AsSpan(1, 6));
            Assert.Equal(new byte[] { 0xAA, 0x04, 0x03, 0x02, 0x01, 0x05, 0x06, 0xBB }, bytes);
        }

        [Fact]
        public void SwapBytes_Span_UnitSize8_SwapsOnlyWithinSlice()
        {
            var bytes = new byte[] { 0xAA, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xBB };
            Endian.SwapBytes(8, bytes.AsSpan(1, 9));
            Assert.Equal(new byte[] { 0xAA, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x09, 0xBB }, bytes);
        }

        [Fact]
        public void SwapBytes_Span_UnitSize3_ReversesGroupsAndLeavesRemainder()
        {
            var bytes = new byte[] { 0xAA, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0xBB };
            Endian.SwapBytes(3, bytes.AsSpan(1, 8));
            Assert.Equal(new byte[] { 0xAA, 0x03, 0x02, 0x01, 0x06, 0x05, 0x04, 0x07, 0x08, 0xBB }, bytes);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void SwapBytes_Span_UnitSizeOneOrLess_LeavesBytesUntouched(int bytesToSwap)
        {
            var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            Endian.SwapBytes(bytesToSwap, bytes.AsSpan());
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, bytes);
        }

        [Fact]
        public void SwapShortArray_RoundTrip_RestoresOriginal()
        {
            var values = new short[] { 0x0102, 0x0304, -1, short.MinValue, short.MaxValue };
            var original = (short[])values.Clone();

            Endian.Swap(values);
            Endian.Swap(values);

            Assert.Equal(original, values);
        }

        [Fact]
        public void SwapIntArray_RoundTrip_RestoresOriginal()
        {
            var values = new int[] { 0x01020304, -1, int.MinValue, int.MaxValue, 0 };
            var original = (int[])values.Clone();

            Endian.Swap(values);
            Endian.Swap(values);

            Assert.Equal(original, values);
        }
    }
}
