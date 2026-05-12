// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Numerics;

namespace FellowOakDicom.Imaging.Mathematics
{

    public static class Constants
    {

        public static readonly double Epsilon = 0.000000001; // the epsilon in mm to check if a value is quasi zero

        public static readonly decimal EpsilonM = 0.000001m; // the epsilon in mm to check if a value is quasi zero

    }

    public class Vector3<T> where T : INumber<T>
    {
        #region Constants

        public static readonly Vector3<T> Zero = new Vector3<T>(T.Zero, T.Zero, T.Zero);

        public static readonly Vector3<T> AxisX = new Vector3<T>(T.One, T.Zero, T.Zero);

        public static readonly Vector3<T> AxisY = new Vector3<T>(T.Zero, T.One, T.Zero);

        public static readonly Vector3<T> AxisZ = new Vector3<T>(T.Zero, T.Zero, T.One);

        #endregion

        #region Public Constructors

        public Vector3()
        {
        }

        public Vector3(Vector3<T> v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(T[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3(T[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public T X { get; set; }

        public T Y { get; set; }

        public T Z { get; set; }

        #endregion

        #region Public Methods

        public bool IsZero
            => T.IsZero(X) && T.IsZero(Y) && T.IsZero(Z);

        public T Length()
            => T.CreateTruncating(Math.Sqrt(double.CreateChecked((X * X) + (Y * Y) + (Z * Z))));

        public T Magnitude()
            => T.CreateTruncating(Math.Sqrt(double.CreateChecked(DotProduct(this))));

        public Vector3<T> Normalize()
            => this * (T.One / Magnitude());

        public T DotProduct(Vector3<T> b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public T DotProduct(Point3<T> b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public Vector3<T> CrossProduct(Vector3<T> b)
            => new Vector3<T>((Y * b.Z) - (Z * b.Y), (Z * b.X) - (X * b.Z), (X * b.Y) - (Y * b.X));

        public T Distance(Vector3<T> b)
            => T.CreateTruncating(Math.Sqrt(double.CreateChecked((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z))));

        public bool IsPerpendicular(Vector3<T> b)
            => DotProduct(b) == T.Zero;

        public static Vector3<T> Max(Vector3<T> a, Vector3<T> b)
            => (a >= b) ? a : b;

        public static Vector3<T> Min(Vector3<T> a, Vector3<T> b)
            => (a <= b) ? a : b;

        public Vector3<T> Rotate(Vector3<T> axis, T angle)
        {
            axis = axis.Normalize();
            Vector3<T> parallel = axis * DotProduct(axis);
            Vector3<T> perpendicular = this - parallel;
            Vector3<T> mutualPerpendicular = axis.CrossProduct(perpendicular);
            Vector3<T> rotatePerpendicular = (perpendicular * T.CreateTruncating(Math.Cos(double.CreateChecked(angle)))) + (mutualPerpendicular * T.CreateTruncating(Math.Sin(double.CreateChecked(angle))));
            return rotatePerpendicular + parallel;
        }

        public Vector3<T> Reflect(Vector3<T> normal)
        {
            T dot = DotProduct(normal);
            return new Vector3<T>(
                X - ((dot * (T.One + T.One)) * normal.X),
                Y - ((dot * (T.One + T.One)) * normal.Y),
                Z - ((dot * (T.One + T.One)) * normal.Z));
        }

        public Vector3<T> NearestAxis()
        {
            var b = Zero.Clone();
            T xabs = T.Abs(X);
            T yabs = T.Abs(Y);
            T zabs = T.Abs(Z);

            if (xabs >= yabs && xabs >= zabs)
            {
                b.X = (X > T.Zero) ? T.One : -T.One;
            }
            else if (yabs >= zabs)
            {
                b.Y = (Y > T.Zero) ? T.One : -T.One;
            }
            else
            {
                b.Z = (Z > T.Zero) ? T.One : -T.One;
            }

            return b;
        }

        public override int GetHashCode() => (X + Y + Z).GetHashCode();

        public override bool Equals(object obj) => obj is Vector3<T> other && this == other;

        public override string ToString() => $"({X}, {Y}, {Z})";

        public Vector3<T> Clone() => new Vector3<T>(X, Y, Z);

        public Point3<T> ToPoint() => new Point3<T>(X, Y, Z);

        public T[] ToArray() => [X, Y, Z];

        #endregion

        #region Operators

        public static Vector3<T> operator +(Vector3<T> a, Vector3<T> b)
        {
            return new Vector3<T>(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3<T> operator -(Vector3<T> a, Vector3<T> b)
        {
            return new Vector3<T>(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3<T> operator *(Vector3<T> a, T b)
        {
            return new Vector3<T>(a.X * b, a.Y * b, a.Z * b);
        }

        public static T operator *(Vector3<T> a, Vector3<T> b)
            => a.DotProduct(b);

        public static T operator *(Vector3<T> a, Point3<T> b)
            => a.DotProduct(b);

        public static Vector3<T> operator *(T a, Vector3<T> b)
        {
            return b * a;
        }

        public static Vector3<T> operator /(Vector3<T> a, T b)
        {
            return new Vector3<T>(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3<T> operator -(Vector3<T> a)
        {
            return new Vector3<T>(-a.X, -a.Y, -a.Z);
        }

        public static Vector3<T> operator +(Vector3<T> a)
        {
            return new Vector3<T>(+a.X, +a.Y, +a.Z);
        }

        public static bool operator <(Vector3<T> a, Vector3<T> b)
        {
            return a.DotProduct(a) < b.DotProduct(b);
        }

        public static bool operator >(Vector3<T> a, Vector3<T> b)
        {
            return a.DotProduct(a) > b.DotProduct(b);
        }

        public static bool operator <=(Vector3<T> a, Vector3<T> b)
        {
            return a.DotProduct(a) <= b.DotProduct(b);
        }

        public static bool operator >=(Vector3<T> a, Vector3<T> b)
        {
            return a.DotProduct(a) >= b.DotProduct(b);
        }

        public static bool operator ==(Vector3<T> a, Vector3<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((a is null) || (b is null))
            {
                return false;
            }

            return T.IsZero(a.X - b.X) && T.IsZero(a.Y - b.Y) && T.IsZero(a.Z - b.Z);
        }

        public static bool operator !=(Vector3<T> a, Vector3<T> b)
        {
            return !(a == b);
        }

        #endregion
    }

    public class Point3<T> where T : INumber<T>
    {
        #region Constants

        public static readonly Point3<T> Zero = new Point3<T>(T.Zero, T.Zero, T.Zero);

        #endregion

        #region Public Constructors

        public Point3()
        {
        }

        public Point3(Point3<T> v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Point3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(T[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3(T[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public T X { get; set; }

        public T Y { get; set; }

        public T Z { get; set; }

        #endregion

        #region Public Methods

        public T Distance(Point3<T> b)
            => T.CreateTruncating(Math.Sqrt(double.CreateChecked((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z))));

        public Point3<T> Move(Vector3<T> axis, T distance) => this + (axis.Normalize() * distance);

        public Point3<T> Clone() => new Point3<T>(X, Y, Z);

        public Vector3<T> ToVector() => new Vector3<T>(X, Y, Z);

        public T[] ToArray() => [X, Y, Z];

        #endregion

        #region Operators

        public static Point3<T> operator +(Point3<T> p, Vector3<T> v)
        {
            return new Point3<T>(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Vector3<T> operator -(Point3<T> a, Point3<T> b)
        {
            return new Vector3<T>(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static bool operator ==(Point3<T> a, Point3<T> b)
        {
            return T.IsZero(a.X - b.X) && T.IsZero(a.Y - b.Y) && T.IsZero(a.Z - b.Z);
        }

        public static bool operator !=(Point3<T> a, Point3<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
            => (X + Y + Z).GetHashCode();

        public override bool Equals(object obj)
            => obj is Point3<T> other && this == other;

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        #endregion
    }

    public class Line3<T> where T : INumber<T>
    {

        #region Public Constructors

        public Line3()
        {
            Point = Point3<T>.Zero.Clone();
            Vector = Vector3<T>.Zero.Clone();
        }

        public Line3(Point3<T> p, Vector3<T> v)
        {
            Point = p.Clone();
            Vector = v.Clone();
        }

        public Line3(Point3<T> p1, Point3<T> p2)
        {
            Point = p1.Clone();
            Vector = new Vector3<T>(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        }

        public Line3(Line3<T> line)
        {
            Point = line.Point.Clone();
            Vector = line.Vector.Clone();
        }

        #endregion

        #region Public Properties

        public Point3<T> Point { get; set; }

        public Vector3<T> Vector { get; set; }

        #endregion

        #region Public Members

        public Point3<T> ClosestPoint(Point3<T> point)
        {
            T n = (point.ToVector() - Point.ToVector()).DotProduct(Vector);
            T d = Vector.Length();
            return Point + (Vector * (n / d));
        }

        public bool ClosestPoints(Line3<T> b, out Point3<T> pa, out Point3<T> pb)
        {
            pa = null;
            pb = null;

            if (Vector == b.Vector || Vector == -b.Vector)
            {
                return false;
            }

            Vector3<T> p0 = Point.ToVector();
            Vector3<T> p1 = b.Point.ToVector();
            Vector3<T> d0 = Vector;
            Vector3<T> d1 = b.Vector;
            Vector3<T> d0n = d0.Normalize();

            var c = new Vector3<T>();
            var d = new Vector3<T>();

            d.X = d1.X - d0n.X * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.X = p1.X - p0.X + d0n.X * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Y = d1.Y - d0n.Y * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Y = p1.Y - p0.Y + d0n.Y * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Z = d1.Z - d0n.Z * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Z = p1.Z - p0.Z + d0n.Z * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            T t = -(c.X * d.X + c.Y * d.Y + c.Z * d.Z) / (d.X * d.X + d.Y * d.Y + d.Z * d.Z);

            pb = b.Point + (b.Vector * t);
            pa = ClosestPoint(pb);

            return true;
        }

        #endregion
    }

    public class Segment3<T> where T : INumber<T>
    {

        #region Public Constructors

        public Segment3()
        {
            A = Point3<T>.Zero.Clone();
            B = Point3<T>.Zero.Clone();
        }

        public Segment3(Point3<T> a, Point3<T> b)
        {
            A = a.Clone();
            B = b.Clone();
        }

        #endregion

        #region Public Properties

        public Point3<T> A { get; set; }

        public Point3<T> B { get; set; }

        public T Length => A.Distance(B);

        public Vector3<T> Vector => new Vector3<T>(B.X - A.X, B.Y - A.Y, B.Z - A.Z);

        public Vector3<T> NormalVector => Vector.Normalize();

        #endregion
    }

    public class Plane3<T> where T : INumber<T>
    {

        #region Public Constructors

        public Plane3(Vector3<T> normal, Point3<T> point)
        {
            Normal = normal;
            Point = point;
        }

        public Plane3(Point3<T> a, Point3<T> b, Point3<T> c)
        {
            Vector3<T> av = a.ToVector();
            Vector3<T> bv = b.ToVector();
            Vector3<T> cv = c.ToVector();

            Normal = (bv - av).CrossProduct(cv - av).Normalize();
            Point = a;
        }

        #endregion

        #region Public Properties

        public Vector3<T> Normal { get; set; }

        public Point3<T> Point { get; set; }

        public T Distance => Point.Distance(Point3<T>.Zero);

        #endregion

        #region Public Members

        public bool IsParallel(Line3<T> line)
            => line.Vector.DotProduct(Normal) == T.Zero;

        public bool IsParallel(Plane3<T> plane)
            => Normal == plane.Normal;

        public bool Intersect(Line3<T> line, out Point3<T> intersection)
        {
            if (IsParallel(line))
            {
                intersection = null;
                return false;
            }
            T t = (Distance - Normal.DotProduct(line.Point.ToVector())) / Normal.DotProduct(line.Vector);
            intersection = line.Point + (t * line.Vector);
            return true;
        }

        public bool Intersect(Plane3<T> b, out Line3<T> intersection)
        {
            intersection = null;

            if (IsParallel(b))
            {
                return false;
            }

            Point3<T> p;
            var v1 = Normal.CrossProduct(b.Normal);
            var v2 = new Vector3<T>(v1.X * v1.X, v1.Y * v1.Y, v1.Z * v1.Z);
            T w1 = -Distance;
            T w2 = -b.Distance;
            T id;

            if ((v2.Z > v2.Y) && (v2.Z > v2.X) && (v2.Z > T.CreateChecked(Constants.Epsilon)))
            {
                // point on XY plane
                id = T.One / v1.Z;
                p = new Point3<T>(Normal.Y * w2 - b.Normal.Y * w1, b.Normal.X * w1 - Normal.X * w2, T.Zero);
            }
            else if ((v2.Y > v2.X) && (v2.Y > T.CreateChecked(Constants.Epsilon)))
            {
                // point on XZ plane
                id = -T.One / v1.Y;
                p = new Point3<T>(Normal.Z * w2 - b.Normal.Z * w1, T.Zero, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else if (v2.X > T.CreateChecked(Constants.Epsilon))
            {
                // point on YZ plane
                id = T.One / v1.X;
                p = new Point3<T>(T.Zero, Normal.Z * w2 - b.Normal.Z * w1, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else
            {
                return false;
            }

            p = (p.ToVector() * id).ToPoint();
            id = T.One / T.CreateTruncating(Math.Sqrt(double.CreateChecked(v2.X + v2.Y + v2.Z)));
            v1 *= id;

            intersection = new Line3<T>(p, p.ToVector() + v1);

            return true;
        }

        public Point3<T> ClosestPoint(Point3<T> point)
        {
            var pv = point.ToVector();
            T d = Normal.DotProduct(pv - Point.ToVector());
            return (pv - (Normal * d)).ToPoint();
        }

        #endregion
    }

    public class Slice3<T> where T : INumber<T>
    {
        #region Public Constructors

        public Slice3(Vector3<T> normal, Point3<T> topLeft, T width, T height)
        {
            Vector3<T> right = normal.Rotate(Vector3<T>.AxisY, T.CreateChecked(-90));
            Vector3<T> down = normal.Rotate(Vector3<T>.AxisX, T.CreateChecked(-90.0));

            TopLeft = topLeft;
            TopRight = TopLeft + (right * width);
            BottomLeft = TopLeft + (down * height);
            BottomRight = BottomLeft + (right * width);

            Normal = normal;
            Width = width;
            Height = height;
            Plane = new Plane3<T>(normal, TopLeft);
        }

        #endregion

        #region Public Properties

        public Vector3<T> Normal { get; }

        public Plane3<T> Plane { get; }

        public Point3<T> TopLeft { get; }

        public Point3<T> TopRight { get; }

        public Point3<T> BottomLeft { get; }

        public Point3<T> BottomRight { get; }

        public T Width { get; }

        public T Height { get; }

        #endregion

        #region Public Methods

        public Point3<T> Project(Point3<T> point)
        {
            throw new NotImplementedException();
        }

        public Segment3<T> Project(Segment3<T> segment)
        {
            return new Segment3<T>(Project(segment.A), Project(segment.B));
        }

        public bool Intersect(Slice3<T> b, out Segment3<T> intersection)
        {
            // todo: fill this out parameter with the section of the intersection
            intersection = null;
            return Plane.Intersect(b.Plane, out var _);
        }

        #endregion
    }

    public class Orientation3<T> where T : INumber<T>
    {
        #region Public Constructors

        public Orientation3()
        {
            Forward = Vector3<T>.AxisX;
            Down = Vector3<T>.AxisZ;
        }

        public Orientation3(Vector3<T> forward, Vector3<T> down)
        {
            Forward = forward;
            Down = down;
        }

        public Orientation3(Orientation3<T> orientation)
        {
            Forward = orientation.Forward.Clone();
            Down = orientation.Down.Clone();
        }

        #endregion

        #region Public Properties

        public Vector3<T> Forward { get; private set; }

        public Vector3<T> Backward => -Forward;

        public Vector3<T> Left => -Right;

        public Vector3<T> Right => Down.CrossProduct(Forward);

        public Vector3<T> Up => -Down;

        public Vector3<T> Down { get; private set; }

        #endregion

        #region Public Methods

        public void Pitch(T angle)
        {
            Vector3<T> right = Right;
            Forward = Forward.Rotate(right, angle);
            Down = Down.Rotate(right, angle);
        }

        public void Roll(T angle)
        {
            Down = Down.Rotate(Forward, angle);
        }

        public void Yaw(T angle)
        {
            Forward = Forward.Rotate(Down, angle);
        }

        #endregion
    }
}
