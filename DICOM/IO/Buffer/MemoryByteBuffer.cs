using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Buffer {
	public sealed class MemoryByteBuffer : IByteBuffer {
		public MemoryByteBuffer(byte[] Data) {
			int len = Data.Length;
			this.Data = new byte[len];
			System.Buffer.BlockCopy(Data, 0, this.Data, 0, len);
		}

		public bool IsMemory {
			get { return true; }
		}

		public byte[] Data {
			get;
			private set;
		}

		public uint Size {
			get { return (uint)Data.Length; }
		}

		public byte[] GetByteRange(int offset, int count) {
			if (offset == 0 && count == Size)
				return Data;

			byte[] buffer = new byte[count];
			Array.Copy(Data, offset, buffer, 0, count);
			return buffer;
		}

        public override bool Equals(object obj) {
            return Equals(obj as IByteBuffer);
        }

        public bool Equals(IByteBuffer other) {
            if (other == null)
                return false;
            if (other.IsMemory)
                return Data.SequenceEqual(other.Data);
            else
                return false;
        }
	}
}
