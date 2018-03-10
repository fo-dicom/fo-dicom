// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using Dicom.Imaging.Render;
using Dicom.IO;

namespace Dicom.Imaging
{
    /// <summary>
    /// Convenience class for non-generic access to <see cref="WinFormsImage"/> image objects.
    /// </summary>
    public static class WinFormsImageExtensions
    {
        /// <summary>
        /// Convenience method to access WinForms <see cref="IImage"/> instance as WinForms <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <param name="image"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        [Obsolete("use AsClonedBitmap or AsSharedBitmap instead.")]
        public static Bitmap AsBitmap(this IImage image)
        {
            return image.As<Bitmap>();
        }

        /// <summary>
        /// Convenience method to access WinForms <see cref="IImage"/> instance as WinForms <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> is cloned and must be disposed by caller.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        public static Bitmap AsClonedBitmap(this IImage iimage)
        {
#pragma warning disable 618
            return iimage.As<Bitmap>()?.Clone() as Bitmap;
#pragma warning restore 618
        }

        /// <summary>
        /// Convenience method to access WinForms <see cref="IImage"/> instance as WinForms <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        public static Bitmap AsSharedBitmap(this IImage iimage)
        {
#pragma warning disable 618
            return iimage.As<Bitmap>();
#pragma warning restore 618
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
            var stride = GetStride(this.width, format);

            this.image = new Bitmap(this.width, this.height, stride, format, this.pixels.Pointer);

            var rotateFlipType = GetRotateFlipType(flipX, flipY, rotation);
            if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
            {
                this.image.RotateFlip(rotateFlipType);
            }
        }

        /// <inheritdoc />
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            using (var g = Graphics.FromImage(this.image))
            {
                foreach (var graphic in graphics)
                {
#pragma warning disable 618
                    var layer = graphic.RenderImage(null).As<Image>();
#pragma warning restore 618
                    g.DrawImage(layer, graphic.ScaledOffsetX, graphic.ScaledOffsetY, graphic.ScaledWidth, graphic.ScaledHeight);
                }
            }
        }

        /// <inheritdoc />
        public override IImage Clone()
        {
            return new WinFormsImage(
                this.width,
                this.height,
                new PinnedIntArray(this.pixels.Data),
                this.image == null ? null : new Bitmap(this.image));
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
                switch (rotation)
                {
                    case 90:
                        return RotateFlipType.Rotate90FlipXY;
                    case 180:
                        return RotateFlipType.Rotate180FlipXY;
                    case 270:
                        return RotateFlipType.Rotate270FlipXY;
                    default:
                        return RotateFlipType.RotateNoneFlipXY;
                }
            }

            if (flipX)
            {
                switch (rotation)
                {
                    case 90:
                        return RotateFlipType.Rotate90FlipX;
                    case 180:
                        return RotateFlipType.Rotate180FlipX;
                    case 270:
                        return RotateFlipType.Rotate270FlipX;
                    default:
                        return RotateFlipType.RotateNoneFlipX;
                }
            }

            if (flipY)
            {
                switch (rotation)
                {
                    case 90:
                        return RotateFlipType.Rotate90FlipY;
                    case 180:
                        return RotateFlipType.Rotate180FlipY;
                    case 270:
                        return RotateFlipType.Rotate270FlipY;
                    default:
                        return RotateFlipType.RotateNoneFlipY;
                }
            }

            switch (rotation)
            {
                case 90:
                    return RotateFlipType.Rotate90FlipNone;
                case 180:
                    return RotateFlipType.Rotate180FlipNone;
                case 270:
                    return RotateFlipType.Rotate270FlipNone;
                default:
                    return RotateFlipType.RotateNoneFlipNone;
            }
        }

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// The returned bitmap will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        /// <remarks>overridden only for obsolete warning</remarks>
        [Obsolete("do NOT invoke this method directly, use extention methods GetClonedBitmap, GetSharedBitmap, GetClonedWriteableBitmap instead.")]
#pragma warning disable 0809
        public override T As<T>()
#pragma warning restore 0809
        {
            return base.As<T>();
        }

        #endregion
    }
}