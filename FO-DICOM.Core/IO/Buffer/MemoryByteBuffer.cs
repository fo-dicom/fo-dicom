// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{

    public sealed class MemoryByteBuffer : IByteBuffer
    {

        /// <summary>
        /// Creates a new MemoryByteBuffer based on a byte-array. This class takes over ownersip of the array, so only pass an array that will not be used/manipulated by other classes, or pass a new instance of byte array
        /// </summary>
        /// <param name="Data"></param>
        public MemoryByteBuffer(byte[] data)
        {
            Data = data;
        }

        public bool IsMemory => true;

        public byte[] Data { get; private set; }

        public long Size => Data.Length;

        public byte[] GetByteRange(long offset, int count)
        {
            byte[] buffer = new byte[count];
            Array.Copy(Data, (int)offset, buffer, 0, count);
            return buffer;
        }

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(Data, (int)offset, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(Data, (int)offset, count);

    }
}
