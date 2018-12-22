// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;

namespace Dicom.IO.Buffer
{
    public sealed class MemoryByteBuffer : IByteBuffer
    {
        public MemoryByteBuffer(byte[] Data)
        {
            int len = Data.Length;
            this.Data = new byte[len];
            System.Buffer.BlockCopy(Data, 0, this.Data, 0, len);
        }

        public bool IsMemory
        {
            get
            {
                return true;
            }
        }

        public byte[] Data { get; private set; }

        public uint Size
        {
            get
            {
                return (uint)Data.Length;
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            byte[] buffer = new byte[count];
            Array.Copy(Data, offset, buffer, 0, count);
            return buffer;
        }

        /// <summary>
        /// equivalent to MemoryByteBuffer's constructor
        /// for symmetricity.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MemoryByteBuffer Create(byte[] bytes)
        {
            return new MemoryByteBuffer(bytes);
        }

        /// <summary>
        /// create memory buffer from ushort(word) array.
        /// words are assumed to be machine-local byte order.
        /// should be consistent with EndianByteBuffer.
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static MemoryByteBuffer Create(ushort[] words)
        {
            return Create(
                words.AsParallel().SelectMany(BitConverter.GetBytes).ToArray()
            );
        }
    }
}
