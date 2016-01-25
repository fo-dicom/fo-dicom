// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Buffer
{
    public class BulkDataUriByteBuffer : IBulkDataUriByteBuffer
    {
        private byte[] _buffer;

        public BulkDataUriByteBuffer(string bulkDataUri)
        {
            BulkDataUri = bulkDataUri;
        }

        public bool IsMemory
        {
            get { return _buffer != null; }
        }

        public string BulkDataUri { get; private set; }

        public virtual byte[] Data
        {
            get
            {
                if (_buffer == null)
                    throw new InvalidOperationException(
                        "BulkDataUriByteBuffer cannot provide Data until either GetData() has been called.");
                return _buffer;
            }

            set
            {
                _buffer = value;
            }
        }

        public virtual byte[] GetByteRange(int offset, int count)
        {
            if (_buffer == null)
                throw new InvalidOperationException(
                    "BulkDataUriByteBuffer cannot provide GetByteRange data until the Data property has been set.");

            var range = new byte[count];
            Array.Copy(Data, offset, range, 0, count);
            return range;
        }

        public virtual uint Size
        {
            get
            {
                if (_buffer == null)
                    throw new InvalidOperationException(
                        "BulkDataUriByteBuffer cannot provide Size until the Data property has been set.");

                return (uint)_buffer.Length;
            }
        }
    }
}