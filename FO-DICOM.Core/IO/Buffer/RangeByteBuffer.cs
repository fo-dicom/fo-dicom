// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
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
            => Internal.GetByteRange(Offset + offset, count);

        public void CopyToStream(Stream s, long offset, int count)
            => Internal.CopyToStream(s, Offset + offset, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => Internal.CopyToStreamAsync(s, Offset + offset, count);

    }
}
