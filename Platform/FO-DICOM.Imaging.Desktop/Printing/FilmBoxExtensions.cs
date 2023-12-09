// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Drawing;
using FellowOakDicom.Imaging.Mathematics;

namespace FellowOakDicom.Printing
{

    /// <summary>
    /// Extension methods on instance of the <see cref="FilmBox"/> class.
    /// </summary>
    public static class FilmBoxExtensions
    {
        #region METHODS

        /// <summary>
        /// Get the film box size in inches.
        /// </summary>
        /// <param name="filmBox">Film box object.</param>
        /// <returns>Size in inches of film box.</returns>
        public static SizeF GetSizeInInch(this FilmBox filmBox)
        {
            // ReSharper disable once InconsistentNaming
            const float CM_PER_INCH = 2.54f;
            var filmSizeId = filmBox.FilmSizeID;

            if (filmSizeId.Contains("IN"))
            {
                var parts = filmSizeId.Split(new[] { "IN" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var width = parts[0].Replace('_', '.');
                    var height = parts[1].TrimStart('X').Replace('_', '.');

                    return new SizeF(float.Parse(width), float.Parse(height));
                }
            }
            else if (filmSizeId.Contains("CM"))
            {
                var parts = filmSizeId.Split(new[] { "CM" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var width = parts[0].Replace('_', '.');
                    var height = parts[1].TrimStart('X').Replace('_', '.');

                    return new SizeF(float.Parse(width) / CM_PER_INCH, float.Parse(height) / CM_PER_INCH);
                }
            }
            else if (filmSizeId == "A3")
            {
                return new SizeF(29.7f / CM_PER_INCH, 42.0f / CM_PER_INCH);
            }

            return new SizeF(210 / CM_PER_INCH, 297 / CM_PER_INCH);
        }

        /// <summary>
        /// Print a film box on specified graphics.
        /// </summary>
        /// <param name="filmBox">Film box.</param>
        /// <param name="graphics">Graphics on which to draw the film box.</param>
        /// <param name="marginBounds">Margin bounds.</param>
        /// <param name="imageResolution">Image resolution.</param>
        public static void Print(this FilmBox filmBox, Graphics graphics, Rectangle marginBounds, int imageResolution)
        {
            var parts = filmBox.ImageDisplayFormat.Split('\\', ',');

            if (parts.Length > 0)
            {
                RectF[] boxes = null;
                if (parts[0] == "STANDARD")
                {
                    boxes = FilmBox.PrintStandardFormat(parts, ToRectF(marginBounds));
                }
                else if (parts[0] == "ROW")
                {
                    boxes = FilmBox.PrintRowFormat(parts, ToRectF(marginBounds));
                }
                else if (parts[0] == "COL")
                {
                    boxes = FilmBox.PrintColumnFormat(parts, ToRectF(marginBounds));
                }

                if (boxes == null)
                {
                    throw new InvalidOperationException($"ImageDisplayFormat {filmBox.ImageDisplayFormat} invalid");
                }

                for (var i = 0; i < filmBox.BasicImageBoxes.Count; i++)
                {
                    filmBox.BasicImageBoxes[i].Print(graphics, boxes[i], imageResolution);
                }
            }
        }

        /// <summary>
        /// Convert <see cref="Rectangle"/> object into <see cref="RectF"/> object.
        /// </summary>
        /// <param name="rectangle">Rectangle to convert.</param>
        /// <returns>Rectangle expressed as <see cref="RectF"/>.</returns>
        private static RectF ToRectF(Rectangle rectangle) => new RectF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        #endregion
    }
}
