// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using System;

namespace FellowOakDicom.Media
{
    /// <summary>
    /// Utility for building DICOM Icon Image Sequences from grayscale pixel data.
    /// Provides consistent icon formatting across all imaging implementations.
    /// </summary>
    public static class DicomIconImageSequenceBuilder
    {
        private const int BitsAllocated = 8;
        private const int BitsStored = 8;
        private const int HighBit = 7;

        /// <summary>
        /// Creates a DICOM Icon Image Sequence from 8-bit grayscale pixel data.
        /// </summary>
        /// <param name="width">Icon width in pixels.</param>
        /// <param name="height">Icon height in pixels.</param>
        /// <param name="grayscalePixelData">8-bit grayscale pixel data (Monochrome2 format).</param>
        /// <returns>A <see cref="DicomSequence"/> containing the icon image data.</returns>
        /// <exception cref="ArgumentNullException">If grayscalePixelData is null.</exception>
        /// <exception cref="ArgumentException">If dimensions are invalid or pixel data size doesn't match dimensions.</exception>
        public static DicomSequence Build(int width, int height, byte[] grayscalePixelData)
        {
            if (grayscalePixelData == null)
            {
                throw new ArgumentNullException(nameof(grayscalePixelData));
            }

            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException($"Icon dimensions must be positive. Got width={width}, height={height}");
            }

            if (width > 128 || height > 128)
            {
                throw new ArgumentException($"Icon dimensions must not exceed 128 pixels. Got width={width}, height={height}");
            }

            var expectedPixelCount = width * height;
            if (grayscalePixelData.Length != expectedPixelCount)
            {
                throw new ArgumentException(
                    $"Pixel data length ({grayscalePixelData.Length}) does not match dimensions ({width}x{height}={expectedPixelCount})");
            }

            var iconDataset = new DicomDataset
            {
                new DicomUnsignedShort(DicomTag.Columns, (ushort)width),
                new DicomUnsignedShort(DicomTag.Rows, (ushort)height),
                new DicomUnsignedShort(DicomTag.SamplesPerPixel, 1),
                new DicomUnsignedShort(DicomTag.BitsAllocated, BitsAllocated),
                new DicomUnsignedShort(DicomTag.BitsStored, BitsStored),
                new DicomUnsignedShort(DicomTag.HighBit, HighBit),
                new DicomUnsignedShort(DicomTag.PixelRepresentation, 0), // Unsigned
                new DicomCodeString(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value),
                new DicomCodeString(DicomTag.NumberOfFrames, "1"),
                new DicomOtherByte(DicomTag.PixelData, grayscalePixelData)
            };

            return new DicomSequence(DicomTag.IconImageSequence, iconDataset);
        }
    }
}