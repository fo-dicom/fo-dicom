// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    /// <summary>
    /// .NET/Windows Desktop implementation of the I/O manager.
    /// </summary>
    public sealed class DesktopIOManager : IOManager
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the desktop I/O manager.
        /// </summary>
        public static readonly IOManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="DesktopIOManager"/>
        /// </summary>
        static DesktopIOManager()
        {
            Instance = new DesktopIOManager();
        }

        /// <summary>
        /// Initializes a <see cref="DesktopIOManager"/> object.
        /// </summary>
        private DesktopIOManager()
        {
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isTempFile">Indicates whether the file should be handled as a temporary file or not.</param>
        /// <returns>A file reference object.</returns>
        public override IFileReference CreateFileReference(string fileName, bool isTempFile = false)
        {
            return new FileReference(fileName, isTempFile);
        }

        /// <summary>
        /// Create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        public override IDirectoryReference CreateDirectoryReference(string directoryName)
        {
            return new DesktopDirectoryReference(directoryName);
        }

        #endregion
    }
}