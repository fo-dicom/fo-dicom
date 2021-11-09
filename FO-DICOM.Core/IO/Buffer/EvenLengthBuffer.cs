// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
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
        public long Size => Buffer.Size + 1;

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
        public byte[] GetByteRange(long offset, int count)
        {
            var data = new byte[count];
            GetByteRange(offset, count, data);
            return data;
        }

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
            
            System.Buffer.BlockCopy(Buffer.Data, (int)offset, output, 0, (int)Math.Min(Buffer.Size - offset, count));
        }

        public void CopyToStream(Stream stream) => throw new NotImplementedException();

        public Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken) => throw new NotImplementedException();

        public void CopyToStream(Stream s, long offset, int count)
        {
            s.Write(Buffer.Data, (int)offset, (int)Math.Min(Buffer.Size - offset, count));
            if (count > Buffer.Size - offset)
            {
                for (var i = 0; i < count - (Buffer.Size - offset); i++)
                {
                    s.WriteByte(0);
                }
            }
        }

        public async Task CopyToStreamAsync(Stream s, long offset, int count)
        {
            await s.WriteAsync(Buffer.Data, (int)offset, (int)Math.Min(Buffer.Size - offset, count));
            if (count > Buffer.Size - offset)
            {
                for (var i = 0; i < count - (Buffer.Size - offset); i++)
                {
                    s.WriteByte(0);
                }
            }
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
            => (buffer.Size & 1) == 1
            ? new EvenLengthBuffer(buffer)
            : buffer;
    }
}
