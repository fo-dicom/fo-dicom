// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH625
    {

        [Fact]
        public void EndianByteBuffer_GetByteRange_ReturnsCorrectData()
        {
            var bigEndianArray = new byte[] { 0x00, 0xFF };
            var littleEndianArray = bigEndianArray.Reverse().ToArray();
            var memoryBuffer = new MemoryByteBuffer(bigEndianArray.ToArray());
            var endianArray = EndianByteBuffer.Create(memoryBuffer, Endian.Big, 2);

            var first = new byte[2];
            endianArray.GetByteRange(0, 2, first);
            Assert.Equal(bigEndianArray, memoryBuffer.Data); // buffer data altered

            var second = new byte[2];
            endianArray.GetByteRange(0, 2, second);
            Assert.Equal(bigEndianArray, memoryBuffer.Data); // buffer data altered

            Assert.Equal(first, littleEndianArray); // first call wrong endian
            Assert.Equal(second, littleEndianArray); // second call wrong endian
            Assert.Equal(first, second); // first not equal second
        }

    }
}
