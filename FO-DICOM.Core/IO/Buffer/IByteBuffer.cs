// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Threading;
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
        /// Gets a subset of the data and fills it in the provided output buffer
        /// </summary>
        /// <param name="offset">Offset from beginning of data array.</param>
        /// <param name="count">Number of bytes to return.</param>
        /// <param name="output">The array where the data will be written to</param>
        void GetByteRange(long offset, int count, byte[] output);

        /// <summary>
        /// Copies the contents of this buffer to the provided <paramref name="stream"/>
        /// </summary>
        /// <param name="stream">A stream that will receive the contents of this buffer</param>
        void CopyToStream(Stream stream);

        /// <summary>
        /// Copies the contents of this buffer to the provided <paramref name="stream"/>
        /// </summary>
        /// <param name="stream">A stream that will receive the contents of this buffer</param>
        /// <param name="cancellationToken">A cancellation token that halts the execution of the copy operation</param>
        Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}
