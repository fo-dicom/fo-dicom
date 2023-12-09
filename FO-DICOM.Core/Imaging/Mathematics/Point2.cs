// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Coordinate in 2D space with integer values
    /// </summary>
    public class Point2 : IComparable<Point2>, IEquatable<Point2>
    {
        public static readonly Point2 Origin = new Point2();

        public Point2()
        {
        }

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Position on X axis</summary>
        public int X { get; set; }

        /// <summary>Position on Y axis</summary>
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is Point2)
            {
                Point2 other = obj as Point2;
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public bool Equals(Point2 other)
        {
            if (other == null) return false;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode() => X ^ Y;

        /// <summary>Gets a human-readable string representing this <see cref="Point2"/> object.</summary>
        /// <returns>String representation</returns>
        public override string ToString() => string.Format("({0},{1})", X, Y);

        /// <summary>IComparable interface implementation</summary>
        /// <param name="other">Point to compare</param>
        /// <returns>Compare result</returns>
        public int CompareTo(Point2 other)
        {
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            return 0;
        }
    }
}
