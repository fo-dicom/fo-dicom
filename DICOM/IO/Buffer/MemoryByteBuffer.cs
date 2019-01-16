// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

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

        public bool IsMemory => true;

        public byte[] Data { get; private set; }

        public long Size => Data.Length;

        public byte[] GetByteRange(long offset, int count)
        {
            byte[] buffer = new byte[count];
            Array.Copy(Data, (int)offset, buffer, 0, count);
            return buffer;
        }
    }
}
