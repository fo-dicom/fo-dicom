// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;

namespace FellowOakDicom.IO
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
        /// Initializes the single platform specific I/O manager.
        /// </summary>
        static IOManager()
        {
            SetImplementation(Setup.GetSinglePlatformInstance<IOManager>());
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the base encoding for the current platform.
        /// </summary>
        public static Encoding BaseEncoding
        {
            get
            {
                return implementation.BaseEncodingImpl;
            }
        }

        /// <summary>
        /// Implementation of the base encoding getter.
        /// </summary>
        protected  abstract Encoding BaseEncodingImpl { get; }

        /// <summary>
        /// Gets the path helper implementation.
        /// </summary>
        public static IPath Path
        {
            get
            {
                return implementation.PathImpl;
            }
        }

        /// <summary>
        /// Gets the platform-specific path helper implementation.
        /// </summary>
        protected abstract IPath PathImpl { get; }

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
        /// <returns>A file reference object.</returns>
        public static IFileReference CreateFileReference(string fileName)
        {
            return implementation.CreateFileReferenceImpl(fileName);
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
        /// <returns>A file reference object.</returns>
        protected abstract IFileReference CreateFileReferenceImpl(string fileName);

        /// <summary>
        /// Platform-specific implementation to create a directory reference.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>A directory reference object.</returns>
        protected abstract IDirectoryReference CreateDirectoryReferenceImpl(string directoryName);

        #endregion
    }
}
