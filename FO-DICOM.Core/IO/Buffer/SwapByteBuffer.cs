// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public class SwapByteBuffer : IByteBuffer
    {
        public SwapByteBuffer(IByteBuffer buffer, int unitSize)
        {
            Internal = buffer;
            UnitSize = unitSize;
        }

        public IByteBuffer Internal { get; private set; }

        public int UnitSize { get; private set; }

        public bool IsMemory => Internal.IsMemory;

        public long Size => Internal.Size;

        public byte[] Data
        {
            get
            {
                byte[] data = null;
                if (IsMemory)
                {
                    data = new byte[Size];
                    System.Buffer.BlockCopy(Internal.Data, 0, data, 0, data.Length);
                }
                else
                {
                    data = Internal.Data;
                }

                Endian.SwapBytes(UnitSize, data);

                return data;
            }
        }

        public byte[] GetByteRange(long offset, int count)
        {
            byte[] data = Internal.GetByteRange(offset, count);
            Endian.SwapBytes(UnitSize, data);
            return data;
        }

        public void GetByteRange(long offset, int count, byte[] output)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (output.Length < count)
            {
                throw new ArgumentException($"Output array with {output.Length} bytes cannot fit {count} bytes of data");
            }
            
            Internal.GetByteRange(offset, count, output);
            Endian.SwapBytes(UnitSize, output);
        }

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