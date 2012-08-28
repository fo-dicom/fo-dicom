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

		public string Name {
			get { return _file; }
		}

		public void Dispose() {
			TempFileRemover.Delete(_file);
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
				path = Path.GetTempPath();
			}

			// set temporary file attribute so that the file system
			// attempts to keep all of the data in memory
			File.SetAttributes(path, FileAttributes.Temporary);

			return path;
		}
		#endregion
	}
}
