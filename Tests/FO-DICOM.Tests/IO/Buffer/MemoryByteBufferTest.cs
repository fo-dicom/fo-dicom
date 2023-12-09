// Copyright (c) 2012-2023 fo-dicom contributors.
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
    public class MemoryBufferTest
    {
        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            var expected = Enumerable.Range(0, 254).Select(i => (byte)i).ToArray();
            var buffer = new MemoryByteBuffer(expected);

            var actual = buffer.Data;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(4, 3)]
        [InlineData(5, 2)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var buffer = new MemoryByteBuffer(bytes);
            var expected = bytes.Skip(offset).Take(count).ToArray();

            // Act
            var range = new byte[count];
            buffer.GetByteRange(offset, count, range);

            // Assert
            Assert.Equal(count, range.Length);
            Assert.Equal(expected, range);
        }

        [Fact]
        public void CopyToStream_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var memoryBuffer = new MemoryByteBuffer(bytes);

            using var ms = new MemoryStream(7);
            var expected = bytes;

            // Act
            memoryBuffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var memoryBuffer = new MemoryByteBuffer(bytes);
            var expected = bytes;
            using var ms = new MemoryStream(7);

            // Act
            await memoryBuffer.CopyToStreamAsync(ms, CancellationToken.None);
            var actual = ms.ToArray();


            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }
    }
}
