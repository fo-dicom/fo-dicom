// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Collections.Generic;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// Convenience class for non-generic access to <see cref="RawImage"/> image objects.
    /// </summary>
    public static class RawImageExtensions
    {
        /// <summary>
        /// Convenience method to access raw <see cref="IImage"/> instance as byte array.
        /// </summary>
        /// <param name="image"><see cref="IImage"/> object.</param>
        /// <returns>Byte array contents of <paramref name="image"/>.</returns>
        public static byte[] AsBytes(this IImage image)
        {
            return image.As<byte[]>();
        }
    }

    /// <summary>
    /// <see cref="IImage"/> implementation as a raw BGRA32 byte array.
    /// </summary>
    public sealed class RawImage : ImageBase<byte[]>
    {
        #region FIELDS

        private const int SizeOfBgra = 4;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="RawImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public RawImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="RawImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <param name="image">Raw byte array image.</param>
        private RawImage(int width, int height, PinnedIntArray pixels, byte[] image)
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
            var w = this.width;
            var h = this.height;

            this.image = ToBytes(ref w, ref h, components, flipX, flipY, rotation, this.pixels.Data);
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            var dstStride = SizeOfBgra * this.width;

            foreach (var graphic in graphics)
            {
                var srcStride = SizeOfBgra * graphic.ScaledWidth;
                var h = graphic.ScaledHeight;
                var x0 = graphic.ScaledOffsetX;
                var y0 = graphic.ScaledOffsetY;

                var layer = graphic.RenderImage(null).As<byte[]>();

                for (int y = 0, srcIdx = 0, dstIdx = y0 * dstStride + SizeOfBgra * x0;
                     y < h;
                     ++y, srcIdx += srcStride, dstIdx += dstStride)
                {
                    Array.Copy(layer, srcIdx, this.image, dstIdx, srcStride);
                }
            }
        }

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        public override IImage Clone()
        {
            byte[] bytes = null;
            if (this.image != null)
            {
                var length = this.image.Length;
                bytes = new byte[length];
                Array.Copy(this.image, bytes, length);
            }

            return new RawImage(this.width, this.height, new PinnedIntArray(this.pixels.Data), bytes);
        }

        #endregion
    }
}
