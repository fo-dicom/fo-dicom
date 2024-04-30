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
    public sealed class StreamByteBuffer : IByteBuffer
    {
        private readonly IMemoryProvider _memoryProvider;
            
        /// <summary>
        /// Since <see cref="SemaphoreSlim"/> implements <see cref="IDisposable"/> and <see cref="StreamByteBuffer"/> doesn't,
        /// this SemaphoreSlim can't be disposed at the right time
        /// However, <a href="https://stackoverflow.com/questions/32033416/do-i-need-to-dispose-a-semaphoreslim">this excerpt from Stack Overflow</a>
        /// suggests that not disposing of SemaphoreSlim is okay as long as <see cref="SemaphoreSlim.AvailableWaitHandle"/> is never used
        /// </summary>
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public StreamByteBuffer(Stream stream, long position, long length) : this(stream, position, length, Setup.ServiceProvider.GetRequiredService<IMemoryProvider>())
        {
        }
        
        public StreamByteBuffer(Stream stream, long position, long length, IMemoryProvider memoryProvider)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            Stream = stream;
            Position = position;
            Size = length;
        }

        public bool IsMemory => false;

        public Stream Stream { get; }

        public long Position { get; }

        public long Size { get; }

        public byte[] Data
        {
            get
            {
                if (!Stream.CanRead)
                {
                    throw new DicomIoException("cannot read from stream - maybe closed");
                }

                byte[] data = new byte[Size];
                ReadStream(data, 0, Size);
                
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
            
            if (!Stream.CanRead)
            {
                throw new DicomIoException("cannot read from stream - maybe closed");
            }
            
            ReadStream(output, offset, count);
        }

        public void CopyToStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            if (!stream.CanWrite)
            {
                throw new DicomIoException("Cannot copy to non-writable stream");
            }

            if (!Stream.CanRead)
            {
                throw new DicomIoException("Cannot read from stream - maybe closed");
            }

            int bufferSize = 1024 * 1024;
            using IMemory buffer = _memoryProvider.Provide(bufferSize);

            _semaphore.Wait();
            try
            {
                Stream.Position = Position;

                long totalNumberOfBytesRead = 0L;
                int numberOfBytesToRead = (int)Math.Min(Size, bufferSize);
                int numberOfBytesRead;
                while (numberOfBytesToRead > 0
                      && (numberOfBytesRead = Stream.Read(buffer.Bytes, 0, numberOfBytesToRead)) > 0)
                {
                    stream.Write(buffer.Bytes, 0, numberOfBytesRead);
                    totalNumberOfBytesRead += numberOfBytesRead;
                    numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, bufferSize);
                }
            }
            finally
            {
                _semaphore.Release();
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
                throw new DicomIoException("Cannot copy to non-writable stream");
            }

            if (!Stream.CanRead)
            {
                throw new DicomIoException("Cannot read from stream - maybe closed");
            }

            int bufferSize = 1024 * 1024;
            using IMemory buffer = _memoryProvider.Provide(bufferSize);

            await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                Stream.Position = Position;

                long totalNumberOfBytesRead = 0L;
                int numberOfBytesToRead = (int)Math.Min(Size, bufferSize);
                int numberOfBytesRead;
                while (numberOfBytesToRead > 0
                    && !cancellationToken.IsCancellationRequested
                    && (numberOfBytesRead = await Stream.ReadAsync(buffer.Bytes, 0, numberOfBytesToRead, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    await stream.WriteAsync(buffer.Bytes, 0, numberOfBytesRead, cancellationToken).ConfigureAwait(false);
                    totalNumberOfBytesRead += numberOfBytesRead;
                    numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, bufferSize);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private void ReadStream(byte[] buffer, long offset, long count)
        {
            _semaphore.Wait();
            try
            {
                Stream.Position = Position + offset;

                long totalBytesRead = 0L;
                int bytesRemaining = (int)Math.Min(Size, count);
                int bytesReadNow;
                while (bytesRemaining > 0
                        && (bytesReadNow = Stream.Read(buffer, (int)totalBytesRead, bytesRemaining)) > 0)
                {
                    totalBytesRead += bytesReadNow;
                    bytesRemaining = (int)Math.Min(Size - totalBytesRead, count - totalBytesRead);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }
}