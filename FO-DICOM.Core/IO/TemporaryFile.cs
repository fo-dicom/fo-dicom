// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Support class for creating a temporary file.
    /// </summary>
    public static class TemporaryFile
    {
        #region FIELDS

        private static string _storagePath;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the directory location of the temporary files.
        /// </summary>
        public static string StoragePath
        {
            get => _storagePath ?? Path.GetTempPath();
            set
            {
                _storagePath = value;
                if (_storagePath != null)
                {
                    var directory = new DirectoryReference(_storagePath);
                    if (!directory.Exists)
                    {
                        directory.Create();
                    }
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

            if (_storagePath != null)
            {
                // create file in user specified path
                var path = Path.Combine(_storagePath, Guid.NewGuid().ToString());
                file = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(path);
                file.Create().Dispose();
            }
            else
            {
                // allow OS to create file in system temp path
                file = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
                file.Create().Dispose();
            }
            file.IsTempFile = true;

            return file;
        }

        #endregion
    }
}
