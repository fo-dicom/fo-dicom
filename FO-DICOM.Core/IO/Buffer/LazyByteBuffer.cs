using System;
using System.IO;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{
    public sealed class LazyByteBuffer : IByteBuffer
    {
        private readonly Func<byte[]> _lazyFunc;

        public LazyByteBuffer(Func<byte[]> lazyFunction)
        {
            _lazyFunc = lazyFunction;
        }

        public bool IsMemory => true;

        public long Size => _lazyFunc().LongLength;

        public byte[] Data => _lazyFunc();

        public void CopyToStream(Stream s, long offset, int count)
            => s.Write(_lazyFunc(), (int)offset, count);

        public Task CopyToStreamAsync(Stream s, long offset, int count)
            => s.WriteAsync(_lazyFunc(), (int)offset, count);

        public byte[] GetByteRange(long offset, int count)
        {
            var content = _lazyFunc();
            byte[] buffer = new byte[count];
            Array.Copy(content, (int)offset, buffer, 0, count);
            return buffer;
        }
            
    }
}
