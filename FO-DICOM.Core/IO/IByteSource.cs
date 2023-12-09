// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System.Threading.Tasks;
using System.IO;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Delegate for <see cref="IByteSource"/> callback functions.
    /// </summary>
    /// <param name="source">Byte source.</param>
    /// <param name="state">Callback state.</param>
    public delegate void ByteSourceCallback(IByteSource source, object state);

    /// <summary>
    /// Byte source interface for reading operations.
    /// </summary>
    public interface IByteSource
    {
        /// <summary>
        /// Gets or sets the endianness.
        /// </summary>
        Endian Endian { get; set; }

        /// <summary>
        /// Gets the current read position.
        /// </summary>
        long Position { get; }

        /// <summary>
        /// Gets the position of the current marker.
        /// </summary>
        long Marker { get; }

        /// <summary>
        /// Gets whether end-of-source is reached.
        /// </summary>
        bool IsEOF { get; }

        /// <summary>
        /// Gets whether its possible to rewind the source.
        /// </summary>
        bool CanRewind { get; }

        /// <summary>
        /// Gets the milestone levels count.
        /// </summary>
        int MilestonesCount { get; }

        /// <summary>
        /// Gets one byte from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single byte.</returns>
        byte GetUInt8();

        /// <summary>
        /// Gets a signed short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed short.</returns>
        short GetInt16();

        /// <summary>
        /// Gets an unsigned short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned short.</returns>
        ushort GetUInt16();

        /// <summary>
        /// Gets a signed integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed integer.</returns>
        int GetInt32();

        /// <summary>
        /// Gets an unsigned integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned integer.</returns>
        uint GetUInt32();

        /// <summary>
        /// Gets a signed long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed long.</returns>
        long GetInt64();

        /// <summary>
        /// Gets an unsigned long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned long.</returns>
        ulong GetUInt64();

        /// <summary>
        /// Gets a single precision floating point value (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single precision floating point value.</returns>
        float GetSingle();

        /// <summary>
        /// Gets a double precision floating point value (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Double precision floating point value.</returns>
        double GetDouble();

        /// <summary>
        /// Gets a specified number of bytes from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Array of bytes.</returns>
        byte[] GetBytes(int count);

        /// <summary>
        /// Gets a specified number of bytes from the current position and moves to subsequent position.
        /// The bytes will be written to <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer">The buffer to write the bytes to</param>
        /// <param name="index">The index in the buffer at which to start writing</param>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>The number of bytes that were filled in the buffer</returns>
        int GetBytes(byte[] buffer, int index, int count);

        /// <summary>
        /// Gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte buffer containing the read bytes.</returns>
        IByteBuffer GetBuffer(uint count);

        /// <summary>
        /// Asynchronously gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Awaitable byte buffer containing the read bytes.</returns>
        Task<IByteBuffer> GetBufferAsync(uint count);

        /// <summary>
        /// Skip position <paramref name="count"/> number of bytes.
        /// </summary>
        /// <param name="count">Number of bytes to skip.</param>
        void Skip(uint count);

        /// <summary>
        /// Set a mark at the current position.
        /// </summary>
        void Mark();

        /// <summary>
        /// Rewind byte source to latest <see cref="Marker"/>.
        /// </summary>
        void Rewind();

        /// <summary>
        /// Mark the position of a new level of milestone.
        /// </summary>
        /// <param name="count">Expected distance in bytes from the current position to the milestone.</param>
        void PushMilestone(uint count);

        /// <summary>
        /// Pop the uppermost level of milestone.
        /// </summary>
        void PopMilestone();

        /// <summary>
        /// Checks whether the byte source position is at the uppermost milestone position.
        /// </summary>
        /// <returns>true if uppermost milestone is reached, false otherwise.</returns>
        bool HasReachedMilestone();

        /// <summary>
        /// Verifies that there is a sufficient number of bytes to read.
        /// </summary>
        /// <param name="count">Required number of bytes.</param>
        /// <returns>true if source contains sufficient number of remaining bytes, false otherwise.</returns>
        bool Require(uint count);

        /// <summary>
        /// Verifies that there is a sufficient number of bytes to read.
        /// </summary>
        /// <param name="count">Required number of bytes.</param>
        /// <param name="callback">Byte source callback.</param>
        /// <param name="state">Callback state.</param>
        /// <returns>true if source contains sufficient number of remaining bytes, false otherwise.</returns>
        bool Require(uint count, ByteSourceCallback callback, object state);

        /// <summary>
        /// Get stream of this byte source.
        /// </summary>
        /// <returns>The stream.</returns>
        Stream GetStream();

    }
}
