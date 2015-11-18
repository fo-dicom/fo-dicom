// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
    public sealed class WinFormsImage : IImage
    {
        #region FIELDS

        private readonly int width;

        private readonly int height;

        private PinnedIntArray pixels;

        private Bitmap image;

        private bool disposed;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WinFormsImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        internal WinFormsImage(int width, int height)
            : this(width, height, new PinnedIntArray(width * height), null)
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
        {
            this.width = width;
            this.height = height;
            this.pixels = pixels;
            this.image = image;
            this.disposed = false;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets the array of pixels associated with the image.
        /// </summary>
        public PinnedIntArray Pixels
        {
            get
            {
                return this.pixels;
            }
        }

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        public T As<T>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(Bitmap)))
            {
                throw new DicomImagingException("WinFormsImage cannot return images in format other than Bitmap or Image");
            }

            return (T)(object)this.image;
        }

        /// <summary>
        /// Renders the image given the specified parameters.
        /// </summary>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        public void Render(int components, bool flipX, bool flipY, int rotation)
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
                    var layer = graphic.RenderImage(null).As<Image>();
                    g.DrawImage(layer, graphic.ScaledOffsetX, graphic.ScaledOffsetY, graphic.ScaledWidth, graphic.ScaledHeight);
                }
            }
        }

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        public IImage Clone()
        {
            return new WinFormsImage(
                this.width,
                this.height,
                new PinnedIntArray(this.pixels.Data),
                this.image == null ? null : new Bitmap(this.image));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed) return;

            if (this.image != null)
            {
                this.image.Dispose();
                this.image = null;
            }

            if (this.pixels != null)
            {
                this.pixels.Dispose();
                this.pixels = null;
            }

            this.disposed = true;
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
    }
}