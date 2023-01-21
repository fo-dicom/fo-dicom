// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Representation of a spatial 2D transform.
    /// </summary>
    public class SpatialTransform
    {

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of <see cref="SpatialTransform"/>.
        /// </summary>
        public SpatialTransform()
        {
            Pan = new Point2(0, 0);
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
        public Point2 Pan { get; set; }

        /// <summary>
        /// Gets whether the transform is set or reset.
        /// </summary>
        public bool IsTransformed => Scale != 1.0 || Rotation != 0 || !Pan.Equals(Point2.Origin);

        #endregion

        #region Public Members

        /// <summary>
        /// Add further rotation to the transform.
        /// </summary>
        /// <param name="angle">Angle with which to rotate.</param>
        public void Rotate(int angle)
        {
            Rotation += angle;
        }

        /// <summary>
        /// Reset the transform.
        /// </summary>
        public void Reset()
        {
            Scale = 1.0;
            Rotation = 0;
            FlipX = false;
            FlipY = false;
            Pan.X = 0;
            Pan.Y = 0;
        }

        #endregion
    }
}
