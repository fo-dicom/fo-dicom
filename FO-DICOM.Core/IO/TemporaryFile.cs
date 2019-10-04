// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System;

    /// <summary>
    /// Support class for creating a temporary file.
    /// </summary>
    public static class TemporaryFile
    {
        #region FIELDS

        private static string storagePath;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the directory location of the temporary files.
        /// </summary>
        public static string StoragePath
        {
            get
            {
                if (storagePath != null) return storagePath;
                return IOManager.Path.GetTempDirectory();
            }
            set
            {
                storagePath = value;
                if (storagePath != null)
                {
                    var directory = IOManager.CreateDirectoryReference(storagePath);
                    if (!directory.Exists) directory.Create();
                }
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Creates a temporary file and returns its name.
        /// </summary>
        /// <returns>Name of the temporary file.</returns>
        public static IFileReference Create()
        {
            IFileReference file;

            if (storagePath != null)
            {
                // create file in user specified path
                var path = IOManager.Path.Combine(storagePath, Guid.NewGuid().ToString());
                file = IOManager.CreateFileReference(path);
                file.Create().Dispose();
            }
            else
            {
                // allow OS to create file in system temp path
                file = IOManager.CreateFileReference(IOManager.Path.GetTempFileName());
            }
            file.IsTempFile = true;

            return file;
        }

        #endregion
    }
}
