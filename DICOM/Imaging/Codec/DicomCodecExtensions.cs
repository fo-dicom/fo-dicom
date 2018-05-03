// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Imaging.Codec
{
    /// <summary>
    /// Extension methods associated with DICOM transfer syntax change.
    /// </summary>
    public static class DicomCodecExtensions
    {
        /// <summary>
        /// Create a copy of the specified DICOM file with requested transfer syntax.
        /// </summary>
        /// <param name="file">DICOM file to copy.</param>
        /// <param name="syntax">Requested transfer syntax for the created DICOM file.</param>
        /// <param name="parameters">Codec parameters.</param>
        /// <returns>DICOM file with modified transfer syntax.</returns>
        public static DicomFile Clone(
            this DicomFile file,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            var transcoder = new DicomTranscoder(file.FileMetaInfo.TransferSyntax, syntax, null, parameters);
            return transcoder.Transcode(file);
        }

        /// <summary>
        /// Create a copy of the specified DICOM dataset with requested transfer syntax.
        /// </summary>
        /// <param name="dataset">DICOM dataset to copy.</param>
        /// <param name="syntax">Requested transfer syntax for the created DICOM dataset.</param>
        /// <param name="parameters">Codec parameters.</param>
        /// <returns>DICOM dataset with modified transfer syntax.</returns>
        public static DicomDataset Clone(
            this DicomDataset dataset,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            var transcoder = new DicomTranscoder(dataset.InternalTransferSyntax, syntax, null, parameters);
            return transcoder.Transcode(dataset);
        }
        /// <summary>
        /// Create a copy of the specified DICOM file with requested transfer syntax.
        /// </summary>
        /// <param name="file">DICOM file to copy.</param>
        /// <param name="syntax">Requested transfer syntax for the created DICOM file.</param>
        /// <param name="parameters">Codec parameters.</param>
        /// <returns>DICOM file with modified transfer syntax.</returns>
        [Obsolete("Deprecated, will be removed in later version. Use Clone instead.")]
        public static DicomFile ChangeTransferSyntax(
            this DicomFile file,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            return Clone(file, syntax, parameters);
        }

        /// <summary>
        /// Create a copy of the specified DICOM dataset with requested transfer syntax.
        /// </summary>
        /// <param name="dataset">DICOM dataset to copy.</param>
        /// <param name="syntax">Requested transfer syntax for the created DICOM dataset.</param>
        /// <param name="parameters">Codec parameters.</param>
        /// <returns>DICOM dataset with modified transfer syntax.</returns>
        [Obsolete("Deprecated, will be removed in later version. Use Clone instead.")]
        public static DicomDataset ChangeTransferSyntax(
            this DicomDataset dataset,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            return Clone(dataset, syntax, parameters);
        }
    }
}
