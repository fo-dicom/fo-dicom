using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Dicom.IO.Buffer;
using Dicom.Imaging.Mathematics
;

namespace Dicom.IO {
	public static class ByteConverter {
		public static IByteBuffer ToByteBuffer(string value, Encoding encoding=null) {
			if (encoding == null)
				encoding = Encoding.UTF8;

			byte[] bytes = encoding.GetBytes(value);

			return new MemoryByteBuffer(bytes);
		}

		public static IByteBuffer ToByteBuffer(string value, Encoding encoding, byte padding) {
			if (encoding == null)
				encoding = Encoding.UTF8;

			byte[] bytes = encoding.GetBytes(value);

			if (bytes.Length.IsOdd()) {
				Array.Resize(ref bytes, bytes.Length + 1);
				bytes[bytes.Length - 1] = padding;
			}

			return new MemoryByteBuffer(bytes);
		}

		public static IByteBuffer ToByteBuffer<T>(T[] values) where T: struct {
			int size = System.Buffer.ByteLength(values);
			byte[] data = new byte[size];
			System.Buffer.BlockCopy(values, 0, data, 0, size);
			return new MemoryByteBuffer(data);
		}

		public static T[] ToArray<T>(IByteBuffer buffer) {
			uint size = (uint)Marshal.SizeOf(typeof(T));
			uint padding = buffer.Size % size;
			uint count = buffer.Size / size;
			T[] values = new T[count];
			System.Buffer.BlockCopy(buffer.Data, 0, values, 0, (int)(buffer.Size - padding));
			return values;
		}

		public static T Get<T>(IByteBuffer buffer, int n) {
			int size = Marshal.SizeOf(typeof(T));
			T[] values = new T[1];
			if (buffer.IsMemory)
				System.Buffer.BlockCopy(buffer.Data, size * n, values, 0, size);
			else {
				byte[] temp = buffer.GetByteRange(size * n, size);
				System.Buffer.BlockCopy(temp, 0, values, 0, size);
			}
			return values[0];
		}

		public static IByteBuffer UnpackLow16(IByteBuffer data) {
			byte[] bytes = new byte[data.Size / 2];
			byte[] datab = data.Data;
			for (int i = 0; i < bytes.Length && (i * 2) < datab.Length; i++) {
				bytes[i] = datab[i * 2];
			}
			return new MemoryByteBuffer(bytes);
		}

		public static IByteBuffer UnpackHigh16(IByteBuffer data) {
			byte[] bytes = new byte[data.Size / 2];
			byte[] datab = data.Data;
			for (int i = 0; i < bytes.Length && ((i * 2) + 1) < datab.Length; i++) {
				bytes[i] = datab[(i * 2) + 1];
			}
			return new MemoryByteBuffer(bytes);
		}
	}
}
