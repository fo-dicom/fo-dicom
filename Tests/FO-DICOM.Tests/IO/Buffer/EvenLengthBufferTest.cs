// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{

    [Collection("General")]
    public class EvenLengthBufferTest
    {
        [Theory]
        [InlineData(0, 8)]
        [InlineData(4, 4)]
        [InlineData(5, 3)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var evenLengthBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var buffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));
            var range = new byte[count];
            buffer.GetByteRange(offset, count, range);
            Assert.Equal(count, range.Length);
            Assert.Equal(evenLengthBytes.Skip(offset).Take(count).ToArray(), range);
        }

        [Fact]
        public void CopyToStream_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var evenLengthBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var evenLengthBuffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            using var ms = new MemoryStream(new byte[8]);

            // Act
            evenLengthBuffer.CopyToStream(ms);

            // Assert
            Assert.Equal(evenLengthBytes, ms.ToArray());
        }

        [Fact]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var evenLengthBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 };
            var evenLengthBuffer = EvenLengthBuffer.Create(new MemoryByteBuffer(bytes));

            using var ms = new MemoryStream(new byte[8]);

            // Act
            await evenLengthBuffer.CopyToStreamAsync(ms, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.Equal(evenLengthBytes, ms.ToArray());
        }
    }
}
