// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Buffer
{
    public sealed class EmptyBuffer : IByteBuffer
    {
        public static readonly IByteBuffer Value = new EmptyBuffer();

        internal EmptyBuffer()
        {
            Data = new byte[0];
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
                return 0;
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            if (offset != 0 || count != 0)
                throw new ArgumentOutOfRangeException(
                    "offset",
                    "Offset and count cannot be greater than 0 in EmptyBuffer");
            return Data;
        }
    }
}
