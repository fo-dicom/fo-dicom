// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.IO;

namespace FellowOakDicom.Media
{

    public delegate void DicomScanProgressCallback(DicomFileScanner scanner, string directory, int count);

    public delegate void DicomScanFileFoundCallback(DicomFileScanner scanner, DicomFile file, string fileName);

    public delegate void DicomScanCompleteCallback(DicomFileScanner scanner);

    public class DicomFileScanner
    {
        #region Private Members

        private readonly string _pattern;

        private readonly bool _recursive;

        private bool _stop;
        private int _count;

        #endregion

        #region Public Constructor

        public DicomFileScanner()
        {
            _pattern = null;
            _recursive = true;
            ProgressOnDirectoryChange = true;
            ProgressFilesCount = 10;
        }

        #endregion

        public event DicomScanProgressCallback Progress;

        public event DicomScanFileFoundCallback FileFound;

        public event DicomScanCompleteCallback Complete;

        #region Public Properties

        public bool ProgressOnDirectoryChange { get; set; }

        public int ProgressFilesCount { get; set; }

        public bool CheckForValidHeader { get; set; }

        #endregion

        #region Public Methods

        public void Start(string directory)
        {
            _stop = false;
            _count = 0;
            Task.Run(() => ScanProc(directory));
        }

        public void Stop()
        {
            _stop = true;
        }

        #endregion

        #region Private Methods

        private void ScanProc(string directory)
        {
            ScanDirectory(directory);
            Complete?.Invoke(this);
        }

        private void ScanDirectory(string path)
        {
            if (_stop) { return; }

            if (Progress != null && ProgressOnDirectoryChange)
            {
                Progress(this, path, _count);
            }

            try
            {
                var directory = new DirectoryReference(path);
                var files = directory.EnumerateFileNames(_pattern);

                foreach (string file in files)
                {
                    if (_stop) { return; }

                    ScanFile(file);

                    _count++;
                    if ((_count % ProgressFilesCount) == 0 && Progress != null)
                    {
                        Progress(this, path, _count);
                    }
                }

                if (!_recursive) { return; }

                var dirs = directory.EnumerateDirectoryNames();
                foreach (string dir in dirs)
                {
                    if (_stop) { return; }

                    ScanDirectory(dir);
                }
            }
            catch
            {
                // ignore exceptions?
            }
        }

        private void ScanFile(string file)
        {
            try
            {
                if (CheckForValidHeader && !DicomFile.HasValidHeader(file)) return;

                var df = DicomFile.Open(file);

                FileFound?.Invoke(this, df, file);
            }
            catch
            {
                // ignore exceptions?
            }
        }

        #endregion
    }
}
