// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Drawing;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Mathematics;

namespace FellowOakDicom.Printing
{

    /// <summary>
    /// Extension methods to support <see cref="ImageBox"/> class.
    /// </summary>
    public static class ImageBoxExtensions
    {
        #region FIELDS

        //border in 100th of inches
        private const float _border = (float)(100 * 2 / 25.4);

        #endregion

        #region METHODS

        /// <summary>
        /// Prints an image box onto the specified graphics.
        /// </summary>
        /// <param name="imageBox">Image box to print.</param>
        /// <param name="graphics">Graphics in which image box should be contained.</param>
        /// <param name="box">Rectangle within which the image box should be contained.</param>
        /// <param name="imageResolution">Image resolution.</param>
        public static void Print(this ImageBox imageBox, Graphics graphics, RectF box, int imageResolution)
        {
            var state = graphics.Save();

            FillBox(imageBox.FilmBox, box, graphics);

            var boxCopy = box;
            if (imageBox.FilmBox.Trim == "YES")
            {
                boxCopy.Inflate(-_border, -_border);
            }

            if (imageBox.ImageSequence != null && imageBox.ImageSequence.Contains(DicomTag.PixelData))
            {
                Image bitmap = null;
                try
                {
                    var image = new DicomImage(imageBox.ImageSequence);
                    var frame = image.RenderImage().As<Image>();

                    bitmap = frame;

                    DrawBitmap(graphics, boxCopy, bitmap, imageResolution, imageBox.FilmBox.EmptyImageDensity);
                }
                finally
                {
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                    }
                }
            }

            graphics.Restore(state);
        }

        /// <summary>
        /// If requested, fill the box with black color.
        /// </summary>
        /// <param name="filmBox">Film box.</param>
        /// <param name="box">Rectangle.</param>
        /// <param name="graphics">Graphics.</param>
        private static void FillBox(FilmBox filmBox, RectF box, Graphics graphics)
        {
            if (filmBox.EmptyImageDensity == "BLACK")
            {
                RectF fillBox = box;
                if (filmBox.BorderDensity == "WHITE" && filmBox.Trim == "YES")
                {
                    fillBox.Inflate(-_border, -_border);
                }
                using var brush = new SolidBrush(Color.Black);
                graphics.FillRectangle(brush, fillBox.X, fillBox.Y, fillBox.Width, fillBox.Height);
            }
        }

        /// <summary>
        /// Draw image bitmap in the specified rectangle.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        /// <param name="box">Rectangle in which to draw.</param>
        /// <param name="bitmap">Image to draw.</param>
        /// <param name="imageResolution">Image resolution.</param>
        /// <param name="emptyImageDensity">Empty image density.</param>
        private static void DrawBitmap(Graphics graphics, RectF box, Image bitmap, int imageResolution, string emptyImageDensity)
        {
            var imageWidthInInch = 100 * bitmap.Width / imageResolution;
            var imageHeightInInch = 100 * bitmap.Height / imageResolution;
            double factor = Math.Min(box.Height / imageHeightInInch, box.Width / imageWidthInInch);

            if (factor > 1)
            {
                var targetWidth = (int)(imageResolution * box.Width / 100);
                var targetHeight = (int)(imageResolution * box.Height / 100);


                using var membmp = new Bitmap(targetWidth, targetHeight);
                membmp.SetResolution(imageResolution, imageResolution);

                using var memg = Graphics.FromImage(membmp);
                memg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                memg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (emptyImageDensity == "BLACK")
                {
                    using var brush = new SolidBrush(Color.Black);
                    memg.FillRectangle(brush, 0, 0, targetWidth, targetHeight);
                }

                factor = Math.Min(
                    targetHeight / (double)bitmap.Height,
                    targetWidth / (double)bitmap.Width);

                var x = (float)((targetWidth - bitmap.Width * factor) / 2.0f);
                var y = (float)((targetHeight - bitmap.Height * factor) / 2.0f);
                var width = (float)(bitmap.Width * factor);
                var height = (float)(bitmap.Height * factor);

                memg.DrawImage(bitmap, x, y, width, height);
                graphics.DrawImage(membmp, box.X, box.Y, box.Width, box.Height);
            }
            else
            {
                var x = box.X + (float)(box.Width - imageWidthInInch * factor) / 2.0f;
                var y = box.Y + (float)(box.Height - imageHeightInInch * factor) / 2.0f;
                var width = (float)(imageWidthInInch * factor);
                var height = (float)(imageHeightInInch * factor);

                graphics.DrawImage(bitmap, x, y, width, height);
            }
        }

        #endregion
    }
}
