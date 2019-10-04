// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// .NET/Windows Desktop implementation of the <see cref="IPath"/> interface.
    /// </summary>
    public class DesktopPath : IPath
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the <see cref="DesktopPath"/> class.
        /// </summary>
        public static readonly IPath Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static members of <see cref="DesktopPath"/>.
        /// </summary>
        static DesktopPath()
        {
            Instance = new DesktopPath();
        }

        /// <summary>
        /// Initializes a <see cref="DesktopPath"/> object.
        /// </summary>
        private DesktopPath()
        {
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>
        /// Directory information for path, or null if path denotes a root directory or is null. 
        /// Returns <see cref="string.Empty"/> if path does not contain directory information.
        /// </returns>
        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Returns the path of the current user's temporary folder.
        /// </summary>
        /// <returns>The path to the temporary folder, ending with a backslash.</returns>
        public string GetTempDirectory()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
        /// </summary>
        /// <returns>The full path of the temporary file.</returns>
        public string GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        /// <summary>
        /// Combines an array of strings into a path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The combined paths.</returns>
        public string Combine(params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

#if NET35
            var combined = string.Empty;
            foreach (var path in paths)
            {
                combined = Path.Combine(combined, path);
            }

            return combined;
#else
            return Path.Combine(paths);
#endif
        }

        #endregion
    }
}
