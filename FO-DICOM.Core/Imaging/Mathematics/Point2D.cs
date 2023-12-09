// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Coordinate in 2D space with floating point values
    /// </summary>
    public class Point2D : IComparable<Point2D>, IEquatable<Point2D>
    {
        public static readonly Point2D Origin = new Point2D();

        public Point2D()
        {
        }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Position on X axis</summary>
        public double X { get; set; }

        /// <summary>Position on Y axis</summary>
        public double Y { get; set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is Point2D other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }


        public bool Equals(Point2D other)
        {
            if (other == null) return false;
            return X == other.X && Y == other.Y;
        }


        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();


        /// <summary>Gets a human-readable string representing this <see cref="Point2D"/> object.</summary>
        /// <returns>String representation</returns>
        public override string ToString() => string.Format("({0},{1})", X, Y);


        /// <summary>IComparable interface implementation</summary>
        /// <param name="other">Point to compare</param>
        /// <returns>Compare result</returns>
        public int CompareTo(Point2D other)
        {
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            return 0;
        }


        public Point2 Round() => new Point2((int)Math.Round(X), (int)Math.Round(Y));

    }
}
