using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Buffer {
	public sealed class EmptyBuffer : IByteBuffer {
		public readonly static IByteBuffer Value = new EmptyBuffer();

		internal EmptyBuffer() {
			Data = new byte[0];
		}

		public bool IsMemory {
			get { return true; }
		}

		public byte[] Data {
			get;
			private set;
		}

		public uint Size {
			get { return 0; }
		}

		public byte[] GetByteRange(int offset, int count) {
			if (offset != 0 || count != 0)
				throw new ArgumentOutOfRangeException("offset", "Offset and count cannot be greater than 0 in EmptyBuffer");
			return Data;
		}
	}
}
