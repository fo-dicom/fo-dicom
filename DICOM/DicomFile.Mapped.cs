// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using Dicom.IO;

using Dicom.IO.Reader;

namespace Dicom
{
    public partial class DicomFile
    {
        /// <summary>
        /// Reads a DICOM file with the specified filename, using default DICOM encoding, and returns a DicomFile object.
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file.</param>
        /// <returns>DicomFile instance.</returns>
        /// <remarks>Uses MemoryMappedFile to avoid unneccesary copying of data into memory.</remarks>
        public static DicomFile OpenMemoryMapped(string fileName)
        {
            return OpenMemoryMapped(fileName, DicomEncoding.Default);
        }

        /// <summary>
        /// Reads a DICOM file with the specified filename and returns a DicomFile object.
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file.</param>
        /// <param name="fallbackEncoding">Encoding to apply when attribute Specific Character Set is not available.</param>
        /// <returns>DicomFile instance.</returns>
        /// <remarks>Uses MemoryMappedFile to avoid unneccesary copying of data into memory.</remarks>
        public static DicomFile OpenMemoryMapped(string fileName, Encoding fallbackEncoding)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException("fallbackEncoding");
            }
            var df = new DicomFile();

            try
            {
                df.File = new FileReference(fileName);

                using (var source = new MemoryMappedFileByteSource(fileName))
                {
                    var reader = new DicomFileReader();
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
    }
}
