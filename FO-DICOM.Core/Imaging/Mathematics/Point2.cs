// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Numerics;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Coordinate in 2D space with integer values
    /// </summary>
    public sealed class Point2 : IComparable<Point2>, IEquatable<Point2>
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
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Point2 other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public bool Equals(Point2 other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode() => X ^ Y;

        /// <summary>Gets a human-readable string representing this <see cref="Point2"/> object.</summary>
        /// <returns>String representation</returns>
        public override string ToString() => string.Format("({0},{1})", X, Y);

        /// <summary>IComparable interface implementation</summary>
        /// <param name="other">Point to compare</param>
        /// <returns>Compare result</returns>
        public int CompareTo(Point2 other) => other switch
        {
            { X: var x } when X < x => -1,
            { X: var x } when X > x => 1,
            { Y: var y } when Y < y => -1,
            { Y: var y } when Y > y => 1,
            _ => 0
        };
    }


    /// <summary>
    /// Coordinate in 2D space with floating point values
    /// </summary>
    public sealed class Point2<T> : IComparable<Point2<T>>, IEquatable<Point2<T>> where T : INumber<T>, IFloatingPoint<T>
    {
        public static readonly Point2<T> Origin = new Point2<T>();

        public Point2()
        {
        }

        public Point2(T x, T y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Position on X axis</summary>
        public T X { get; set; }

        /// <summary>Position on Y axis</summary>
        public T Y { get; set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is Point2<T> other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }


        public bool Equals(Point2<T> other)
        {
            if (other == null) return false;
            return X == other.X && Y == other.Y;
        }


        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();


        /// <summary>Gets a human-readable string representing this <see cref="Point2"/> object.</summary>
        /// <returns>String representation</returns>
        public override string ToString() => string.Format("({0},{1})", X, Y);


        /// <summary>IComparable interface implementation</summary>
        /// <param name="other">Point to compare</param>
        /// <returns>Compare result</returns>
        public int CompareTo(Point2<T> other)
        {
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            return 0;
        }


        public Point2<T> Round() => new Point2<T>(T.Round(X), T.Round(Y));

    }

}
