// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{

    /// <summary>
    /// Representation of an endian-aware byte buffer.
    /// </summary>
    public class EndianByteBuffer : IByteBuffer
    {
        /// <summary>
        /// Initializes an instance of the <see cref="EndianByteBuffer"/> class.
        /// </summary>
        /// <param name="buffer">Original byte buffer.</param>
        /// <param name="endian">Endianness of the <paramref name="buffer"/>.</param>
        /// <param name="unitSize">Unit size of the components in the byte buffer.</param>
        private EndianByteBuffer(IByteBuffer buffer, Endian endian, int unitSize)
        {
            Internal = buffer;
            Endian = endian;
            UnitSize = unitSize;
        }

        /// <summary>
        /// Gets the original representation of the byte buffer.
        /// </summary>
        public IByteBuffer Internal { get; }

        /// <summary>
        /// Gets the endianness of the byte buffer.
        /// </summary>
        public Endian Endian { get; }

        /// <summary>
        /// Gets the unit size of the components in the byte buffer, typically 1 for bytes and 2 for words.
        /// </summary>
        public int UnitSize { get; }

        /// <inheritdoc />
        public bool IsMemory => Internal.IsMemory;

        /// <inheritdoc />
        public long Size => Internal.Size;

        /// <inheritdoc />
        public byte[] Data
        {
            get
            {
                byte[] data;
                if (IsMemory)
                {
                    data = new byte[Size];
                    System.Buffer.BlockCopy(Internal.Data, 0, data, 0, data.Length);
                }
                else
                {
                    data = Internal.Data;
                }

                if (Endian != Endian.LocalMachine)
                {
                    Endian.SwapBytes(UnitSize, data);
                }

                return data;
            }
        }

        /// <inheritdoc />
        public byte[] GetByteRange(long offset, int count)
        {
            var data = Internal.GetByteRange(offset, count);

            if (Endian != Endian.LocalMachine)
            {
                Endian.SwapBytes(UnitSize, data);
            }

            return data;
        }

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(GetByteRange(offset, count), 0, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(GetByteRange(offset, count), 0, count);

        /// <summary>
        /// Creates a <see cref="IByteBuffer"/> accounting for endianness and unit size.
        /// </summary>
        /// <param name="buffer">Original byte buffer.</param>
        /// <param name="endian">Requested endianness.</param>
        /// <param name="unitSize">Unit size of the individual components in the <paramref name="buffer"/>.</param>
        /// <returns>If required given the <paramref name="endian">endianness</paramref> of the local machine and the 
        /// byte buffer <paramref name="unitSize">component size</paramref>, creates an instance of the 
        /// <see cref="EndianByteBuffer"/> class, otherwise returns the original <paramref name="buffer"/>.</returns>
        public static IByteBuffer Create(IByteBuffer buffer, Endian endian, int unitSize)
            => endian == Endian.LocalMachine || unitSize == 1 
            ? buffer 
            : new EndianByteBuffer(buffer, endian, unitSize);
    }
}
