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
    public sealed class FileByteBuffer : IByteBuffer
    {
        private readonly IMemoryProvider _memoryProvider;

        public FileByteBuffer(IFileReference file, long position, long length): this(file, position, length, Setup.ServiceProvider.GetRequiredService<IMemoryProvider>())
        {
        }

        public FileByteBuffer(IFileReference file, long position, long length, IMemoryProvider memoryProvider)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            File = file;
            Position = position;
            Size = length;
        }

        public bool IsMemory => false;

        public IFileReference File { get; private set; }

        public long Position { get; private set; }

        public long Size { get; private set; }

        public byte[] Data => File.GetByteRange(Position, (int)Size);

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
            
            using IMemory buffer = _memoryProvider.Provide(1024 * 1024);
            using var fileStream = File.OpenRead();
            fileStream.Position = Position;

            long totalNumberOfBytesRead = 0L;
            int numberOfBytesRead;
            int numberOfBytesToRead = (int)Math.Min(Size, buffer.Length);
            while (numberOfBytesToRead > 0
                   && (numberOfBytesRead = fileStream.Read(buffer.Bytes, 0, numberOfBytesToRead)) > 0)
            {
                stream.Write(buffer.Bytes, 0, numberOfBytesRead);
                totalNumberOfBytesRead += numberOfBytesRead;
                numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, buffer.Length);
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

            using IMemory buffer = _memoryProvider.Provide(1024 * 1024);
            using var fileStream = File.OpenRead();
            fileStream.Position = Position;

            long totalNumberOfBytesRead = 0L;
            int numberOfBytesRead;
            int numberOfBytesToRead = (int)Math.Min(Size, buffer.Length);
            while (numberOfBytesToRead > 0
                   && (numberOfBytesRead = await fileStream.ReadAsync(buffer.Bytes, 0, numberOfBytesToRead, cancellationToken).ConfigureAwait(false)) > 0)
            {
                await stream.WriteAsync(buffer.Bytes, 0, numberOfBytesRead, cancellationToken).ConfigureAwait(false);

                totalNumberOfBytesRead += numberOfBytesRead;
                numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, buffer.Length);
            }
        }
    }
}