// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO;
using SkiaSharp;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Convenience class for non-generic access to <see cref="SkiaSharpImage"/> image objects.
    /// </summary>
    public static class SkiaSharpImageExtensions
    {

        /// <summary>
        /// Convenience method to access SkiaSharpImage <see cref="IImage"/> instance as SkiaSharp <see cref="SKBitmap"/>.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Image"/> contents of <paramref name="image"/>.</returns>
        public static SKBitmap AsSKBitmap(this IImage iimage)
        {
            return (iimage as SkiaSharpImage).RenderedImage;
        }

    }

    /// <summary>
    /// <see cref="IImage"/> implementation of a <see cref="Image"/> in the <code>SkiaSharp</code> namespace.
    /// </summary>
    public class SkiaSharpImage : ImageBase<SKBitmap>
    {

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="SkiaSharpImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public SkiaSharpImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SkiaSharpImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Pixel array.</param>
        /// <param name="image">Bitmap image.</param>
        private SkiaSharpImage(int width, int height, PinnedIntArray pixels, SKBitmap image)
            : base(width, height, pixels, image)
        {
        }

        #endregion

        #region Properties

        public SKBitmap RenderedImage => _image;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public override void Render(int components, bool flipX, bool flipY, int rotation)
        {
            var data = new byte[_pixels.ByteSize];
            Marshal.Copy(_pixels.Pointer, data, 0, _pixels.ByteSize);

            _image = SKBitmap.FromImage(
                SKImage.FromPixelCopy(
                    new SKImageInfo(_width, _height, SKColorType.Bgra8888),
                    _pixels.Pointer));

            var (flipMode, rotationMode) = GetFlipAndRotateMode(flipX, flipY, rotation);

            if (flipMode != FlipMode.None && rotationMode != RotateMode.None)
            {
                _image = RotateFlip(_image, rotationMode, flipMode);
            }
        }

        private SKBitmap RotateFlip(SKBitmap image, RotateMode rotateMode, FlipMode flipMode)
        {
            var (destWith, destHeight) = rotateMode switch
            {
                RotateMode.None or RotateMode.Rotate180 => (image.Width, image.Height),
                RotateMode.Rotate90 or RotateMode.Rotate270 => (image.Height, image.Width)
            };
            var degrees = rotateMode switch
            {
                RotateMode.Rotate90 => 90f,
                RotateMode.Rotate180 => 180f,
                RotateMode.Rotate270 => 270f,
                _ => 0f
            };
            var rotatedBitmap = new SKBitmap(destWith, destHeight);
            using (var surface = new SKCanvas(rotatedBitmap))
            {
                surface.Clear();
                if (flipMode == FlipMode.Horizontal)
                {
                    surface.Scale(-1, 1, _image.Width / 2.0f, 0);
                }
                if (flipMode == FlipMode.Vertical)
                {
                    surface.Scale(1, -1, 0, _image.Height / 2.0f);
                }

                surface.Translate(destWith / 2, destHeight / 2);
                surface.RotateDegrees(degrees, destWith / 2.0f, destHeight / 2.0f);

                surface.DrawBitmap(image, new SKPoint());
            }
            return rotatedBitmap;
        }

        private (FlipMode, RotateMode) GetFlipAndRotateMode(bool flipX, bool flipY, int rotation)
        {
            FlipMode flipMode;
            if (flipX && flipY)
            {
                // flipping both horizontally and vertically is equal to rotating 180 degrees
                rotation += 180;
                flipMode = FlipMode.None;
            }
            else if (flipX)
            {
                flipMode = FlipMode.Horizontal;
            }
            else if (flipY)
            {
                flipMode = FlipMode.Vertical;
            }
            else
            {
                flipMode = FlipMode.None;
            }

            RotateMode rotationMode;
            switch (rotation % 360)
            {
                case 90: rotationMode = RotateMode.Rotate90; break;
                case 180: rotationMode = RotateMode.Rotate180; break;
                case 270: rotationMode = RotateMode.Rotate270; break;
                default: rotationMode = RotateMode.None; break;
            }

            return (flipMode, rotationMode);
        }


        /// <inheritdoc />
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            var newBitmap = new SKBitmap(_image.Width, _image.Height);
            using (var surface = new SKCanvas(newBitmap))
            {
                surface.DrawBitmap(_image, 0f, 0f);
                foreach (var graphic in graphics)
                {
                    var layer = (graphic.RenderImage(null) as SkiaSharpImage)._image;
                    surface.DrawBitmap(layer, graphic.ScaledOffsetX, graphic.ScaledOffsetY);
                }
            }
            _image = newBitmap;
        }


        /// <inheritdoc />
        public override IImage Clone()
        {
            return new SkiaSharpImage(_width, _height, new PinnedIntArray(_pixels.Data), _image);
        }

        #endregion

    }

    internal enum RotateMode
    {
        None,
        Rotate90,
        Rotate180,
        Rotate270
    }

    internal enum FlipMode
    {
        None,
        Horizontal,
        Vertical
    }
}
