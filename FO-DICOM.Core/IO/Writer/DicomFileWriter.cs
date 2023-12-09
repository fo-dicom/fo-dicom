// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Writer
{

    /// <summary>
    /// Writer for DICOM Part 10 objects.
    /// </summary>
    public class DicomFileWriter
    {
        #region FIELDS

        private readonly DicomWriteOptions _options;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of a <see cref="DicomFileWriter"/>.
        /// </summary>
        /// <param name="options">Writer options.</param>
        public DicomFileWriter(DicomWriteOptions options)
        {
            _options = options ?? DicomWriteOptions.Default;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Write DICOM Part 10 object to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">File meta information.</param>
        /// <param name="dataset">Dataset.</param>
        public void Write(IByteTarget target, DicomFileMetaInformation fileMetaInfo, DicomDataset dataset)
        {
            WritePreamble(target);
            WriteFileMetaInfo(target, fileMetaInfo, _options);
            WriteDataset(target, fileMetaInfo.TransferSyntax, dataset, _options);
        }

        /// <summary>
        /// Write DICOM Part 10 object to <paramref name="target"/> asynchronously.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">File meta information.</param>
        /// <param name="dataset">Dataset.</param>
        /// <returns>Awaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task WriteAsync(IByteTarget target, DicomFileMetaInformation fileMetaInfo, DicomDataset dataset)
        {
            await WritePreambleAsync(target).ConfigureAwait(false);
            await WriteFileMetaInfoAsync(target, fileMetaInfo, _options).ConfigureAwait(false);
            await WriteDatasetAsync(target, fileMetaInfo.TransferSyntax, dataset, _options).ConfigureAwait(false);
        }

        /// <summary>
        /// Write DICOM file preamble.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        private static void WritePreamble(IByteTarget target)
        {
            var preamble = new byte[132];
            preamble[128] = (byte)'D';
            preamble[129] = (byte)'I';
            preamble[130] = (byte)'C';
            preamble[131] = (byte)'M';

            target.Write(preamble, 0, 132);
        }

        /// <summary>
        /// Write DICOM file preamble.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        private static Task WritePreambleAsync(IByteTarget target)
        {
            var preamble = new byte[132];
            preamble[128] = (byte)'D';
            preamble[129] = (byte)'I';
            preamble[130] = (byte)'C';
            preamble[131] = (byte)'M';

            return target.WriteAsync(preamble, 0, 132);
        }

        /// <summary>
        /// Write DICOM file meta information.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">File meta information.</param>
        /// <param name="options">Writer options.</param>
        private static void WriteFileMetaInfo(
            IByteTarget target,
            DicomDataset fileMetaInfo,
            DicomWriteOptions options)
        {
            // recalculate FMI group length as required by standard
            fileMetaInfo.RecalculateGroupLengths();

            var writer = new DicomWriter(DicomTransferSyntax.ExplicitVRLittleEndian, options, target);
            var walker = new DicomDatasetWalker(fileMetaInfo);
            walker.Walk(writer);
        }

        /// <summary>
        /// Write DICOM file meta information.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">File meta information.</param>
        /// <param name="options">Writer options.</param>
        private static Task WriteFileMetaInfoAsync(
            IByteTarget target,
            DicomDataset fileMetaInfo,
            DicomWriteOptions options)
        {
            // recalculate FMI group length as required by standard
            fileMetaInfo.RecalculateGroupLengths();

            var writer = new DicomWriter(DicomTransferSyntax.ExplicitVRLittleEndian, options, target);
            var walker = new DicomDatasetWalker(fileMetaInfo);
            return walker.WalkAsync(writer);
        }

        /// <summary>
        /// Write DICOM dataset.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="syntax">Transfer syntax applicable to dataset.</param>
        /// <param name="dataset">Dataset.</param>
        /// <param name="options">Writer options.</param>
        private static void WriteDataset(
            IByteTarget target,
            DicomTransferSyntax syntax,
            DicomDataset dataset,
            DicomWriteOptions options)
        {
            dataset.OnBeforeSerializing();
            UpdateDatasetGroupLengths(syntax, dataset, options);

            if (syntax.IsDeflate)
            {
                using var uncompressed = new MemoryStream();
                var temp = new StreamByteTarget(uncompressed);
                WalkDataset(temp, syntax, dataset, options);

                uncompressed.Seek(0, SeekOrigin.Begin);
                using var compressed = new MemoryStream();
                using (var compressor = new DeflateStream(compressed, CompressionMode.Compress, true))
                {
                    uncompressed.CopyTo(compressor);
                }

                target.Write(compressed.ToArray(), 0, (uint)compressed.Length);
            }
            else
            {
                WalkDataset(target, syntax, dataset, options);
            }
        }

        private static void WalkDataset(
            IByteTarget target,
            DicomTransferSyntax syntax,
            DicomDataset dataset,
            DicomWriteOptions options)
        {
            var writer = new DicomWriter(syntax, options, target);
            var walker = new DicomDatasetWalker(dataset);
            walker.Walk(writer);
        }

        /// <summary>
        /// Write DICOM dataset.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="syntax">Transfer syntax applicable to dataset.</param>
        /// <param name="dataset">Dataset.</param>
        /// <param name="options">Writer options.</param>
        private static async Task WriteDatasetAsync(
            IByteTarget target,
            DicomTransferSyntax syntax,
            DicomDataset dataset,
            DicomWriteOptions options)
        {
            dataset.OnBeforeSerializing();
            UpdateDatasetGroupLengths(syntax, dataset, options);

            if (syntax.IsDeflate)
            {
                using (var uncompressed = new MemoryStream())
                {
                    var temp = new StreamByteTarget(uncompressed);
                    await WalkDatasetAsync(temp, syntax, dataset, options).ConfigureAwait(false);

                    uncompressed.Seek(0, SeekOrigin.Begin);
                    using (var compressed = new MemoryStream())
                    {
                        using (var compressor = new DeflateStream(compressed, CompressionMode.Compress, true))
                        {
                            uncompressed.CopyTo(compressor);
                        }

                        target.Write(compressed.ToArray(), 0, (uint)compressed.Length);
                    }
                }
            }
            else
            {
                await WalkDatasetAsync(target, syntax, dataset, options).ConfigureAwait(false);
            }
        }

        private static Task WalkDatasetAsync(
            IByteTarget target,
            DicomTransferSyntax syntax,
            DicomDataset dataset,
            DicomWriteOptions options)
        {
            var writer = new DicomWriter(syntax, options, target);
            var walker = new DicomDatasetWalker(dataset);
            return walker.WalkAsync(writer);
        }

        /// <summary>
        /// If necessary, update dataset syntax and group lengths.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <param name="dataset">DICOM dataset.</param>
        /// <param name="options">Writer options.</param>
        private static void UpdateDatasetGroupLengths(
            DicomTransferSyntax syntax,
            DicomDataset dataset,
            DicomWriteOptions options)
        {
            if (options.KeepGroupLengths)
            {
                // update transfer syntax and recalculate existing group lengths
                dataset.InternalTransferSyntax = syntax;
                dataset.RecalculateGroupLengths(false);
            }
            else
            {
                // remove group lengths as suggested in PS 3.5 7.2
                //
                //	2. It is recommended that Group Length elements be removed during storage or transfer 
                //	   in order to avoid the risk of inconsistencies arising during coercion of data 
                //	   element values and changes in transfer syntax.
                dataset.RemoveGroupLengths();
            }
        }

        #endregion
    }
}
