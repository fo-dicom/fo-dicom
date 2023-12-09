// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace FellowOakDicom.IO.Buffer
{

    /// <summary>
    /// Byte source for reading in form of byte buffer.
    /// </summary>
    public class ByteBufferByteSource : IByteSource
    {
        #region FIELDS

        private readonly List<IByteBuffer> _buffers;

        private readonly Stack<long> _milestones;

        private long _expired;

        private long _marker;

        private long _position;

        private long _length;

        private int _current;

        private long _currentPos;

        private byte[] _currentData;

        private bool _fixed;

        private uint _required;

        private ByteSourceCallback _callback;

        private object _callbackState;

        private readonly object _lock;

        private Endian _endian;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="ByteBufferByteSource"/>.
        /// Internal list of buffers is empty.
        /// </summary>
        public ByteBufferByteSource()
        {
            _expired = 0;
            _marker = 0;
            _position = 0;
            _length = 0;
            _endian = Endian.LocalMachine;

            _milestones = new Stack<long>();
            _buffers = new List<IByteBuffer>();
            _fixed = false;

            _current = -1;
            _lock = new object();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ByteBufferByteSource"/>.
        /// </summary>
        /// <param name="buffers">Initial collection of buffers.</param>
        public ByteBufferByteSource(params IByteBuffer[] buffers)
        {
            _expired = 0;
            _marker = 0;
            _position = 0;
            _length = 0;
            _endian = Endian.LocalMachine;

            _milestones = new Stack<long>();
            _buffers = new List<IByteBuffer>(buffers);
            foreach (var x in _buffers) _length += x.Size;
            _fixed = true;

            _current = -1;
            _lock = new object();
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public Endian Endian
        {
            get => _endian;
            set
            {
                _endian = value;
                SwapBuffers();
            }
        }

        /// <inheritdoc />
        public long Position => _position;

        /// <inheritdoc />
        public long Marker => _marker;

        /// <inheritdoc />
        public bool IsEOF
        {
            get
            {
                lock (_lock)
                {
                    return _fixed && _position >= _length;
                }
            }
        }

        /// <inheritdoc />
        public bool CanRewind => true;

        /// <inheritdoc />
        public int MilestonesCount => _milestones.Count;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public byte GetUInt8()
            => NextByte();

        /// <inheritdoc />
        public short GetInt16()
            => Endian == Endian.LocalMachine
                ? (short)((NextByte() << 0) | (NextByte() << 8))
                : (short)((NextByte() << 8) | (NextByte() << 0));

        /// <inheritdoc />
        public ushort GetUInt16()
            => Endian == Endian.LocalMachine
                ? (ushort)((NextByte() << 0) | (NextByte() << 8))
                : (ushort)((NextByte() << 8) | (NextByte() << 0));

        /// <inheritdoc />
        public int GetInt32()
            => Endian == Endian.LocalMachine
                ? (NextByte() << 0) | (NextByte() << 8) | (NextByte() << 16) | (NextByte() << 24)
                : (NextByte() << 24) | (NextByte() << 16) | (NextByte() << 8) | (NextByte() << 0);

        /// <inheritdoc />
        public uint GetUInt32()
            => Endian == Endian.LocalMachine
                ? (uint)((NextByte() << 0) | (NextByte() << 8) | (NextByte() << 16) | (NextByte() << 24))
                : (uint)((NextByte() << 24) | (NextByte() << 16) | (NextByte() << 8) | (NextByte() << 0));

        /// <inheritdoc />
        public long GetInt64()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine)
            {
                Array.Reverse(b);
            }

            return BitConverter.ToInt64(b, 0);
        }

        /// <inheritdoc />
        public ulong GetUInt64()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine)
            {
                Array.Reverse(b);
            }

            return BitConverter.ToUInt64(b, 0);
        }

        /// <inheritdoc />
        public float GetSingle()
        {
            byte[] b = GetBytes(4);
            if (Endian != Endian.LocalMachine)
            {
                Array.Reverse(b);
            }

            return BitConverter.ToSingle(b, 0);
        }

        /// <inheritdoc />
        public double GetDouble()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine)
            {
                Array.Reverse(b);
            }

            return BitConverter.ToDouble(b, 0);
        }

        /// <inheritdoc />
        public byte[] GetBytes(int count)
        {
            var buffer = new byte[count];
            var bytesRead = GetBytes(buffer, 0, count);
            if (bytesRead != count)
            {
                throw new DicomIoException($"Failed to get {count} bytes");
            }

            return buffer;
        }

        public int GetBytes(byte[] buffer, int index, int count)
        {
            if (buffer.Length < count)
            {
                throw new ArgumentException($"Buffer is not large enough to hold {count} bytes");
            }

            lock (_lock)
            {
                int p = index;
                int bytesToRead = count;
                while (bytesToRead > 0)
                {
                    if (_current == -1 || _currentPos >= _currentData.Length)
                    {
                        if (!SwapBuffers())
                        {
                            throw new DicomIoException($"Tried to retrieve {count} bytes past end of source.");
                        }
                    }

                    int n = (int)Math.Min(_currentData.Length - _currentPos, count);
                    Array.Copy(_currentData, _currentPos, buffer, p, n);

                    bytesToRead -= n;
                    p += n;
                    _position += n;
                    _currentPos += n;
                }
                return count;
            }
        }

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count)
        {
            return new MemoryByteBuffer(GetBytes((int)count));
        }

        /// <inheritdoc />
        public Task<IByteBuffer> GetBufferAsync(uint count)
        {
            return Task.FromResult(GetBuffer(count));
        }

        /// <inheritdoc />
        public void Skip(uint count)
        {
            lock (_lock)
            {
                _position += count;
                _currentPos += count;
                SwapBuffers();
            }
        }

        /// <inheritdoc />
        public void Mark()
        {
            lock (_lock)
            {
                _marker = _position;

                while (_buffers.Count > 0 && (_expired + _buffers[0].Size) < _marker)
                {
                    _expired += _buffers[0].Size;
                    _buffers.RemoveAt(0);
                }
            }
        }

        /// <inheritdoc />
        public void Rewind()
        {
            lock (_lock)
            {
                _position = _marker;
                SwapBuffers();
            }
        }

        /// <inheritdoc />
        public void PushMilestone(uint count)
        {
            lock (_lock) _milestones.Push(_position + count);
        }

        /// <inheritdoc />
        public void PopMilestone()
        {
            lock (_lock) _milestones.Pop();
        }

        /// <inheritdoc />
        public bool HasReachedMilestone()
        {
            lock (_lock)
            {
                if (_milestones.Count > 0 && _position >= _milestones.Peek()) return true;
                return false;
            }
        }

        /// <inheritdoc />
        public bool Require(uint count)
        {
            return Require(count, null, null);
        }

        /// <inheritdoc />
        public bool Require(uint count, ByteSourceCallback callback, object state)
        {
            lock (_lock)
            {
                if ((_position + count) <= _length)
                {
                    return true;
                }

                if (_fixed)
                {
                    throw new DicomIoException($"Requested {count} bytes past end of byte source.");
                }

                _required = count;
                _callback = callback ?? throw new DicomIoException($"Requested {count} bytes past end of byte source without providing a callback.");
                _callbackState = state;

                return false;
            }
        }

        /// <summary>
        /// Add byte buffer to byte source.
        /// </summary>
        /// <param name="buffer">Byte buffer to add.</param>
        /// <param name="last">true if added buffer is the last to add, false otherwise.</param>
        public void Add(IByteBuffer buffer, bool last)
        {
            lock (_lock)
            {
                if (_fixed) throw new DicomIoException("Tried to extend fixed length byte source.");

                if (buffer != null && buffer.Size > 0)
                {
                    _buffers.Add(buffer);
                    _length += buffer.Size;

                    if (_callback != null)
                    {
                        if ((_length - _position) >= _required)
                        {
                            _callback.BeginInvoke(this, _callbackState, Callback, _callback);
                            _callback = null;
                            _callbackState = null;
                            _required = 0;
                        }
                    }
                }

                _fixed = last;
            }
        }

        /// <summary>
        /// End callback method when invoking member callback.
        /// </summary>
        /// <param name="result">Asynchronous result.</param>
        private static void Callback(IAsyncResult result)
        {
            try
            {
                var cb = (ByteSourceCallback)result.AsyncState;
                cb.EndInvoke(result);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Swap buffers.
        /// </summary>
        /// <returns>true if swap operation complete, false otherwise.</returns>
        private bool SwapBuffers()
        {
            lock (_lock)
            {
                var pos = _position - _expired;

                for (var i = 0; i < _buffers.Count; i++)
                {
                    if (pos < _buffers[i].Size)
                    {
                        _current = i;
                        _currentPos = pos;
                        _currentData = _buffers[i].Data;
                        return true;
                    }
                    pos -= _buffers[i].Size;
                }

                return false;
            }
        }

        /// <summary>
        /// Get next byte and step forward one position.
        /// </summary>
        /// <returns>Next byte.</returns>
        private byte NextByte()
        {
            lock (_lock)
            {
                if (_current == -1 || _currentPos >= _currentData.Length)
                {
                    if (!SwapBuffers()) throw new DicomIoException("Tried to retrieve byte past end of source.");
                }

                _position++;
                return _currentData[_currentPos++];
            }
        }


        public Stream GetStream()
        {
            lock (_lock)
            {
                Stream stream = new MemoryStream();
                foreach (var item in _buffers)
                {
                    byte[] data = item.Data;
                    stream.Write(data, 0, data.Length);
                }
                return stream;
            }
        }


        #endregion
    }
}
