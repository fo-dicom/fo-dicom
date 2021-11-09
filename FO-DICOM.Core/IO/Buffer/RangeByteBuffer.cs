// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Buffers;
using System.IO;
using System.Threading;
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

        public IByteBuffer Internal { get; }

        public long Offset { get; }

        public int Length { get; }

        public bool IsMemory => Internal.IsMemory;

        public long Size => Length;

        public byte[] Data => Internal.GetByteRange(Offset, Length);

        public byte[] GetByteRange(long offset, int count) => Internal.GetByteRange(Offset + offset, Math.Max(count, Length));

        public void GetByteRange(long offset, int count, byte[] output) => Internal.GetByteRange(Offset + offset, Math.Max(count, Length), output);

        public void CopyToStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            int bufferSize = 1024 * 1024;
            byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            long remaining = Size;
            long offset = 0;
            try
            {
                while (offset < remaining)
                {
                    var count = (int)Math.Min(remaining - offset, bufferSize);

                    GetByteRange(offset, count, buffer);

                    stream.Write(buffer, 0, count);

                    offset += count;
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        public async Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            int bufferSize = 1024 * 1024;
            byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            long size = Size;
            long offset = 0;
            try
            {
                while (offset < size)
                {
                    var count = (int)Math.Min(size - offset, bufferSize);

                    GetByteRange(offset, count, buffer);

                    await stream.WriteAsync(buffer, 0, count, cancellationToken);

                    offset += count;
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}