// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Collections.Generic;

    using Android.Graphics;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// Convenience class for non-generic access to <see cref="AndroidImage"/> image objects.
    /// </summary>
    public static class AndroidImageExtensions
    {
        /// <summary>
        /// Convenience method to access Android <see cref="IImage"/> instance as Android <see cref="Bitmap"/>.
        /// The returned <see cref="Bitmap"/> will be disposed when the <see cref="IImage"/> is disposed.
        /// </summary>
        /// <param name="image"><see cref="IImage"/> object.</param>
        /// <returns><see cref="Bitmap"/> contents of <paramref name="image"/>.</returns>
        public static Bitmap AsBitmap(this IImage image)
        {
            return image.As<Bitmap>();
        }
    }

    /// <summary>
    /// <see cref="IImage"/> implementation of a Windows Forms <see cref="Bitmap"/>.
    /// </summary>
    public sealed class AndroidImage : ImageDisposableBase<Bitmap>
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="AndroidImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        public AndroidImage(int width, int height)
            : base(width, height, new PinnedIntArray(width * height), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AndroidImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <param name="image">Bitmap image.</param>
        private AndroidImage(int width, int height, PinnedIntArray pixels, Bitmap image)
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
            if (flipX || flipY || (rotation % 360) != 0)
            {
                using (var bitmap = Bitmap.CreateBitmap(this.pixels.Data, this.width, this.height, Bitmap.Config.Argb8888))
                {
                    var matrix = new Matrix();
                    matrix.PostRotate(rotation);
                    matrix.PostScale(
                        flipX ? -1.0f : 1.0f,
                        flipY ? -1.0f : 1.0f,
                        flipX ? this.width : 0.0f,
                        flipY ? this.height : 0.0f);

                    this.image =
                        Bitmap.CreateBitmap(bitmap, 0, 0, this.width, this.height, matrix, true)
                            .Copy(Bitmap.Config.Argb8888, true);
                }
            }
            else
            {
                this.image = Bitmap.CreateBitmap(this.pixels.Data, this.width, this.height, Bitmap.Config.Argb8888);
            }
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            using (var canvas = new Canvas(this.image))
            {
                foreach (var graphic in graphics)
                {
                    var layer = graphic.RenderImage(null).As<Bitmap>();
                    canvas.DrawBitmap(layer, 0, 0, null);
                }
            }
        }

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        public override IImage Clone()
        {
            return new AndroidImage(
                this.width,
                this.height,
                new PinnedIntArray(this.pixels.Data),
                this.image == null ? null : Bitmap.CreateBitmap(this.image));
        }

        #endregion
    }
}
