// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Buffer
{
    public sealed class FileByteBuffer : IByteBuffer
    {

        public FileByteBuffer(IFileReference file, long position, uint length)
        {
            File = file;
            Position = position;
            Size = length;
        }

        public bool IsMemory
        {
            get => false;
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
