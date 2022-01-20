// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
                Stream.Position = Position;
                Stream.Read(data, 0, (int)Size);
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
            
            Stream.Position = Position + offset;
            Stream.Read(output, 0, count);
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
            
            Stream.Position = Position;
            
            long totalNumberOfBytesRead = 0L;
            int numberOfBytesToRead = (int)Math.Min(Size, bufferSize);
            int numberOfBytesRead;
            while(numberOfBytesToRead > 0
                  && (numberOfBytesRead = Stream.Read(buffer.Bytes, 0, numberOfBytesToRead)) > 0)
            {
                stream.Write(buffer.Bytes, 0, numberOfBytesRead);
                totalNumberOfBytesRead += numberOfBytesRead;
                numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, bufferSize);
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

            Stream.Position = Position;
            
            long totalNumberOfBytesRead = 0L;
            int numberOfBytesToRead = (int)Math.Min(Size, bufferSize);
            int numberOfBytesRead;
            while(numberOfBytesToRead > 0 
                && (numberOfBytesRead = await Stream.ReadAsync(buffer.Bytes, 0, numberOfBytesToRead, cancellationToken).ConfigureAwait(false)) > 0)
            {
                await stream.WriteAsync(buffer.Bytes, 0, numberOfBytesRead, cancellationToken).ConfigureAwait(false);
                totalNumberOfBytesRead += numberOfBytesRead;
                numberOfBytesToRead = (int)Math.Min(Size - totalNumberOfBytesRead, bufferSize);
            }
        }
    }
}