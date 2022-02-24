// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO
{
    /// <summary>
    /// File byte source for reading.
    /// </summary>
    public sealed class FileByteSource : IByteSource
    {
        #region FIELDS

        private readonly IFileReference _file;
        private readonly Stream _stream;
        private readonly Stack<long> _milestones;
        private readonly object _lock;
        private readonly FileReadOption _readOption;

        private Endian _endian;
        private BinaryReader _reader;
        private int _isDisposed;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="FileByteSource"/>.
        /// </summary>
        /// <param name="file">File to read from.</param>
        /// <param name="readOption">Option how to deal with large values, if they should be loaded directly into memory or lazy loaded on demand</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        public FileByteSource(IFileReference file, FileReadOption readOption, int largeObjectSize)
        {
            _file = file;
            _stream = _file.OpenRead();
            _endian = Endian.LocalMachine;
            _reader = EndianBinaryReader.Create(_stream, _endian, false);
            Marker = 0;
            // here the mapping of the default option is applied - may be extracted into some GlobalSettings class or similar
            _readOption = (readOption == FileReadOption.Default) ? FileReadOption.ReadLargeOnDemand : readOption;

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
        public long Marker { get; private set; }

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
            ThrowIfAlreadyDisposed();
            return _reader.ReadByte();
        }

        /// <inheritdoc />
        public short GetInt16()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadInt16();
        }

        /// <inheritdoc />
        public ushort GetUInt16()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadUInt16();
        }

        /// <inheritdoc />
        public int GetInt32()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadInt32();
        }

        /// <inheritdoc />
        public uint GetUInt32()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadUInt32();
        }

        /// <inheritdoc />
        public long GetInt64()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadInt64();
        }

        /// <inheritdoc />
        public ulong GetUInt64()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadUInt64();
        }

        /// <inheritdoc />
        public float GetSingle()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadSingle();
        }

        /// <inheritdoc />
        public double GetDouble()
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadDouble();
        }

        /// <inheritdoc />
        public byte[] GetBytes(int count)
        {
            ThrowIfAlreadyDisposed();
            return _reader.ReadBytes(count);
        }

        /// <inheritdoc />
        public IByteBuffer GetBuffer(uint count)
        {
            ThrowIfAlreadyDisposed();
            IByteBuffer buffer;
            if (count == 0)
            {
                buffer = EmptyBuffer.Value;
            }
            else if (count >= LargeObjectSize && _readOption == FileReadOption.ReadLargeOnDemand)
            {
                buffer = new FileByteBuffer(_file, _stream.Position, count);
                _stream.Seek(count, SeekOrigin.Current);
            }
            else if (count >= LargeObjectSize && _readOption == FileReadOption.SkipLargeTags)
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
        public Task<IByteBuffer> GetBufferAsync(uint count)
        {
            ThrowIfAlreadyDisposed();
            return Task.FromResult(GetBuffer(count));
        }

        /// <inheritdoc />
        /// <param name="count">Number of bytes to skip.</param>
        public void Skip(uint count)
        {
            ThrowIfAlreadyDisposed();
            _stream.Seek(count, SeekOrigin.Current);
        }

        /// <inheritdoc />
        public void Mark() => Marker = _stream.Position;

        /// <inheritdoc />
        public void Rewind() => _stream.Position = Marker;

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
                return (_stream.Length - _stream.Position) >= count;
            }
        }


        public Stream GetStream()
        {
            ThrowIfAlreadyDisposed();
            return _stream;
        }

        #endregion

        #region Disposal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfAlreadyDisposed()
        {
            if (Interlocked.CompareExchange(ref _isDisposed, 0, 0) == 0)
            {
                return;
            }

            ThrowDisposedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowDisposedException() => throw new ObjectDisposedException($"This file byte source for file {_file.Name} is already disposed and can no longer be used");

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Perform disposal.
        /// </summary>
        /// <param name="disposing">true if disposal request originates from Dispose call, false if request originates from finalizer.</param>
        private void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 1)
            {
                return;
            }

            if (disposing)
            {
                _reader.Dispose();
                _stream.Dispose();
            }
        }

        public ValueTask DisposeAsync()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return default;
        }

        #endregion
    }
}