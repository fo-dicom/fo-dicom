// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

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

            using (var stream = _file.OpenWrite())
            {
                stream.Write(data, 0, (int)Size);
            }
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
        public long Size { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data => GetByteRange(0, (int)Size);

        #endregion

        #region METHODS

        /// <summary>
        /// Gets a subset of the data.
        /// </summary>
        /// <param name="offset">Offset from beginning of data array.</param>
        /// <param name="count">Number of bytes to return.</param>
        /// <returns>Requested sub-range of the <see name="Data"/> array.</returns>
        public byte[] GetByteRange(long offset, int count)
        {
            var buffer = new byte[count];

            using (var fs = _file.OpenRead())
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buffer, 0, count);
            }

            return buffer;
        }

        #endregion

    }
}
