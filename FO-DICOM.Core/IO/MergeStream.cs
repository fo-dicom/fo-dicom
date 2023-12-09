// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FellowOakDicom.IO
{
    public class MergeStream : Stream
    {

        private readonly Queue<Stream> _streams;
        private long _position;

        public MergeStream(params Stream[] streams)
        {
            _streams = new Queue<Stream>(streams);
        }

        public override void Flush() => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_streams.Count == 0)
            {
                return 0;
            }

            var read = _streams.Peek().Read(buffer, offset, count);
            while (read == 0)
            {
                var _ = _streams.Dequeue();

                if (_streams.Count == 0)
                {
                    return 0;
                }

                read = _streams.Peek().Read(buffer, offset, count);
            }

            _position += read;

            return read;
        }

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _streams.Sum(s => s.Length);

        public override long Position
        {
            get => _position;
            set => throw new NotImplementedException();
        }

    }
}
