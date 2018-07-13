// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Buffer
{
    /// <summary>
    /// Wrapper class for uneven length buffers that needs to be represented as even length buffers.
    /// </summary>
    public class EvenLengthBuffer : IByteBuffer
    {
        /// <summary>
        /// Initializes an instance of the <see cref="EvenLengthBuffer"/> class.
        /// </summary>
        /// <param name="buffer">Uneven length buffer.</param>
        /// <remarks>Constructor is private to ensure that instance is not created for an even length buffer. Static method <see cref="Create"/>
        /// should be used to initialize buffers.</remarks>
        private EvenLengthBuffer(IByteBuffer buffer)
        {
            Buffer = buffer;
        }

        /// <summary>
        /// Underlying uneven length buffer.
        /// </summary>
        public IByteBuffer Buffer { get; }

        /// <summary>
        /// Gets whether the buffer is held in memory.
        /// </summary>
        public bool IsMemory => Buffer.IsMemory;

        /// <summary>
        /// Gets the size of the even length buffer, which is always equal to the underlying (uneven length) buffer plus 1.
        /// </summary>
        public uint Size => Buffer.Size + 1;

        /// <summary>
        /// Gets the buffer data, which is equal to the underlying buffer data plus a padding byte at the end.
        /// </summary>
        public byte[] Data
        {
            get
            {
                var data = new byte[Size];
                System.Buffer.BlockCopy(Buffer.Data, 0, data, 0, (int)Buffer.Size);
                return data;
            }
        }

        /// <summary>
        /// Gets a subset of the data.
        /// </summary>
        /// <param name="offset">Offset from beginning of data array.</param>
        /// <param name="count">Number of bytes to return.</param>
        /// <returns>Requested sub-range of the <see name="Data"/> array.</returns>
        /// <remarks>Allows for reach to the padded byte at the end of the even length buffer.</remarks>
        public byte[] GetByteRange(int offset, int count)
        {
            var data = new byte[count];
            System.Buffer.BlockCopy(Buffer.Data, offset, data, 0, Math.Min((int)Buffer.Size - offset, count));
            return data;
        }

        /// <summary>
        /// If necessary, creates an even length buffer for the specified <paramref name="buffer"/>.
        /// </summary>
        /// <param name="buffer">Buffer that is required to be of even length.</param>
        /// <returns>
        /// If <paramref name="buffer"/> is of uneven length, returns an even length buffer wrapping the <paramref name="buffer"/>,
        /// otherwise returns the buffer itself.
        /// </returns>
        public static IByteBuffer Create(IByteBuffer buffer)
        {
            if ((buffer.Size & 1) == 1) return new EvenLengthBuffer(buffer);
            return buffer;
        }
    }
}
