// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Collections.Generic;

    using Android.Graphics;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// <see cref="IImage"/> implementation of a Windows Forms <see cref="Bitmap"/>.
    /// </summary>
    public sealed class AndroidImage : IImage
    {
        #region FIELDS

        private Bitmap image;

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
        public AndroidImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            if (flipX || flipY || (rotation % 360) != 0)
            {
                using (var bitmap = Bitmap.CreateBitmap(pixels.Data, width, height, Bitmap.Config.Argb8888))
                {
                    var matrix = new Matrix();
                    matrix.PostRotate(rotation);
                    matrix.PostScale(flipX ? -1.0f : 1.0f, flipY ? -1.0f : 1.0f, flipX ? width : 0.0f, flipY ? height : 0.0f);

                    this.image =
                        Bitmap.CreateBitmap(bitmap, 0, 0, width, height, matrix, true)
                            .Copy(Bitmap.Config.Argb8888, true);
                }
            }
            else
            {
                this.image = Bitmap.CreateBitmap(pixels.Data, width, height, Bitmap.Config.Argb8888);
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
            return (T)(object)this.image;
        }

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public void DrawGraphics(IEnumerable<IGraphic> graphics)
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

        #endregion
    }
}