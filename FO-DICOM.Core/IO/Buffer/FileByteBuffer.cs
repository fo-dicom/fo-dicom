// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class FileByteBuffer : IByteBuffer
    {
        public FileByteBuffer(IFileReference file, long position, long length)
        {
            File = file;
            Position = position;
            Size = length;
        }

        public bool IsMemory => false;

        public IFileReference File { get; private set; }

        public long Position { get; private set; }

        public long Size { get; private set; }

        public byte[] Data => File.GetByteRange(Position, (int)Size);

        public byte[] GetByteRange(long offset, int count)
            => File.GetByteRange(Position + offset, count);

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

            File.GetByteRange(Position + offset, count, output);
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

            var buffer = ArrayPool<byte>.Shared.Rent(1024 * 1024);
            try
            {
                using var fileStream = File.OpenRead();
                fileStream.Position = Position;

                int totalNumberOfBytesRead = 0;
                int numberOfBytesRead;
                int numberOfBytesToRead = (int)Math.Min(Size, buffer.Length);
                while (numberOfBytesToRead > 0
                       && (numberOfBytesRead = fileStream.Read(buffer, 0, numberOfBytesToRead)) > 0)
                {
                    stream.Write(buffer, 0, numberOfBytesRead);
                    totalNumberOfBytesRead += numberOfBytesRead;
                    numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, buffer.Length);
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

            var buffer = ArrayPool<byte>.Shared.Rent(1024 * 1024);
            try
            {
                using var fileStream = File.OpenRead();
                fileStream.Position = Position;

                int totalNumberOfBytesRead = 0;
                int numberOfBytesRead;
                int numberOfBytesToRead = (int)Math.Min(Size, buffer.Length);
                while (numberOfBytesToRead > 0
                       && (numberOfBytesRead = await fileStream.ReadAsync(buffer, 0, numberOfBytesToRead, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, numberOfBytesRead, cancellationToken).ConfigureAwait(false);

                    totalNumberOfBytesRead += numberOfBytesRead;
                    numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, buffer.Length);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}