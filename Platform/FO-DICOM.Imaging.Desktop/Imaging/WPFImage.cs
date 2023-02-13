// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Convenience class for non-generic access to <see cref="WPFImage"/> image objects.
    /// </summary>
    public static class WPFImageExtensions
    {
        /// <summary>
        /// Convenience method to access WPF <see cref="IImage"/> instance as WPF <see cref="WriteableBitmap"/>.
        /// The returned <see cref="WriteableBitmap"/> will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <param name="image"><see cref="IImage"/> object.</param>
        /// <returns><see cref="WriteableBitmap"/> contents of <paramref name="image"/>.</returns>
        public static WriteableBitmap AsWriteableBitmap(this IImage image)
        {
            return image.As<WriteableBitmap>();
        }
    }

    /// <summary>
    /// <see cref="IImage"/> implementation of a WPF <see cref="ImageSource"/>.
    /// </summary>
    public sealed class WPFImage : ImageBase<WriteableBitmap>
    {
        #region FIELDS

        private const double DPI = 96;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WPFImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public WPFImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
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
            : base(width, height, pixels, image)
        {
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Renders the image given the specified parameters.
        /// </summary>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        public override void Render(int components, bool flipX, bool flipY, int rotation)
        {
            var bitmap = CreateBitmap(_width, _height, components, _pixels.Data);
            _image = ApplyFlipRotate(bitmap, flipX, flipY, rotation);
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                var layer = graphic.RenderImage(null).As<WriteableBitmap>();

                var overlay = new int[graphic.ScaledWidth * graphic.ScaledHeight];
                var stride = 4 * graphic.ScaledWidth;
                layer.CopyPixels(overlay, stride, 0);

                _image.WritePixels(
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
        public override IImage Clone()
        {
            return new WPFImage(
                _width,
                _height,
                new PinnedIntArray(_pixels.Data),
                _image == null ? null : new WriteableBitmap(_image));
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
