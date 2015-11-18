// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// <see cref="IImage"/> implementation of a WPF <see cref="ImageSource"/>.
    /// </summary>
    public sealed class WPFImage : IImage
    {
        #region FIELDS

        private const int DPI = 96;

        private readonly int width;

        private readonly int height;

        private WriteableBitmap image;

        private PinnedIntArray pixels;

        private bool disposed;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WPFImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        internal WPFImage(int width, int height)
            : this(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="WPFImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <param name="image">Writeable bitmap image.</param>
        private WPFImage(int width, int height, PinnedIntArray pixels, WriteableBitmap image)
        {
            this.width = width;
            this.height = height;
            this.pixels = pixels;
            this.image = image;
            this.disposed = false;
        }

        #endregion

        #region PROPERTIES

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

        #endregion

        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        public T As<T>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(WriteableBitmap)))
            {
                throw new DicomImagingException("WPFImage cannot return images in format other than WriteableBitmap");
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
            var bitmap = CreateBitmap(this.width, this.height, components, this.pixels.Data);
            this.image = ApplyFlipRotate(bitmap, flipX, flipY, rotation);
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                var layer = graphic.RenderImage(null).As<WriteableBitmap>();

                var overlay = new int[graphic.ScaledWidth * graphic.ScaledHeight];
                var stride = 4 * graphic.ScaledWidth;
                layer.CopyPixels(overlay, stride, 0);

                this.image.WritePixels(
                    new Int32Rect(
                        graphic.ScaledOffsetX,
                        graphic.ScaledOffsetY,
                        graphic.ScaledWidth,
                        graphic.ScaledHeight),
                    overlay,
                    stride,
                    0);
            }
        }

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        public IImage Clone()
        {
            return new WPFImage(
                this.width,
                this.height,
                new PinnedIntArray(this.pixels.Data),
                this.image == null ? null : new WriteableBitmap(this.image));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed) return;

            this.image = null;

            if (this.pixels != null)
            {
                this.pixels.Dispose();
                this.pixels = null;
            }

            this.disposed = true;
        }

        private static WriteableBitmap CreateBitmap(int width, int height, int components, int[] pixelData)
        {
            var format = components == 4 ? PixelFormats.Bgra32 : PixelFormats.Bgr32;
            var bitmap = new WriteableBitmap(width, height, DPI, DPI, format, null);

            // Reserve the back buffer for updates.
            bitmap.Lock();

            Marshal.Copy(pixelData, 0, bitmap.BackBuffer, pixelData.Length);

            // Specify the area of the bitmap that changed.
            bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));

            // Release the back buffer and make it available for display.
            bitmap.Unlock();

            return bitmap;
        }

        private static WriteableBitmap ApplyFlipRotate(WriteableBitmap bitmap, bool flipX, bool flipY, int rotation)
        {
            if (rotation == 0 && !flipX && !flipY)
            {
                return bitmap;
            }

            var rotFlipTransform = new TransformGroup();
            rotFlipTransform.Children.Add(new RotateTransform(rotation));
            rotFlipTransform.Children.Add(new ScaleTransform(flipX ? -1 : 1, flipY ? -1 : 1));

            return new WriteableBitmap(new TransformedBitmap(bitmap, rotFlipTransform));
        }

        #endregion
    }
}