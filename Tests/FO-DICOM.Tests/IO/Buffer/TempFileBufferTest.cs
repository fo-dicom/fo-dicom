// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{

    [Collection(TestCollections.General)]
    public class TempFileBufferTest
    {
        #region Unit tests

        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            var expected = new byte[254];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = (byte)i;
            }
            var buffer = new TempFileBuffer(expected);

            var actual = buffer.Data;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(20, 150)]
        [InlineData(0, 128)]
        [InlineData(5, 1)]
        [InlineData(30, 224)]
        [InlineData(12, 0)]
        public void GetByteRange_CompareWithInitializer_ExactMatch(int offset, int count)
        {
            var data = new byte[255];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)i;
            }
            var buffer = new TempFileBuffer(data);

            var expected = new ArraySegment<byte>(data, offset, count);
            var actual = new byte[count];
            buffer.GetByteRange(offset, count, actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CopyToStream_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[10_000];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) (i % 256);
            }
            var fileByteBuffer = new TempFileBuffer(bytes);

            using var ms = new MemoryStream(bytes.Length);

            // Act
            fileByteBuffer.CopyToStream(ms);

            // Assert
            Assert.Equal(bytes, ms.ToArray());
        }

        [Fact]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly()
        {
            // Arrange
            var bytes = new byte[10_000];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) (i % 256);
            }
            var fileByteBuffer = new TempFileBuffer(bytes);

            using var ms = new MemoryStream(bytes.Length);

            // Act
            await fileByteBuffer.CopyToStreamAsync(ms, CancellationToken.None);

            // Assert
            Assert.Equal(bytes, ms.ToArray());
        }

        #endregion
    }
}
