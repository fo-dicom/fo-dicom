// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class EmptyBuffer : IByteBuffer
    {
        public static readonly IByteBuffer Value = new EmptyBuffer();

        internal EmptyBuffer()
        {
            Data = Array.Empty<byte>();
        }

        public bool IsMemory => true;

        public byte[] Data { get; }

        public long Size => 0;

        /// <inheritdoc />
        public void GetByteRange(long offset, int count, byte[] output)
        {
            if (offset != 0 || count != 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    "Offset and count cannot be greater than 0 in EmptyBuffer");
            }
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
        }

        public Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            return Task.CompletedTask;
        }
    }
}