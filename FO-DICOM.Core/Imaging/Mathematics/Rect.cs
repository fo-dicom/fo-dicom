// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Numerics;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Representation of a floating-point rectangle.
    /// </summary>
    public struct Rect<T> where T : INumber<T>
    {

        /// <summary>
        /// Initializes an instance of <see cref="Rect"/>.
        /// </summary>
        /// <param name="x">The start x coordinate.</param>
        /// <param name="y">The start y coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rect(T x, T y, T width, T height)
        {
            X = x;
            Y = y;
            Width = (width >= T.Zero) ? width : throw new ArgumentOutOfRangeException(nameof(width), "Negative width not supported.");
            Height = (height >= T.Zero) ? height : throw new ArgumentOutOfRangeException(nameof(height), "Negative height not supported.");
        }


        /// <summary>
        /// Gets the rectangle start coordinate in X direction.
        /// </summary>
        public T X { get; set; }

        /// <summary>
        /// Gets the rectangle start coordinate in Y direction.
        /// </summary>
        public T Y { get; set; }

        /// <summary>
        /// Gets the rectangle width.
        /// </summary>
        public T Width { get; set; }

        /// <summary>
        /// Gets the rectangle height.
        /// </summary>
        public T Height { get; set; }


        /// <summary>
        /// Enlarges this <see cref="RectF"/> structure by the specified amount.
        /// </summary>
        /// <param name="x">The amount to inflate this <see cref="Rect"/> structure horizontally.</param>
        /// <param name="y">The amount to inflate this <see cref="Rect"/> structure vertically.</param>
        public void Inflate(T x, T y)
        {
            var two = T.One + T.One;
            if (x < -Width / two)
            {
                x = -Width / two;
            }
            if (y < -Height / two)
            {
                y = -Height / two;
            }

            X -= x;
            Y -= y;
            Width += two * x;
            Height += two * y;
        }

    }
}
