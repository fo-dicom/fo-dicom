// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Storage.Streams;

    /// <summary>
    /// Windows Universal Platform implementation of the <see cref="IFileReference"/> interface.
    /// </summary>
    public sealed class WindowsFileReference : IFileReference
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="WindowsFileReference"/> object.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public WindowsFileReference(string fileName)
        {
            this.Name = Path.GetFullPath(fileName);
            this.IsTempFile = false;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~WindowsFileReference()
        {
            if (this.IsTempFile)
            {
                TemporaryFileRemover.Delete(this);
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets whether the file exist or not.
        /// </summary>
        public bool Exists
        {
            get
            {
                return IsFileExistingAsync(this.Name).Result;
            }
        }

        /// <summary>Gets and sets whether the file is temporary or not.</summary>
        /// <remarks>Temporary file will be deleted when object is <c>Disposed</c>.</remarks>
        public bool IsTempFile { get; set; }

        /// <summary>
        /// Gets the directory reference of the file.
        /// </summary>
        public IDirectoryReference Directory
        {
            get
            {
                return new DesktopDirectoryReference(Path.GetDirectoryName(this.Name));
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Creates a new file for reading and writing. Overwrites existing file.
        /// </summary>
        /// <returns>Stream to the created file.</returns>
        public Stream Create()
        {
            return CreateAsync(this.Name).Result;
        }

        /// <summary>
        /// Open an existing file stream for reading and writing.
        /// </summary>
        /// <returns>Read/write stream to the opened file.</returns>
        public Stream Open()
        {
            return OpenExistingAsync(this.Name, true, true).Result;
        }

        /// <summary>
        /// Open a file stream for reading.
        /// </summary>
        /// <returns>Readable stream to the opened file.</returns>
        public Stream OpenRead()
        {
            return OpenExistingAsync(this.Name, true, false).Result;
        }

        /// <summary>
        /// Open a file stream for writing, creates the file if not existing.
        /// </summary>
        /// <returns>Writeable stream to the opened file.</returns>
        public Stream OpenWrite()
        {
            return OpenOrCreateAsync(this.Name).Result;
        }

        /// <summary>
        /// Delete the file.
        /// </summary>
        public void Delete()
        {
            DeleteAsync(this.Name).Wait();
        }

        /// <summary>
        /// Moves file and updates internal reference.
        /// 
        /// Calling this method will also remove set the <see cref="IsTempFile"/> property to <c>False</c>.
        /// </summary>
        /// <param name="dstFileName">Name of the destination file.</param>
        /// <param name="overwrite">True if <paramref name="dstFileName"/> should be overwritten if it already exists, false otherwise.</param>
        public void Move(string dstFileName, bool overwrite = false)
        {
            MoveAsync(this.Name, dstFileName, overwrite).Wait();

            this.Name = Path.GetFullPath(dstFileName);
            this.IsTempFile = false;
        }

        /// <summary>
        /// Gets a sub-range of the bytes in the file.
        /// </summary>
        /// <param name="offset">Offset from the start position of the file.</param>
        /// <param name="count">Number of bytes to select.</param>
        /// <returns>The specified sub-range of bytes in the file.</returns>
        public byte[] GetByteRange(int offset, int count)
        {
            return GetByteRangeAsync(this.Name, offset, count).Result;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.IsTempFile ? string.Format("{0} [TEMP]", this.Name) : this.Name;
        }

        private static async Task<bool> IsFileExistingAsync(string path)
        {
            try
            {
                var folderName = Path.GetDirectoryName(path);
                var folder = await StorageFolder.GetFolderFromPathAsync(folderName).AsTask().ConfigureAwait(false);

                var fileName = Path.GetFileName(path);
                var file = await folder.GetFileAsync(fileName).AsTask().ConfigureAwait(false);
                return file != null;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        private static async Task<Stream> CreateAsync(string path)
        {
            var folder = await GetFolderAsync(path).ConfigureAwait(false);

            var fileName = Path.GetFileName(path);
            var file =
                await
                folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).AsTask().ConfigureAwait(false);

            return (await file.OpenAsync(FileAccessMode.ReadWrite).AsTask().ConfigureAwait(false)).AsStream();
        }

        private static async Task<StorageFolder> GetFolderAsync(string path)
        {
            var folderName = Path.GetDirectoryName(path);

            StorageFolder folder;
            try
            {
                folder = await StorageFolder.GetFolderFromPathAsync(folderName).AsTask().ConfigureAwait(false);
            }
            catch (FileNotFoundException)
            {
                await Task.Run(() => System.IO.Directory.CreateDirectory(folderName)).ConfigureAwait(false);
                folder = await StorageFolder.GetFolderFromPathAsync(folderName).AsTask().ConfigureAwait(false);
            }

            return folder;
        }

        private static async Task<Stream> OpenExistingAsync(string path, bool read, bool write)
        {
            var file = await StorageFile.GetFileFromPathAsync(path).AsTask().ConfigureAwait(false);

            if (read && write)
            {
                return (await file.OpenAsync(FileAccessMode.ReadWrite).AsTask().ConfigureAwait(false)).AsStream();
            }
            if (read)
            {
                return await file.OpenStreamForReadAsync().ConfigureAwait(false);
            }
            if (write)
            {
                return await file.OpenStreamForWriteAsync().ConfigureAwait(false);
            }

            throw new ArgumentException("Return stream should be read, write or read/write");
        }

        private static async Task<Stream> OpenOrCreateAsync(string path)
        {
            var folder = await GetFolderAsync(path).ConfigureAwait(false);

            var fileName = Path.GetFileName(path);
            var file =
                await
                folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);

            return await file.OpenStreamForWriteAsync().ConfigureAwait(false);
        }

        private static async Task DeleteAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path).AsTask().ConfigureAwait(false);
            await file.DeleteAsync().AsTask().ConfigureAwait(false);
        }

        private static async Task MoveAsync(string path, string destFileName, bool overwrite)
        {
            var destPath = Path.GetFullPath(destFileName);
            var destFolder = await GetFolderAsync(destPath).ConfigureAwait(false);
            var destName = Path.GetFileName(destPath);

            var file = await StorageFile.GetFileFromPathAsync(path).AsTask().ConfigureAwait(false);
            await
                file.MoveAsync(
                    destFolder,
                    destName,
                    overwrite ? NameCollisionOption.ReplaceExisting : NameCollisionOption.FailIfExists);
        }

        private static async Task<byte[]> GetByteRangeAsync(string path, int offset, int count)
        {
            var buffer = new Windows.Storage.Streams.Buffer((uint)count);

            var file = await StorageFile.GetFileFromPathAsync(path).AsTask().ConfigureAwait(false);
            using (var fs = await file.OpenAsync(FileAccessMode.Read).AsTask().ConfigureAwait(false))
            {
                fs.Seek((ulong)offset);
                await fs.ReadAsync(buffer, (uint)count, InputStreamOptions.None).AsTask().ConfigureAwait(false);
                return buffer.ToArray();
            }
        }

        #endregion
    }
}
