// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.IO;
using Dicom.IO.Buffer;
using System.Linq;
using Xunit;

namespace Dicom.Bugs
{
    public class GH625
    {

        [Fact]
        public void EndianByteBuffer_GetByteRange_ReturnsCorrectData()
        {
            var bigEndianArray = new byte[] { 0x00, 0xFF };
            var littleEndianArray = bigEndianArray.Reverse().ToArray();
            var memoryBuffer = new MemoryByteBuffer(bigEndianArray.ToArray());
            var endianArray = EndianByteBuffer.Create(memoryBuffer, Endian.Big, 2);

            var first = endianArray.GetByteRange(0, 2).ToArray();
            Assert.Equal(bigEndianArray, memoryBuffer.Data); // buffer data altered

            var second = endianArray.GetByteRange(0, 2).ToArray();
            Assert.Equal(bigEndianArray, memoryBuffer.Data); // buffer data altered

            Assert.Equal(first, littleEndianArray); // first call wrong endian
            Assert.Equal(second, littleEndianArray); // second call wrong endian
            Assert.Equal(first, second); // first not equal second
        }

    }
}
