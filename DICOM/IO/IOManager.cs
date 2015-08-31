// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    /// <summary>
    /// Abstract manager class for file and directory based I/O.
    /// </summary>
    public abstract class IOManager
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static members of <see cref="IOManager"/>
        /// </summary>
        static IOManager()
        {
            Default = DesktopIOManager.Instance;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the IOManager implementation type.
        /// </summary>
        public static IOManager Default { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isTempFile">Indicates whether the file should be handled as a temporary file or not.</param>
        /// <returns>A file reference object.</returns>
        public abstract IFileReference CreateFileReference(string fileName, bool isTempFile = false);

        /// <summary>
        /// Create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        public abstract IDirectoryReference CreateDirectoryReference(string directoryName);

        #endregion
    }
}