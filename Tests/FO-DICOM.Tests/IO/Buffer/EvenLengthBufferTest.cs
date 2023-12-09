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
    public class EvenLengthBufferTest
    {
        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var buffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            // Act
            var actual = buffer.Data;

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 8)]
        [InlineData(4, 4)]
        [InlineData(5, 3)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var buffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            // Act
            var actual = new byte[count];
            buffer.GetByteRange(offset, count, actual);

            // Assert
            Assert.Equal(count, actual.Length);
            Assert.Equal(expected.Skip(offset).Take(count).ToArray(), actual);
        }

        [Fact]
        public void CopyToStream_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var evenLengthBuffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            using var ms = new MemoryStream(new byte[8]);

            // Act
            evenLengthBuffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var evenLengthBuffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            using var ms = new MemoryStream(new byte[8]);

            // Act
            await evenLengthBuffer.CopyToStreamAsync(ms, CancellationToken.None);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
