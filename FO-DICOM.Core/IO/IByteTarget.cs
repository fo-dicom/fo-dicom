// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO
{
    /// <summary>
    /// Callback delegate for asynchronous <see cref="IByteTarget"/> operations.
    /// </summary>
    /// <param name="target">Byte target.</param>
    /// <param name="state">Callback state.</param>
    public delegate void ByteTargetCallback(IByteTarget target, object state);

    /// <summary>
    /// Interface representing a byte target for write operations.
    /// </summary>
    public interface IByteTarget
    {
        /// <summary>
        /// Gets or sets the endianness of the byte target.
        /// </summary>
        Endian Endian { get; set; }

        /// <summary>
        /// Gets the current write position.
        /// </summary>
        long Position { get; }

        /// <summary>
        /// Write one <see cref="byte"/> to target.
        /// </summary>
        /// <param name="v"><see cref="byte"/> to write.</param>
        void Write(byte v);

        /// <summary>
        /// Write one <see cref="short"/> to target.
        /// </summary>
        /// <param name="v"><see cref="short"/> to write.</param>
        void Write(short v);

        /// <summary>
        /// Write one <see cref="ushort"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ushort"/> to write.</param>
        void Write(ushort v);

        /// <summary>
        /// Write one <see cref="int"/> to target.
        /// </summary>
        /// <param name="v"><see cref="int"/> to write.</param>
        void Write(int v);

        /// <summary>
        /// Write one <see cref="uint"/> to target.
        /// </summary>
        /// <param name="v"><see cref="uint"/> to write.</param>
        void Write(uint v);

        /// <summary>
        /// Write one <see cref="long"/> to target.
        /// </summary>
        /// <param name="v"><see cref="long"/> to write.</param>
        void Write(long v);

        /// <summary>
        /// Write one <see cref="ulong"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ulong"/> to write.</param>
        void Write(ulong v);

        /// <summary>
        /// Write one <see cref="float"/> to target.
        /// </summary>
        /// <param name="v"><see cref="float"/> to write.</param>
        void Write(float v);

        /// <summary>
        /// Write one <see cref="double"/> to target.
        /// </summary>
        /// <param name="v"><see cref="double"/> to write.</param>
        void Write(double v);

        /// <summary>
        /// Write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        void Write(byte[] buffer, uint offset, uint count);

        /// <summary>
        /// Asynchronously write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        /// <returns>Avaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        Task WriteAsync(byte[] buffer, uint offset, uint count);

        /// <summary>
        /// Exposes the current byte target as a writable stream
        /// Do not dispose of this stream! It will be disposed when the byte target is disposed
        /// </summary>
        /// <returns>A stream that, when written to, will write to the underlying byte target</returns>
        Stream AsWritableStream();
    }
}
