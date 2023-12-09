// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="StreamByteSource"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <param name="readOption">Defines how large values are handled.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passed, then the default of 64k is used.</param>
        public StreamByteSource(Stream stream, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            _stream = stream;
            _endian = Endian.LocalMachine;
            _reader = EndianBinaryReader.Create(_stream, _endian, false);
            _mark = 0;
            // here the mapping of the default option is applied - may be extracted into some GlobalSettings class or similar
            ReadOption = (readOption == FileReadOption.Default) ? FileReadOption.ReadLargeOnDemand : readOption;

            LargeObjectSize = largeObjectSize <= 0 ? 64 * 1024 : largeObjectSize;

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
                        _reader = EndianBinaryReader.Create(_stream, _endian, false);
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

        /// <summary>
        /// Gets the mode for handling large values.
        /// </summary>
        public FileReadOption ReadOption { get; }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public byte GetUInt8() => _reader.ReadByte();

        /// <inheritdoc />
        public short GetInt16() => _reader.ReadInt16();

        /// <inheritdoc />
        public ushort GetUInt16() => _reader.ReadUInt16();

        /// <inheritdoc />
        public int GetInt32() => _reader.ReadInt32();

        /// <inheritdoc />
        public uint GetUInt32() => _reader.ReadUInt32();

        /// <inheritdoc />
        public long GetInt64() => _reader.ReadInt64();

        /// <inheritdoc />
        public ulong GetUInt64() => _reader.ReadUInt64();

        /// <inheritdoc />
        public float GetSingle() => _reader.ReadSingle();

        /// <inheritdoc />
        public double GetDouble() => _reader.ReadDouble();

        /// <inheritdoc />
        public byte[] GetBytes(int count) => _reader.ReadBytes(count);

        public int GetBytes(byte[] buffer, int index, int count) => _reader.Read(buffer, index, count);

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count)
        {
            IByteBuffer buffer;
            if (count == 0)
            {
                buffer = EmptyBuffer.Value;
            }
            else if (count >= LargeObjectSize && ReadOption == FileReadOption.ReadLargeOnDemand)
            {
                buffer = new StreamByteBuffer(_stream, _stream.Position, count);
                _stream.Seek(count, SeekOrigin.Current);
            }
            else if (count >= LargeObjectSize && ReadOption == FileReadOption.SkipLargeTags)
            {
                buffer = null;
                Skip(count);
            }
            else // count < LargeObjectSize || ReadOption == FileReadOption.ReadAll
            {
                if (count < MemoryByteBuffer.MaxArrayLength)
                {
                    buffer = new MemoryByteBuffer(GetBytes((int)count));
                }
                else
                {
                    var numberOfBuffers = (int) Math.Ceiling((double) count / MemoryByteBuffer.MaxArrayLength);
                    var buffers = new IByteBuffer[numberOfBuffers];
                    for (var i = 0; i < numberOfBuffers - 1; i++)
                    {
                        var bufferData = new byte[MemoryByteBuffer.MaxArrayLength];
                        GetBytes(bufferData, 0, bufferData.Length);
                        buffers[i] = new MemoryByteBuffer(bufferData);
                    }
                    var lastBufferData = new byte[count % MemoryByteBuffer.MaxArrayLength];
                    GetBytes(lastBufferData, 0, lastBufferData.Length);
                    buffers[numberOfBuffers-1] = new MemoryByteBuffer(lastBufferData);
                    buffer = new CompositeByteBuffer(buffers);
                }
            }
            return buffer;
        }

        /// <inheritdoc />
        public Task<IByteBuffer> GetBufferAsync(uint count) => Task.FromResult(GetBuffer(count));

        /// <inheritdoc />
        public void Skip(uint count) => _stream.Seek(count, SeekOrigin.Current);

        /// <inheritdoc />
        public void Mark() => _mark = _stream.Position;

        /// <inheritdoc />
        public void Rewind() => _stream.Position = _mark;

        /// <inheritdoc />
        public void PushMilestone(uint count)
        {
            lock (_lock)
            {
                _milestones.Push(_stream.Position + count);
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
                return _milestones.Count > 0 && _stream.Position >= _milestones.Peek();
            }
        }

        /// <inheritdoc />
        public bool Require(uint count) => Require(count, null, null);

        /// <inheritdoc />
        public bool Require(uint count, ByteSourceCallback callback, object state)
        {
            lock (_lock)
            {
                return (_stream.Length - Position) >= count;
            }
        }


        public Stream GetStream()
        {
            return _stream;
        }

        #endregion
    }
}
