﻿// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System;
    using System.Collections.Generic;

#if NET35
    using System.Threading;
#else
    using System.Threading.Tasks;
#endif

    /// <summary>
    /// Support class for removing temporary files, with repeated attempts if required.
    /// </summary>
    public class TemporaryFileRemover : IDisposable
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of the temporary file remover.
        /// </summary>
        private static readonly TemporaryFileRemover Instance = new TemporaryFileRemover();

        private readonly object locker = new object();

        private readonly List<IFileReference> files = new List<IFileReference>();

        private bool running;

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
            this.DeleteAllRemainingFiles();
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
            Instance.DeletePrivate(file);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.DeleteAllRemainingFiles();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Final attempt to delete all remaining files.
        /// </summary>
        private void DeleteAllRemainingFiles()
        {
            foreach (var file in this.files)
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
                    lock (this.locker)
                    {
                        this.files.Add(file);
                        if (!this.running)
                        {
                            this.running = true;
#if NET35
                            ThreadPool.QueueUserWorkItem(this.DeleteAll);
#else
                            Task.Run((Action)this.DeleteAll);
#endif
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for repeated file removal attempts.
        /// </summary>
#if NET35
        private void DeleteAll(object dummy)
#else
        private async void DeleteAll()
#endif
        {
            while (true)
            {
                lock (this.locker)
                {
                    foreach (var file in this.files)
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
                    this.files.RemoveAll(file => !file.Exists);

                    if (this.files.Count == 0)
                    {
                        this.running = false;
                        return;
                    }
                }

#if NET35
                Thread.Sleep(1000);
#else
                await Task.Delay(1000).ConfigureAwait(false);
#endif
            }
        }

        #endregion
    }
}
