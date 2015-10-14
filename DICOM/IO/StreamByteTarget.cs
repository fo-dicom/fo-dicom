﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Representation of a stream byte target.
    /// </summary>
    public class StreamByteTarget : IByteTarget
    {
        private readonly Stream _stream;

        private Endian _endian;

        private BinaryWriter _writer;

        private readonly object _lock;

        /// <summary>
        /// Initializes an instance of <see cref="StreamByteTarget"/>.
        /// </summary>
        /// <param name="stream">Stream subject to writing.</param>
        public StreamByteTarget(Stream stream)
        {
            _stream = stream;
            _endian = Endian.LocalMachine;
            _writer = EndianBinaryWriter.Create(_stream, _endian);
            _lock = new object();
        }

        /// <summary>
        /// Gets or sets the endianness of the byte target.
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
                        _writer = EndianBinaryWriter.Create(_stream, _endian);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current write position.
        /// </summary>
        public long Position
        {
            get
            {
                return _stream.Position;
            }
        }

        /// <summary>
        /// Write one <see cref="byte"/> to target.
        /// </summary>
        /// <param name="v"><see cref="byte"/> to write.</param>
        public void Write(byte v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="short"/> to target.
        /// </summary>
        /// <param name="v"><see cref="short"/> to write.</param>
        public void Write(short v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="ushort"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ushort"/> to write.</param>
        public void Write(ushort v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="int"/> to target.
        /// </summary>
        /// <param name="v"><see cref="int"/> to write.</param>
        public void Write(int v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="uint"/> to target.
        /// </summary>
        /// <param name="v"><see cref="uint"/> to write.</param>
        public void Write(uint v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="long"/> to target.
        /// </summary>
        /// <param name="v"><see cref="long"/> to write.</param>
        public void Write(long v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="ulong"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ulong"/> to write.</param>
        public void Write(ulong v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="float"/> to target.
        /// </summary>
        /// <param name="v"><see cref="float"/> to write.</param>
        public void Write(float v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="double"/> to target.
        /// </summary>
        /// <param name="v"><see cref="double"/> to write.</param>
        public void Write(double v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        public void Write(byte[] buffer, uint offset, uint count)
        {
            _stream.Write(buffer, (int)offset, (int)count);
        }

        /// <summary>
        /// Asynchronously write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        /// <returns>Avaitable <see cref="Task"/>.</returns>
        public Task WriteAsync(byte[] buffer, uint offset, uint count)
        {
            return _stream.WriteAsync(buffer, (int)offset, (int)count);
        }
    }
}
