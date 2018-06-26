// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;

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
#if NETSTANDARD
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            Instance = new DesktopIOManager();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Implementation of the base encoding getter.
        /// </summary>
        protected override Encoding BaseEncodingImpl => Encoding.ASCII;

        /// <summary>
        /// Gets the platform-specific path helper implementation.
        /// </summary>
        protected override IPath PathImpl => DesktopPath.Instance;

        /// <summary>
        /// Platform-specific implementation to create a file reference.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A file reference object.</returns>
        protected override IFileReference CreateFileReferenceImpl(string fileName)
        {
            return new DesktopFileReference(fileName);
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
