using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dicom.IO {
	#region Endian
	public struct Endian {
		public static readonly Endian Little = new Endian(false);
		public static readonly Endian Big = new Endian(true);
		public static readonly Endian LocalMachine = BitConverter.IsLittleEndian ? Little : Big;
		public static readonly Endian Network = Big;

		private bool _isBigEndian;
		private Endian(bool isBigEndian) {
			_isBigEndian = isBigEndian;
		}

		public override bool Equals(object obj) {
			if (obj == null)
				return false;
			if (obj is Endian)
				return this == (Endian)obj;
			return false;
		}

		public override int GetHashCode() {
			return _isBigEndian.GetHashCode();
		}

		public override string ToString() {
			if (_isBigEndian)
				return "Big Endian";
			return "Little Endian";
		}

		public static bool operator ==(Endian e1, Endian e2) {
			if ((object)e1 == null && (object)e2 == null)
				return true;
			if ((object)e1 == null || (object)e2 == null)
				return false;
			return e1._isBigEndian == e2._isBigEndian;
		}
		public static bool operator !=(Endian e1, Endian e2) {
			return !(e1 == e2);
		}

		public static void SwapBytes(int bytesToSwap, byte[] bytes) {
			if (bytesToSwap == 1)
				return;
			if (bytesToSwap == 2) { SwapBytes2(bytes); return; }
			if (bytesToSwap == 4) { SwapBytes4(bytes); return; }
			//if (bytesToSwap == 8) { Swap8(); return; }
			unchecked {
				int l = bytes.Length - (bytes.Length % bytesToSwap);
				for (int i = 0; i < l; i += bytesToSwap) {
					Array.Reverse(bytes, i, bytesToSwap);
				}
			}
		}
		public static void SwapBytes2(byte[] bytes) {
			unchecked {
				byte b;
				int l = bytes.Length - (bytes.Length % 2);
				for (int i = 0; i < l; i += 2) {
					b = bytes[i + 1];
					bytes[i + 1] = bytes[i];
					bytes[i] = b;
				}
			}
		}
		public static void SwapBytes4(byte[] bytes) {
			unchecked {
				byte b;
				int l = bytes.Length - (bytes.Length % 4);
				for (int i = 0; i < l; i += 4) {
					b = bytes[i + 3];
					bytes[i + 3] = bytes[i];
					bytes[i] = b;
					b = bytes[i + 2];
					bytes[i + 2] = bytes[i + 1];
					bytes[i + 1] = b;
				}
			}
		}
		public static void SwapBytes8(byte[] bytes) {
			SwapBytes(8, bytes);
		}

		public static short Swap(short value) {
			return (short)Swap((ushort)value);
		}
		public static ushort Swap(ushort value) {
			return unchecked((ushort)((value >> 8) | (value << 8)));
		}

		public static int Swap(int value) {
			return (int)Swap((uint)value);
		}
		public static uint Swap(uint value) {
			return unchecked(
					((value & 0x000000ffU) << 24) |
					((value & 0x0000ff00U) <<  8) |
					((value & 0x00ff0000U) >>  8) |
					((value & 0xff000000U) >> 24));
		}

		public static long Swap(long value) {
			return (long)Swap((ulong)value);
		}
		public static ulong Swap(ulong value) {
			return unchecked(
					((value & 0x00000000000000ffU) << 56) |
					((value & 0x000000000000ff00U) << 40) |
					((value & 0x0000000000ff0000U) << 24) |
					((value & 0x00000000ff000000U) <<  8) |
					((value & 0x000000ff00000000U) >>  8) |
					((value & 0x0000ff0000000000U) >> 24) |
					((value & 0x00ff000000000000U) >> 40) |
					((value & 0xff00000000000000U) >> 56));
		}

		public static float Swap(float value) {
			byte[] b = BitConverter.GetBytes(value);
			Array.Reverse(b);
			return BitConverter.ToSingle(b, 0);
		}

		public static double Swap(double value) {
			byte[] b = BitConverter.GetBytes(value);
			Array.Reverse(b);
			return BitConverter.ToDouble(b, 0);
		}

		public static void Swap(short[] values) {
			Parallel.For(0, values.Length, (i) => {
				values[i] = Swap(values[i]);
			});
		}

		public static void Swap(ushort[] values) {
			Parallel.For(0, values.Length, (i) => {
				values[i] = Swap(values[i]);
			});
		}

		public static void Swap(int[] values) {
			Parallel.For(0, values.Length, (i) => {
				values[i] = Swap(values[i]);
			});
		}

		public static void Swap(uint[] values) {
			Parallel.For(0, values.Length, (i) => {
				values[i] = Swap(values[i]);
			});
		}

		public static void Swap<T>(T[] values) {
			if (typeof(T) == typeof(short))
				Swap(values as short[]);
			else if (typeof(T) == typeof(ushort))
				Swap(values as ushort[]);
			else if (typeof(T) == typeof(int))
				Swap(values as int[]);
			else if (typeof(T) == typeof(uint))
				Swap(values as uint[]);
			else
				throw new InvalidOperationException("Attempted to byte swap non-specialized type: " + typeof(T).Name);
		}
	}
	#endregion

	#region EndianBinaryReader
	public class EndianBinaryReader : BinaryReader {
		#region Private Members
		private bool SwapBytes = false;
		private byte[] InternalBuffer = new byte[8];
		#endregion

		#region Public Constructors
		public EndianBinaryReader(Stream input) : base(input) {
		}
		public EndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding) {
		}
		public EndianBinaryReader(Stream input, Endian endian) : base(input) {
			Endian = endian;
		}
		public EndianBinaryReader(Stream input, Encoding encoding, Endian endian) : base(input, encoding) {
			Endian = endian;
		}

		public static BinaryReader Create(Stream input, Endian endian) {
			if (input == null)
				throw new ArgumentNullException("input");
			if (endian == null)
				throw new ArgumentNullException("endian");

			if (BitConverter.IsLittleEndian) {
				if (Endian.Little == endian) {
					return new BinaryReader(input);
				} else {
					return new EndianBinaryReader(input, endian);
				}
			} else {
				if (Endian.Big == endian) {
					return new BinaryReader(input);
				} else {
					return new EndianBinaryReader(input, endian);
				}
			}
		}

		public static BinaryReader Create(Stream input, Encoding encoding, Endian endian) {
			if (encoding == null)
				return Create(input, endian);
			if (input == null)
				throw new ArgumentNullException("input");
			if (endian == null)
				throw new ArgumentNullException("endian");

			if (BitConverter.IsLittleEndian) {
				if (Endian.Little == endian) {
					return new BinaryReader(input, encoding);
				}
				else {
					return new EndianBinaryReader(input, encoding, endian);
				}
			}
			else {
				if (Endian.Big == endian) {
					return new BinaryReader(input, encoding);
				}
				else {
					return new EndianBinaryReader(input, encoding, endian);
				}
			}
		}
		#endregion

		#region Public Properties
		public Endian Endian {
			get {
				if (BitConverter.IsLittleEndian) {
					return SwapBytes ? Endian.Big : Endian.Little;
				} else {
					return SwapBytes ? Endian.Little : Endian.Big;
				}
			}
			set {
				if (BitConverter.IsLittleEndian) {
					SwapBytes = (Endian.Big == value);
				} else {
					SwapBytes = (Endian.Little == value);
				}
			}
		}

		public bool UseInternalBuffer {
			get {
				return (InternalBuffer != null);
			}
			set {
				if (value && (InternalBuffer == null)) {
					InternalBuffer = new byte[8];
				} else {
					InternalBuffer = null;
				}
			}
		}
		#endregion

		#region Private Methods
		private byte[] ReadBytesInternal(int count) {
			byte[] Buffer = null;
			if (InternalBuffer != null) {
				base.Read(InternalBuffer, 0, count);
				Buffer = InternalBuffer;
			} else {
				Buffer = base.ReadBytes(count);
			}
			if (SwapBytes) {
				Array.Reverse(Buffer, 0, count);
			}
			return Buffer;
		}
		#endregion

		#region BinaryReader Overrides
		public override short ReadInt16() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadInt16());
			}
			return base.ReadInt16();
		}

		public override int ReadInt32() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadInt32());
			}
			return base.ReadInt32();
		}

		public override long ReadInt64() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadInt64());
			}
			return base.ReadInt64();
		}

		public override float ReadSingle() {
			if (SwapBytes) {
				byte[] b = ReadBytesInternal(4);
				return BitConverter.ToSingle(b, 0);
			}
			return base.ReadSingle();
		}

		public override double ReadDouble() {
			if (SwapBytes) {
				byte[] b = ReadBytesInternal(8);
				return BitConverter.ToDouble(b, 0);
			}
			return base.ReadDouble();
		}

		public override ushort ReadUInt16() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadUInt16());
			}
			return base.ReadUInt16();
		}

		public override uint ReadUInt32() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadUInt32());
			}
			return base.ReadUInt32();
		}

		public override ulong ReadUInt64() {
			if (SwapBytes) {
				return Endian.Swap(base.ReadUInt64());
			}
			return base.ReadUInt64();
		}
		#endregion
	}
	#endregion

	#region EndianBinaryWriter
	public class EndianBinaryWriter : BinaryWriter {
		#region Private Members
		private bool SwapBytes = false;
		#endregion

		#region Public Constructors
		public EndianBinaryWriter(Stream output) : base(output) {
		}
		public EndianBinaryWriter(Stream output, Encoding encoding) : base(output, encoding) {
		}
		public EndianBinaryWriter(Stream output, Endian endian) : base(output) {
			Endian = endian;
		}
		public EndianBinaryWriter(Stream output, Encoding encoding, Endian endian) : base(output, encoding) {
			Endian = endian;
		}

		public static BinaryWriter Create(Stream output, Endian endian) {
			if (output == null)
				throw new ArgumentNullException("output");
			if (endian == null)
				throw new ArgumentNullException("endian");

			if (BitConverter.IsLittleEndian) {
				if (Endian.Little == endian) {
					return new BinaryWriter(output);
				} else {
					return new EndianBinaryWriter(output, endian);
				}
			} else {
				if (Endian.Big == endian) {
					return new BinaryWriter(output);
				} else {
					return new EndianBinaryWriter(output, endian);
				}
			}
		}

		public static BinaryWriter Create(Stream output, Encoding encoding, Endian endian) {
			if (encoding == null)
				return Create(output, endian);
			if (output == null)
				throw new ArgumentNullException("output");
			if (endian == null)
				throw new ArgumentNullException("endian");

			if (BitConverter.IsLittleEndian) {
				if (Endian.Little == endian) {
					return new BinaryWriter(output, encoding);
				}
				else {
					return new EndianBinaryWriter(output, encoding, endian);
				}
			}
			else {
				if (Endian.Big == endian) {
					return new BinaryWriter(output, encoding);
				}
				else {
					return new EndianBinaryWriter(output, encoding, endian);
				}
			}
		}
		#endregion

		#region Public Properties
		public Endian Endian {
			get {
				if (BitConverter.IsLittleEndian) {
					return SwapBytes ? Endian.Big : Endian.Little;
				} else {
					return SwapBytes ? Endian.Little : Endian.Big;
				}
			}
			set {
				if (BitConverter.IsLittleEndian) {
					SwapBytes = (Endian.Big == value);
				} else {
					SwapBytes = (Endian.Little == value);
				}
			}
		}
		#endregion

		#region Private Methods
		private void WriteInternal(byte[] Buffer) {
			if (SwapBytes) {
				Array.Reverse(Buffer);
			}
			base.Write(Buffer);
		}
		#endregion

		#region BinaryWriter Overrides
		public override void Write(double value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(float value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(int value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(long value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(short value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(uint value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(ulong value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}

		public override void Write(ushort value) {
			if (SwapBytes) {
				byte[] b = BitConverter.GetBytes(value);
				WriteInternal(b);
			} else {
				base.Write(value);
			}
		}
		#endregion
	}
	#endregion
}