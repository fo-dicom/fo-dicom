// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.IO;
    using System.Text;

#if !NET35
    using System.Threading.Tasks;
#endif

    using Dicom.IO;
    using Dicom.IO.Reader;
    using Dicom.IO.Writer;

    /// <summary>
    /// Container class for DICOM file parsing states.
    /// </summary>
    public sealed class ParseState
    {
        #region PROPERTIES

        /// <summary>
        /// Gets or sets the DICOM tag associated with the parse state.
        /// </summary>
        public DicomTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the sequence depth (zero-based) associated with the parse state.
        /// </summary>
        public int SequenceDepth { get; set; }

        #endregion
    }

    /// <summary>
    /// Representation of one DICOM file.
    /// </summary>
    public class DicomFile
    {
        #region CONSTRUCTORS

        public DicomFile()
        {
            FileMetaInfo = new DicomFileMetaInformation();
            Dataset = new DicomDataset();
            Format = DicomFileFormat.DICOM3;
            IsPartial = false;
        }

        public DicomFile(DicomDataset dataset)
        {
            Dataset = dataset;
            FileMetaInfo = new DicomFileMetaInformation(Dataset);
            Format = DicomFileFormat.DICOM3;
            IsPartial = false;
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

        /// <summary>
        /// Gets whether the parsing of the file ended prematurely.
        /// </summary>
        public bool IsPartial { get; protected set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Save DICOM file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="options">Options to apply during writing.</param>
        public void Save(string fileName, DicomWriteOptions options = null)
        {
            this.PreprocessFileMetaInformation();

            this.File = IOManager.CreateFileReference(fileName);
            this.File.Delete();

            this.OnSave();

            using (var target = new FileByteTarget(this.File))
            {
                var writer = new DicomFileWriter(options);
                writer.Write(target, this.FileMetaInfo, this.Dataset);
            }
        }

        /// <summary>
        /// Save DICOM file to stream.
        /// </summary>
        /// <param name="stream">Stream on which to save DICOM file.</param>
        /// <param name="options">Options to apply during writing.</param>
        public void Save(Stream stream, DicomWriteOptions options = null)
        {
            this.PreprocessFileMetaInformation();
            this.OnSave();

            var target = new StreamByteTarget(stream);
            var writer = new DicomFileWriter(options);
            writer.Write(target, this.FileMetaInfo, this.Dataset);
        }

#if !NET35
        /// <summary>
        /// Save to file asynchronously.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        /// <param name="options">Options to apply during writing.</param>
        /// <returns>Awaitable <see cref="Task"/>.</returns>
        public async Task SaveAsync(string fileName, DicomWriteOptions options = null)
        {
            this.PreprocessFileMetaInformation();

            this.File = IOManager.CreateFileReference(fileName);
            this.File.Delete();

            this.OnSave();

            using (var target = new FileByteTarget(this.File))
            {
                var writer = new DicomFileWriter(options);
                await writer.WriteAsync(target, this.FileMetaInfo, this.Dataset).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Asynchronously save DICOM file to stream.
        /// </summary>
        /// <param name="stream">Stream on which to save DICOM file.</param>
        /// <param name="options">Options to apply during writing.</param>
        /// <returns>Awaitable task.</returns>
        public async Task SaveAsync(Stream stream, DicomWriteOptions options = null)
        {
            this.PreprocessFileMetaInformation();
            this.OnSave();

            var target = new StreamByteTarget(stream);
            var writer = new DicomFileWriter(options);
            await writer.WriteAsync(target, this.FileMetaInfo, this.Dataset).ConfigureAwait(false);
        }
#endif

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
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <returns>DicomFile instance</returns>
        public static DicomFile Open(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null)
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
                    var reader = new DicomFileReader();
                    var result = reader.Read(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        stop);

                    if (result == DicomReaderResult.Processing)
                    {
                        throw new DicomFileException(df, "Invalid read return state: {state}", result);
                    }
                    if (result == DicomReaderResult.Error)
                    {
                        return null;
                    }
                    df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;

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
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <returns>Read <see cref="DicomFile"/>.</returns>
        public static DicomFile Open(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null)
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
                var result = reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                    stop);

                if (result == DicomReaderResult.Processing)
                {
                    throw new DicomFileException(df, "Invalid read return state: {state}", result);
                }
                if (result == DicomReaderResult.Error)
                {
                    return null;
                }
                df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;

                df.Format = reader.FileFormat;

                df.Dataset.InternalTransferSyntax = reader.Syntax;

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

#if !NET35
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
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static async Task<DicomFile> OpenAsync(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException("fallbackEncoding");
            }
            var df = new DicomFile();

            try
            {
                df.File = IOManager.CreateFileReference(fileName);

                using (var source = new FileByteSource(df.File))
                {
                    var reader = new DicomFileReader();
                    var result =
                        await
                        reader.ReadAsync(
                            source,
                            new DicomDatasetReaderObserver(df.FileMetaInfo),
                            new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                            stop).ConfigureAwait(false);

                    if (result == DicomReaderResult.Processing)
                    {
                        throw new DicomFileException(df, "Invalid read return state: {state}", result);
                    }
                    if (result == DicomReaderResult.Error)
                    {
                        return null;
                    }
                    df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;

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
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <returns>Awaitable <see cref="DicomFile"/> instance.</returns>
        public static async Task<DicomFile> OpenAsync(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null)
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
                var result =
                    await
                    reader.ReadAsync(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        stop).ConfigureAwait(false);

                if (result == DicomReaderResult.Processing)
                {
                    throw new DicomFileException(df, "Invalid read return state: {state}", result);
                }
                if (result == DicomReaderResult.Error)
                {
                    return null;
                }
                df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;

                df.Format = reader.FileFormat;
                df.Dataset.InternalTransferSyntax = reader.Syntax;

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }
#endif

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

        /// <summary>
        /// Reads the specified file and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="file">The file reference of the DICOM file</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <returns>DicomFile instance</returns>
        internal static DicomFile Open(IFileReference file, Encoding fallbackEncoding)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException("fallbackEncoding");
            }
            DicomFile df = new DicomFile();

            try
            {
                df.File = file;

                using (var source = new FileByteSource(file))
                {
                    DicomFileReader reader = new DicomFileReader();
                    var result = reader.Read(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding));

                    if (result == DicomReaderResult.Processing)
                    {
                        throw new DicomFileException(df, "Invalid read return state: {state}", result);
                    }
                    if (result == DicomReaderResult.Error)
                    {
                        return null;
                    }
                    df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;

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
        /// Method to call before performing the actual saving.
        /// </summary>
        protected virtual void OnSave()
        {
        }

        /// <summary>
        /// Preprocess file meta information before save.
        /// </summary>
        /// <exception cref="DicomFileException">If file format is ACR-NEMA version 2 or 3.</exception>
        private void PreprocessFileMetaInformation()
        {
            if (this.Format == DicomFileFormat.ACRNEMA1 || this.Format == DicomFileFormat.ACRNEMA2)
            {
                throw new DicomFileException(this, "Unable to save ACR-NEMA file");
            }

            // create file meta information from dataset or update existing file meta information.
            this.FileMetaInfo = this.Format == DicomFileFormat.DICOM3NoFileMetaInfo
                                    ? new DicomFileMetaInformation(this.Dataset)
                                    : new DicomFileMetaInformation(this.FileMetaInfo);
        }

        #endregion
    }
}
