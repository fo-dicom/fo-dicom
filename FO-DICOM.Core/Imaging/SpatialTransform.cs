// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Dicom.Imaging.Mathematics;

    /// <summary>
    /// Representation of a spatial 2D transform.
    /// </summary>
    public class SpatialTransform
    {
        #region Private Members

        private Point2 _pan;

        #endregion

        #region Public Constructors
        
        /// <summary>
        /// Initializes an instance of <see cref="SpatialTransform"/>.
        /// </summary>
        public SpatialTransform()
        {
            _pan = new Point2(0, 0);
            Reset();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the scale of the transform.
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the transform.
        /// </summary>
        public int Rotation { get; set; }

        /// <summary>
        /// Gets or sets whether to flip in X direction.
        /// </summary>
        public bool FlipX { get; set; }

        /// <summary>
        /// Gets or sets whether to flip in Y direction.
        /// </summary>
        public bool FlipY { get; set; }

        /// <summary>
        /// Gets or sets the pan of the transform.
        /// </summary>
        public Point2 Pan
        {
            get
            {
                return _pan;
            }
            set
            {
                _pan = value;
            }
        }

        /// <summary>
        /// Gets whether the transform is set or reset.
        /// </summary>
        public bool IsTransformed
        {
            get
            {
                return this.Scale != 1.0 || this.Rotation != 0 || !this.Pan.Equals(Point2.Origin);
            }
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Add further rotation to the transform.
        /// </summary>
        /// <param name="angle">Angle with which to rotate.</param>
        public void Rotate(int angle)
        {
            this.Rotation += angle;
        }

        /// <summary>
        /// Reset the transform.
        /// </summary>
        public void Reset()
        {
            this.Scale = 1.0;
            this.Rotation = 0;
            this.FlipX = false;
            this.FlipY = false;
            _pan.X = 0;
            _pan.Y = 0;
        }

        #endregion
    }
}
