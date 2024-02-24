// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO;

namespace FellowOakDicom.Imaging
{
    /// <summary>
    /// Convenience class for non-generic access to <see cref="WinFormsImage"/> image objects.
    /// </summary>
    public static class WinFormsImageExtensions
    {

        /// <summary>
        /// Convenience method to access WinForms <see cref="IImage"/> instance as WinForms <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> is cloned and must be disposed by caller.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        public static Bitmap AsClonedBitmap(this IImage iimage)
        {
            return iimage.As<Bitmap>()?.Clone() as Bitmap;
        }

        /// <summary>
        /// Convenience method to access WinForms <see cref="IImage"/> instance as WinForms <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        public static Bitmap AsSharedBitmap(this IImage iimage)
        {
            return iimage.As<Bitmap>();
        }

    }

    /// <summary>
    /// <see cref="IImage"/> implementation of a <see cref="Bitmap"/> in the <code>System.Drawing</code> namespace.
    /// </summary>
    public sealed class WinFormsImage : ImageDisposableBase<Bitmap>
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WinFormsImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public WinFormsImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="WinFormsImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Pixel array.</param>
        /// <param name="image">Bitmap image.</param>
        private WinFormsImage(int width, int height, PinnedIntArray pixels, Bitmap image)
            : base(width, height, pixels, image)
        {
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public override void Render(int components, bool flipX, bool flipY, int rotation)
        {
            var format = components == 4 ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb;
            var stride = GetStride(_width, format);

            _image = new Bitmap(_width, _height, stride, format, _pixels.Pointer);

            var rotateFlipType = GetRotateFlipType(flipX, flipY, rotation);
            if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
            {
                _image.RotateFlip(rotateFlipType);
            }
        }

        /// <inheritdoc />
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            using var g = Graphics.FromImage(_image);
            foreach (var graphic in graphics)
            {
                var layer = graphic.RenderImage(null).As<Image>();
                g.DrawImage(layer, graphic.ScaledOffsetX, graphic.ScaledOffsetY, graphic.ScaledWidth, graphic.ScaledHeight);
            }
        }

        /// <inheritdoc />
        public override IImage Clone()
        {
            return new WinFormsImage(
                _width,
                _height,
                new PinnedIntArray(_pixels.Data),
                _image == null ? null : new Bitmap(_image));
        }

        private static int GetStride(int width, PixelFormat format)
        {
            var bitsPerPixel = ((int)format & 0xff00) >> 8;
            var bytesPerPixel = (bitsPerPixel + 7) / 8;
            return 4 * ((width * bytesPerPixel + 3) / 4);
        }

        private static RotateFlipType GetRotateFlipType(bool flipX, bool flipY, int rotation)
        {
            if (flipX && flipY)
            {
                return rotation switch
                {
                    90 => RotateFlipType.Rotate90FlipXY,
                    180 => RotateFlipType.Rotate180FlipXY,
                    270 => RotateFlipType.Rotate270FlipXY,
                    _ => RotateFlipType.RotateNoneFlipXY,
                };
            }

            if (flipX)
            {
                return rotation switch
                {
                    90 => RotateFlipType.Rotate90FlipX,
                    180 => RotateFlipType.Rotate180FlipX,
                    270 => RotateFlipType.Rotate270FlipX,
                    _ => RotateFlipType.RotateNoneFlipX,
                };
            }

            if (flipY)
            {
                return rotation switch
                {
                    90 => RotateFlipType.Rotate90FlipY,
                    180 => RotateFlipType.Rotate180FlipY,
                    270 => RotateFlipType.Rotate270FlipY,
                    _ => RotateFlipType.RotateNoneFlipY,
                };
            }

            return rotation switch
            {
                90 => RotateFlipType.Rotate90FlipNone,
                180 => RotateFlipType.Rotate180FlipNone,
                270 => RotateFlipType.Rotate270FlipNone,
                _ => RotateFlipType.RotateNoneFlipNone,
            };
        }


        #endregion
    }
}
