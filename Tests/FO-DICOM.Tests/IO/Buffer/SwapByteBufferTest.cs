// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{
    [Collection(TestCollections.General)]
    public class SwapByteBufferTest
    {
        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var expected = new byte[] { 2, 1, 4, 3, 6, 5, 8, 7 };
            var buffer = new SwapByteBuffer(new MemoryByteBuffer(bytes), 2);

            // Act
            var actual = buffer.Data;

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 4, 8)]
        [InlineData(0, 3, 4)]
        [InlineData(2, 5, 8)]
        public void GetByteRange_OutputLargerThanCount_ShouldOnlySwapCountBytes(int offset, int count, int outputLength)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var buffer = new SwapByteBuffer(new MemoryByteBuffer(bytes), 2);

            // Pre-fill output with distinct sentinels so bytes past count can be checked
            var sentinels = new byte[outputLength];
            for (var i = 0; i < outputLength; i++)
            {
                sentinels[i] = (byte)(0xA0 + i);
            }

            var actual = (byte[])sentinels.Clone();
            var expected = (byte[])sentinels.Clone();

            // Only whole units within count are swapped; everything else stays as-is
            System.Array.Copy(bytes, offset, expected, 0, count);
            for (var i = 0; i + 1 < count; i += 2)
            {
                // Swap the two bytes of this 2-byte unit
                var next = i + 1;
                (expected[i], expected[next]) = (expected[next], expected[i]);
            }

            // Act
            buffer.GetByteRange(offset, count, actual);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(0, 8)]
        [InlineData(4, 4)]
        [InlineData(2, 4)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var expected = new byte[] { 2, 1, 4, 3, 6, 5, 8, 7 };
            var buffer = new SwapByteBuffer(new MemoryByteBuffer(bytes), 2);

            // Act
            var actual = new byte[count];
            buffer.GetByteRange(offset, count, actual);

            // Assert
            Assert.Equal(count, actual.Length);
            Assert.Equal(expected.Skip(offset).Take(count).ToArray(), actual);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5 * (1024*1024) + 256)]
        public void CopyToStream_ShouldWorkCorrectly(int length)
        {
            // Arrange
            var bytes = new byte[length];
            var expected = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)i;
                expected[i] = i % 2 == 0
                    ? (byte)(i + 1)
                    : (byte)(i - 1);
            }

            var buffer = new SwapByteBuffer(new MemoryByteBuffer(bytes), 2);

            using var ms = new MemoryStream(length);

            // Act
            buffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5 * (1024*1024) + 256)]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly(int length)
        {
            // Arrange
            var bytes = new byte[length];
            var expected = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)i;
                expected[i] = i % 2 == 0
                    ? (byte)(i + 1)
                    : (byte)(i - 1);
            }
            var buffer = new SwapByteBuffer(new MemoryByteBuffer(bytes), 2);
            using var ms = new MemoryStream(length);

            // Act
            await buffer.CopyToStreamAsync(ms, CancellationToken.None);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
