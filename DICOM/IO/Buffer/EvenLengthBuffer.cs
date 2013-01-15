using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Buffer {
	public class EvenLengthBuffer : IByteBuffer {
		private EvenLengthBuffer(IByteBuffer buffer) {
			Buffer = buffer;
		}

		public IByteBuffer Buffer {
			get;
			private set;
		}

		public bool IsMemory {
			get { return true; }
		}

		public uint Size {
			get { return Buffer.Size + 1; }
		}

		public byte[] Data {
			get {
				byte[] data = new byte[Size];
				System.Buffer.BlockCopy(Buffer.Data, 0, data, 0, (int)Buffer.Size);
				return data;
			}
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] data = new byte[count];
			System.Buffer.BlockCopy(Buffer.Data, offset, data, 0, Math.Min((int)Buffer.Size, count));
			return data;
		}

		public static IByteBuffer Create(IByteBuffer buffer) {
			if ((buffer.Size & 1) == 1)
				return new EvenLengthBuffer(buffer);
			return buffer;
		}
	}
}
