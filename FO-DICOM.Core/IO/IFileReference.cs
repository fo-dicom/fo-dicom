// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Interface for a factory to create <see cref="IFileReference"/>
    /// </summary>
    public interface IFileReferenceFactory
    {
        IFileReference Create(string fileName);
    }

    /// <summary>
    /// Interface representing reference to a single file.
    /// </summary>
    public interface IFileReference
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets whether the file exist or not.
        /// </summary>
        bool Exists { get; }

        /// <summary>Gets and sets whether the file is temporary or not.</summary>
        bool IsTempFile { get; set; }

        /// <summary>
        /// Gets the directory reference of the file.
        /// </summary>
        IDirectoryReference Directory { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Creates a new file for reading and writing. Overwrites existing file.
        /// </summary>
        /// <returns>Stream to the created file.</returns>
        Stream Create();

        /// <summary>
        /// Open an existing file stream for reading and writing.
        /// </summary>
        /// <returns></returns>
        Stream Open();

        /// <summary>
        /// Open a file stream for reading.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        Stream OpenRead();

        /// <summary>
        /// Open a file stream for writing, creates the file if not existing.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        Stream OpenWrite();

        /// <summary>
        /// Delete the file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Moves file and updates internal reference.
        /// Calling this method will also set the <see cref="IsTempFile"/> property to <c>False</c>.
        /// </summary>
        /// <param name="dstFileName">Full name of the moved file.</param>
        /// <param name="overwrite">True if already existing file should be overwritten, false otherwise.</param>
        void Move(string dstFileName, bool overwrite = false);

        /// <summary>
        /// Gets a sub-range of the bytes in the file.
        /// </summary>
        /// <param name="offset">Offset from the start position of the file.</param>
        /// <param name="count">Number of bytes to select.</param>
        /// <returns>The specified sub-range of bytes in the file.</returns>
        byte[] GetByteRange(long offset, int count);

        /// <summary>
        /// Gets a sub-range of the bytes in the file.
        /// </summary>
        /// <param name="offset">Offset from the start position of the file.</param>
        /// <param name="count">Number of bytes to select.</param>
        /// <param name="output">The output array where the contents will be written </param>
        /// <returns>The specified sub-range of bytes in the file.</returns>
        void GetByteRange(long offset, int count, byte[] output);

        #endregion
    }
}
