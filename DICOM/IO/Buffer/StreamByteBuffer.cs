using System;
using System.IO;

namespace Dicom.IO.Buffer {
	public sealed class StreamByteBuffer : IByteBuffer {
		public StreamByteBuffer(Stream Stream, long Position, uint Length) {
			this.Stream = Stream;
			this.Position = Position;
			this.Size = Length;
		}

		public bool IsMemory {
			get { return false; }
		}

		public Stream Stream {
			get;
			private set;
		}

		public long Position {
			get;
			private set;
		}

		public uint Size {
			get;
			private set;
		}

		public byte[] Data {
			get {
				byte[] data = new byte[Size];
				Stream.Position = Position;
				Stream.Read(data, 0, (int)Size);
				return data;
			}
		}

		public byte[] GetByteRange(int offset, int count) {
			if (offset == 0 && count == Size)
				return Data;

			byte[] buffer = new byte[count];
			Stream.Position = Position + offset;
			Stream.Read(buffer, 0, count);
			return buffer;
		}
	}
}
