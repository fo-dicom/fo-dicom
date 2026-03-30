// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.StructuredReport
{
    /// <summary>
    /// Class for managing DICOM Structured report objects
    /// </summary>
    public class DicomStructuredReport : DicomContentItem
    {
        public DicomStructuredReport(DicomDataset dataset)
            : base(dataset)
        {
        }

        public DicomStructuredReport(DicomCodeItem code, params DicomContentItem[] items)
            : base(code, DicomRelationship.Contains, DicomContinuity.Separate, items)
        {
            // relationship type is not needed for root element
            Dataset.Remove(DicomTag.RelationshipType);
        }

        /// <summary>
        /// Read DICOM StructuredReport
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns><see cref="DicomStructuredReport"/> instance.</returns>
        public static DicomStructuredReport Open(Stream stream)
        {
            var file = DicomFile.Open(stream);
            return new DicomStructuredReport(file.Dataset);
        }

        /// <summary>
        /// Read DICOM StructuredReport
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to use when reading the stream</param>
        /// <returns><see cref="DicomStructuredReport"/> instance.</returns>
        public static DicomStructuredReport Open(Stream stream, Encoding fallbackEncoding)
        {
            var file = DicomFile.Open(stream, fallbackEncoding);
            return new DicomStructuredReport(file.Dataset);
        }

        /// <summary>
        /// Read DICOM StructuredReport
        /// </summary>
        /// <param name="filename">Name of file to read.</param>
        /// <returns><see cref="DicomStructuredReport"/> instance.</returns>
        public static DicomStructuredReport Open(string filename)
        {
            var file = DicomFile.Open(filename);
            return new DicomStructuredReport(file.Dataset);
        }

        /// <summary>
        /// Read DICOM StructuredReport
        /// </summary>
        /// <param name="filename">Name of file to read.</param>
        /// <param name="fallbackEncoding">Encoding to use when reading the file</param>
        /// <returns><see cref="DicomStructuredReport"/> instance.</returns>
        public static DicomStructuredReport Open(string filename, Encoding fallbackEncoding)
        {
            var file = DicomFile.Open(filename, fallbackEncoding);
            return new DicomStructuredReport(file.Dataset);
        }

        /// <summary>
        /// Save DICOM Structured Report to file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public void Save(string filename)
        {
            var file = new DicomFile(this.Dataset);
            file.Save(filename, new IO.Writer.DicomWriteOptions { ExplicitLengthSequenceItems = true, ExplicitLengthSequences = true });
        }

        /// <summary>
        /// Save DICOM Structured Report to stream.
        /// </summary>
        /// <param name="stream">Stream on which to save DICOM file.</param>
        public void Save(Stream stream)
        {
            var file = new DicomFile(this.Dataset);
            file.Save(stream, new IO.Writer.DicomWriteOptions { ExplicitLengthSequenceItems = true, ExplicitLengthSequences = true });
        }

        /// <summary>
        /// Save to file asynchronously.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        /// <returns>Awaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        public Task SaveAsync(string fileName)
        {
            var file = new DicomFile(this.Dataset);
            return file.SaveAsync(fileName, new IO.Writer.DicomWriteOptions { ExplicitLengthSequenceItems = true, ExplicitLengthSequences = true });
        }

        /// <summary>
        /// Asynchronously save DICOM Structured Report to stream.
        /// </summary>
        /// <param name="stream">Stream on which to save DICOM file.</param>
        /// <returns>Awaitable task.</returns>
        public Task SaveAsync(Stream stream)
        {
            var file = new DicomFile(this.Dataset);
            return file.SaveAsync(stream, new IO.Writer.DicomWriteOptions { ExplicitLengthSequenceItems = true, ExplicitLengthSequences = true });
        }

    }
}
