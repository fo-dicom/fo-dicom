// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Dicom.Log;

namespace Dicom.Imaging
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// <see cref="IImage"/> implementation of a Windows Forms <see cref="Image"/>.
    /// </summary>
    public sealed class WinFormsImage : IImage, IDisposable
    {
        #region FIELDS

        private PinnedIntArray pixelsCopy;
        private Bitmap image;
        private bool disposed;
        private readonly Logger logger;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WinFormsImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        public WinFormsImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            this.disposed = false;
            this.logger = LogManager.GetLogger("DICOM.Imaging.WinFormsImage");

            var format = components == 4 ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb;
            var stride = GetStride(width, format);

            //copy pixels and pass the copy to the bitmap
            this.pixelsCopy = new PinnedIntArray(pixels.Data);
            this.image = new Bitmap(width, height, stride, format, this.pixelsCopy.Pointer);

            var rotateFlipType = GetRotateFlipType(flipX, flipY, rotation);
            if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
            {
                this.image.RotateFlip(rotateFlipType);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        public T As<T>()
        {
            if (typeof(T) != typeof(Bitmap) || typeof(T) != typeof(Image))
            {
                throw new DicomImagingException("WinFormsImage cannot return images in format other than Bitmap or Image");
            }
            return (T)(object)this.image;
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            using (var g = Graphics.FromImage(this.image))
            {
                foreach (var graphic in graphics)
                {
                    var layer = graphic.RenderImage(null).As<Bitmap>();
                    g.DrawImage(layer, graphic.ScaledOffsetX, graphic.ScaledOffsetY, graphic.ScaledWidth, graphic.ScaledHeight);
                }
            }
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
            else if (flipX)
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
            else if (flipY)
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
            else
            {
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
        }

        #endregion

        public void Dispose()
        {
            if (this.image != null)
            {
                var i = this.image;
                this.image = null;
                i.Dispose();
            }
            if (this.pixelsCopy != null)
            {
                var pc = this.pixelsCopy;
                this.pixelsCopy = null;
                pc.Dispose();
            }
        }

        
    }
}