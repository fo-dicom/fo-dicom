// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Interface for convenience methods associated with file or directory paths.
    /// </summary>
    public interface IPath
    {
        #region METHODS

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>
        /// Directory information for path, or null if path denotes a root directory or is null. 
        /// Returns <see cref="string.Empty"/> if path does not contain directory information.
        /// </returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// Returns the path of the current user's temporary folder.
        /// </summary>
        /// <returns>The path to the temporary folder, ending with a backslash.</returns>
        string GetTempDirectory();

        /// <summary>
        /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
        /// </summary>
        /// <returns>The full path of the temporary file.</returns>
        string GetTempFileName();

        /// <summary>
        /// Combines an array of strings into a path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The combined paths.</returns>
        string Combine(params string[] paths);

        #endregion
    }
}
