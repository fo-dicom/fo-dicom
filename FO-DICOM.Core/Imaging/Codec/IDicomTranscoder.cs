// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.Imaging.Codec
{

    /// <summary>
    /// DICOM transcoder interface.
    /// </summary>
    public interface IDicomTranscoder
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the transfer syntax of the input codec.
        /// </summary>
        DicomTransferSyntax InputSyntax { get; }

        /// <summary>
        /// Gets the parameters associated with the input codec.
        /// </summary>
        DicomCodecParams InputCodecParams { get; }

        /// <summary>
        /// Gets the transfer syntax of the output codec.
        /// </summary>
        DicomTransferSyntax OutputSyntax { get; }

        /// <summary>
        /// Gets the parameters associated with the output codec.
        /// </summary>
        DicomCodecParams OutputCodecParams { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Transcode a <see cref="DicomFile"/> from <see cref="InputSyntax"/> to <see cref="OutputSyntax"/>.
        /// </summary>
        /// <param name="file">DICOM file.</param>
        /// <returns>New, transcoded, DICOM file.</returns>
        DicomFile Transcode(DicomFile file);

        /// <summary>
        /// Transcode a <see cref="DicomDataset"/> from <see cref="InputSyntax"/> to <see cref="OutputSyntax"/>.
        /// </summary>
        /// <param name="dataset">DICOM dataset.</param>
        /// <returns>New, transcoded, DICOM dataset.</returns>
        DicomDataset Transcode(DicomDataset dataset);

        /// <summary>
        /// Decompress single frame from DICOM dataset and return uncompressed frame buffer.
        /// </summary>
        /// <param name="dataset">DICOM dataset.</param>
        /// <param name="frame">Frame number.</param>
        /// <returns>Uncompressed frame buffer.</returns>
        IByteBuffer DecodeFrame(DicomDataset dataset, int frame);

        /// <summary>
        /// Decompress pixel data from DICOM dataset and return uncompressed pixel data.
        /// </summary>
        /// <param name="dataset">DICOM dataset.</param>
        /// <param name="frame">Frame number.</param>
        /// <returns>Uncompressed pixel data.</returns>
        IPixelData DecodePixelData(DicomDataset dataset, int frame);

        #endregion
    }
}
