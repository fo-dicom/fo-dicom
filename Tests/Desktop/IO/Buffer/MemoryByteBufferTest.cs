// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.IO.Buffer
{
    /// <summary>
    ///
    /// </summary>
    public class MemoryByteBufferTest
    {
        /// <summary>
        /// create from bytes
        /// </summary>
        [Fact]
        public void Create_From_Bytes()
        {
            byte[] bytes = { 0x01, 0x02, 0x03, 0x04 };
            var buffer = MemoryByteBuffer.Create(bytes);
            Assert.Equal(bytes, buffer.Data);
        }

        /// <summary>
        /// create from words in 'natural' byte order
        /// </summary>
        [Fact]
        public void Create_From_Words()
        {
            ushort[] words = { 0x0102, 0x0304, 0x0506, 0x0708 };
            var buffer = MemoryByteBuffer.Create(words);

            if (Endian.LocalMachine == Endian.Little)
            {
                Assert.Equal(new byte[] { 0x02, 0x01, 0x04, 0x03, 0x06, 0x05, 0x08, 0x07 }, buffer.Data);
            }
            else
            {
                Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, buffer.Data);
            }
        }
    }
}
