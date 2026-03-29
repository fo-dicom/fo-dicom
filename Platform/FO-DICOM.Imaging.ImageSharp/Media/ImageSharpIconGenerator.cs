// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom;
using FellowOakDicom.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace FellowOakDicom.Media
{
    /// <summary>
    /// Generates thumbnail icons for DICOMDIR using ImageSharp.
    /// Creates 8-bit grayscale icons, maximum 128x128 pixels with aspect ratio preserved.
    /// Uses high-quality Lanczos3 resampling for optimal clarity.
    /// </summary>
    public class ImageSharpIconGenerator : IIconGenerator
    {
        private const int MaxIconDimension = 128;

        /// <summary>
        /// Gets or sets whether to apply sharpening filter during downsampling for improved clarity.
        /// Default is false. Enable for better visual quality at small icon sizes.
        /// </summary>
        public bool EnableSharpening { get; set; }

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

                if (!(renderedImage is ImageSharpImage imageSharpImage))
                {
                    // Not an ImageSharp image, cannot process
                    return null;
                }

                var sourceImage = imageSharpImage.AsSharpImage();
                if (sourceImage == null)
                {
                    return null;
                }

                // Calculate scaled dimensions preserving aspect ratio
                var (iconWidth, iconHeight) = CalculateIconDimensions(sourceImage.Width, sourceImage.Height);

                // Generate grayscale pixel data
                var pixelData = CreateGrayscaleIconPixelData(sourceImage, iconWidth, iconHeight);

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
        /// Creates grayscale icon pixel data with high-quality resampling.
        /// </summary>
        private byte[] CreateGrayscaleIconPixelData(Image<Bgra32> source, int targetWidth, int targetHeight)
        {
            // Create grayscale image at target size
            using var iconImage = new Image<L8>(targetWidth, targetHeight);

            // Resize with high-quality Lanczos3 resampler and convert to grayscale
            iconImage.Mutate(ctx =>
            {
                // First resize the source
                var resized = source.Clone(x => x.Resize(targetWidth, targetHeight, KnownResamplers.Lanczos3));

                // Convert to grayscale and draw onto our L8 image
                ctx.DrawImage(resized, 1.0f);

                // Optional sharpening for better clarity at small sizes
                if (EnableSharpening)
                {
                    ctx.GaussianSharpen(0.5f);
                }

                resized.Dispose();
            });

            // Extract pixel data using safe API
            var pixelData = new byte[targetWidth * targetHeight];
            iconImage.CopyPixelDataTo(pixelData);

            return pixelData;
        }
    }
}