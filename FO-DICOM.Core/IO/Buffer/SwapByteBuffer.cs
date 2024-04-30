// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public class SwapByteBuffer : IByteBuffer
    {
        private readonly IMemoryProvider _memoryProvider;

        public SwapByteBuffer(IByteBuffer buffer, int unitSize): this(buffer, unitSize, Setup.ServiceProvider.GetRequiredService<IMemoryProvider>())
        {
        }

        public SwapByteBuffer(IByteBuffer buffer, int unitSize, IMemoryProvider memoryProvider)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
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
            using IMemory buffer = _memoryProvider.Provide(bufferSize);
            long remaining = Size;
            long offset = 0;
            while (offset < remaining)
            {
                var count = (int)Math.Min(remaining - offset, bufferSize);

                GetByteRange(offset, count, buffer.Bytes);

                stream.Write(buffer.Bytes, 0, count);

                offset += count;
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
            using IMemory buffer = _memoryProvider.Provide(bufferSize);
            long size = Size;
            long offset = 0;
            while (offset < size)
            {
                var count = (int)Math.Min(size - offset, bufferSize);

                GetByteRange(offset, count, buffer.Bytes);

                await stream.WriteAsync(buffer.Bytes, 0, count, cancellationToken).ConfigureAwait(false);

                offset += count;
            }
        }
    }
}