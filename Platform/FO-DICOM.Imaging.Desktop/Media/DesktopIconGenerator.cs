// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FellowOakDicom.Media
{
    /// <summary>
    /// Generates thumbnail icons for DICOMDIR using System.Drawing/GDI+.
    /// Creates 8-bit grayscale icons, maximum 128x128 pixels with aspect ratio preserved.
    /// Uses high-quality bicubic interpolation for optimal clarity.
    /// </summary>
    public class DesktopIconGenerator : IIconGenerator
    {
        private const int MaxIconDimension = 128;

        /// <inheritdoc />
        public DicomSequence? GenerateIconImageSequence(DicomDataset dataset, int frameIndex = 0)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }

            try
            {
                // Render the DICOM image
                var dicomImage = new DicomImage(dataset);
                var renderedImage = dicomImage.RenderImage(frameIndex);

                if (!(renderedImage is WinFormsImage winFormsImage))
                {
                    // Not a WinForms image, cannot process
                    return null;
                }

                var sourceBitmap = winFormsImage.As<Bitmap>();
                if (sourceBitmap == null)
                {
                    return null;
                }

                // Calculate scaled dimensions preserving aspect ratio
                var (iconWidth, iconHeight) = CalculateIconDimensions(sourceBitmap.Width, sourceBitmap.Height);

                // Generate grayscale pixel data
                var pixelData = CreateGrayscaleIconPixelData(sourceBitmap, iconWidth, iconHeight);

                // Use common builder to create DICOM sequence
                return DicomIconImageSequenceBuilder.Build(iconWidth, iconHeight, pixelData);
            }
            catch
            {
                // Icon generation is optional - if it fails, return null
                // This ensures DICOMDIR creation continues even if icon generation fails
                return null;
            }
        }

        /// <summary>
        /// Calculate icon dimensions maintaining aspect ratio, max 128x128.
        /// </summary>
        private static (int width, int height) CalculateIconDimensions(int sourceWidth, int sourceHeight)
        {
            if (sourceWidth <= 0 || sourceHeight <= 0)
            {
                throw new ArgumentException("Source dimensions must be positive");
            }

            var aspectRatio = (double)sourceWidth / sourceHeight;

            int iconWidth, iconHeight;

            if (sourceWidth >= sourceHeight)
            {
                // Landscape or square
                iconWidth = Math.Min(MaxIconDimension, sourceWidth);
                iconHeight = (int)Math.Round(iconWidth / aspectRatio);
            }
            else
            {
                // Portrait
                iconHeight = Math.Min(MaxIconDimension, sourceHeight);
                iconWidth = (int)Math.Round(iconHeight * aspectRatio);
            }

            // Ensure dimensions are at least 1
            iconWidth = Math.Max(1, iconWidth);
            iconHeight = Math.Max(1, iconHeight);

            return (iconWidth, iconHeight);
        }

        /// <summary>
        /// Creates grayscale icon pixel data with high-quality interpolation.
        /// </summary>
        private static byte[] CreateGrayscaleIconPixelData(Bitmap source, int targetWidth, int targetHeight)
        {
            // Create 8-bit grayscale bitmap
            using var iconBitmap = new Bitmap(targetWidth, targetHeight, PixelFormat.Format8bppIndexed);

            // Set up grayscale palette
            var palette = iconBitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            iconBitmap.Palette = palette;

            // Draw resized image with high-quality interpolation
            using (var graphics = Graphics.FromImage(iconBitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.DrawImage(source, 0, 0, targetWidth, targetHeight);
            }

            // Extract pixel data using LockBits
            var pixelData = new byte[targetWidth * targetHeight];
            var bitmapData = iconBitmap.LockBits(
                new Rectangle(0, 0, targetWidth, targetHeight),
                ImageLockMode.ReadOnly,
                PixelFormat.Format8bppIndexed);

            try
            {
                // Copy pixel data row by row
                var sourcePtr = bitmapData.Scan0;
                var stride = bitmapData.Stride;

                for (int y = 0; y < targetHeight; y++)
                {
                    Marshal.Copy(
                        sourcePtr + (y * stride),
                        pixelData,
                        y * targetWidth,
                        targetWidth);
                }
            }
            finally
            {
                iconBitmap.UnlockBits(bitmapData);
            }

            return pixelData;
        }
    }
}