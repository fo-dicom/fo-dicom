// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Media
{
    /// <summary>
    /// Interface for generating DICOMDIR icon images from DICOM datasets.
    /// Icons are 8-bit grayscale thumbnails, maximum 128x128 pixels with preserved aspect ratio.
    /// </summary>
    public interface IIconGenerator
    {
        /// <summary>
        /// Generates an Icon Image Sequence for a DICOM dataset.
        /// </summary>
        /// <param name="dataset">The DICOM dataset containing the image data.</param>
        /// <param name="frameIndex">Frame index to use for multi-frame images (default: 0).</param>
        /// <returns>
        /// A <see cref="DicomSequence"/> containing the icon image data conforming to DICOM standard,
        /// or null if icon generation fails or is not applicable for this dataset.
        /// </returns>
        DicomSequence? GenerateIconImageSequence(DicomDataset dataset, int frameIndex = 0);
    }
}
