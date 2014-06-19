using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dicom.Media {
	public delegate void DicomScanProgressCallback(DicomFileScanner scanner, string directory, int count);
	public delegate void DicomScanFileFoundCallback(DicomFileScanner scanner, DicomFile file, string fileName);
	public delegate void DicomScanCompleteCallback(DicomFileScanner scanner);

	public class DicomFileScanner {
		#region Private Members
		private string _pattern;
		private bool _recursive;
		private bool _stop;
		private bool _progressOnDirectory;
		private int _progressAfterCount;
		private bool _checkForValidHeader;
		private int _count;
		#endregion

		#region Public Constructor
		public DicomFileScanner() {
			_pattern = null;
			_recursive = true;
			_progressOnDirectory = true;
			_progressAfterCount = 10;
		}
		#endregion

		public event DicomScanProgressCallback Progress;
		public event DicomScanFileFoundCallback FileFound;
		public event DicomScanCompleteCallback Complete;

		#region Public Properties
		public bool ProgressOnDirectoryChange {
			get { return _progressOnDirectory; }
			set { _progressOnDirectory = value; }
		}

		public int ProgressFilesCount {
			get { return _progressAfterCount; }
			set { _progressAfterCount = value; }
		}

		public bool CheckForValidHeader {
			get { return _checkForValidHeader; }
			set { _checkForValidHeader = value; }
		}
		#endregion

		#region Public Methods
		public void Start(string directory) {
			_stop = false;
			_count = 0;
			new Thread(ScanProc).Start(directory);
		}

		public void Stop() {
			_stop = true;
		}
		#endregion

		#region Private Methods
		private void ScanProc(object state) {
			string directory = (string)state;
			ScanDirectory(directory);

			if (Complete != null)
				Complete(this);
		}

		private void ScanDirectory(string directory) {
			if (_stop)
				return;

			if (Progress != null && _progressOnDirectory)
				Progress(this, directory, _count);

			try {
				string[] files;
				if (!String.IsNullOrEmpty(_pattern))
					files = Directory.GetFiles(directory, _pattern);
				else
					files = Directory.GetFiles(directory);

				foreach (string file in files) {
					if (_stop)
						return;

					ScanFile(file);

					_count++;
					if ((_count % _progressAfterCount) == 0 && Progress != null)
						Progress(this, directory, _count);
				}

				if (!_recursive)
					return;

				string[] dirs = Directory.GetDirectories(directory);
				foreach (string dir in dirs) {
					if (_stop)
						return;

					ScanDirectory(dir);
				}
			} catch {
			}
		}

		private void ScanFile(string file) {
			try {
				if (CheckForValidHeader && !DicomFile.HasValidHeader(file))
					return;

				var df = DicomFile.Open(file);

				if (FileFound != null)
					FileFound(this, df, file);
			} catch {
				// ignore exceptions?
			}
		}
		#endregion
	}
}