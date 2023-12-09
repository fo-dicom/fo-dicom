// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
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

        
        /// <inheritdoc />
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
            
            if (_buffer == null)
            {
                throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide GetByteRange data until the Data property has been set.");
            }

            Array.Copy(Data, (int)offset, output, 0, count);
        }

        public void CopyToStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            if (_buffer == null)
            {
                throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide GetByteRange data until the Data property has been set.");
            }

            byte[] data = Data;

            stream.Write(data, 0, data.Length);
        }

        public Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            if (_buffer == null)
            {
                throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide GetByteRange data until the Data property has been set.");
            }

            byte[] data = Data;

            return stream.WriteAsync(data, 0, data.Length, cancellationToken);
        }

        /// <summary>
        /// Gets the size of the buffered data. Throws an InvalidOperationException if the Data has not been set.
        /// </summary>
        public virtual long Size
            => _buffer?.Length
               ?? throw new InvalidOperationException("BulkDataUriByteBuffer cannot provide Size until the Data property has been set.");
    }
}