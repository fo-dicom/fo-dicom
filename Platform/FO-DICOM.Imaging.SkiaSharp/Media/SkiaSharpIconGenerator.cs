// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using SkiaSharp;
using System;

namespace FellowOakDicom.Media
{
    /// <summary>
    /// Generates thumbnail icons for DICOMDIR using SkiaSharp.
    /// Creates 8-bit grayscale icons, maximum 128x128 pixels with aspect ratio preserved.
    /// Uses high-quality downsampling with optional sharpening for optimal clarity.
    /// </summary>
    public class SkiaSharpIconGenerator : IIconGenerator
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

                if (renderedImage is not SkiaSharpImage skiaImage)
                {
                    // Not a SkiaSharp image, cannot process
                    return null;
                }

                var sourceBitmap = skiaImage.AsSKBitmap();
                if (sourceBitmap == null)
                {
                    return null;
                }

                // Calculate scaled dimensions preserving aspect ratio
                var (iconWidth, iconHeight) = CalculateIconDimensions(sourceBitmap.Width, sourceBitmap.Height);

                // Generate grayscale pixel data
                using var iconBitmap = CreateGrayscaleIcon(sourceBitmap, iconWidth, iconHeight);
                var pixelData = ExtractGrayscalePixelData(iconBitmap);

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
        /// Creates a grayscale icon bitmap with high-quality filtering and optional sharpening.
        /// </summary>
        private SKBitmap CreateGrayscaleIcon(SKBitmap source, int targetWidth, int targetHeight)
        {
            // Create target bitmap in grayscale color space
            var iconBitmap = new SKBitmap(targetWidth, targetHeight, SKColorType.Gray8, SKAlphaType.Opaque);

            using var canvas = new SKCanvas(iconBitmap);
            using var paint = new SKPaint
            {
                IsAntialias = true
            };

            // Optional sharpening for better clarity at small sizes
            if (EnableSharpening)
            {
                // Subtle unsharp mask kernel for edge enhancement
                paint.ImageFilter = SKImageFilter.CreateMatrixConvolution(
                    new SKSizeI(3, 3),
                    new float[]
                    {
                        0, -0.25f, 0,
                        -0.25f, 2, -0.25f,
                        0, -0.25f, 0
                    },
                    gain: 1.0f,
                    bias: 0.0f,
                    kernelOffset: new SKPointI(1, 1),
                    tileMode: SKShaderTileMode.Clamp,
                    convolveAlpha: false);
            }

            // Scale to target size with high-quality sampling
            var destRect = new SKRect(0, 0, targetWidth, targetHeight);
            canvas.DrawBitmap(source, destRect, paint);

            return iconBitmap;
        }

        /// <summary>
        /// Extracts 8-bit grayscale pixel data from SKBitmap using safe APIs.
        /// </summary>
        private static byte[] ExtractGrayscalePixelData(SKBitmap bitmap)
        {
            var pixelCount = bitmap.Width * bitmap.Height;
            var pixels = new byte[pixelCount];

            // Use safe Span<byte> API instead of unsafe pointers
            var pixelSpan = bitmap.GetPixelSpan();
            pixelSpan.CopyTo(pixels);

            return pixels;
        }
    }
}