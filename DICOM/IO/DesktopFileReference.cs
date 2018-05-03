// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    /// <summary>
    /// .NET/Windows Desktop implementation of the <see cref="IFileReference"/> interface.
    /// </summary>
    public sealed class DesktopFileReference : IFileReference
    {
        private bool isTempFile;

        /// <summary>
        /// Initializes a <see cref="DesktopFileReference"/> object.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public DesktopFileReference(string fileName)
        {
            this.Name = fileName;
            this.IsTempFile = false;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DesktopFileReference()
        {
            if (this.IsTempFile)
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
        public bool Exists
        {
            get
            {
                return File.Exists(this.Name);
            }
        }

        /// <summary>Gets and sets whether the file is temporary or not.</summary>
        /// <remarks>Temporary file will be deleted when object is <c>Disposed</c>.</remarks>
        public bool IsTempFile
        {
            get
            {
                return this.isTempFile;
            }
            set
            {
                if (value && this.Exists)
                {
                    try
                    {
                        // set temporary file attribute so that the file system
                        // will attempt to keep all of the data in memory
                        File.SetAttributes(this.Name, File.GetAttributes(this.Name) | FileAttributes.Temporary);
                    }
                    catch
                    {
                        // sometimes fails with invalid argument exception
                    }
                }

                this.isTempFile = value;
            }
        }

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

        /// <summary>
        /// Creates a new file for reading and writing. Overwrites existing file.
        /// </summary>
        /// <returns>Stream to the created file.</returns>
        public Stream Create()
        {
            return File.Create(this.Name);
        }

        /// <summary>
        /// Open an existing file stream for reading and writing.
        /// </summary>
        /// <returns></returns>
        public Stream Open()
        {
            return File.Open(this.Name, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Open a file stream for reading.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        public Stream OpenRead()
        {
            return File.OpenRead(this.Name);
        }

        /// <summary>
        /// Open a file stream for writing, creates the file if not existing.
        /// </summary>
        /// <returns>Stream to the opened file.</returns>
        public Stream OpenWrite()
        {
            return File.OpenWrite(this.Name);
        }

        /// <summary>
        /// Delete the file.
        /// </summary>
        public void Delete()
        {
            File.Delete(this.Name);
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

            File.Move(this.Name, dstFileName);
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
            byte[] buffer = new byte[count];

            using (var fs = this.OpenRead())
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
            return this.IsTempFile ? string.Format("{0} [TEMP]", this.Name) : this.Name;
        }
    }
}
