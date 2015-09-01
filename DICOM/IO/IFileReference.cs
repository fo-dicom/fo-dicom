// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    /// <summary>
    /// Interface representing reference to a single file.
    /// </summary>
    public interface IFileReference
    {
        /// <summary>
        /// Gets the file name.
        /// </summary>
        string Name { get; }

        /// <summary>Gets and sets whether the file is temporary or not.</summary>
        bool IsTempFile { get; set; }

        /// <summary>
        /// Open a file stream for reading.
        /// </summary>
        /// <returns></returns>
        Stream OpenRead();

        /// <summary>
        /// Open a file stream for writing.
        /// </summary>
        /// <returns></returns>
        Stream OpenWrite();

        /// <summary>
        /// Delete the file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Moves file and updates internal reference.
        /// Calling this method will also set the <see cref="DesktopFileReference.IsTempFile"/> property to <c>False</c>.
        /// </summary>
        /// <param name="dstFileName">Full name of the moved file.</param>
        /// <param name="overwrite">True if already existing file should be overwritten, false otherwise.</param>
        void Move(string dstFileName, bool overwrite = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        byte[] GetByteRange(int offset, int count);
    }
}