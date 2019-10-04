// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.IO;

#if !NET35
using System.Threading.Tasks;
#endif

using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO
{

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

        private readonly FileReadOption _readOption;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="StreamByteSource"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        public StreamByteSource(Stream stream, FileReadOption readOption = FileReadOption.Default)
        {
            _stream = stream;
            _endian = Endian.LocalMachine;
            _reader = EndianBinaryReader.Create(_stream, _endian);
            _mark = 0;
            // here the mapping of the default option is applied - may be extracted into some GlobalSettings class or similar
            _readOption = (readOption == FileReadOption.Default) ? FileReadOption.ReadLargeOnDemand : readOption;

            LargeObjectSize = 64 * 1024;

            _milestones = new Stack<long>();
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

        /// <inheritdoc />
        public long Position => _stream.Position;

        /// <inheritdoc />
        public long Marker => _mark;

        /// <inheritdoc />
        public bool IsEOF => _stream.Position >= _stream.Length;

        /// <inheritdoc />
        public bool CanRewind => _stream.CanSeek;

        /// <inheritdoc />
        public int MilestonesCount => _milestones.Count;

        /// <summary>
        /// Gets or sets the size of what is considered a large object.
        /// </summary>
        public int LargeObjectSize { get; set; }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public byte GetUInt8()
        {
            return _reader.ReadByte();
        }

        /// <inheritdoc />
        public short GetInt16()
        {
            return _reader.ReadInt16();
        }

        /// <inheritdoc />
        public ushort GetUInt16()
        {
            return _reader.ReadUInt16();
        }

        /// <inheritdoc />
        public int GetInt32()
        {
            return _reader.ReadInt32();
        }

        /// <inheritdoc />
        public uint GetUInt32()
        {
            return _reader.ReadUInt32();
        }

        /// <inheritdoc />
        public long GetInt64()
        {
            return _reader.ReadInt64();
        }

        /// <inheritdoc />
        public ulong GetUInt64()
        {
            return _reader.ReadUInt64();
        }

        /// <inheritdoc />
        public float GetSingle()
        {
            return _reader.ReadSingle();
        }

        /// <inheritdoc />
        public double GetDouble()
        {
            return _reader.ReadDouble();
        }

        /// <inheritdoc />
        public byte[] GetBytes(int count)
        {
            return _reader.ReadBytes(count);
        }

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count)
        {
            IByteBuffer buffer;
            if (count == 0)
            {
                buffer = EmptyBuffer.Value;
            }
            else if (count >= LargeObjectSize && _readOption == FileReadOption.ReadLargeOnDemand)
            {
                buffer = new StreamByteBuffer(_stream, _stream.Position, count);
                _stream.Seek((int)count, SeekOrigin.Current);
            }
            else if (count >= LargeObjectSize && _readOption == FileReadOption.SkipLargeTags)
            {
                buffer = null;
                Skip((int)count);
            }
            else // count < LargeOpjectSize || _readOption == FileReadOption.ReadAll
            {
                buffer = new MemoryByteBuffer(GetBytes((int)count));
            }
            return buffer;
        }

#if !NET35
        /// <inheritdoc />
        public Task<IByteBuffer> GetBufferAsync(uint count)
        {
            return Task.FromResult(this.GetBuffer(count));
        }
#endif

        /// <inheritdoc />
        public void Skip(int count)
        {
            _stream.Seek(count, SeekOrigin.Current);
        }

        /// <inheritdoc />
        public void Mark()
        {
            _mark = _stream.Position;
        }

        /// <inheritdoc />
        public void Rewind()
        {
            _stream.Position = _mark;
        }

        /// <inheritdoc />
        public void PushMilestone(uint count)
        {
            lock (_lock) _milestones.Push(_stream.Position + count);
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
                if (_milestones.Count > 0 && _stream.Position >= _milestones.Peek()) return true;
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
                return (_stream.Length - _stream.Position) >= count;
            }
        }

        #endregion
    }
}
