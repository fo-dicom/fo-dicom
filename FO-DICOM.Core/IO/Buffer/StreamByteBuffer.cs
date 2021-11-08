// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class StreamByteBuffer : IByteBuffer
    {
        public StreamByteBuffer(Stream stream, long position, long length)
        {
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

        public byte[] GetByteRange(long offset, int count)
        {
            if (!Stream.CanRead)
            {
                throw new DicomIoException("cannot read from stream - maybe closed");
            }

            byte[] buffer = new byte[count];
            
            GetByteRange(offset, count, buffer);
            
            return buffer;
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
            var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(bufferSize);
            try
            {
                Stream.Position = Position;

                int numberOfBytesRead;
                while((numberOfBytesRead = Stream.Read(buffer, 0, bufferSize)) > 0)
                {
                    stream.Write(buffer, 0, numberOfBytesRead);
                }
            }
            finally
            {
                System.Buffers.ArrayPool<byte>.Shared.Return(buffer);
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
            var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(bufferSize);
            try
            {
                Stream.Position = Position;
                
                int numberOfBytesRead;
                while((numberOfBytesRead = await Stream.ReadAsync(buffer, 0, bufferSize, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, numberOfBytesRead, cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                System.Buffers.ArrayPool<byte>.Shared.Return(buffer);
            }      
        }
    }
}