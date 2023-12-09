// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class MemoryByteBuffer : IByteBuffer
    {
        /// <summary>
        /// This is the maximum number of bytes C# allows to store in a byte array (note that it is slightly lower than int.MaxValue)
        /// </summary>
        public const int MaxArrayLength = 2_147_483_591;
        
        /// <summary>
        /// Creates a new MemoryByteBuffer based on a byte-array. This class takes over ownership of the array, so only pass an array that will not be used/manipulated by other classes, or pass a new instance of byte array
        /// </summary>
        /// <param name="data"></param>
        public MemoryByteBuffer(byte[] data)
        {
            Data = data;
        }

        public bool IsMemory => true;

        public byte[] Data { get; }

        public long Size => Data.Length;

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

            Array.Copy(Data, (int)offset, output, 0, count);
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

            byte[] data = Data;

            stream.Write(data, 0, data.Length);
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

            byte[] data = Data;

            await stream.WriteAsync(data, 0, data.Length, cancellationToken).ConfigureAwait(false);
        }
    }
}
