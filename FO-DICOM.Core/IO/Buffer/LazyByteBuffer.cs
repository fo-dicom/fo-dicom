using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class LazyByteBuffer : IByteBuffer
    {
        private readonly Func<byte[]> _bytes;

        public LazyByteBuffer(Func<byte[]> bytes)
        {
            _bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
        }

        private byte[] Bytes => _bytes();
        
        public bool IsMemory => true;

        public long Size => Bytes.LongLength;

        public byte[] Data => Bytes;

        public byte[] GetByteRange(long offset, int count)
        {
            byte[] buffer = new byte[count];

            GetByteRange(offset, count, buffer);

            return buffer;
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

            var content = Data;

            Array.Copy(content, (int)offset, output, 0, count);
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