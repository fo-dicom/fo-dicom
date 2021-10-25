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
    /// Stream byte source for reading.
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

        private readonly StreamByteSource _byteSource;

        private readonly MemoryStream _buffer;

        private readonly BinaryReader _bufferReader;

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
            _bufferState = BufferState.Unused;
        }

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        /// <inheritdoc />
        public Endian Endian
        {
            get => _byteSource.Endian;
            set => _byteSource.Endian = value;
        }

        /// <inheritdoc />
        public long Position => _byteSource.Position;

        /// <inheritdoc />
        public long Marker => _byteSource.Marker;

        /// <inheritdoc />
        public bool IsEOF => _byteSource.IsEOF;

        /// <inheritdoc />
        public bool CanRewind => _byteSource.CanRewind;

        /// <inheritdoc />
        public int MilestonesCount => _byteSource.MilestonesCount;

        /// <inheritdoc />
        public byte GetUInt8()
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 1)
                {
                    UpdateBufferState();
                    if (bufferByteCount == 1)
                    {
                        var read = _bufferReader.ReadByte();
                        _buffer.SetLength(0);
                        return read;
                    }

                    _buffer.SetLength(0);
                }
            }

            var data = _byteSource.GetUInt8();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(1);
                _buffer.WriteByte(data);
            }

            return data;
        }

        private void ResizeBuffer(int i)
        {
            if (_buffer.Length < _buffer.Position + i)
            {
                _buffer.SetLength(_buffer.Position + i);
            }
        }

        /// <inheritdoc />
        public short GetInt16()
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 2)
                {
                    UpdateBufferState();
                    if (bufferByteCount < 2)
                    {
                        _buffer.SetLength(0);
                    }
                }

                if (bufferByteCount >= 2)
                {
                    var read = _bufferReader.ReadInt16();
                    if (bufferByteCount == 2)
                    {
                        _buffer.SetLength(0);
                    }

                    return read;
                }
            }

            var data = _bufferReader.ReadInt16();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(2);
                _buffer.WriteByte((byte)data);
                _buffer.WriteByte((byte)(data >> 8));
            }

            return data;
        }

        /// <inheritdoc />
        public ushort GetUInt16()
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 2)
                {
                    UpdateBufferState();
                    if (bufferByteCount < 2)
                    {
                        _buffer.SetLength(0);
                    }
                }

                if (bufferByteCount >= 2)
                {
                    var read = _bufferReader.ReadUInt16();
                    if (bufferByteCount == 2)
                    {
                        _buffer.SetLength(0);
                    }

                    return read;
                }
            }

            var data = _byteSource.GetUInt16();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(2);
                _buffer.WriteByte((byte)data);
                _buffer.WriteByte((byte)(data >> 8));
            }

            return data;
        }

        /// <inheritdoc />
        public int GetInt32()
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 4)
                {
                    UpdateBufferState();
                    if (bufferByteCount == 2)
                    {
                        var lower = _bufferReader.ReadUInt16();
                        var upper = GetUInt16();
                        var read = (int)(lower + (uint)upper << 16);
                        _buffer.SetLength(0);
                        return read;
                    }

                    if (bufferByteCount < 4)
                    {
                        _buffer.SetLength(0);
                    }
                }

                if (bufferByteCount >= 4)
                {
                    var read = _bufferReader.ReadInt32();
                    if (bufferByteCount == 4)
                    {
                        _buffer.SetLength(0);
                    }

                    return read;
                }
            }

            var data = _byteSource.GetInt32();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(4);
                _buffer.WriteByte((byte)data);
                _buffer.WriteByte((byte)(data >> 8));
                _buffer.WriteByte((byte)(data >> 16));
                _buffer.WriteByte((byte)(data >> 24));
            }

            return data;
        }

        /// <inheritdoc />
        public uint GetUInt32()
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= 4)
                {
                    UpdateBufferState();

                    if (bufferByteCount == 2)
                    {
                        var lower = _bufferReader.ReadUInt16();
                        var upper = GetUInt16();
                        var read = (lower + (uint)upper << 16);
                        _buffer.SetLength(0);
                        return read;
                    }

                    if (bufferByteCount < 4)
                    {
                        _buffer.SetLength(0);
                    }
                }

                if (bufferByteCount >= 4)
                {
                    var read = _bufferReader.ReadUInt32();
                    if (bufferByteCount == 4)
                    {
                        _buffer.SetLength(0);
                    }

                    return read;
                }
            }

            var data = _byteSource.GetUInt32();
            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer(4);
                _buffer.WriteByte((byte)data);
                _buffer.WriteByte((byte)(data >> 8));
                _buffer.WriteByte((byte)(data >> 16));
                _buffer.WriteByte((byte)(data >> 24));
            }

            return data;
        }

        /// <inheritdoc />
        public long GetInt64() => _byteSource.GetInt64();

        /// <inheritdoc />
        public ulong GetUInt64() => _byteSource.GetUInt64();

        /// <inheritdoc />
        public float GetSingle() => _byteSource.GetSingle();

        /// <inheritdoc />
        public double GetDouble() => _byteSource.GetDouble();

        /// <inheritdoc />
        public byte[] GetBytes(int count)
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
                if (bufferByteCount <= count)
                {
                    UpdateBufferState();
                    if (bufferByteCount == count)
                    {
                        var read = _bufferReader.ReadBytes(count);
                        _buffer.SetLength(0);
                        return read;
                    }

                    _buffer.SetLength(0);

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
                _buffer.Write(data, 0, count);
            }

            return data;
        }

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count) => _byteSource.GetBuffer(count);

        /// <inheritdoc />
        public Task<IByteBuffer> GetBufferAsync(uint count) => Task.FromResult(GetBuffer(count));

        /// <inheritdoc />
        public void Skip(uint count)
        {
            if (_bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite)
            {
                // TODO
            }

            if (_bufferState == BufferState.Write)
            {
                ResizeBuffer((int)count);
                _buffer.Write(_byteSource.GetBytes((int)count), 0, (int)count);
            }
            else
            {
                _byteSource.Skip(count);
            }
        }

        /// <inheritdoc />
        public void Mark()
        {
            // until another Mark call or a Rewind
            // all following reads will be copied into the buffer
            var position = _buffer.Position;
            _bufferState = position < _buffer.Length ? BufferState.ReadWrite : BufferState.Write;
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
            if (_bufferState == BufferState.Write || _bufferState == BufferState.ReadWrite)
            {
                _buffer.Position = 0;
                _bufferState = BufferState.Read;
            }
            else
            {
                throw new InvalidOperationException("Rewind without Mark not allowed.");
            }
        }

        /// <inheritdoc />
        public void PushMilestone(uint count)
        {
            // TODO: milestone support
            _byteSource.PushMilestone(count);
        }

        /// <inheritdoc />
        public void PopMilestone()
        {
            _byteSource.PopMilestone();
        }

        /// <inheritdoc />
        public bool HasReachedMilestone()
        {
            return _byteSource.HasReachedMilestone();
        }

        /// <inheritdoc />
        public bool Require(uint count) => Require(count, null, null);

        /// <inheritdoc />
        public bool Require(uint count, ByteSourceCallback callback, object state)
        {
            // TODO: read into buffer and check for EOF instead?
            // alternatively move the error handling into the read part and do nothing here
            return _byteSource.Require(count, callback, state);
        }


        /// <inheritdoc />
        public Stream GetStream() => _byteSource.GetStream();

        private void UpdateBufferState() =>
            _bufferState = _bufferState == BufferState.Read ? BufferState.Unused : BufferState.Write;

        #endregion
    }
}