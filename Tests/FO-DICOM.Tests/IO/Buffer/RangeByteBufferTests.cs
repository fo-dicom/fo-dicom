// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{
    [Collection(TestCollections.General)]
    public class RangeByteBufferTest
    {
        #region Unit tests

        [Theory]
        [InlineData(0, 255)]
        [InlineData(0, 10)]
        [InlineData(10, 245)]
        public void Data_CompareWithInitializer_ExactMatch(int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            var expected = new ArraySegment<byte>(bytes, offset, count);
            var buffer = new RangeByteBuffer(new MemoryByteBuffer(bytes), offset, count);

            // Act
            var actual = buffer.Data;

            // Assert
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 255, 20, 150)]
        [InlineData(0, 255, 0, 128)]
        [InlineData(0, 255, 5, 1)]
        [InlineData(0, 255, 30, 224)]
        [InlineData(0, 255, 12, 0)]
        [InlineData(50, 100, 20, 15)]
        [InlineData(50, 100, 5, 1)]
        [InlineData(50, 100, 12, 0)]
        public void GetByteRange_CompareWithInitializer_ExactMatch(int ctorOffset, int ctorCount, int byteRangeOffset, int byteRangeCount)
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            var buffer = new RangeByteBuffer(new MemoryByteBuffer(bytes), ctorOffset, ctorCount);
            var expected = new ArraySegment<byte>(bytes, ctorOffset + byteRangeOffset, Math.Min(ctorCount, byteRangeCount));

            // Act
            var actual = new byte[Math.Min(ctorCount, byteRangeCount)];
            buffer.GetByteRange(byteRangeOffset, byteRangeCount, actual);

            // Assert
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByteRange_WhenBufferIsSmallerThanRequestedCount_Throws()
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            var rangeByteBuffer = new RangeByteBuffer(new MemoryByteBuffer(bytes), 0, 255);

            // Act + Assert
            var buffer = new byte[10];
            Assert.Throws<ArgumentException>(() => rangeByteBuffer.GetByteRange(0, 20, buffer));
        }

        [Theory]
        [InlineData(255, 20, 150)]
        [InlineData(255, 0, 128)]
        [InlineData(255, 5, 1)]
        [InlineData(255, 30, 224)]
        [InlineData(255, 12, 0)]
        [InlineData(5 * (1024*1024) + 256, 0, 5 * (1024*1024) + 256)]
        [InlineData(5 * (1024*1024) + 256, 0, 5 * (1024*1024))]
        [InlineData(5 * (1024*1024) + 256, 256, 5 * (1024*1024))]
        public void CopyToStream_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new RangeByteBuffer(new MemoryByteBuffer(bytes), offset, count);
            var expected = new ArraySegment<byte>(bytes, offset, count);

            // Act
            buffer.CopyToStream(outputMs);

            // Assert
            var actual = outputMs.ToArray();
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(255, 20, 150)]
        [InlineData(255, 0, 128)]
        [InlineData(255, 5, 1)]
        [InlineData(255, 30, 224)]
        [InlineData(255, 12, 0)]
        [InlineData(5 * (1024*1024) + 256, 0, 5 * (1024*1024) + 256)]
        [InlineData(5 * (1024*1024) + 256, 0, 5 * (1024*1024))]
        [InlineData(5 * (1024*1024) + 256, 256, 5 * (1024*1024))]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var inputMs = new MemoryStream(bytes);
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new RangeByteBuffer(new MemoryByteBuffer(bytes), offset, count);
            var expected = new ArraySegment<byte>(bytes, offset, count);

            // Act
            await buffer.CopyToStreamAsync(outputMs, CancellationToken.None);

            // Assert
            var actual = outputMs.ToArray();
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
