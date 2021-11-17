// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        /// <summary> Reader for buffer that accounts for Endianess. </summary>
        private BinaryReader _bufferReader;

        /// <summary> Writer for buffer that accounts for Endianess. </summary>
        private BinaryWriter _bufferWriter;

        private BufferState _bufferState;

        private readonly Stack<long> _milestones;

        private readonly object _lock;

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
            if (readOption == FileReadOption.Default || readOption == FileReadOption.ReadLargeOnDemand)
            {
                // with a no-seek stream we are not able to read large tags on demand,
                // so we default to read all tags
                // TODO: log warning?
                readOption = FileReadOption.ReadAll;
            }

            _byteSource = new StreamByteSource(stream, readOption, largeObjectSize);
            _buffer = new MemoryStream();
            _bufferReader = EndianBinaryReader.Create(_buffer, Endian.LocalMachine);
            _bufferWriter = EndianBinaryWriter.Create(_buffer, Endian.LocalMachine);
            _bufferState = BufferState.Unused;
            // we cannot use the milestones of the stream byte source, as these don't
            // account for the buffer
            _milestones = new Stack<long>();
            _lock = new object();
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public Endian Endian
        {
            get => _byteSource.Endian;
            set
            {
                if (_byteSource.Endian != value)
                {
                    lock (_lock)
                    {
                        _byteSource.Endian = value;
                        _bufferReader = EndianBinaryReader.Create(_buffer, value);
                        _bufferWriter = EndianBinaryWriter.Create(_buffer, value);
                    }
                }
            }
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
        public int MilestonesCount => _milestones.Count;

        private bool IsReadingBuffer => _bufferState == BufferState.Read || _bufferState == BufferState.ReadWrite;

        private bool IsWritingBuffer => _bufferState == BufferState.Write || _bufferState == BufferState.ReadWrite;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public byte GetUInt8() => GetNumber(_byteSource.GetUInt8, _bufferReader.ReadByte, _bufferWriter.Write);

        /// <inheritdoc />
        public short GetInt16() => GetNumber(_byteSource.GetInt16, _bufferReader.ReadInt16, _bufferWriter.Write);

        /// <inheritdoc />
        public ushort GetUInt16() => GetNumber(_byteSource.GetUInt16, _bufferReader.ReadUInt16, _bufferWriter.Write);

        /// <inheritdoc />
        public int GetInt32() => GetNumber(_byteSource.GetInt32, _bufferReader.ReadInt32, _bufferWriter.Write);

        /// <inheritdoc />
        public uint GetUInt32() => GetNumber(_byteSource.GetUInt32, _bufferReader.ReadUInt32, _bufferWriter.Write);

        /// <inheritdoc />
        public long GetInt64() => GetNumber(_byteSource.GetInt64, _bufferReader.ReadInt64, _bufferWriter.Write);

        /// <inheritdoc />
        public ulong GetUInt64() => GetNumber(_byteSource.GetUInt64, _bufferReader.ReadUInt64, _bufferWriter.Write);

        /// <inheritdoc />
        public float GetSingle() => GetNumber(_byteSource.GetSingle, _bufferReader.ReadSingle, _bufferWriter.Write);

        /// <inheritdoc />
        public double GetDouble() => GetNumber(_byteSource.GetDouble, _bufferReader.ReadDouble, _bufferWriter.Write);

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
            if (IsWritingBuffer)
            {
                _buffer.Position = 0;
                _bufferState = _buffer.Length > 0 ? BufferState.Read : BufferState.Unused;
            }
            else
            {
                throw new InvalidOperationException("Rewind without Mark not allowed.");
            }
        }

        /// <inheritdoc />
        public void PushMilestone(uint count)
        {
            lock (_lock)
            {
                _milestones.Push(Position + count);
            }
        }

        /// <inheritdoc />
        public void PopMilestone()
        {
            lock (_lock)
            {
                _milestones.Pop();
            }
        }

        /// <inheritdoc />
        public bool HasReachedMilestone()
        {
            lock (_lock)
            {
                return _milestones.Count > 0 && Position >= _milestones.Peek();
            }
        }

        /// <inheritdoc />
        public bool Require(uint count) => Require(count, null, null);

        /// <inheritdoc />
        public bool Require(uint count, ByteSourceCallback callback, object state) =>
            _byteSource.Require(count, callback, state);


        /// <inheritdoc />
        public Stream GetStream()
        {
            // load the rest of the file into the buffer and return the buffer
            // this is inefficient, but difficult to change without changing decompression
            // where this is needed (for deflated transfer syntax)
            Mark();
            GetBytes((int)(_byteSource.GetStream().Length - Position));
            Rewind();
            return _buffer;
        }

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
            // we can only clean the buffer if we are not in buffering mode   
            if (!IsWritingBuffer)
            {
                _buffer.SetLength(0);
            }
        }

        /// <summary>
        /// If there is not enough data to read a number with the given count of bytes,
        /// the remaining bytes are read from the original stream and added to the buffer.
        /// We need to have the bytes as contiguous memory for subsequently using ReadXXX
        /// to read the number in the correct Endianess.
        /// </summary>
        /// <param name="count">Number of bytes to read, e.g. the size of the type to read (2-8).</param>
        /// <returns>True if the buffer will be exhausted after reading the given number of bytes.</returns>
        private bool FillBufferForReading(int count)
        {
            var bufferByteCount = (int)(_buffer.Length - _buffer.Position);
            if (bufferByteCount <= count)
            {
                UpdateBufferState();
                if (bufferByteCount < count)
                {
                    var nrBytesToRead = count - bufferByteCount;
                    // make sure the read data is placed at the end of the buffer
                    _buffer.Position = _buffer.Length;
                    ResizeBuffer(nrBytesToRead);
                    _byteSource.GetBytes(nrBytesToRead);
                    // set the position back for reading the number
                    _buffer.Position = _buffer.Length - count;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Read and return a number of type T from the stream at current position, accounting for the buffer.
        /// </summary>
        private T GetNumber<T>(Func<T> getter, Func<T> reader, Action<T> writer) where T : struct
        {
            if (IsReadingBuffer)
            {
                // if we are reading the buffer, we have to handle the case that the buffer is 
                // not large enough to hold the number; in this case, it is filled with the remaining
                // part from the stream
                var filled = FillBufferForReading(Marshal.SizeOf(typeof(T)));
                var read = reader();
                if (filled)
                {
                    // if we have read the whole buffer, we can clear it 
                    ClearBuffer();
                }

                return read;
            }

            // read the data from the stream
            var data = getter();
            if (_bufferState == BufferState.Write)
            {
                // if we in buffering mode (after Mark()), also write the data to the buffer
                ResizeBuffer(Marshal.SizeOf(typeof(T)));
                writer(data);
            }

            return data;
        }

        #endregion
    }
}