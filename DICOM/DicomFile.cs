// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Dicom.IO;
    using Dicom.IO.Reader;
    using Dicom.IO.Writer;

    /// <summary>
    /// Representation of one DICOM file.
    /// </summary>
    public partial class DicomFile
    {
        #region CONSTRUCTORS

        public DicomFile()
        {
            FileMetaInfo = new DicomFileMetaInformation();
            Dataset = new DicomDataset();
            Format = DicomFileFormat.DICOM3;
        }

        public DicomFile(DicomDataset dataset)
        {
            Dataset = dataset;
            FileMetaInfo = new DicomFileMetaInformation(Dataset);
            Format = DicomFileFormat.DICOM3;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the file reference of the DICOM file.
        /// </summary>
        public IFileReference File { get; protected set; }

        /// <summary>
        /// Gets the DICOM file format.
        /// </summary>
        public DicomFileFormat Format { get; protected set; }

        /// <summary>
        /// Gets the DICOM file meta information of the file.
        /// </summary>
        public DicomFileMetaInformation FileMetaInfo { get; protected set; }

        /// <summary>
        /// Gets the DICOM dataset of the file.
        /// </summary>
        public DicomDataset Dataset { get; protected set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Method to call before performing the actual saving.
        /// </summary>
        protected virtual void OnSave()
        {
        }

        /// <summary>
        /// Save DICOM file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public void Save(string fileName)
        {
            if (Format == DicomFileFormat.ACRNEMA1 || Format == DicomFileFormat.ACRNEMA2) throw new DicomFileException(this, "Unable to save ACR-NEMA file");

            if (Format == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                // create file meta information from dataset
                FileMetaInfo = new DicomFileMetaInformation(Dataset);
            }

            File = IOManager.CreateFileReference(fileName);
            File.Delete();

            OnSave();

            using (var target = new FileByteTarget(File))
            {
                DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
                writer.Write(target, FileMetaInfo, Dataset);
            }
        }

        /// <summary>
        /// Save DICOM file to stream.
        /// </summary>
        /// <param name="stream">Stream on which to save DICOM file.</param>
        public void Save(Stream stream)
        {
            if (Format == DicomFileFormat.ACRNEMA1 || Format == DicomFileFormat.ACRNEMA2) throw new DicomFileException(this, "Unable to save ACR-NEMA file");

            if (Format == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                // create file meta information from dataset
                FileMetaInfo = new DicomFileMetaInformation(Dataset);
            }

            OnSave();

            var target = new StreamByteTarget(stream);
            DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
            writer.Write(target, FileMetaInfo, Dataset);
        }

        /// <summary>
        /// Save to file asynchronously.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        /// <returns>Awaitable <see cref="Task"/>.</returns>
        public async Task SaveAsync(string fileName)
        {
            if (this.Format == DicomFileFormat.ACRNEMA1 || this.Format == DicomFileFormat.ACRNEMA2)
            {
                throw new DicomFileException(this, "Unable to save ACR-NEMA file");
            }

            if (this.Format == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                // create file meta information from dataset
                this.FileMetaInfo = new DicomFileMetaInformation(this.Dataset);
            }

            this.File = IOManager.CreateFileReference(fileName);
            this.File.Delete();

            this.OnSave();

            using (var target = new FileByteTarget(this.File))
            {
                var writer = new DicomFileWriter(DicomWriteOptions.Default);
                await writer.WriteAsync(target, this.FileMetaInfo, this.Dataset).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <returns>DicomFile instance</returns>
        public static DicomFile Open(string fileName)
        {
            return Open(fileName, DicomEncoding.Default);
        }

        /// <summary>
        /// Reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <returns>DicomFile instance</returns>
        public static DicomFile Open(string fileName, Encoding fallbackEncoding)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException("fallbackEncoding");
            }
            DicomFile df = new DicomFile();

            try
            {
                df.File = IOManager.CreateFileReference(fileName);

                using (var source = new FileByteSource(df.File))
                {
                    DicomFileReader reader = new DicomFileReader();
                    reader.Read(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding));

                    df.Format = reader.FileFormat;

                    df.Dataset.InternalTransferSyntax = reader.Syntax;

                    return df;
                }
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns>Read <see cref="DicomFile"/>.</returns>
        public static DicomFile Open(Stream stream)
        {
            return Open(stream, DicomEncoding.Default);
        }

        /// <summary>
        /// Read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to use if encoding cannot be obtained from DICOM file.</param>
        /// <returns>Read <see cref="DicomFile"/>.</returns>
        public static DicomFile Open(Stream stream, Encoding fallbackEncoding)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException("fallbackEncoding");
            }
            var df = new DicomFile();

            try
            {
                var source = new StreamByteSource(stream);

                var reader = new DicomFileReader();
                reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding));

                df.Format = reader.FileFormat;

                df.Dataset.InternalTransferSyntax = reader.Syntax;

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Asynchronously reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static Task<DicomFile> OpenAsync(string fileName)
        {
            return OpenAsync(fileName, DicomEncoding.Default);
        }

        /// <summary>
        /// Asynchronously reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static Task<DicomFile> OpenAsync(string fileName, Encoding fallbackEncoding)
        {
            return Task.Run(() => Open(fileName, fallbackEncoding));
        }

        /// <summary>
        /// Asynchronously read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static Task<DicomFile> OpenAsync(Stream stream)
        {
            return OpenAsync(stream, DicomEncoding.Default);
        }

        /// <summary>
        /// Asynchronously read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to use if encoding cannot be obtained from DICOM file.</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static Task<DicomFile> OpenAsync(Stream stream, Encoding fallbackEncoding)
        {
            return Task.Run(() => Open(stream, fallbackEncoding));
        }

        /// <summary>
        /// Test if file has a valid preamble and DICOM 3.0 header.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>True if valid DICOM 3.0 file header is detected.</returns>
        public static bool HasValidHeader(string path)
        {
            try
            {
                var file = IOManager.CreateFileReference(path);
                using (var fs = file.OpenRead())
                {
                    fs.Seek(128, SeekOrigin.Begin);
                    return fs.ReadByte() == 'D' && fs.ReadByte() == 'I' && fs.ReadByte() == 'C' && fs.ReadByte() == 'M';
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("DICOM File [{0}]", this.Format);
        }

        #endregion
    }
}
