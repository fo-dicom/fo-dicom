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
        private byte[] _data;

        /// <summary>
        /// Creates a new RentedMemoryByteBuffer based on rented memory.
        /// </summary>
        /// <param name="memory"></param>
        public RentedMemoryByteBuffer(IMemory memory)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
        }

        public bool IsMemory => true;

        public byte[] Data
        {
            get
            {
                // This should be avoided at all cost
                if (_data != null)
                {
                    _data = _memory.Span.ToArray();
                }

                return _data;
            }
        }

        public long Size => _memory.Length;

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

            Array.Copy(Data, (int)offset, output, 0, count);
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

            stream.Write(_memory.Bytes, 0, _memory.Length);
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

            await stream.WriteAsync(_memory.Bytes, 0, _memory.Length, cancellationToken).ConfigureAwait(false);
        }

        public void Dispose() => _memory.Dispose();
    }
}