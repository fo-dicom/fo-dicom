// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Linq;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{
    [Collection(TestCollections.General)]
    public class ByteBufferByteSourceTests
    {
        [Fact]
        public void GetBytes_ReadSpansTwoBuffers_CopiesOnlyCountBytes()
        {
            // Arrange
            var bufferA = new MemoryByteBuffer(new byte[] { 1, 2, 3, 4 });
            var bufferB = new MemoryByteBuffer(Enumerable.Range(5, 10).Select(i => (byte)i).ToArray());
            var source = new ByteBufferByteSource(bufferA, bufferB);
            source.Skip(2);
            var output = new byte[10];

            // Act
            var read = source.GetBytes(output, 0, 5);

            // Assert
            Assert.Equal(5, read);
            Assert.Equal(new byte[] { 3, 4, 5, 6, 7, 0, 0, 0, 0, 0 }, output);
            Assert.Equal(7, source.Position);
        }
        
        [Fact]
        public void GetBytes_BufferTooSmallForIndexPlusCount_ThrowsWithoutAdvancing()
        {
            // Arrange
            var bufferA = new MemoryByteBuffer(new byte[] { 1, 2, 3, 4 });
            var bufferB = new MemoryByteBuffer(new byte[] { 5, 6, 7, 8 });
            var source = new ByteBufferByteSource(bufferA, bufferB);
            var output = new byte[6];

            // Act & Assert
            Assert.Throws<ArgumentException>(() => source.GetBytes(output, 2, 5));
            Assert.Equal(0, source.Position);
        }
    }
}