// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Interface representing reference to a single directory.
    /// </summary>
    public interface IDirectoryReference
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the path name of the current directory.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets whether the directory exists or not.
        /// </summary>
        bool Exists { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Create the directory path.
        /// </summary>
        void Create();

        /// <summary>
        /// Gets the file names of the files in the current directory.
        /// </summary>
        /// <param name="searchPattern">File search pattern; if null or empty all files in the directory should be returned.</param>
        /// <returns>File names of the files in the current directory.</returns>
        IEnumerable<string> EnumerateFileNames(string searchPattern = null);

        /// <summary>
        /// Gets the names of the sub-directories in the current directory.
        /// </summary>
        /// <returns>Names of the sub-directories in the current directory.</returns>
        IEnumerable<string> EnumerateDirectoryNames();

        #endregion
    }
}
