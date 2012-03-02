using System;

namespace Dicom.IO.Buffer {
	public class RangeByteBuffer : IByteBuffer {
		public RangeByteBuffer(IByteBuffer buffer, uint offset, uint length) {
			Internal = buffer;
			Offset = offset;
			Length = length;
		}

		public IByteBuffer Internal {
			get;
			private set;
		}

		public uint Offset {
			get;
			private set;
		}

		public uint Length {
			get;
			private set;
		}

		public bool IsMemory {
			get { return Internal.IsMemory; }
		}

		public uint Size {
			get { return Length; }
		}

		public byte[] Data {
			get {
				return Internal.GetByteRange((int)Offset, (int)Length);
			}
		}

		public byte[] GetByteRange(int offset, int count) {
			return Internal.GetByteRange((int)Offset + offset, count);
		}
	}
}
