// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// .NET/Windows Desktop implementation of the <see cref="IDirectoryReference"/> interface.
    /// </summary>
    public class DesktopDirectoryReference : IDirectoryReference
    {
        #region FIELDS

        private readonly DirectoryInfo directoryInfo;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="DesktopDirectoryReference"/> object.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public DesktopDirectoryReference(string directoryName)
        {
            this.directoryInfo = new DirectoryInfo(directoryName);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Path name of the current directory.
        /// </summary>
        public string Name
        {
            get
            {
                return this.directoryInfo.FullName;
            }
        }

        /// <summary>
        /// Gets whether the directory exists or not.
        /// </summary>
        public bool Exists
        {
            get
            {
                return this.directoryInfo.Exists;
            }
        }

        /// <summary>
        /// Create the directory path.
        /// </summary>
        public void Create()
        {
            this.directoryInfo.Create();
        }

        /// <summary>
        /// Gets the file names of the files in the current directory.
        /// </summary>
        /// <param name="searchPattern">File search pattern; if null or empty all files in the directory should be returned.</param>
        /// <returns>File names of the files in the current directory.</returns>
        public IEnumerable<string> EnumerateFileNames(string searchPattern = null)
        {
            return string.IsNullOrEmpty(searchPattern?.Trim())
                       ? this.directoryInfo.GetFiles().Select(fi => fi.FullName)
                       : this.directoryInfo.GetFiles(searchPattern).Select(fi => fi.FullName);
        }

        /// <summary>
        /// Gets the names of the sub-directories in the current directory.
        /// </summary>
        /// <returns>Names of the sub-directories in the current directory.</returns>
        public IEnumerable<string> EnumerateDirectoryNames()
        {
            return this.directoryInfo.EnumerateDirectories().Select(di => di.FullName);
        }

        #endregion
    }
}
