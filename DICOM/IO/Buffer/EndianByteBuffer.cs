using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Buffer {
	public class EndianByteBuffer : IByteBuffer {
		protected EndianByteBuffer(IByteBuffer buffer, Endian endian, int unitSize) {
			Internal = buffer;
			Endian = endian;
			UnitSize = unitSize;
		}

		public IByteBuffer Internal {
			get;
			private set;
		}

		public Endian Endian {
			get;
			private set;
		}

		public int UnitSize {
			get;
			private set;
		}

		public bool IsMemory {
			get { return Internal.IsMemory; }
		}

		public uint Size {
			get { return Internal.Size; }
		}

		public byte[] Data {
			get {
				byte[] data = null;
				if (IsMemory) {
					data = new byte[Size];
					System.Buffer.BlockCopy(Internal.Data, 0, data, 0, data.Length);
				} else {
					data = Internal.Data;
				}

				if (Endian != Endian.LocalMachine)
					Endian.SwapBytes(UnitSize, data);

				return data;
			}
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] data = Internal.GetByteRange(offset, count);

			if (Endian != Endian.LocalMachine)
				Endian.SwapBytes(UnitSize, data);

			return data;
		}

		public static IByteBuffer Create(IByteBuffer buffer, Endian endian, int unitSize) {
			if (endian == Endian.LocalMachine || unitSize == 1)
				return buffer;
			return new EndianByteBuffer(buffer, endian, unitSize);
		}
	}
}
