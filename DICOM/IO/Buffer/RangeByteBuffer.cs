// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Buffer
{

    public class RangeByteBuffer : IByteBuffer
    {

        public RangeByteBuffer(IByteBuffer buffer, long offset, int length)
        {
            Internal = buffer;
            Offset = offset;
            Length = length;
        }

        public IByteBuffer Internal { get; private set; }

        public long Offset { get; private set; }

        public int Length { get; private set; }

        public bool IsMemory => Internal.IsMemory;

        public long Size => Length;

        public byte[] Data => Internal.GetByteRange(Offset, Length);

        public byte[] GetByteRange(long offset, int count)
        {
            return Internal.GetByteRange(Offset + offset, count);
        }
    }
}
