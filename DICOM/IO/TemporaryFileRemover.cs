using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dicom.IO {
	public class TemporaryFileRemover : IDisposable {
		private static TemporaryFileRemover _instance = new TemporaryFileRemover();
		private object _lock = new object();
		private List<string> _files = new List<string>();
		private Timer _timer;
		private bool _running = false;

		private TemporaryFileRemover() {
			_timer = new Timer(OnTick);
		}

		~TemporaryFileRemover() {
			DeleteAllRemainingFiles();
		}

		public void Dispose() {
			DeleteAllRemainingFiles();
			GC.SuppressFinalize(this);
		}

		private void DeleteAllRemainingFiles() {
			// one last try to delete all of the files
			for (int i = 0; i < _files.Count; i++) {
				try {
					File.Delete(_files[i]);
				} catch {
				}
			}
		}

		public static void Delete(string file) {
			_instance.DeletePrivate(file);
		}

		private void DeletePrivate(string file) {
			try {
				if (File.Exists(file))
					File.Delete(file);
			} catch {
				if (File.Exists(file)) {
					lock (_lock) {
						_files.Add(file);
						if (!_running) {
							_timer.Change(1000, 1000);
							_running = true;
						}
					}
				}
			}
		}

		private void OnTick(object state) {
			lock (_lock) {
				for (int i = 0; i < _files.Count; i++) {
					try {
						File.Delete(_files[i]);
						_files.RemoveAt(i--);
					} catch {
						if (!File.Exists(_files[i]))
							_files.RemoveAt(i--);
					}
				}

				if (_files.Count == 0) {
					_timer.Change(Timeout.Infinite, Timeout.Infinite);
					_running = false;
				}
			}
		}
	}
}
