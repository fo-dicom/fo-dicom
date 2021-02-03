// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
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

        public Stream Stream { get; private set; }

        public long Position { get; private set; }

        public long Size { get; private set; }

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
            Stream.Position = Position + offset;
            Stream.Read(buffer, 0, count);
            return buffer;
        }

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(GetByteRange(offset, count), 0, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(GetByteRange(offset, count), 0, count);

    }
}
