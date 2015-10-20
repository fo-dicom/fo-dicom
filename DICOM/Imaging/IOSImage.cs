// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Collections.Generic;

    using CoreGraphics;

    using CoreImage;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    using UIKit;

    /// <summary>
    /// <see cref="IImage"/> implementation of a Windows Forms <see cref="CGImage"/>.
    /// </summary>
    public sealed class IOSImage : IImage
    {
        #region FIELDS

        private CGImage image;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="IOSImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        public IOSImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            using (
                var context = new CGBitmapContext(
                    pixels.Pointer,
                    width,
                    height,
                    8,
                    4 * width,
                    CGColorSpace.CreateDeviceRGB(),
                    CGImageAlphaInfo.PremultipliedLast))
            {
                var transform = CGAffineTransform.MakeRotation((float)(rotation * Math.PI / 180.0));
                transform.Scale(flipX ? -1.0f : 1.0f, flipY ? -1.0f : 1.0f);
                transform.Translate(flipX ? width : 0.0f, flipY ? height : 0.0f);
                context.ConcatCTM(transform);

                this.image = context.ToImage();
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
            if (typeof(T) == typeof(UIImage))
            {
                return (T)(object)new UIImage(this.image);
            }
            if (typeof(T) == typeof(CIImage))
            {
                return (T)(object)new CIImage(this.image);
            }
            return (T)(object)this.image;
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            using (
                var context = new CGBitmapContext(
                    IntPtr.Zero,
                    this.image.Width,
                    this.image.Height,
                    this.image.BitsPerComponent,
                    this.image.BytesPerRow,
                    this.image.ColorSpace,
                    this.image.AlphaInfo))
            {
                context.DrawImage(new CGRect(0.0, 0.0, this.image.Width, this.image.Height), this.image);

                foreach (var graphic in graphics)
                {
                    var layer = graphic.RenderImage(null).As<CGImage>();
                    context.DrawImage(
                        new CGRect(
                            graphic.ScaledOffsetX,
                            graphic.ScaledOffsetY,
                            graphic.ScaledWidth,
                            graphic.ScaledHeight),
                        layer);
                }

                this.image = context.ToImage();
            }
        }

        #endregion
    }
}