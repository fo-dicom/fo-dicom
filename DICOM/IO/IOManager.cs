// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    /// <summary>
    /// Abstract manager class for file and directory based I/O.
    /// </summary>
    public abstract class IOManager
    {
        #region FIELDS

        private static IOManager implementation;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static members of <see cref="IOManager"/>.
        /// </summary>
        static IOManager()
        {
            SetImplementation(DesktopIOManager.Instance);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Set I/O manager implementation to use for file/directory reference initialization.
        /// </summary>
        /// <param name="impl">I/O manager implementation to use.</param>
        public static void SetImplementation(IOManager impl)
        {
            implementation = impl;
        }

        /// <summary>
        /// Create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isTempFile">Indicates whether the file should be handled as a temporary file or not.</param>
        /// <returns>A file reference object.</returns>
        public static IFileReference CreateFileReference(string fileName, bool isTempFile = false)
        {
            return implementation.CreateFileReferenceImpl(fileName, isTempFile);
        }

        /// <summary>
        /// Create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        public static IDirectoryReference CreateDirectoryReference(string directoryName)
        {
            return implementation.CreateDirectoryReferenceImpl(directoryName);
        }

        /// <summary>
        /// Platform-specific implementation to create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isTempFile">Indicates whether the file should be handled as a temporary file or not.</param>
        /// <returns>A file reference object.</returns>
        protected abstract IFileReference CreateFileReferenceImpl(string fileName, bool isTempFile = false);

        /// <summary>
        /// Platform-specific implementation to create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        protected abstract IDirectoryReference CreateDirectoryReferenceImpl(string directoryName);

        #endregion
    }
}