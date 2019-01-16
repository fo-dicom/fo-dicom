// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Buffer
{
    /// <summary>
    /// Byte buffer representing a Bulk Data byte buffer, e.g. as in the DICOM Json model, in PS3.18 Chapter F.2.2.
    /// </summary>
    public class BulkDataUriByteBuffer : IBulkDataUriByteBuffer
    {
        private byte[] _buffer;

        /// <summary>
        /// Initialize the BulkData URI Byte Buffer
        /// </summary>
        /// <param name="bulkDataUri">The URI for retrieving the referenced bulk data.</param>
        public BulkDataUriByteBuffer(string bulkDataUri)
        {
            BulkDataUri = bulkDataUri;
        }

        /// <summary>
        /// Gets whether data is buffered in memory or not.
        /// </summary>
        public bool IsMemory => _buffer != null;

        /// <summary>
        /// The URI for retrieving the referenced bulk data.
        /// </summary>
        public string BulkDataUri { get; private set; }

        /// <summary>
        /// Gets or sets the bulk data. Throws an InvalidOperationException if the Data has not been set.
        /// </summary>
        public virtual byte[] Data
        {
            get => _buffer
                ?? throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide Data until either GetData() has been called.");
            set => _buffer = value;
        }

        /// <summary>
        /// Gets a subset of the data. Throws an InvalidOperationException if the Data has not been set.
        /// </summary>
        /// <param name="offset">Offset from beginning of data array.</param>
        /// <param name="count">Number of bytes to return.</param>
        /// <returns>Requested sub-range of the <see name="Data"/> array.</returns>
        public virtual byte[] GetByteRange(long offset, int count)
        {
            if (_buffer == null)
                throw new InvalidOperationException(
                    "BulkDataUriByteBuffer cannot provide GetByteRange data until the Data property has been set.");

            var range = new byte[count];
            Array.Copy(Data, (int)offset, range, 0, count);
            return range;
        }

        /// <summary>
        /// Gets the size of the buffered data. Throws an InvalidOperationException if the Data has not been set.
        /// </summary>
        public virtual long Size
        {
            get => _buffer?.Length
                ?? throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide Size until the Data property has been set.");
        }

    }
}
