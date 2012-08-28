using System;
using System.IO;

namespace Dicom.IO.Buffer {
	public sealed class TempFileBuffer : IByteBuffer, IDisposable {
		private string _path;
		private uint _size;

		public TempFileBuffer(byte[] data) {
			_path = Path.GetTempFileName();

			// set temporary file attribute so that the file system
			// attempts to keep all of the data in memory
			File.SetAttributes(_path, FileAttributes.Temporary);

			File.WriteAllBytes(_path, data);
			_size = (uint)data.Length;
		}

		public void Dispose() {
			if (_path != null) {
				TempFileRemover.Delete(_path);
				_path = null;
			}
		}

		public bool IsMemory {
			get { return false; }
		}

		public uint Size {
			get { return _size; }
		}

		public byte[] Data {
			get { return File.ReadAllBytes(_path); }
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] buffer = new byte[count];

			using (Stream fs = File.OpenRead(_path)) {
				fs.Seek(offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, count);
			}

			return buffer;
		}
	}
}
