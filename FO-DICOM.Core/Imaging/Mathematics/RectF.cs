// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Representation of a floating-point rectangle.
    /// </summary>
    public struct RectF
    {
        #region FIELDS

        private float x;

        private float y;

        private float width;

        private float height;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="RectF"/>.
        /// </summary>
        /// <param name="x">The start x coordinate.</param>
        /// <param name="y">The start y coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RectF(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = (width >= 0f) ? width : throw new ArgumentOutOfRangeException(nameof(width), "Negative width not supported.");
            this.height = (height >= 0f) ? height : throw new ArgumentOutOfRangeException(nameof(height), "Negative height not supported.");
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the rectangle start coordinate in X direction.
        /// </summary>
        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets the rectangle start coordinate in Y direction.
        /// </summary>
        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets the rectangle width.
        /// </summary>
        public float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        /// <summary>
        /// Gets the rectangle height.
        /// </summary>
        public float Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Enlarges this <see cref="RectF"/> structure by the specified amount.
        /// </summary>
        /// <param name="x">The amount to inflate this <see cref="RectF"/> structure horizontally.</param>
        /// <param name="y">The amount to inflate this <see cref="RectF"/> structure vertically.</param>
        public void Inflate(float x, float y)
        {
            if (x < -this.width / 2.0f)
            {
                x = -this.width / 2.0f;
            }
            if (y < -this.height / 2.0f)
            {
                y = -this.height / 2.0f;
            }

            this.x -= x;
            this.y -= y;
            this.width += 2.0f * x;
            this.height += 2.0f * y;
        }

        #endregion
    }
}
