// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FellowOakDicom.IO
{
    /// <summary>
    /// Factory for creating a stream byte source for reading.
    /// </summary>
    public static class StreamByteSourceFactory
    {
        /// <summary>
        /// Returns a newly created  instance of a stream byte source class.
        /// The actual class depends on the stream capabilities.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <param name="readOption">Defines the handling of large tags.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not.
        /// If 0 is passed, then the default of 64k is used.</param>
        public static IByteSource Create(Stream stream, FileReadOption readOption = FileReadOption.Default,
            int largeObjectSize = 0)
        {
            if (stream.CanSeek)
            {
                return new StreamByteSource(stream, readOption, largeObjectSize);
            }

            return new NoSeekStreamByteSource(stream, readOption, largeObjectSize);
        }
    }

    /// <summary>
    /// Stream byte source for reading streams without seek capability.
    /// </summary>
    public class NoSeekStreamByteSource : IByteSource
    {
        enum BufferState
        {
            /// <summary>
            /// Buffer is not used.
            /// </summary>
            Unused,

            /// <summary>
            /// Data is read from buffer.
            /// If the buffer is all read, the state switches to Unused.
            /// </summary>
            Read,

            /// <summary>
            /// Data is read from source and written into buffer.
            /// </summary>
            Write,

            /// <summary>
            /// Data is read from buffer.
            /// If the buffer is all read, the state switches to Write.
            /// </summary>
            ReadWrite
        }

        #region FIELDS

        /// <summary> The actual stream source. </summary>
        private readonly StreamByteSource _byteSource;

        /// <summary> The buffer needed to emulate Seek / SetPosition. </summary>
        private readonly MemoryStream _buffer;

        private readonly BinaryReader _bufferReader;

        private readonly BinaryWriter _bufferWriter;

        private bool _marked;

        private BufferState _bufferState;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="NoSeekStreamByteSource"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <param name="readOption">Defines the handling of large tags.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not.
        /// If 0 is passed, then the default of 64k is used.</param>
        public NoSeekStreamByteSource(Stream stream, FileReadOption readOption = FileReadOption.Default,
            int largeObjectSize = 0)
        {
            _byteSource = new StreamByteSource(stream, readOption, largeObjectSize);
            _buffer = new MemoryStream();
            _bufferReader = EndianBinaryReader.Create(_buffer, Endian.LocalMachine);
            _bufferWriter = EndianBinaryWriter.Create(_buffer, Endian.LocalMachine);
            _bufferState = BufferState.Unused;
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public Endian Endian
        {
            get => _byteSource.Endian;
            set => _byteSource.Endian = value;
        }

        /// <inheritdoc />
        public long Position
        {
            get
            {
                if (IsReadingBuffer)
                {
                    return _byteSource.Position + _buffer.Position - _buffer.Length;
                }

                return _byteSource.Position;
            }
        }

        /// <inheritdoc />
        public long Marker => _byteSource.Marker;

        /// <inheritdoc />
        public bool IsEOF => _byteSource.IsEOF;

        /// <inheritdoc />
        public bool CanRewind => _byteSource.CanRewind;

        /// <inheritdoc />
        public int MilestonesCount => _byteSource.MilestonesCount;

        private bool IsReadingBuffer => _bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public byte GetUInt8()
        {
            if (IsReadingBuffer)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 1)
                {
                    UpdateBufferState();
                    if (bufferByteCount == 1)
                    {
                        var read = _bufferReader.ReadByte();
                        ClearBuffer();
                        return read;
                    }

                    ClearBuffer();
                }
                else
                {
                    return _bufferReader.ReadByte();
                }
            }

            var data = _byteSource.GetUInt8();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(1);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public short GetInt16()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(2);
                var read = _bufferReader.ReadInt16();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _bufferReader.ReadInt16();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(2);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public ushort GetUInt16()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(2);
                var read = _bufferReader.ReadUInt16();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetUInt16();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(2);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public int GetInt32()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(4);
                var read = _bufferReader.ReadInt32();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetInt32();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(4);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public uint GetUInt32()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(4);
                var read = _bufferReader.ReadUInt32();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetUInt32();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(4);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public long GetInt64()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(8);
                var read = _bufferReader.ReadInt64();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetInt64();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(8);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public ulong GetUInt64()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(8);
                var read = _bufferReader.ReadUInt64();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetUInt64();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(8);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public float GetSingle()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(4);
                var read = _bufferReader.ReadSingle();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetSingle();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(4);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public double GetDouble()
        {
            if (IsReadingBuffer)
            {
                var filled = FillBufferForReading(8);
                var read = _bufferReader.ReadDouble();
                if (filled)
                {
                    ClearBuffer();
                }

                return read;
            }

            var data = _byteSource.GetDouble();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(8);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public byte[] GetBytes(int count)
        {
            if (IsReadingBuffer)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= count)
                {
                    UpdateBufferState();
                    if (bufferByteCount == count)
                    {
                        var read = _bufferReader.ReadBytes(count);
                        ClearBuffer();
                        return read;
                    }

                    ClearBuffer();

                    if (bufferByteCount == 0)
                    {
                        return _byteSource.GetBytes(count);
                    }

                    var nrBytesInBuffer = (int)(_buffer.Length - _buffer.Position);
                    var bytesInBuffer = _bufferReader.ReadBytes(nrBytesInBuffer);
                    var bytesInSource = GetBytes(count - nrBytesInBuffer);
                    return bytesInBuffer.Concat(bytesInSource).ToArray();
                }

                return _bufferReader.ReadBytes(count);
            }

            var data = _byteSource.GetBytes(count);
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(count);
                _bufferWriter.Write(data);
            }

            return data;
        }

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count)
        {
            IByteBuffer buffer;
            if (count == 0)
            {
                buffer = EmptyBuffer.Value;
            }
            else if (count >= _byteSource.LargeObjectSize && _byteSource.ReadOption == FileReadOption.ReadLargeOnDemand)
            {
                buffer = new StreamByteBuffer(_byteSource.GetStream(), _byteSource.Position, count);
                Skip(count);
            }
            else if (count >= _byteSource.LargeObjectSize && _byteSource.ReadOption == FileReadOption.SkipLargeTags)
            {
                buffer = null;
                Skip(count);
            }
            else // count < LargeObjectSize || _readOption == FileReadOption.ReadAll
            {
                buffer = new MemoryByteBuffer(GetBytes((int)count));
            }

            return buffer;
        }

        /// <inheritdoc />
        public Task<IByteBuffer> GetBufferAsync(uint count) => Task.FromResult(GetBuffer(count));

        /// <inheritdoc />
        public void Skip(uint count) => GetBytes((int)count);

        /// <inheritdoc />
        public void Mark()
        {
            // until another Mark call or a Rewind
            // all following reads will be copied into the buffer
            var position = _buffer.Position;
            _bufferState = position < _buffer.Length ? BufferState.ReadWrite : BufferState.Write;
            _marked = true;
            if (position > 0)
            {
                var nrRemainingBytes = (int)(_buffer.Length - position);
                if (nrRemainingBytes > 0)
                {
                    _buffer.Position = 0;
                    _buffer.Write(_buffer.GetBuffer(), (int)position, nrRemainingBytes);
                }

                _buffer.SetLength(nrRemainingBytes);
                _buffer.Position = 0;
            }
        }

        /// <inheritdoc />
        public void Rewind()
        {
            if (_marked)
            {
                _buffer.Position = 0;
                _bufferState = _buffer.Length > 0 ? BufferState.Read : BufferState.Unused;
                _marked = false;
            }
            else
            {
                throw new InvalidOperationException("Rewind without Mark not allowed.");
            }
        }

        /// <inheritdoc />
        public void PushMilestone(uint count) => _byteSource.PushMilestone(count);

        /// <inheritdoc />
        public void PopMilestone() => _byteSource.PopMilestone();

        /// <inheritdoc />
        public bool HasReachedMilestone() => _byteSource.HasReachedMilestone();

        /// <inheritdoc />
        public bool Require(uint count) => Require(count, null, null);

        /// <inheritdoc />
        public bool Require(uint count, ByteSourceCallback callback, object state) =>
            _byteSource.Require(count, callback, state);


        /// <inheritdoc />
        public Stream GetStream() => _byteSource.GetStream();

        private void ResizeBuffer(int count)
        {
            if (_buffer.Length < _buffer.Position + count)
            {
                // the buffer most times does not change its capacity,
                // so this is not expansive to call
                _buffer.SetLength(_buffer.Position + count);
            }
        }

        private void UpdateBufferState() =>
            _bufferState = _bufferState == BufferState.Read ? BufferState.Unused : BufferState.Write;

        private void ClearBuffer()
        {
            if (!_marked)
            {
                _buffer.SetLength(0);
            }
        }

        /// <summary>
        /// If there is not enough data to read the given count of bytes,
        /// the remaining bytes are read from the original stream and added to the buffer.
        /// We need to have the bytes as contiguous memory for subsequently using ReadXXX
        /// to read number in the correct Endianess.
        /// </summary>
        /// <param name="count">Number of bytes to read (2-8).</param>
        /// <returns>True if the buffer is exhausted after reading the given number of bytes.</returns>
        private bool FillBufferForReading(int count)
        {
            var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
            if (bufferByteCount <= count)
            {
                UpdateBufferState();
                if (bufferByteCount < count)
                {
                    var nrBytesToRead = count - bufferByteCount;
                    _bufferReader.ReadBytes(nrBytesToRead);
                    _buffer.Position -= nrBytesToRead;
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}