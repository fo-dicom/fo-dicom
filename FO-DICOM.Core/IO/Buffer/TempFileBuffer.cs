// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
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
        private readonly List<string> _messages;

        #region FIELDS

        private readonly IFileReference _file;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="TempFileBuffer"/> object.
        /// </summary>
        /// <param name="data">Byte array subject to buffering.</param>
        public TempFileBuffer(byte[] data, List<string> messages = null)
        {
            _messages = messages ?? new List<string>();
            _messages.Add("[TempFileBuffer] Creating temporary file");
            _file = TemporaryFile.Create(messages);
            _messages.Add("[TempFileBuffer] Added temporary file : " + _file.Name);
            _messages.Add("[TempFileBuffer]          File exists : " + _file.Exists);
            _messages.Add("[TempFileBuffer]    File is temporary : " + _file.IsTempFile);
            Size = data.Length;
            _messages.Add("[TempFileBuffer] Writing data to " + _file.Name);
            
            using (var stream = _file.OpenWrite())
            {
                stream.Write(data, 0, (int)Size);
            }
            _messages.Add("[TempFileBuffer] Data has been written to " + _file.Name);
            _messages.Add("[TempFileBuffer]          File exists : " + _file.Exists);
            _messages.Add("[TempFileBuffer]    File is temporary : " + _file.IsTempFile);
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
            
            _messages.Add("[TempFileBuffer]   Getting byte range : " + _file.Name);
            _messages.Add("[TempFileBuffer]          File exists : " + _file.Exists);
            _messages.Add("[TempFileBuffer]    File is temporary : " + _file.IsTempFile);
            
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

            await fs.CopyToAsync(stream);
        }

        #endregion
    }
}