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
    /// Temporary file-based byte buffer.
    /// </summary>
    public sealed class TempFileBuffer : IByteBuffer
    {
        #region FIELDS

        private readonly IFileReference _file;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="TempFileBuffer"/> object.
        /// </summary>
        /// <param name="data">Byte array subject to buffering.</param>
        public TempFileBuffer(byte[] data)
        {
            _file = TemporaryFile.Create();
            Size = data.Length;

            using var stream = _file.OpenWrite();
            stream.Write(data, 0, (int)Size);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets whether data is buffered in memory or not.
        /// </summary>
        public bool IsMemory => false;

        /// <summary>
        /// Gets the size of the buffered data.
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                var size = (int)Size;
                var data = new byte[size];
                GetByteRange(0, size, data);
                return data;
            }
        }

        #endregion

        #region METHODS

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
            
            using var fs = _file.OpenRead();
            
            fs.Seek(offset, SeekOrigin.Begin);
            fs.Read(output, 0, count);
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

            using var fs = _file.OpenRead();

            fs.CopyTo(stream);
        }

        public async Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }

            using var fs = _file.OpenRead();

            await fs.CopyToAsync(stream).ConfigureAwait(false);
        }

        #endregion
    }
}