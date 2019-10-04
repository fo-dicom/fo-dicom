// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.IO.Buffer
{

    public sealed class EmptyBuffer : IByteBuffer
    {
        public static readonly IByteBuffer Value = new EmptyBuffer();

        internal EmptyBuffer()
        {
            Data = new byte[0];
        }

        public bool IsMemory => true;

        public byte[] Data { get; private set; }

        public long Size => 0;

        public byte[] GetByteRange(long offset, int count)
        {
            if (offset != 0 || count != 0)
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    "Offset and count cannot be greater than 0 in EmptyBuffer");
            return Data;
        }
    }
}
