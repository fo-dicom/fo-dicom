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
    public class RangeByteBuffer : IByteBuffer
    {
        private readonly IMemoryProvider _memoryProvider;

        public RangeByteBuffer(IByteBuffer buffer, long offset, int length): this(buffer, offset, length, Setup.ServiceProvider.GetRequiredService<IMemoryProvider>())
        {
            
        }
        
        public RangeByteBuffer(IByteBuffer buffer, long offset, int length, IMemoryProvider memoryProvider)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            Internal = buffer;
            Offset = offset;
            Length = length;
        }

        public IByteBuffer Internal { get; }

        public long Offset { get; }

        public int Length { get; }

        public bool IsMemory => Internal.IsMemory;

        public long Size => Length;

        public byte[] Data
        {
            get
            {
                var data = new byte[Length];
                Internal.GetByteRange(Offset, Length, data);
                return data;
            }
        }

        /// <inheritdoc />
        public void GetByteRange(long offset, int count, byte[] output) => Internal.GetByteRange(Offset + offset, Math.Min(count, Length), output);

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