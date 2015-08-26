// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).


namespace Dicom.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.MemoryMappedFiles;

    using Dicom.IO.Buffer;

    /// <summary>
    /// Class for handling large files as memory mapped byte sources.
    /// </summary>
    public sealed class MemoryMappedFileByteSource : IByteSource, IDisposable
    {
        #region FIELDS

        /// <summary>
        /// The memory mapped file.
        /// </summary>
        private readonly MemoryMappedFile _file;

        /// <summary>
        /// The view stream of the memory mapped file.
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Endian setting.
        /// </summary>
        private Endian _endian;

        /// <summary>
        /// Binary reader of view stream.
        /// </summary>
        private BinaryReader _reader;

        /// <summary>
        /// Current mark.
        /// </summary>
        private long _mark;

        /// <summary>
        /// Large object size specification.
        /// </summary>
        private int _largeObjectSize;

        /// <summary>
        /// Stack of current milestones.
        /// </summary>
        private readonly Stack<long> _milestones;

        /// <summary>
        /// Lock object.
        /// </summary>
        private readonly object _lock;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="MemoryMappedFileByteSource"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public MemoryMappedFileByteSource(string fileName)
        {
            _file = MemoryMappedFile.CreateFromFile(fileName);
            _stream = _file.CreateViewStream();

            _endian = Endian.LocalMachine;
            _reader = EndianBinaryReader.Create(_stream, _endian);
            _mark = 0;

            _largeObjectSize = 64 * 1024;

            _milestones = new Stack<long>();
            _lock = new object();
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or set the endian.
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
        /// Gets the current stream position.
        /// </summary>
        public long Position
        {
            get
            {
                return _stream.Position;
            }
        }

        /// <summary>
        /// Gets the current marker position.
        /// </summary>
        public long Marker
        {
            get
            {
                return _mark;
            }
        }

        /// <summary>
        /// Gets true if stream position is at end-of-file, false otherwise.
        /// </summary>
        public bool IsEOF
        {
            get
            {
                return _stream.Position >= _stream.Length;
            }
        }

        /// <summary>
        /// Gets true if stream can be rewound, false otherwise.
        /// </summary>
        public bool CanRewind
        {
            get
            {
                return _stream.CanSeek;
            }
        }

        /// <summary>
        /// Gets or sets the large object size.
        /// </summary>
        public int LargeObjectSize
        {
            get
            {
                return _largeObjectSize;
            }
            set
            {
                _largeObjectSize = value;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Read from the current position of the stream one byte.
        /// </summary>
        /// <returns>The read byte.</returns>
        public byte GetUInt8()
        {
            return _reader.ReadByte();
        }

        /// <summary>
        /// Read from the current position of the stream one short.
        /// </summary>
        /// <returns>The read short.</returns>
        public short GetInt16()
        {
            return _reader.ReadInt16();
        }

        /// <summary>
        /// Read from the current position of the stream one ushort.
        /// </summary>
        /// <returns>The read ushort.</returns>
        public ushort GetUInt16()
        {
            return _reader.ReadUInt16();
        }

        /// <summary>
        /// Read from the current position of the stream one int.
        /// </summary>
        /// <returns>The read int.</returns>
        public int GetInt32()
        {
            return _reader.ReadInt32();
        }

        /// <summary>
        /// Read from the current position of the stream one uint.
        /// </summary>
        /// <returns>The read uint.</returns>
        public uint GetUInt32()
        {
            return _reader.ReadUInt32();
        }

        /// <summary>
        /// Read from the current position of the stream one long.
        /// </summary>
        /// <returns>The read long.</returns>
        public long GetInt64()
        {
            return _reader.ReadInt64();
        }

        /// <summary>
        /// Read from the current position of the stream one ulong.
        /// </summary>
        /// <returns>The read ulong.</returns>
        public ulong GetUInt64()
        {
            return _reader.ReadUInt64();
        }

        /// <summary>
        /// Read from the current position of the stream one floating-point single.
        /// </summary>
        /// <returns>The read floating-point single.</returns>
        public float GetSingle()
        {
            return _reader.ReadSingle();
        }

        /// <summary>
        /// Read from the current position of the stream one floating-point double.
        /// </summary>
        /// <returns>The read floating-point double.</returns>
        public double GetDouble()
        {
            return _reader.ReadDouble();
        }

        /// <summary>
        /// Starting from the current position of the stream, read <paramref name="count"/> bytes.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>The read bytes.</returns>
        public byte[] GetBytes(int count)
        {
            return _reader.ReadBytes(count);
        }

        /// <summary>
        /// Starting from the current position of the stream, read <paramref name="count"/> bytes and return in a buffer.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Buffer containing the read bytes.</returns>
        public IByteBuffer GetBuffer(uint count)
        {
            IByteBuffer buffer;
            if (count == 0)
            {
                buffer = EmptyBuffer.Value;
            }
            else if (count >= _largeObjectSize)
            {
                buffer = new StreamByteBuffer(_file.CreateViewStream(_stream.Position, count), 0, count);
                _stream.Seek((int)count, SeekOrigin.Current);
            }
            else
            {
                buffer = new MemoryByteBuffer(GetBytes((int)count));
            }

            return buffer;
        }

        /// <summary>
        /// Jump <paramref name="count"/> bytes in the stream.
        /// </summary>
        /// <param name="count">Number of bytes to jump.</param>
        public void Skip(int count)
        {
            _stream.Seek(count, SeekOrigin.Current);
        }

        /// <summary>
        /// Set the marker at the current position of the stream.
        /// </summary>
        public void Mark()
        {
            _mark = _stream.Position;
        }

        /// <summary>
        /// Rewind to the current marker position.
        /// </summary>
        public void Rewind()
        {
            _stream.Position = _mark;
        }

        /// <summary>
        /// Add a milestone at <paramref name="count"/> bytes ahead of the current position.
        /// </summary>
        /// <param name="count">Number of bytes from the current position to the new milestone.</param>
        public void PushMilestone(uint count)
        {
            lock (_lock) _milestones.Push(_stream.Position + count);
        }

        /// <summary>
        /// Remove the latest added milestone from the stack.
        /// </summary>
        public void PopMilestone()
        {
            lock (_lock) _milestones.Pop();
        }

        /// <summary>
        /// Check whether the stream position has reached the most recently added milestone.
        /// </summary>
        /// <returns>True if the most recently added milestone has been reached, false otherwise.</returns>
        public bool HasReachedMilestone()
        {
            lock (_lock)
            {
                if (_milestones.Count > 0 && _stream.Position >= _milestones.Peek()) return true;
                return false;
            }
        }

        /// <summary>
        /// Check whether there are enough bytes left of the stream.
        /// </summary>
        /// <param name="count">Number of bytes to be able to read.</param>
        /// <returns>True if <paramref name="count"/> bytes are possible to read without exceeding end-of-file, false otherwise.</returns>
        public bool Require(uint count)
        {
            return Require(count, null, null);
        }

        /// <summary>
        /// Check whether there are enough bytes left of the stream.
        /// </summary>
        /// <param name="count">Number of bytes to be able to read.</param>
        /// <param name="callback">Callback method (ignored).</param>
        /// <param name="state">Callback state (ignored).</param>
        /// <returns>True if <paramref name="count"/> bytes are possible to read without exceeding end-of-file, false otherwise.</returns>
        public bool Require(uint count, ByteSourceCallback callback, object state)
        {
            lock (_lock)
            {
                return (_stream.Length - _stream.Position) >= count;
            }
        }

        /// <summary>
        /// Dispose disposable objects.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _reader.Dispose();
                _stream.Dispose();
                _file.Dispose();
            }
            finally
            {
                GC.SuppressFinalize(this);
            }

        }

        #endregion
    }
}
