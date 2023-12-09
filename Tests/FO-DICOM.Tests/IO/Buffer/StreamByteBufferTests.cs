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
    public class StreamByteBufferTest
    {
        /// <summary>
        /// A fake Stream with an internal buffer.
        /// </summary>
        private class FakeBufferedStream : MemoryStream
        {
            private int _virtualBufferPosition;

            private int _virtualBufferLength;

            public FakeBufferedStream(byte[] bytes, int bufferSize = 512 * 1024) : base(bytes, 0, bytes.Length, false, true)
            {
                _virtualBufferLength = bufferSize;
                _virtualBufferPosition = 0;
            }

            /// <summary>
            /// Returns the lesser of buffer length or requested bytes. A simplified version of the corresponding
            /// method in Azure.Storage.LazyLoadingReadOnlyStream:
            /// https://github.com/Azure/azure-sdk-for-net/blob/59dbd87c84d9ebf09f9075ad30ee440a0c0a5917/sdk/storage/Azure.Storage.Common/src/Shared/LazyLoadingReadOnlyStream.cs#L167
            /// </summary>
            public override int Read(byte[] buffer, int offset, int count)
            {
                if (Position == Length) return 0;

                if (_virtualBufferPosition == _virtualBufferLength)
                {
                    _virtualBufferPosition = 0; // We've reached the end of the buffer, simulating retrieving new bytes
                }

                long remainingBytes = Math.Min(
                    _virtualBufferLength - _virtualBufferPosition,
                    Length - Position);
                int targetCapacity = Math.Min(buffer.Length, count);

                int bytesToWrite = (int)Math.Min(remainingBytes, targetCapacity);

                Array.Copy(GetBuffer(), Position, buffer, offset, bytesToWrite);

                Position += bytesToWrite;
                _virtualBufferPosition += bytesToWrite;

                return bytesToWrite;
            }
        }
        #region Unit tests

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
            using var ms = new MemoryStream(bytes);
            var buffer = new StreamByteBuffer(ms, ctorOffset, ctorCount);
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
            using var ms = new MemoryStream(bytes);
            var streamByteBuffer = new StreamByteBuffer(ms, 0, 255);

            // Act + Assert
            var buffer = new byte[10];
            Assert.Throws<ArgumentException>(() => streamByteBuffer.GetByteRange(0, 20, buffer));
        }

        [Theory]
        [InlineData(255, 20, 150)]
        [InlineData(255, 0, 128)]
        [InlineData(255, 5, 1)]
        [InlineData(255, 30, 224)]
        [InlineData(255, 12, 0)]
        [InlineData(5 * (1024 * 1024) + 256, 0, 5 * (1024 * 1024) + 256)]
        [InlineData(5 * (1024 * 1024) + 256, 0, 5 * (1024 * 1024))]
        [InlineData(5 * (1024 * 1024) + 256, 256, 5 * (1024 * 1024))]
        public void CopyToStream_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var inputMs = new MemoryStream(bytes);
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new StreamByteBuffer(inputMs, offset, count);
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
        [InlineData(5 * (1024 * 1024) + 256, 0, 5 * (1024 * 1024) + 256)]
        [InlineData(5 * (1024 * 1024) + 256, 0, 5 * (1024 * 1024))]
        [InlineData(5 * (1024 * 1024) + 256, 256, 5 * (1024 * 1024))]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var inputMs = new MemoryStream(bytes);
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new StreamByteBuffer(inputMs, offset, count);
            var expected = new ArraySegment<byte>(bytes, offset, count);

            // Act
            await buffer.CopyToStreamAsync(outputMs, CancellationToken.None);

            // Assert
            var actual = outputMs.ToArray();
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(0, 255)]
        [InlineData(0, 10)]
        [InlineData(10, 245)]
        public void DataFromBufferedStream_CompareWithInitializer_ExactMatch(int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            var expected = new ArraySegment<byte>(bytes, offset, count);
            using var fbs = new FakeBufferedStream(bytes, 32);
            var buffer = new StreamByteBuffer(fbs, offset, count);

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
        public void GetByteRangeFromBufferedStream_CompareWithInitializer_ExactMatch(int ctorOffset, int ctorCount, int byteRangeOffset, int byteRangeCount)
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            using var fbs = new FakeBufferedStream(bytes, 32);
            var buffer = new StreamByteBuffer(fbs, ctorOffset, ctorCount);
            var expected = new ArraySegment<byte>(bytes, ctorOffset + byteRangeOffset, Math.Min(ctorCount, byteRangeCount));

            // Act
            var actual = new byte[Math.Min(ctorCount, byteRangeCount)];
            buffer.GetByteRange(byteRangeOffset, byteRangeCount, actual);

            // Assert
            Assert.Equal(expected.Count, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByteRangeFromBufferedStream_WhenBufferIsSmallerThanRequestedCount_Throws()
        {
            // Arrange
            var bytes = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
            using var fbs = new FakeBufferedStream(bytes);
            var streamByteBuffer = new StreamByteBuffer(fbs, 0, 255);

            // Act + Assert
            var buffer = new byte[10];
            Assert.Throws<ArgumentException>(() => streamByteBuffer.GetByteRange(0, 20, buffer));
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
        public void CopyToStreamFromBufferedStream_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var inputFbs = new FakeBufferedStream(bytes);
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new StreamByteBuffer(inputFbs, offset, count);
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
        public async Task CopyToStreamAsyncFromBufferedStream_ShouldWorkCorrectly(int total, int offset, int count)
        {
            // Arrange
            var bytes = Enumerable.Range(0, total).Select(i => (byte)i).ToArray();
            using var inputFbs = new FakeBufferedStream(bytes);
            using var outputMs = new MemoryStream(bytes.Length);
            var buffer = new StreamByteBuffer(inputFbs, offset, count);
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
