// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Dicom.IO;

    /// <summary>
    /// <see cref="IImage"/> implementation of a WPF <see cref="ImageSource"/>.
    /// </summary>
    public sealed class WPFImage : IImage
    {
        #region FIELDS

        private const int DPI = 96;

        private readonly ImageSource image;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WPFImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        public WPFImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            var bitmap = CreateBitmap(width, height, components, pixels.Data);
            this.image = ApplyFlipRotate(bitmap, flipX, flipY, rotation);
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
            return (T)(object)this.image;
        }

        private static BitmapSource CreateBitmap(int width, int height, int components, int[] pixelData)
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

        private static ImageSource ApplyFlipRotate(BitmapSource bitmap, bool flipX, bool flipY, int rotation)
        {
            if (rotation == 0 && !flipX && !flipY)
            {
                return bitmap;
            }

            var rotFlipTransform = new TransformGroup();
            rotFlipTransform.Children.Add(new RotateTransform(rotation));
            rotFlipTransform.Children.Add(new ScaleTransform(flipX ? -1 : 1, flipY ? -1 : 1));
            return new TransformedBitmap(bitmap, rotFlipTransform);
        }

        #endregion
    }
}