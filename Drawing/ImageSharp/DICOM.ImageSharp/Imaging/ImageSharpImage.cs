// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.Render;
using Dicom.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Dicom.Imaging
{
    /// <summary>
    /// Convenience class for non-generic access to <see cref="ImageSharpImage"/> image objects.
    /// </summary>
    public static class ImageSharpImageExtensions
    {

        /// <summary>
        /// Convenience method to access ImageSharpImage <see cref="IImage"/> instance as ImageSharp <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="iimage"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Image"/> contents of <paramref name="image"/>.</returns>
        public static Image<Bgra32> AsSharpImage(this IImage iimage)
        {
            return (iimage as ImageSharpImage).RenderedImage;
        }

    }


    /// <summary>
    /// <see cref="IImage"/> implementation of a <see cref="Image"/> in the <code>SixLabors.ImageSharp</code> namespace.
    /// </summary>
    public class ImageSharpImage : ImageBase<Image<Bgra32>>
    {

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="ImageSharpImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public ImageSharpImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ImageSharpImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Pixel array.</param>
        /// <param name="image">Bitmap image.</param>
        private ImageSharpImage(int width, int height, PinnedIntArray pixels, Image<Bgra32> image)
            : base(width, height, pixels, image)
        {
        }

        #endregion

        #region Properties

        public Image<Bgra32> RenderedImage => image;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public override void Render(int components, bool flipX, bool flipY, int rotation)
        {
            var data = new byte[pixels.ByteSize];
            Marshal.Copy(pixels.Pointer, data, 0, pixels.ByteSize);
            image = Image.LoadPixelData<Bgra32>(data, width, height);
            if (rotation != 0 || flipX || flipY)
            {
                image.Mutate(x => x.RotateFlip(GetRotateMode(rotation), GetFlipMode(flipX, flipY)));
            }
        }

        private FlipMode GetFlipMode(bool flipX, bool flipY)
        {
            var flipMode = FlipMode.None;
            if (flipX)
            {
                flipMode |= FlipMode.Horizontal;
            }
            if (flipY)
            {
                flipMode |= FlipMode.Vertical;
            }
            return flipMode;
        }

        private RotateMode GetRotateMode(int rotation)
        {
            switch (rotation)
            {
                case 90: return RotateMode.Rotate90;
                case 180: return RotateMode.Rotate180;
                case 270: return RotateMode.Rotate270;
                default: return RotateMode.None;
            }
        }

        /// <inheritdoc />
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                var layer = (graphic.RenderImage(null) as ImageSharpImage).image;
                image.Mutate(ctx => ctx
                    .DrawImage(layer, new SixLabors.Primitives.Point(graphic.ScaledOffsetX, graphic.ScaledOffsetY), 1));
            }
        }
 

        /// <inheritdoc />
        public override IImage Clone()
        {
            return new ImageSharpImage(width, height, new PinnedIntArray(pixels.Data), image?.Clone());
        }

        #endregion

    }
}
