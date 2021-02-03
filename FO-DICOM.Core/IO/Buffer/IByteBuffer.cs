// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
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
        long Size { get; }

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
        byte[] GetByteRange(long offset, int count);

        void CopyToStream(Stream s, long offset, int count);

        Task CopyToStreamAsync(Stream s, long offset, int count);

    }
}
