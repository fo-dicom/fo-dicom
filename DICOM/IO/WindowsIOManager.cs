// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.Text;

    /// <summary>
    /// Universal Windows Platform implementation of the I/O manager.
    /// </summary>
    public sealed class WindowsIOManager : IOManager
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the desktop I/O manager.
        /// </summary>
        public static readonly IOManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="WindowsIOManager"/>
        /// </summary>
        static WindowsIOManager()
        {
            Instance = new WindowsIOManager();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Implementation of the base encoding getter.
        /// </summary>
        protected override Encoding BaseEncodingImpl
        {
            get
            {
                return Encoding.ASCII;
            }
        }

        /// <summary>
        /// Gets the platform-specific path helper implementation.
        /// </summary>
        protected override IPath PathImpl
        {
            get
            {
                return DesktopPath.Instance;
            }
        }

        /// <summary>
        /// Platform-specific implementation to create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A file reference object.</returns>
        protected override IFileReference CreateFileReferenceImpl(string fileName)
        {
            return new WindowsFileReference(fileName);
        }

        /// <summary>
        /// Platform-specific implementation to create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        protected override IDirectoryReference CreateDirectoryReferenceImpl(string directoryName)
        {
            return new DesktopDirectoryReference(directoryName);
        }

        #endregion
    }
}
