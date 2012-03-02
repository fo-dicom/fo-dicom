using System;
using System.Collections.Generic;

namespace Dicom.IO.Buffer {
	public static class ByteBufferExtensions {
		public static IEnumerable<T> Enumerate<T>(this IByteBuffer buffer) {
			return ByteBufferEnumerator<T>.Create(buffer);
		}
	}
}
