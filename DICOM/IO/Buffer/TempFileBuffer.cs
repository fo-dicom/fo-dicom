using System;
using System.IO;

namespace Dicom.IO.Buffer {
	public sealed class TempFileBuffer : IByteBuffer {
		private TemporaryFile _file;
		private uint _size;

		public TempFileBuffer(byte[] data) {
			_file = new TemporaryFile();
#if WINDOWS_PHONE
			WPFile.WriteAllBytes(_file.Name, data);
#else
			File.WriteAllBytes(_file.Name, data);
#endif
			_size = (uint)data.Length;
		}

		public bool IsMemory {
			get { return false; }
		}

		public uint Size {
			get { return _size; }
		}

		public byte[] Data {
#if WINDOWS_PHONE
			get { return WPFile.ReadAllBytes(_file.Name); }
#else
			get { return File.ReadAllBytes(_file.Name); }
#endif
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] buffer = new byte[count];

			using (Stream fs = File.OpenRead(_file.Name)) {
				fs.Seek(offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, count);
			}

			return buffer;
		}
	}
}
