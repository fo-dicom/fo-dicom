// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;

namespace FellowOakDicom.IO
{
    /// <summary>
    /// This class is a buffered read-only wrapper around another stream. 
    /// The parser requires to seek within the stream, so when reading from an unseekable stream, such a buffer is necessary.
    /// Because the parser does not jump very wide, a small buffer is just fine.
    /// </summary>
    public class ReadBufferedStream : Stream
    {
        /*  .............................................    underlying stream
         *       [++++++++++++][++++++++++++]               seekbackbuffer and seekcurrentbuffer
         *                |                 |
         *      seekbackbufferposition    underlyingposition
         *      
         * this stream is a cyclic buffer. when reading from the underlying (unseekable) buffer, then 
         * always a array of SeekBackBufferSize is read. But because seeking back also should always
         * be possible, there are two such buffers. so when the current read-position (SeekBackBufferPosition) reaches the
         * end of the buffers close to the underlyingposition, then the SeekCurrentBuffer is set to the SeekBackBuffer and
         * the next data is loaded into SeekCurrentBuffer.
         * With that, a seek back by SeekBackBufferSize bytes is always possible.
         */

        private long _underlyingPosition;
        private long _underlyingLastReadPosition;
        private long _seekBackBufferPosition;

        private byte[] _seekBackBuffer;
        private byte[] _seekCurrentBuffer;
        private readonly int _seekBackBufferSize;
        private bool _hasReachedEnd = false;

        private readonly Stream _underlyingStream;

        public ReadBufferedStream(Stream underlyingStream, int seekBackBufferSize)
        {
            if (!underlyingStream.CanRead)
            {
                throw new NotImplementedException("Provided stream " + underlyingStream + " is not readable");
            }

            _underlyingStream = underlyingStream;
            _seekBackBufferSize = seekBackBufferSize;
            _seekBackBuffer = new byte[seekBackBufferSize];
            _seekCurrentBuffer = new byte[seekBackBufferSize];
        }

        public override bool CanRead => true;
        public override bool CanSeek => true;

        public override int Read(byte[] buffer, int offset, int count)
        {
            int copiedFromBackBufferCount = 0;

            if (_seekBackBufferPosition < _underlyingPosition - _seekBackBufferSize)
            {
                // copy from seekBackBuffer
                var posDiff = (int)(_underlyingPosition - _seekBackBufferPosition);
                var countToCopy = Math.Min(count, posDiff - _seekBackBufferSize);
                System.Buffer.BlockCopy(_seekBackBuffer, 2 * _seekBackBufferSize - posDiff, buffer, offset, countToCopy);
                offset += countToCopy;
                count -= countToCopy;
                _seekBackBufferPosition += countToCopy;
                copiedFromBackBufferCount += countToCopy;
            }

            while (count > 0 && _seekBackBufferPosition < Length)
            {

                if (_underlyingPosition == _seekBackBufferPosition)
                {
                    ReadNextBlock();
                }

                // copy from seekCurrentBuffer
                var posDiff = (int)(_underlyingPosition - _seekBackBufferPosition);
                var countToCopy = Math.Min(count, Math.Min(posDiff, (int)(_underlyingLastReadPosition - _seekBackBufferPosition)));
                System.Buffer.BlockCopy(_seekCurrentBuffer, _seekBackBufferSize - posDiff, buffer, offset, countToCopy);
                offset += countToCopy;
                count -= countToCopy;
                _seekBackBufferPosition += countToCopy;
                copiedFromBackBufferCount += countToCopy;
            }

            return copiedFromBackBufferCount;
        }

        private void ReadNextBlock()
        {
            if (_hasReachedEnd)
            {
                // already at the end, so do nothing
                return;
            }
            (_seekCurrentBuffer, _seekBackBuffer) = (_seekBackBuffer, _seekCurrentBuffer);

            int sumBytesRead = 0;
            int bytesRead = 0;
            do
            {
                bytesRead = _underlyingStream.Read(_seekCurrentBuffer, sumBytesRead, _seekBackBufferSize - sumBytesRead);
                sumBytesRead += bytesRead;
            } while (bytesRead > 0 && sumBytesRead < _seekBackBufferSize);

            _underlyingLastReadPosition += sumBytesRead;
            _underlyingPosition += _seekBackBufferSize;
            if (sumBytesRead < _seekBackBufferSize)
            {
                _hasReachedEnd = true;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.End)
            {
                throw new NotSupportedException();
            }

            var absoluteOffset = origin == SeekOrigin.Current
                ? offset + Position
                : offset;

            if (absoluteOffset == Position)
            {
                return Position;
            }
            else if (absoluteOffset < _underlyingPosition - 2 * _seekBackBufferSize)
            {
                throw new NotSupportedException();
            }
            else if (absoluteOffset > Length)
            {
                throw new NotSupportedException();
            }
            else if (absoluteOffset > _underlyingPosition)
            {
                return SeekForward(absoluteOffset);
            }
            else
            {
                _seekBackBufferPosition = absoluteOffset;
                return _seekBackBufferPosition;
            }
        }

        private long SeekForward(long destOffset)
        {
            while (destOffset > _underlyingPosition)
            {
                ReadNextBlock();
            }
            _seekBackBufferPosition = destOffset;
            return Position;
        }

        public override long Position
        {
            get => _seekBackBufferPosition;
            set => Seek(value, SeekOrigin.Begin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _underlyingStream.Close();
            }

            base.Dispose(disposing);
        }

        public override bool CanTimeout => _underlyingStream.CanTimeout;

        public override bool CanWrite => false;

        public override long Length => _hasReachedEnd ? _underlyingLastReadPosition : long.MaxValue;

        public override void SetLength(long value) => _underlyingStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override void Flush()
        {
        }
    }

}
