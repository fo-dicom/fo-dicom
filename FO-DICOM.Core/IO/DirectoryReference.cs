// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Implementation of the <see cref="IDirectoryReference"/> interface.
    /// </summary>
    public class DirectoryReference : IDirectoryReference
    {

        #region FIELDS

        private readonly DirectoryInfo _directoryInfo;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="DirectoryReference"/> object.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public DirectoryReference(string directoryName)
        {
            _directoryInfo = new DirectoryInfo(directoryName);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Path name of the current directory.
        /// </summary>
        public string Name => _directoryInfo.FullName;

        /// <summary>
        /// Gets whether the directory exists or not.
        /// </summary>
        public bool Exists => _directoryInfo.Exists;

        /// <summary>
        /// Create the directory path.
        /// </summary>
        public void Create()
        {
            _directoryInfo.Create();
        }

        /// <summary>
        /// Gets the file names of the files in the current directory.
        /// </summary>
        /// <param name="searchPattern">File search pattern; if null or empty all files in the directory should be returned.</param>
        /// <returns>File names of the files in the current directory.</returns>
        public IEnumerable<string> EnumerateFileNames(string searchPattern = null) 
            => string.IsNullOrEmpty(searchPattern?.Trim())
                       ? _directoryInfo.GetFiles().Select(fi => fi.FullName)
                       : _directoryInfo.GetFiles(searchPattern).Select(fi => fi.FullName);

        /// <summary>
        /// Gets the names of the sub-directories in the current directory.
        /// </summary>
        /// <returns>Names of the sub-directories in the current directory.</returns>
        public IEnumerable<string> EnumerateDirectoryNames() 
            => _directoryInfo.EnumerateDirectories().Select(di => di.FullName);

        #endregion
    }
}
