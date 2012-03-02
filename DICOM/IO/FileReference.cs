using System;
using System.IO;

namespace Dicom.IO {
	public class FileReference {
		private string _name;

		public FileReference(string fileName) {
			_name = fileName;
		}

		public string Name {
			get { return _name; }
		}

		public Stream OpenRead() {
			return File.OpenRead(_name);
		}

		public Stream OpenWrite() {
			return File.OpenWrite(_name);
		}

		public void Delete() {
			if (File.Exists(_name))
				File.Delete(_name);
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] buffer = new byte[count];

			using (Stream fs = OpenRead()) {
				fs.Seek(offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, count);
			}

			return buffer;
		}
	}
}
