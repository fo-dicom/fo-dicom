// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    /// <summary>
    /// .NET/Windows Desktop implementation of the <see cref="IFileReference"/> interface.
    /// </summary>
    public sealed class DesktopFileReference : IFileReference
    {
        private bool _isTempFile;

        /// <summary>
        /// Initializes a <see cref="DesktopFileReference"/> object.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public DesktopFileReference(string fileName)
        {
            Name = fileName;
            IsTempFile = false;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DesktopFileReference()
        {
            if (IsTempFile)
            {
                TemporaryFileRemover.Delete(this);
            }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets whether the file exist or not.
        /// </summary>
        public bool Exists => File.Exists(Name);

        /// <summary>Gets and sets whether the file is temporary or not.</summary>
        /// <remarks>Temporary file will be deleted when object is <c>Disposed</c>.</remarks>
        public bool IsTempFile
        {
            get => _isTempFile;
            set
            {
                if (value && this.Exists)
                {
                    try
                    {
                        // set temporary file attribute so that the file system
                        // will attempt to keep all of the data in memory
                        File.SetAttributes(Name, File.GetAttributes(Name) | FileAttributes.Temporary);
                    }
                    catch
                    {
                        // sometimes fails with invalid argument exception
                    }
                }

                _isTempFile = value;
            }
        }

        /// <summary>
        /// Gets the directory reference of the file.
        /// </summary>
        public IDirectoryReference Directory => new DesktopDirectoryReference(Path.GetDirectoryName(Name));

        /// <summary>
        /// Creates a new file for reading and writing. Overwrites existing file.
        /// </summary>
        /// <returns>Stream to the created file.</returns>
        public Stream Create()
        {
            return File.Create(Name);
        }

        /// <summary>
        /// Open an existing file stream for reading and writing.
        /// </summary>
        /// <returns></returns>
        public Stream Open()
        {
            return File.Open(Name, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Open a file stream for reading.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        public Stream OpenRead()
        {
            return File.OpenRead(Name);
        }

        /// <summary>
        /// Open a file stream for writing, creates the file if not existing.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        public Stream OpenWrite()
        {
            return File.OpenWrite(Name);
        }

        /// <summary>
        /// Delete the file.
        /// </summary>
        public void Delete()
        {
            File.Delete(Name);
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
            // delete if overwriting; let File.Move thow IOException if not
            if (File.Exists(dstFileName) && overwrite) File.Delete(dstFileName);

            File.Move(Name, dstFileName);
            Name = Path.GetFullPath(dstFileName);
            IsTempFile = false;
        }

        /// <summary>
        /// Gets a sub-range of the bytes in the file.
        /// </summary>
        /// <param name="offset">Offset from the start position of the file.</param>
        /// <param name="count">Number of bytes to select.</param>
        /// <returns>The specified sub-range of bytes in the file.</returns>
        public byte[] GetByteRange(long offset, int count)
        {
            byte[] buffer = new byte[count];

            using (var fs = OpenRead())
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buffer, 0, count);
            }

            return buffer;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return IsTempFile ? $"{Name} [TEMP]" : Name;
        }
    }
}
