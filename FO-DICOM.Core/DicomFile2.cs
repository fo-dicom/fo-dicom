using FellowOakDicom.IO;
using FellowOakDicom.IO.Reader;
using FellowOakDicom.IO.Writer;
using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom
{
    public class DicomFile2 : DicomFile, IDisposable
    {
        #region CONSTRUCTORS

        public DicomFile2()
        {
            FileMetaInfo = new DicomFileMetaInformation();
            Dataset = new DicomDataset2();
            Format = DicomFileFormat.DICOM3;
            IsPartial = false;
        }

        public DicomFile2(DicomDataset dataset)
        {
            Dataset = dataset;
            FileMetaInfo = new DicomFileMetaInformation(Dataset);
            Format = DicomFileFormat.DICOM3;
            IsPartial = false;
        }

        #endregion

        #region PROPERTIES

        #endregion
        
        #region METHODS
        
        /// <summary>
        /// Reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="readOption">An option how to deal with large dicom tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>DicomFile instance</returns>
        public static DicomFile2 Open(string fileName, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
            => Open(fileName, DicomEncoding.Default, readOption: readOption, largeObjectSize: largeObjectSize);

        /// <summary>
        /// Reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">Option how to deal with large values, if they should be loaded directly into memory or lazy loaded on demand</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>DicomFile instance</returns>
        public static DicomFile2 Open(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomFile2();

            try
            {
                var fileReferenceFactory = Setup.ServiceProvider.GetService<IFileReferenceFactory>();
                var memoryProvider = Setup.ServiceProvider.GetService<IMemoryProvider>();
                df.File = fileReferenceFactory.Create(fileName);

                using var unvalidatedDataset = new UnvalidatedScope(df.Dataset);
                using var source = new FileByteSource(df.File, readOption, largeObjectSize, memoryProvider, true);
                var reader = new DicomFileReader();
                var result = reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                    stop);

                HandleOpenError(df, result);

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

        /// <summary>
        /// Read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passed, then the default of 64k is used.</param>
        /// <returns>Read <see cref="DicomFile2"/>.</returns>
        public static DicomFile2 Open(Stream stream, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
            => Open(stream, DicomEncoding.Default, readOption: readOption, largeObjectSize: largeObjectSize);

        /// <summary>
        /// Read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to use if encoding cannot be obtained from DICOM file.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tag like pixel data</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Read <see cref="DicomFile2"/>.</returns>
        public static DicomFile2 Open(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomFile2();

            try
            {
                var source = StreamByteSourceFactory.Create(stream, readOption, largeObjectSize, true);

                using var unvalidated = new UnvalidatedScope(df.Dataset);
                var reader = new DicomFileReader();
                var result = reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                    stop);

                HandleOpenError(df, result);

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

        /// <summary>
        /// Asynchronously reads the specified filename and returns a DicomFile2 object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="readOption">The option how to deal with large dicom tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomFile2"/> instance.</returns>
        public static Task<DicomFile2> OpenAsync(string fileName, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
            => OpenAsync(fileName, DicomEncoding.Default, readOption: readOption, largeObjectSize: largeObjectSize);

        /// <summary>
        /// Asynchronously reads the specified filename and returns a DicomFile2 object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">Option how to deal with large values, if they should be loaded directly into memory or lazy loaded on demand</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomFile2"/> instance.</returns>
        public static async Task<DicomFile2> OpenAsync(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomFile2();

            try
            {
                var fileReferenceFactory = Setup.ServiceProvider.GetService<IFileReferenceFactory>();
                var memoryProvider = Setup.ServiceProvider.GetService<IMemoryProvider>();
                df.File = fileReferenceFactory.Create(fileName);

                using var unvalidated = new UnvalidatedScope(df.Dataset);
                using var source = new FileByteSource(df.File, readOption, largeObjectSize, memoryProvider, true);
                var reader = new DicomFileReader();
                var result =
                    await
                    reader.ReadAsync(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        stop).ConfigureAwait(false);

                HandleOpenError(df, result);

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

        /// <summary>
        /// Asynchronously read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomFile2"/> instance.</returns>
        public static Task<DicomFile2> OpenAsync(Stream stream, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
            => OpenAsync(stream, DicomEncoding.Default, readOption: readOption, largeObjectSize: largeObjectSize);

        /// <summary>
        /// Asynchronously read a DICOM file from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to use if encoding cannot be obtained from DICOM file.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomFile2"/> instance.</returns>
        public static async Task<DicomFile2> OpenAsync(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomFile2();

            try
            {
                var source = StreamByteSourceFactory.Create(stream, readOption, largeObjectSize, true);
                using var unvalidated = new UnvalidatedScope(df.Dataset);
                var reader = new DicomFileReader();
                var result =
                    await
                    reader.ReadAsync(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        stop).ConfigureAwait(false);

                HandleOpenError(df, result);

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

        public void Dispose()
        {
            if (Dataset is DicomDataset2 dataSet2)
            {
                dataSet2.Dispose();
            }
        }
        
        #endregion
    }
}