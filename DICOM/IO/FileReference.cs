using System;
using System.IO;

namespace Dicom.IO {
	public sealed class FileReference {
		public FileReference(string fileName, bool isTempFile = false) {
			Name = fileName;
			IsTempFile = isTempFile;
		}

		~FileReference() {
			if (IsTempFile) {
				try {
					Delete();
				} catch {
				}
			}
		}

		public string Name {
			get;
			private set;
		}

		/// <summary>File will be deleted when object is Disposed.</summary>
		public bool IsTempFile {
			get;
			internal set;
		}

		public Stream OpenRead() {
			return File.OpenRead(Name);
		}

		public Stream OpenWrite() {
			return File.OpenWrite(Name);
		}

		public void Delete() {
			if (IsTempFile)
				TemporaryFileRemover.Delete(Name);
			else if (File.Exists(Name))
				File.Delete(Name);
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
