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
    public class CompositeByteBufferTest
    {
        #region Unit tests

        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            // Arrange
            var bytes1 = Enumerable.Repeat((byte) 1, 10).ToArray();
            var bytes2 = Enumerable.Repeat((byte) 2, 10).ToArray();
            var bytes3 = Enumerable.Repeat((byte) 3, 10).ToArray();
            var memoryByteBuffer1 = new MemoryByteBuffer(bytes1);
            var memoryByteBuffer2 = new MemoryByteBuffer(bytes2);
            var memoryByteBuffer3 = new MemoryByteBuffer(bytes3);
            var compositeByteBuffer = new CompositeByteBuffer(memoryByteBuffer1, memoryByteBuffer2, memoryByteBuffer3);
            var expected = bytes1.Concat(bytes2).Concat(bytes3).ToArray();

            // Act
            var actual = compositeByteBuffer.Data;

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 30)]
        [InlineData(10, 10)]
        [InlineData(10, 20)]
        [InlineData(5, 25)]
        [InlineData(5, 0)]
        public void GetByteRange_CompareWithInitializer_ExactMatch(int offset, int count)
        {
            // Arrange
            var bytes1 = Enumerable.Repeat((byte) 1, 10).ToArray();
            var bytes2 = Enumerable.Repeat((byte) 2, 10).ToArray();
            var bytes3 = Enumerable.Repeat((byte) 3, 10).ToArray();
            var memoryByteBuffer1 = new MemoryByteBuffer(bytes1);
            var memoryByteBuffer2 = new MemoryByteBuffer(bytes2);
            var memoryByteBuffer3 = new MemoryByteBuffer(bytes3);
            var compositeByteBuffer = new CompositeByteBuffer(memoryByteBuffer1, memoryByteBuffer2, memoryByteBuffer3);
            var expected = bytes1.Concat(bytes2).Concat(bytes3).Skip(offset).Take(count).ToArray();

            // Act
            var actual = new byte[count];
            compositeByteBuffer.GetByteRange(offset, count, actual);

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByteRange_WhenBufferIsSmallerThanRequestedCount_Throws()
        {
            // Arrange
            var bytes1 = Enumerable.Repeat((byte) 1, 10).ToArray();
            var bytes2 = Enumerable.Repeat((byte) 2, 10).ToArray();
            var bytes3 = Enumerable.Repeat((byte) 3, 10).ToArray();
            var memoryByteBuffer1 = new MemoryByteBuffer(bytes1);
            var memoryByteBuffer2 = new MemoryByteBuffer(bytes2);
            var memoryByteBuffer3 = new MemoryByteBuffer(bytes3);
            var compositeByteBuffer = new CompositeByteBuffer(memoryByteBuffer1, memoryByteBuffer2, memoryByteBuffer3);

            // Act + Assert
            var actual = new byte[10];
            Assert.Throws<ArgumentException>(() => compositeByteBuffer.GetByteRange(0, 20, actual));
        }

        [Fact]
        public void CopyToStream_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes1 = Enumerable.Repeat((byte) 1, 10).ToArray();
            var bytes2 = Enumerable.Repeat((byte) 2, 10).ToArray();
            var bytes3 = Enumerable.Repeat((byte) 3, 10).ToArray();
            var memoryByteBuffer1 = new MemoryByteBuffer(bytes1);
            var memoryByteBuffer2 = new MemoryByteBuffer(bytes2);
            var memoryByteBuffer3 = new MemoryByteBuffer(bytes3);
            var compositeByteBuffer = new CompositeByteBuffer(memoryByteBuffer1, memoryByteBuffer2, memoryByteBuffer3);
            var expected = bytes1.Concat(bytes2).Concat(bytes3).ToArray();
            using var ms = new MemoryStream(new byte[30]);

            // Act
            compositeByteBuffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes1 = Enumerable.Repeat((byte) 1, 10).ToArray();
            var bytes2 = Enumerable.Repeat((byte) 2, 10).ToArray();
            var bytes3 = Enumerable.Repeat((byte) 3, 10).ToArray();
            var memoryByteBuffer1 = new MemoryByteBuffer(bytes1);
            var memoryByteBuffer2 = new MemoryByteBuffer(bytes2);
            var memoryByteBuffer3 = new MemoryByteBuffer(bytes3);
            var compositeByteBuffer = new CompositeByteBuffer(memoryByteBuffer1, memoryByteBuffer2, memoryByteBuffer3);
            var expected = bytes1.Concat(bytes2).Concat(bytes3).ToArray();
            using var ms = new MemoryStream(new byte[30]);

            // Act
            await compositeByteBuffer.CopyToStreamAsync(ms, CancellationToken.None);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
