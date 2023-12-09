// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.IO
{

    #region Endian

    /// <summary>
    /// Representation and convenience methods associated with endianness.
    /// </summary>
    public struct Endian
    {
        /// <summary>
        /// Little endian.
        /// </summary>
        public static readonly Endian Little = new Endian(false);

        /// <summary>
        /// Big endian.
        /// </summary>
        public static readonly Endian Big = new Endian(true);

        /// <summary>
        /// Endianness of the local machine, according to <see cref="System.BitConverter.IsLittleEndian"/>.
        /// </summary>
        public static readonly Endian LocalMachine = BitConverter.IsLittleEndian ? Little : Big;

        /// <summary>
        /// Network endian (big).
        /// </summary>
        public static readonly Endian Network = Big;

        private readonly bool _isBigEndian;

        /// <summary>
        /// Initializes an instance of the <see cref="Endian"/> struct.
        /// </summary>
        /// <param name="isBigEndian"></param>
        private Endian(bool isBigEndian)
        {
            _isBigEndian = isBigEndian;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Endian) return this == (Endian)obj;
            return false;
        }

        public override int GetHashCode()
        {
            return _isBigEndian.GetHashCode();
        }

        public override string ToString()
        {
            return _isBigEndian ? "Big Endian" : "Little Endian";
        }

        /// <summary>
        /// <see cref="Endian"/> equivalence operator.
        /// </summary>
        /// <param name="e1">First <see cref="Endian"/> object.</param>
        /// <param name="e2">Second <see cref="Endian"/> object.</param>
        /// <returns>True if <paramref name="e1"/> equals <paramref name="e2"/>, false otherwise.</returns>
        public static bool operator ==(Endian e1, Endian e2)
        {
            return e1._isBigEndian == e2._isBigEndian;
        }

        /// <summary>
        /// <see cref="Endian"/> non-equivalence operator.
        /// </summary>
        /// <param name="e1">First <see cref="Endian"/> object.</param>
        /// <param name="e2">Second <see cref="Endian"/> object.</param>
        /// <returns>True if <paramref name="e1"/> does not equal <paramref name="e2"/>, false otherwise.</returns>
        public static bool operator !=(Endian e1, Endian e2)
        {
            return !(e1 == e2);
        }

        /// <summary>
        /// Swap bytes in sequences of <paramref name="bytesToSwap"/>.
        /// </summary>
        /// <param name="bytesToSwap">Number of bytes to swap.</param>
        /// <param name="bytes">Array of bytes.</param>
        public static void SwapBytes(int bytesToSwap, byte[] bytes) => SwapBytes(bytesToSwap, bytes, bytes.Length);

        /// <summary>
        /// Swap bytes in sequences of <paramref name="bytesToSwap"/>.
        /// </summary>
        /// <param name="bytesToSwap">Number of bytes to swap.</param>
        /// <param name="bytes">Array of bytes.</param>
        /// <param name="count">The maximum number of bytes in the array that should be processed</param>
        public static void SwapBytes(int bytesToSwap, byte[] bytes, int count)
        {
            if (count > bytes.Length)
            {
                throw new ArgumentException($"Cannot swap {count} bytes of an array that only contains {bytes.Length} bytes");
            }

            if (count == 0)
            {
                return;
            }

            if (bytesToSwap == 1)
            {
                return;
            }

            if (bytesToSwap == 2)
            {
                SwapBytes2(bytes, count);
                return;
            }
            if (bytesToSwap == 4)
            {
                SwapBytes4(bytes, count);
                return;
            }

            unchecked
            {
                var l = count - (count % bytesToSwap);
                for (var i = 0; i < l; i += bytesToSwap)
                {
                    Array.Reverse(bytes, i, bytesToSwap);
                }
            }
        }

        /// <summary>
        /// Swap bytes in sequences of 2.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        public static void SwapBytes2(byte[] bytes) => SwapBytes2(bytes, bytes.Length);

        /// <summary>
        /// Swap bytes in sequences of 2.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        /// <param name="count">The maximum number of bytes in the array that should be processed</param>
        public static void SwapBytes2(byte[] bytes, int count)
        {
            unchecked
            {
                var l = count - count % 2;
                for (var i = 0; i < l; i += 2)
                {
                    (bytes[i + 1], bytes[i]) = (bytes[i], bytes[i + 1]);
                }
            }
        }

        /// <summary>
        /// Swap bytes in sequences of 4.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        public static void SwapBytes4(byte[] bytes) => SwapBytes4(bytes, bytes.Length);

        /// <summary>
        /// Swap bytes in sequences of 4.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        /// <param name="count">The maximum number of bytes in the array that should be processed</param>
        public static void SwapBytes4(byte[] bytes, int count)
        {
            unchecked
            {
                var l = count - (count % 4);
                for (var i = 0; i < l; i += 4)
                {
                    var b = bytes[i + 3];
                    bytes[i + 3] = bytes[i];
                    bytes[i] = b;
                    b = bytes[i + 2];
                    bytes[i + 2] = bytes[i + 1];
                    bytes[i + 1] = b;
                }
            }
        }

        /// <summary>
        /// Swap byte order in <see cref="short"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static short Swap(short value)
        {
            return (short)Swap((ushort)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="ushort"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static ushort Swap(ushort value)
        {
            return unchecked((ushort)((value >> 8) | (value << 8)));
        }

        /// <summary>
        /// Swap byte order in <see cref="int"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static int Swap(int value)
        {
            return (int)Swap((uint)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="uint"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static uint Swap(uint value)
        {
            return
                unchecked(
                    ((value & 0x000000ffU) << 24) | ((value & 0x0000ff00U) << 8) | ((value & 0x00ff0000U) >> 8)
                    | ((value & 0xff000000U) >> 24));
        }

        /// <summary>
        /// Swap byte order in <see cref="long"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static long Swap(long value)
        {
            return (long)Swap((ulong)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="ulong"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static ulong Swap(ulong value)
        {
            return
                unchecked(
                    ((value & 0x00000000000000ffU) << 56) | ((value & 0x000000000000ff00U) << 40)
                    | ((value & 0x0000000000ff0000U) << 24) | ((value & 0x00000000ff000000U) << 8)
                    | ((value & 0x000000ff00000000U) >> 8) | ((value & 0x0000ff0000000000U) >> 24)
                    | ((value & 0x00ff000000000000U) >> 40) | ((value & 0xff00000000000000U) >> 56));
        }

        /// <summary>
        /// Swap byte order in <see cref="float"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static float Swap(float value)
        {
            var b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// Swap byte order in <see cref="double"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static double Swap(double value)
        {
            var b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return BitConverter.ToDouble(b, 0);
        }

        /// <summary>
        /// Swap byte order in array of <see cref="short"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="short"/> values.</param>
        public static void Swap(short[] values)
        {
            Parallel.For(0, values.Length, i => values[i] = Swap(values[i]));
        }

        /// <summary>
        /// Swap byte order in array of <see cref="ushort"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="ushort"/> values.</param>
        public static void Swap(ushort[] values)
        {
            Parallel.For(0, values.Length, i => values[i] = Swap(values[i]));
        }

        /// <summary>
        /// Swap byte order in array of <see cref="int"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="int"/> values.</param>
        public static void Swap(int[] values)
        {
            Parallel.For(0, values.Length, i => values[i] = Swap(values[i]));
        }

        /// <summary>
        /// Swap byte order in array of <see cref="uint"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="uint"/> values.</param>
        public static void Swap(uint[] values)
        {
            Parallel.For(0, values.Length, i => values[i] = Swap(values[i]));
        }

        /// <summary>
        /// Swap byte order in array of values.
        /// </summary>
        /// <typeparam name="T">Array element type, must be one of <see cref="short"/>, <see cref="ushort"/>, <see cref="int"/> or <see cref="uint"/>.</typeparam>
        /// <param name="values">Array of values to swap.</param>
        /// <exception cref="System.InvalidOperationException">if array element type is not <see cref="short"/>, <see cref="ushort"/>, <see cref="int"/> or <see cref="uint"/>.</exception>
        public static void Swap<T>(T[] values)
        {
            if (typeof(T) == typeof(short)) Swap(values as short[]);
            else if (typeof(T) == typeof(ushort)) Swap(values as ushort[]);
            else if (typeof(T) == typeof(int)) Swap(values as int[]);
            else if (typeof(T) == typeof(uint)) Swap(values as uint[]);
            else throw new InvalidOperationException("Attempted to byte swap non-specialized type: " + typeof(T).Name);
        }
    }

    #endregion

    #region EndianBinaryReader

    /// <summary>
    /// Endian aware binary reader.
    /// </summary>
    public class EndianBinaryReader : BinaryReader
    {
        #region Private Members

        private bool _swapBytes = false;

        private byte[] _internalBuffer = new byte[8];

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryReader(Stream input, bool leaveOpen)
            : base(input, Encoding.UTF8, leaveOpen)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="encoding">Encoding of the <paramref name="input"/>.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryReader(Stream input, Encoding encoding, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryReader(Stream input, Endian endian, bool leaveOpen)
            : base(input, Encoding.UTF8, leaveOpen)
        {
            Endian = endian;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="encoding">Encoding of the <paramref name="input"/>.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryReader(Stream input, Encoding encoding, Endian endian, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
            Endian = endian;
        }

        /// <summary>
        /// Convenience method for creating a binary reader with requested <paramref name="endian">endianness</paramref>.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <returns>Binary reader with requested <paramref name="endian">endianness</paramref>.</returns>
        public static BinaryReader Create(Stream input, Endian endian, bool leaveOpen)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (BitConverter.IsLittleEndian)
            {
                if (Endian.Little == endian)
                {
                    return new BinaryReader(input, Encoding.UTF8, leaveOpen);
                }
                else
                {
                    return new EndianBinaryReader(input, endian, leaveOpen);
                }
            }
            else
            {
                if (Endian.Big == endian)
                {
                    return new BinaryReader(input, Encoding.UTF8, leaveOpen);
                }
                else
                {
                    return new EndianBinaryReader(input, endian, leaveOpen);
                }
            }
        }

        /// <summary>
        /// Convenience method for creating a binary reader with requested <paramref name="endian">endianness</paramref>.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="encoding">Encoding of the <paramref name="input"/>.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:EndianBinaryReaderr" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <returns>Binary reader with requested <paramref name="endian">endianness</paramref>.</returns>
        public static BinaryReader Create(Stream input, Encoding encoding, Endian endian, bool leaveOpen)
        {
            if (encoding == null) return Create(input, endian, leaveOpen);
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (BitConverter.IsLittleEndian)
            {
                if (Endian.Little == endian)
                {
                    return new BinaryReader(input, encoding, leaveOpen);
                }
                else
                {
                    return new EndianBinaryReader(input, encoding, endian, leaveOpen);
                }
            }
            else
            {
                if (Endian.Big == endian)
                {
                    return new BinaryReader(input, encoding, leaveOpen);
                }
                else
                {
                    return new EndianBinaryReader(input, encoding, endian, leaveOpen);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the endianness of the binary reader.
        /// </summary>
        public Endian Endian
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                {
                    return _swapBytes ? Endian.Big : Endian.Little;
                }
                else
                {
                    return _swapBytes ? Endian.Little : Endian.Big;
                }
            }
            protected set
            {
                if (BitConverter.IsLittleEndian)
                {
                    _swapBytes = (Endian.Big == value);
                }
                else
                {
                    _swapBytes = (Endian.Little == value);
                }
            }
        }

        public bool UseInternalBuffer
        {
            get => _internalBuffer != null;
            set
            {
                if (value && (_internalBuffer == null))
                {
                    _internalBuffer = new byte[8];
                }
                else
                {
                    _internalBuffer = null;
                }
            }
        }

        #endregion

        #region Private Methods

        private byte[] ReadBytesInternal(int count)
        {
            byte[] buffer = null;
            if (_internalBuffer != null)
            {
                base.Read(_internalBuffer, 0, count);
                buffer = _internalBuffer;
            }
            else
            {
                buffer = base.ReadBytes(count);
            }
            if (_swapBytes)
            {
                Array.Reverse(buffer, 0, count);
            }
            return buffer;
        }

        #endregion

        #region BinaryReader Overrides

        /// <inheritdoc />
        public override short ReadInt16()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadInt16());
            }
            return base.ReadInt16();
        }

        /// <inheritdoc />
        public override int ReadInt32()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadInt32());
            }
            return base.ReadInt32();
        }

        /// <inheritdoc />
        public override long ReadInt64()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadInt64());
            }
            return base.ReadInt64();
        }

        /// <inheritdoc />
        public override float ReadSingle()
        {
            if (_swapBytes)
            {
                byte[] b = ReadBytesInternal(4);
                return BitConverter.ToSingle(b, 0);
            }
            return base.ReadSingle();
        }

        /// <inheritdoc />
        public override double ReadDouble()
        {
            if (_swapBytes)
            {
                byte[] b = ReadBytesInternal(8);
                return BitConverter.ToDouble(b, 0);
            }
            return base.ReadDouble();
        }

        /// <inheritdoc />
        public override ushort ReadUInt16()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadUInt16());
            }
            return base.ReadUInt16();
        }

        /// <inheritdoc />
        public override uint ReadUInt32()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadUInt32());
            }
            return base.ReadUInt32();
        }

        /// <inheritdoc />
        public override ulong ReadUInt64()
        {
            if (_swapBytes)
            {
                return Endian.Swap(base.ReadUInt64());
            }
            return base.ReadUInt64();
        }

        #endregion
    }

    #endregion

    #region EndianBinaryWriter

    /// <summary>
    /// Endian aware binary writer.
    /// </summary>
    public class EndianBinaryWriter : BinaryWriter
    {
        #region Private Members

        private bool _swapBytes = false;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <remarks>Uses the endianness of the system.</remarks>
        public EndianBinaryWriter(Stream output, bool leaveOpen)
            : base(output, Encoding.UTF8, leaveOpen)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="encoding">Output encoding.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <remarks>Uses the endianness of the system.</remarks>
        public EndianBinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="endian">Endianness of the output.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryWriter(Stream output, Endian endian, bool leaveOpen)
            : base(output, Encoding.UTF8, leaveOpen)
        {
            Endian = endian;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="encoding">Output encoding.</param>
        /// <param name="endian">Endianness of the output.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        public EndianBinaryWriter(Stream output, Encoding encoding, Endian endian, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            Endian = endian;
        }

        /// <summary>
        /// Convenience method for creating a sufficient binary writer based on specified <paramref name="endian">endianness</paramref>.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="endian">Endianness of the output.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <returns>Binary writer with desired <paramref name="endian">endianness</paramref>-</returns>
        public static BinaryWriter Create(Stream output, Endian endian, bool leaveOpen)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (BitConverter.IsLittleEndian)
            {
                if (Endian.Little == endian)
                {
                    return new BinaryWriter(output, Encoding.UTF8, leaveOpen);
                }
                else
                {
                    return new EndianBinaryWriter(output, endian, leaveOpen);
                }
            }
            else
            {
                if (Endian.Big == endian)
                {
                    return new BinaryWriter(output, Encoding.UTF8, leaveOpen);
                }
                else
                {
                    return new EndianBinaryWriter(output, endian, leaveOpen);
                }
            }
        }

        /// <summary>
        /// Convenience method for creating a sufficient binary writer based on specified <paramref name="endian">endianness</paramref>.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="encoding">Output encoding.</param>
        /// <param name="endian">Endianness of the output.</param>
        /// <param name="leaveOpen"><see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
        /// <returns>Binary writer with desired <paramref name="endian">endianness</paramref>-</returns>
        public static BinaryWriter Create(Stream output, Encoding encoding, Endian endian, bool leaveOpen)
        {
            if (encoding == null) return Create(output, endian, leaveOpen);
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (BitConverter.IsLittleEndian)
            {
                if (Endian.Little == endian)
                {
                    return new BinaryWriter(output, encoding, leaveOpen);
                }
                else
                {
                    return new EndianBinaryWriter(output, encoding, endian, leaveOpen);
                }
            }
            else
            {
                if (Endian.Big == endian)
                {
                    return new BinaryWriter(output, encoding, leaveOpen);
                }
                else
                {
                    return new EndianBinaryWriter(output, encoding, endian, leaveOpen);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the endianness of the binary writer.
        /// </summary>
        public Endian Endian
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                {
                    return _swapBytes ? Endian.Big : Endian.Little;
                }
                else
                {
                    return _swapBytes ? Endian.Little : Endian.Big;
                }
            }
            protected set
            {
                if (BitConverter.IsLittleEndian)
                {
                    _swapBytes = Endian.Big == value;
                }
                else
                {
                    _swapBytes = Endian.Little == value;
                }
            }
        }

        #endregion

        #region Private Methods

        private void WriteInternal(byte[] buffer)
        {
            if (_swapBytes)
            {
                Array.Reverse(buffer);
            }
            base.Write(buffer);
        }

        #endregion

        #region BinaryWriter Overrides

        /// <inheritdoc />
        public override void Write(double value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(float value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(int value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(long value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(short value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(uint value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(ulong value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(ushort value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        #endregion
    }

    #endregion
}
