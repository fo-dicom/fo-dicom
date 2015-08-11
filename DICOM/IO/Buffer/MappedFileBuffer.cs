// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Dicom.IO.Buffer
{
    public sealed class MappedFileBuffer : IByteBuffer, IDisposable
    {
        private MemoryMappedFile _file;

        private MemoryMappedViewStream _stream;

        private uint _size;

        private bool _disposed = false;

        public MappedFileBuffer(byte[] data)
        {
            _file = MemoryMappedFile.CreateNew(Guid.NewGuid().ToString(), data.Length);
            _stream = _file.CreateViewStream();
            _stream.Write(data, 0, data.Length);

            //File.WriteAllBytes(_file.Name, data);
            _size = (uint)data.Length;
        }

        ~MappedFileBuffer()
        {
            Dispose();

        }

        public bool IsMemory
        {
            get
            {
                return false;
            }
        }

        public uint Size
        {
            get
            {
                return _size;
            }
        }

        public byte[] Data
        {
            get
            {
                return GetByteRange(0, (int)_size);
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            byte[] buffer = new byte[count];

            _stream.Seek(offset, SeekOrigin.Begin);
            _stream.Read(buffer, 0, count);

            return buffer;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!_disposed)
            {
                _stream.Dispose();
                _file.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}
