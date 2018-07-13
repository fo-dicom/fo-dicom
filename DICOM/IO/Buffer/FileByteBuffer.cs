// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Buffer
{
    public sealed class FileByteBuffer : IByteBuffer
    {
        public FileByteBuffer(IFileReference file, long Position, uint Length)
        {
            this.File = file;
            this.Position = Position;
            this.Size = Length;
        }

        public bool IsMemory
        {
            get
            {
                return false;
            }
        }

        public IFileReference File { get; private set; }

        public long Position { get; private set; }

        public uint Size { get; private set; }

        public byte[] Data
        {
            get
            {
                return File.GetByteRange((int)Position, (int)Size);
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            return File.GetByteRange((int)Position + offset, count);
        }
    }
}
