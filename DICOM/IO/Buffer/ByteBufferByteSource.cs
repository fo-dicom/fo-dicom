// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Buffer
{
    using System;
    using System.Collections.Generic;

#if !NET35
    using System.Threading.Tasks;
#endif

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

        /// <summary>
        /// Gets or sets the endianess.
        /// </summary>
        public Endian Endian
        {
            get
            {
                return _endian;
            }
            set
            {
                _endian = value;
                SwapBuffers();
            }
        }

        /// <summary>
        /// Gets the current read position.
        /// </summary>
        public long Position
        {
            get
            {
                return _position;
            }
        }

        /// <summary>
        /// Gets the position of the current marker.
        /// </summary>
        public long Marker
        {
            get
            {
                return _marker;
            }
        }

        /// <summary>
        /// Gets whether end-of-source is reached.
        /// </summary>
        public bool IsEOF
        {
            get
            {
                lock (_lock)
                {
                    return _fixed && (_position >= _length);
                }
            }
        }

        /// <summary>
        /// Gets whether its possible to rewind the source.
        /// </summary>
        public bool CanRewind
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the milestone levels count.
        /// </summary>
        public int MilestonesCount
        {
            get
            {
                return this._milestones.Count;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets one byte from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single byte.</returns>
        public byte GetUInt8()
        {
            return NextByte();
        }

        /// <summary>
        /// Gets a signed short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed short.</returns>
        public short GetInt16()
        {
            if (Endian == Endian.LocalMachine)
            {
                return unchecked((short)((NextByte() << 0) | (NextByte() << 8)));
            }
            else
            {
                return unchecked((short)((NextByte() << 8) | (NextByte() << 0)));
            }
        }

        /// <summary>
        /// Gets an unsigned short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned short.</returns>
        public ushort GetUInt16()
        {
            if (Endian == Endian.LocalMachine)
            {
                return unchecked((ushort)((NextByte() << 0) | (NextByte() << 8)));
            }
            else
            {
                return unchecked((ushort)((NextByte() << 8) | (NextByte() << 0)));
            }
        }

        /// <summary>
        /// Gets a signed integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed integer.</returns>
        public int GetInt32()
        {
            if (Endian == Endian.LocalMachine)
            {
                return unchecked((int)((NextByte() << 0) | (NextByte() << 8) | (NextByte() << 16) | (NextByte() << 24)));
            }
            else
            {
                return unchecked((int)((NextByte() << 24) | (NextByte() << 16) | (NextByte() << 8) | (NextByte() << 0)));
            }
        }

        /// <summary>
        /// Gets an unsigned integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned integer.</returns>
        public uint GetUInt32()
        {
            if (Endian == Endian.LocalMachine)
            {
                return
                    unchecked((uint)((NextByte() << 0) | (NextByte() << 8) | (NextByte() << 16) | (NextByte() << 24)));
            }
            else
            {
                return
                    unchecked((uint)((NextByte() << 24) | (NextByte() << 16) | (NextByte() << 8) | (NextByte() << 0)));
            }
        }

        /// <summary>
        /// Gets a signed long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed long.</returns>
        public long GetInt64()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine) Array.Reverse(b);
            return BitConverter.ToInt64(b, 0);
        }

        /// <summary>
        /// Gets an unsigned long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned long.</returns>
        public ulong GetUInt64()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine) Array.Reverse(b);
            return BitConverter.ToUInt64(b, 0);
        }

        /// <summary>
        /// Gets a single precision floating point value (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single precision floating point value.</returns>
        public float GetSingle()
        {
            byte[] b = GetBytes(4);
            if (Endian != Endian.LocalMachine) Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// Gets a double precision floating point value (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Double precision floating point value.</returns>
        public double GetDouble()
        {
            byte[] b = GetBytes(8);
            if (Endian != Endian.LocalMachine) Array.Reverse(b);
            return BitConverter.ToDouble(b, 0);
        }

        /// <summary>
        /// Gets a specified number of bytes from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] GetBytes(int count)
        {
            lock (_lock)
            {
                int p = 0;
                byte[] bytes = new byte[count];
                while (count > 0)
                {
                    if (_current == -1 || _currentPos >= _currentData.Length)
                    {
                        if (!SwapBuffers()) throw new DicomIoException("Tried to retrieve {0} bytes past end of source.", count);
                    }

                    int n = (int)System.Math.Min(_currentData.Length - _currentPos, count);
                    Array.Copy(_currentData, (int)_currentPos, bytes, p, n);

                    count -= n;
                    p += n;
                    _position += n;
                    _currentPos += n;
                }
                return bytes;
            }
        }

        /// <summary>
        /// Gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte buffer containing the read bytes.</returns>
        public IByteBuffer GetBuffer(uint count)
        {
            return new MemoryByteBuffer(GetBytes((int)count));
        }

#if !NET35
        /// <summary>
        /// Asynchronously gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Awaitable byte buffer containing the read bytes.</returns>
        public Task<IByteBuffer> GetBufferAsync(uint count)
        {
            return Task.FromResult(this.GetBuffer(count));
        }
#endif

        /// <summary>
        /// Skip position <see cref="count"/> number of bytes.
        /// </summary>
        /// <param name="count">Number of bytes to skip.</param>
        public void Skip(int count)
        {
            lock (_lock)
            {
                _position += count;
                _currentPos += count;
                SwapBuffers();
            }
        }

        /// <summary>
        /// Set a mark at the current position.
        /// </summary>
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

        /// <summary>
        /// Rewind byte source to latest <see cref="IByteSource.Marker"/>.
        /// </summary>
        public void Rewind()
        {
            lock (_lock)
            {
                _position = _marker;
                SwapBuffers();
            }
        }

        /// <summary>
        /// Mark the position of a new level of milestone.
        /// </summary>
        /// <param name="count">Expected distance in bytes from the current position to the milestone.</param>
        public void PushMilestone(uint count)
        {
            lock (_lock) _milestones.Push(_position + count);
        }

        /// <summary>
        /// Pop the uppermost level of milestone.
        /// </summary>
        public void PopMilestone()
        {
            lock (_lock) _milestones.Pop();
        }

        /// <summary>
        /// Checks whether the byte source position is at the uppermost milestone position.
        /// </summary>
        /// <returns>true if uppermost milestone is reached, false otherwise.</returns>
        public bool HasReachedMilestone()
        {
            lock (_lock)
            {
                if (_milestones.Count > 0 && _position >= _milestones.Peek()) return true;
                return false;
            }
        }

        /// <summary>
        /// Verifies that there is a sufficient number of bytes to read.
        /// </summary>
        /// <param name="count">Required number of bytes.</param>
        /// <returns>true if source contains sufficient number of remaining bytes, false otherwise.</returns>
        public bool Require(uint count)
        {
            return Require(count, null, null);
        }

        /// <summary>
        /// Verifies that there is a sufficient number of bytes to read.
        /// </summary>
        /// <param name="count">Required number of bytes.</param>
        /// <param name="callback">Byte source callback.</param>
        /// <param name="state">Callback state.</param>
        /// <returns>true if source contains sufficient number of remaining bytes, false otherwise.</returns>
        public bool Require(uint count, ByteSourceCallback callback, object state)
        {
            lock (_lock)
            {
                if ((_position + count) <= _length) return true;

                if (_fixed) throw new DicomIoException("Requested {0} bytes past end of byte source.", count);

                if (callback == null)
                    throw new DicomIoException(
                        "Requested {0} bytes past end of byte source without providing a callback.",
                        count);

                _required = count;
                _callback = callback;
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
                ByteSourceCallback cb = (ByteSourceCallback)result.AsyncState;
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
                long pos = _position - _expired;

                for (int i = 0; i < _buffers.Count; i++)
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

        #endregion
    }
}
