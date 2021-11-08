using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class LazyByteBuffer : IByteBuffer
    {
        private readonly Lazy<byte[]> _bytes;

        public LazyByteBuffer(Func<byte[]> bytes)
        {
            _bytes = new Lazy<byte[]>(bytes, LazyThreadSafetyMode.PublicationOnly);
        }

        public bool IsMemory => true;

        public long Size => _bytes.Value.LongLength;

        public byte[] Data => _bytes.Value;

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(_bytes.Value, (int)offset, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(_bytes.Value, (int)offset, count);

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

            var content = _bytes.Value;

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
                throw new ArgumentException("Cannot copy to non-writable stream");
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
                throw new ArgumentException("Cannot copy to non-writable stream");
            }

            byte[] data = Data;

            await stream.WriteAsync(data, 0, data.Length, cancellationToken).ConfigureAwait(false);
        }
    }
}