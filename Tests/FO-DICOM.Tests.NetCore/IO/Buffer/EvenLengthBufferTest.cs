// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{

    public class EvenLengthBufferTest
    {
        [Theory]
        [InlineData(0, 8)]
        [InlineData(4, 4)]
        [InlineData(5, 3)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            var buffer = EvenLengthBuffer.Create(new MemoryByteBuffer(new byte[] { 1, 2, 3, 4, 5, 6, 7 }));
            var range = buffer.GetByteRange(offset, count);
            Assert.Equal(count, range.Length);
        }
    }
}
