// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
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

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(File.GetByteRange(Position + offset, count), 0, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(File.GetByteRange(Position + offset, count), 0, count);

    }
}
