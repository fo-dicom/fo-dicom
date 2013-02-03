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

		/// <summary>File will be deleted when object is <c>Disposed</c>.</summary>
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

		/// <summary>
		/// Moves file and updates internal reference.
		/// 
		/// Calling this method will also remove set the <see cref="IsTempFile"/> property to <c>False</c>.
		/// </summary>
		/// <param name="dstFileName"></param>
		public void Move(string dstFileName, bool overwrite = false) {
			// delete if overwriting; let File.Move thow IOException if not
			if (File.Exists(dstFileName) && overwrite)
				File.Delete(dstFileName);

			File.Move(Name, dstFileName);
			Name = Path.GetFullPath(dstFileName);
			IsTempFile = false;
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] buffer = new byte[count];

			using (Stream fs = OpenRead()) {
				fs.Seek(offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, count);
			}

			return buffer;
		}

		public override string ToString() {
			if (IsTempFile)
				return String.Format("{0} [TEMP]", Name);
			return Name;
		}
	}
}
