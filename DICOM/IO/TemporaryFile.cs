using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dicom.IO {
	public class TemporaryFile : IDisposable {
		private string _file;

		public TemporaryFile() {
			_file = Create();
		}

		~TemporaryFile() {
			TemporaryFileRemover.Delete(_file);
		}

		public void Dispose() {
			TemporaryFileRemover.Delete(_file);
			GC.SuppressFinalize(this);
		}

		public string Name {
			get { return _file; }
		}

		#region Static
		private static string _path = null;
		public static string StoragePath {
			get {
				if (_path != null)
					return _path;
				return Path.GetTempPath();
			}
			set {
				_path = value;
				if (!Directory.Exists(_path))
					Directory.CreateDirectory(_path);
			}
		}

		public static string Create() {
			string path = null;

			if (_path != null) {
				// create file in user specified path
				path = Path.Combine(_path, Guid.NewGuid().ToString());
				File.Create(path).Close();
			} else {
				// allow OS to create file in system temp path
				path = Path.GetTempFileName();
			}

			try {
				// set temporary file attribute so that the file system
				// will attempt to keep all of the data in memory
				File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Temporary);
			} catch {
				// sometimes fails with invalid argument exception
			}

			return path;
		}
		#endregion

		public override string ToString() {
			return String.Format("{0} [TEMP]", Name);
		}
	}
}
