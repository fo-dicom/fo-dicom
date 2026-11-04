// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.IO
{
    [Collection(TestCollections.General)]
    public class ReadBufferedStreamTest
    {
        private const int _bufferSize = 4096;

        private class UnseekableStream(byte[] buffer) : MemoryStream(buffer)
        {
            public override bool CanSeek => false;

            public override long Length => throw new NotSupportedException();

            public override long Seek(long offset, SeekOrigin loc) => throw new NotSupportedException();

            public override long Position
            {
                get => throw new NotSupportedException();
                set => throw new NotSupportedException();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(_bufferSize - 1)]
        [InlineData(_bufferSize)]
        [InlineData(_bufferSize + 1)]
        [InlineData(2 * _bufferSize)]
        public void Length_AfterReadingExactlyAllData_IsLengthOfUnderlyingData(int dataLength)
        {
            // The parser checks for the end of data via Position >= Length before it attempts another read,
            // so Length must be correct as soon as the last byte has been read.
            var data = CreateData(dataLength);
            using var stream = new ReadBufferedStream(new UnseekableStream(data), _bufferSize);

            var actual = new byte[dataLength];
            for (var offset = 0; offset < dataLength; offset += 2)
            {
                Assert.Equal(Math.Min(2, dataLength - offset), stream.Read(actual, offset, Math.Min(2, dataLength - offset)));
            }

            Assert.Equal(data, actual);
            Assert.Equal(dataLength, stream.Length);
            Assert.Equal(dataLength, stream.Position);
        }

        [Theory]
        [InlineData(_bufferSize)]
        [InlineData(2 * _bufferSize)]
        public void Read_AtEndOfBlockAlignedData_ReturnsZero(int dataLength)
        {
            using var stream = new ReadBufferedStream(new UnseekableStream(CreateData(dataLength)), _bufferSize);
            ReadAll(stream, _bufferSize);

            Assert.Equal(0, stream.Read(new byte[2], 0, 2));
            Assert.True(stream.Position >= stream.Length);
        }

        [Fact]
        public void Seek_BackAfterDetectingEndOfBlockAlignedData_ReadsLastBytes()
        {
            var data = CreateData(2 * _bufferSize);
            using var stream = new ReadBufferedStream(new UnseekableStream(data), _bufferSize);
            ReadAll(stream, 2);
            Assert.Equal(data.Length, stream.Length);

            stream.Seek(-100, SeekOrigin.Current);
            var actual = ReadAll(stream, 7);

            Assert.Equal(data.Skip(data.Length - 100).ToArray(), actual);
        }

        private static byte[] CreateData(int length) => Enumerable.Range(0, length).Select(i => (byte)i).ToArray();

        private static byte[] ReadAll(Stream stream, int chunkSize)
        {
            using var result = new MemoryStream();
            var chunk = new byte[chunkSize];
            int read;
            while ((read = stream.Read(chunk, 0, chunkSize)) > 0)
            {
                result.Write(chunk, 0, read);
            }
            return result.ToArray();
        }
    }
}
