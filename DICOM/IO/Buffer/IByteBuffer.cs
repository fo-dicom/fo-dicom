// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Buffer
{
    /// <summary>
    /// Common interface for byte buffers.
    /// </summary>
    public interface IByteBuffer
    {
        /// <summary>
        /// Gets whether data is buffered in memory or not.
        /// </summary>
        bool IsMemory { get; }

        /// <summary>
        /// Gets the size of the buffered data.
        /// </summary>
        uint Size { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// Gets a subset of the data.
        /// </summary>
        /// <param name="offset">Offset from beginning of data array.</param>
        /// <param name="count">Number of bytes to return.</param>
        /// <returns>Requested sub-range of the <see name="Data"/> array.</returns>
        byte[] GetByteRange(int offset, int count);
    }
}
