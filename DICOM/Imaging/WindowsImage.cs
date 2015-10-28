// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Dicom.Imaging
{
    using System.Collections.Generic;
    using System.IO;

    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// <see cref="IImage"/> implementation of a Universal Windows Platform <see cref="ImageSource"/>.
    /// </summary>
    public sealed class WindowsImage : IImage
    {
        #region FIELDS

        private readonly WriteableBitmap image;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="WindowsImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        public WindowsImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            var buffer = CreateBuffer(ref width, ref height, components, flipX, flipY, rotation, pixels.Data);
            this.image = Create(width, height, buffer);
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

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            var dstStride = 4 * this.image.PixelWidth;

            using (var stream = this.image.PixelBuffer.AsStream())
            {
                foreach (var graphic in graphics)
                {
                    var layer = graphic.RenderImage(null).As<WriteableBitmap>();
                    var srcStride = 4 * graphic.ScaledWidth;

                    var pixels = layer.PixelBuffer.ToArray();

                    int srcOffs = 0, dstOffs = dstStride * graphic.ScaledOffsetY + 4 * graphic.ScaledOffsetX;
                    for (var j = 0; j < graphic.ScaledHeight; ++j, srcOffs += srcStride, dstOffs += dstStride)
                    {
                        stream.Seek(dstOffs, SeekOrigin.Begin);
                        stream.Write(pixels, srcOffs, graphic.ScaledWidth);
                    }
                }
            }
        }

        private static byte[] CreateBuffer(
            ref int width,
            ref int height,
            int components,
            bool flipX,
            bool flipY,
            int rotation,
            int[] data)
        {
            var processed = Rotate(ref width, ref height, rotation, data);
            processed = Flip(width, height, flipX, flipY, processed);

            // TODO Consider to make use of "components"
            var length = 4 * width * height;
            var buffer = new byte[length];

            Buffer.BlockCopy(processed, 0, buffer, 0, length);
            return buffer;
        }

        private static int[] Rotate(ref int width, ref int height, int angle, int[] data)
        {
            int[] result;
            angle %= 360;

            var i = 0;
            if (angle > 0 && angle <= 90)
            {
                result = new int[width * height];
                for (var x = 0; x < width; x++)
                {
                    for (var y = height - 1; y >= 0; y--, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
                var tmp = width;
                width = height;
                height = tmp;
            }
            else if (angle > 90 && angle <= 180)
            {
                result = new int[width * height];
                for (var y = height - 1; y >= 0; y--)
                {
                    for (var x = width - 1; x >= 0; x--, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
            }
            else if (angle > 180 && angle <= 270)
            {
                result = new int[width * height];
                for (var x = width - 1; x >= 0; x--)
                {
                    for (var y = 0; y < height; y++, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
                var tmp = width;
                width = height;
                height = tmp;
            }
            else
            {
                result = data;
            }
            return result;
        }

        private static int[] Flip(int w, int h, bool flipX, bool flipY, int[] p)
        {
            var i = 0;
            int[] tmp, result;

            if (flipX)
            {
                tmp = new int[w * h];
                for (var y = h - 1; y >= 0; y--)
                {
                    for (var x = 0; x < w; x++, i++)
                    {
                        tmp[i] = p[y * w + x];
                    }
                }
            }
            else
            {
                tmp = p;
            }
            if (flipY)
            {
                result = new int[w * h];
                for (var y = 0; y < h; y++)
                {
                    for (var x = w - 1; x >= 0; x--, i++)
                    {
                        result[i] = tmp[y * w + x];
                    }
                }
            }
            else
            {
                result = tmp;
            }

            return result;
        }

        private static WriteableBitmap Create(int width, int height, byte[] buffer)
        {
            var bitmap = new WriteableBitmap(width, height);
            using (var stream = bitmap.PixelBuffer.AsStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                return bitmap;
            }
        }

        #endregion
    }
}