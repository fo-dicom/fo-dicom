// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;

namespace Dicom.IO
{
    public class TemporaryFile : IDisposable
    {
        private string _file;

        public TemporaryFile()
        {
            _file = Create();
        }

        ~TemporaryFile()
        {
            TemporaryFileRemover.Delete(_file);
        }

        public void Dispose()
        {
            TemporaryFileRemover.Delete(_file);
            GC.SuppressFinalize(this);
        }

        public string Name
        {
            get
            {
                return _file;
            }
        }

        #region Static

        private static string _path = null;

        public static string StoragePath
        {
            get
            {
                if (_path != null) return _path;
                return IOManager.Path.GetTempDirectory();
            }
            set
            {
                _path = value;
                if (_path != null)
                {
                    var directory = IOManager.CreateDirectoryReference(_path);
                    if (!directory.Exists) directory.Create();
                }
            }
        }

        public static string Create()
        {
            string path = null;

            if (_path != null)
            {
                // create file in user specified path
                path = IOManager.Path.Combine(_path, Guid.NewGuid().ToString());
                File.Create(path).Close();
            }
            else
            {
                // allow OS to create file in system temp path
                path = IOManager.Path.GetTempFileName();
            }

            try
            {
                // set temporary file attribute so that the file system
                // will attempt to keep all of the data in memory
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Temporary);
            }
            catch
            {
                // sometimes fails with invalid argument exception
            }

            return path;
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{0} [TEMP]", Name);
        }
    }
}
