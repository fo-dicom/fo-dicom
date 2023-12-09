// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FellowOakDicom.IO
{

    /// <summary>
    /// Support class for removing temporary files, with repeated attempts if required.
    /// </summary>
    public class TemporaryFileRemover : IDisposable
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of the temporary file remover.
        /// </summary>
        private static readonly TemporaryFileRemover _instance = new TemporaryFileRemover();

        private readonly object _locker = new object();

        private readonly List<IFileReference> _files = new List<IFileReference>();

        private Task _running;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="TemporaryFileRemover"/> class.
        /// </summary>
        /// <remarks>Private constructor since only a singleton instance of the class is required.</remarks>
        private TemporaryFileRemover()
        {
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~TemporaryFileRemover()
        {
            DeleteAllRemainingFiles();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Delete the specified temporary file.
        /// </summary>
        /// <param name="file"></param>
        public static void Delete(IFileReference file)
        {
            if (!file.IsTempFile)
            {
                throw new DicomIoException("Only temporary files should be removed through this operation.");
            }
            _instance.DeletePrivate(file);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DeleteAllRemainingFiles();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Final attempt to delete all remaining files.
        /// </summary>
        private void DeleteAllRemainingFiles()
        {
            foreach (var file in _files)
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                    // Deletion failed, do nothing.
                }
            }
        }

        /// <summary>
        /// Delete single file. If removal fails, place the file on queue for re-attempted removal.
        /// </summary>
        /// <param name="file">File name.</param>
        private void DeletePrivate(IFileReference file)
        {
            try
            {
                if (file.Exists) file.Delete();
            }
            catch
            {
                if (file.Exists)
                {
                    lock (_locker)
                    {
                        _files.Add(file);
                        if (_running == null || _running.IsCompleted)
                        { 
                            _running = DeleteAllAsync();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for repeated file removal attempts.
        /// </summary>
        private async Task DeleteAllAsync()
        {
            while (true)
            {
                lock (_locker)
                {
                    foreach (var file in _files)
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            // Just ignore if deletion fails.
                        }
                    }
                    _files.RemoveAll(file => !file.Exists);

                    if (_files.Count == 0)
                    {
                        break;
                    }
                }

                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
