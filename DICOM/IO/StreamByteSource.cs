// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.IO;

using Dicom.IO.Buffer;

namespace Dicom.IO
{
    using System.Threading.Tasks;

    /// <summary>
    /// Stream byte source for reading.
    /// </summary>
    public class StreamByteSource : IByteSource
    {
        #region FIELDS

        private readonly Stream _stream;

        private Endian _endian;

        private BinaryReader _reader;

        private long _mark;

        private readonly Stack<long> _milestones;

        private readonly object _lock;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="StreamByteSource"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        public StreamByteSource(Stream stream)
        {
            _stream = stream;
            _endian = Endian.LocalMachine;
            _reader = EndianBinaryReader.Create(_stream, _endian);
            _mark = 0;

            this.LargeObjectSize = 64 * 1024;

            _milestones = new Stack<long>();
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
                if (_endian != value)
                {
                    lock (_lock)
                    {
                        _endian = value;
                        _reader = EndianBinaryReader.Create(_stream, _endian);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current read position.
        /// </summary>
        public long Position
        {
            get
            {
                return _stream.Position;
            }
        }

        /// <summary>
        /// Gets the position of the current marker.
        /// </summary>
        public long Marker
        {
            get
            {
                return _mark;
            }
        }

        /// <summary>
        /// Gets whether end-of-source is reached.
        /// </summary>
        public bool IsEOF
        {
            get
            {
                return _stream.Position >= _stream.Length;
            }
        }

        /// <summary>
        /// Gets whether its possible to rewind the source.
        /// </summary>
        public bool CanRewind
        {
            get
            {
                return _stream.CanSeek;
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

        /// <summary>
        /// Gets or sets the size of what is considered a large object.
        /// </summary>
        public int LargeObjectSize { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets one byte from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single byte.</returns>
        public byte GetUInt8()
        {
            return _reader.ReadByte();
        }

        /// <summary>
        /// Gets a signed short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed short.</returns>
        public short GetInt16()
        {
            return _reader.ReadInt16();
        }

        /// <summary>
        /// Gets an unsigned short (16 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned short.</returns>
        public ushort GetUInt16()
        {
            return _reader.ReadUInt16();
        }

        /// <summary>
        /// Gets a signed integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed integer.</returns>
        public int GetInt32()
        {
            return _reader.ReadInt32();
        }

        /// <summary>
        /// Gets an unsigned integer (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned integer.</returns>
        public uint GetUInt32()
        {
            return _reader.ReadUInt32();
        }

        /// <summary>
        /// Gets a signed long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Signed long.</returns>
        public long GetInt64()
        {
            return _reader.ReadInt64();
        }

        /// <summary>
        /// Gets an unsigned long (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Unsigned long.</returns>
        public ulong GetUInt64()
        {
            return _reader.ReadUInt64();
        }

        /// <summary>
        /// Gets a single precision floating point value (32 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Single precision floating point value.</returns>
        public float GetSingle()
        {
            return _reader.ReadSingle();
        }

        /// <summary>
        /// Gets a double precision floating point value (64 bits) from the current position and moves to subsequent position.
        /// </summary>
        /// <returns>Double precision floating point value.</returns>
        public double GetDouble()
        {
            return _reader.ReadDouble();
        }

        /// <summary>
        /// Gets a specified number of bytes from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] GetBytes(int count)
        {
            return _reader.ReadBytes(count);
        }

        /// <summary>
        /// Gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte buffer containing the read bytes.</returns>
        public IByteBuffer GetBuffer(uint count)
        {
            IByteBuffer buffer = null;
            if (count == 0) buffer = EmptyBuffer.Value;
            else if (count >= this.LargeObjectSize)
            {
                buffer = new StreamByteBuffer(_stream, _stream.Position, count);
                _stream.Seek((int)count, SeekOrigin.Current);
            }
            else buffer = new MemoryByteBuffer(GetBytes((int)count));
            return buffer;
        }

        /// <summary>
        /// Asynchronously gets a byte buffer of specified length from the current position and moves to subsequent position.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Awaitable byte buffer containing the read bytes.</returns>
        public Task<IByteBuffer> GetBufferAsync(uint count)
        {
            return Task.FromResult(this.GetBuffer(count));
        }

        /// <summary>
        /// Skip position <see cref="count"/> number of bytes.
        /// </summary>
        /// <param name="count">Number of bytes to skip.</param>
        public void Skip(int count)
        {
            _stream.Seek(count, SeekOrigin.Current);
        }

        /// <summary>
        /// Set a mark at the current position.
        /// </summary>
        public void Mark()
        {
            _mark = _stream.Position;
        }

        /// <summary>
        /// Rewind byte source to latest <see cref="IByteSource.Marker"/>.
        /// </summary>
        public void Rewind()
        {
            _stream.Position = _mark;
        }

        /// <summary>
        /// Mark the position of a new level of milestone.
        /// </summary>
        /// <param name="count">Expected distance in bytes from the current position to the milestone.</param>
        public void PushMilestone(uint count)
        {
            lock (_lock) _milestones.Push(_stream.Position + count);
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
                if (_milestones.Count > 0 && _stream.Position >= _milestones.Peek()) return true;
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
                return (_stream.Length - _stream.Position) >= count;
            }
        }

        #endregion
    }
}
