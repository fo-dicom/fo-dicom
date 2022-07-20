// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{
    [Collection("General")]
    public class RentedMemoryBufferTest
    {
        private readonly IMemoryProvider _memoryProvider;

        public RentedMemoryBufferTest()
        {
            _memoryProvider = Setup.ServiceProvider.GetRequiredService<IMemoryProvider>();
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(4, 3)]
        [InlineData(5, 2)]
        public void Data_CompareWithInitializer_ExactMatch(int memoryOffset, int memoryCount)
        {
            var memory = _memoryProvider.Provide(254);
            var bytes = Enumerable.Range(0, 254).Select(i => (byte)i).ToArray();
            bytes.CopyTo(memory.Bytes, 0);
            using var buffer = new RentedMemoryByteBuffer(memory, memoryOffset, memoryCount);
            var expected = bytes.Skip(memoryOffset).Take(memoryCount).ToArray();

            var actual = buffer.Data;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 100, 0, 10)]
        [InlineData(100, 200, 10, 20)]
        [InlineData(100, 200, 20, 10)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int memoryOffset, int memoryCount, int offset, int count)
        {
            // Arrange
            var memory = _memoryProvider.Provide(254);
            var bytes = Enumerable.Range(0, 254).Select(i => (byte)i).ToArray();
            using var buffer = new RentedMemoryByteBuffer(memory, memoryOffset, memoryCount);
            bytes.CopyTo(memory.Bytes, 0);
            var expected = bytes.Skip(memoryOffset).Skip(offset).Take(count).ToArray();

            // Act
            var range = new byte[count];
            buffer.GetByteRange(offset, count, range);

            // Assert
            Assert.Equal(count, range.Length);
            Assert.Equal(expected, range);
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(4, 3)]
        [InlineData(5, 2)]
        public void CopyToStream_ShouldWorkCorrectly(int memoryOffset, int memoryCount)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var memory = _memoryProvider.Provide(bytes.Length);
            using var buffer = new RentedMemoryByteBuffer(memory, memoryOffset, memoryCount);
            bytes.CopyTo(memory.Bytes, 0);

            using var ms = new MemoryStream();
            var expected = bytes.Skip(memoryOffset).Take(memoryCount).ToArray();

            // Act
            buffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(4, 3)]
        [InlineData(5, 2)]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly(int memoryOffset, int memoryCount)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
            var memory = _memoryProvider.Provide(bytes.Length);
            using var buffer = new RentedMemoryByteBuffer(memory, memoryOffset, memoryCount);
            bytes.CopyTo(memory.Bytes, 0);
            var expected = bytes.Skip(memoryOffset).Take(memoryCount).ToArray();
            using var ms = new MemoryStream();

            // Act
            await buffer.CopyToStreamAsync(ms, CancellationToken.None).ConfigureAwait(false);
            var actual = ms.ToArray();


            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }
    }
}
