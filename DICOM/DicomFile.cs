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

        public IFileReference File { get; protected set; }

        public DicomFileFormat Format { get; protected set; }

        public DicomFileMetaInformation FileMetaInfo { get; protected set; }

        public DicomDataset Dataset { get; protected set; }

        protected virtual void OnSave()
        {
        }

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

        public static DicomFile Open(Stream stream)
        {
            return Open(stream, DicomEncoding.Default);
        }

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

        public override string ToString()
        {
            return String.Format("DICOM File [{0}]", Format);
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
    }
}
