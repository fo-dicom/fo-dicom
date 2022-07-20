// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Memory;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class RentedMemoryByteBuffer : IByteBuffer, IDisposable
    {
        private readonly IMemory _memory;
        private readonly int _offset;
        private readonly int _count;

        /// <summary>
        /// Creates a new RentedMemoryByteBuffer based on rented memory.
        /// </summary>
        public RentedMemoryByteBuffer(IMemory memory, int offset, int count)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
            _offset = offset;
            _count = count;
        }

        public bool IsMemory => true;

        public byte[] Data => _memory.Span.Slice(_offset, _count).ToArray();

        public long Size => _count;

        /// <inheritdoc />
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

            if ((_count - offset) < count)
            {
                throw new ArgumentException($"Cannot get {count} bytes from offset {offset} because there only {_count - offset} bytes available starting from that offset");
            }

            Array.Copy(_memory.Bytes, _offset + offset, output, 0, count);
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

            stream.Write(_memory.Bytes, _offset, _count);
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

            await stream.WriteAsync(_memory.Bytes, _offset, _count, cancellationToken).ConfigureAwait(false);
        }

        public void Dispose() => _memory.Dispose();
    }
}