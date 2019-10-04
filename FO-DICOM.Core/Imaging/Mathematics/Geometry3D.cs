// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    public static class Constants
    {

        public static readonly double Epsilon = 0.000000001; // the epsilon in mm to check if a value is quasi zero

    }

    public class Vector3D
    {
        #region Constants

        public static readonly Vector3D Zero = new Vector3D(0.0, 0.0, 0.0);

        public static readonly Vector3D Epsilon = new Vector3D(Double.Epsilon, Double.Epsilon, Double.Epsilon);

        public static readonly Vector3D MinValue = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);

        public static readonly Vector3D MaxValue = new Vector3D(Double.MaxValue, Double.MaxValue, Double.MaxValue);

        public static readonly Vector3D AxisX = new Vector3D(1.0, 0.0, 0.0);

        public static readonly Vector3D AxisY = new Vector3D(0.0, 1.0, 0.0);

        public static readonly Vector3D AxisZ = new Vector3D(0.0, 0.0, 1.0);

        #endregion

        #region Public Constructors

        public Vector3D()
        {
        }

        public Vector3D(Vector3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(double[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3D(double[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(float[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3D(float[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Vector3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(int[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3D(int[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        #endregion

        #region Public Methods

        public bool IsZero
            => X.IsNearlyZero() && Y.IsNearlyZero() && Z.IsNearlyZero();

        public double Length()
            => Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

        public Vector3D Round()
            => new Vector3D(Math.Round(X), Math.Round(Y), Math.Round(Z));

        public double Magnitude()
            => Math.Sqrt(DotProduct(this));

        public Vector3D Normalize()
            => this * (1 / Magnitude());

        public double DotProduct(Vector3D b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public double DotProduct(Point3D b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public Vector3D CrossProduct(Vector3D b)
            => new Vector3D((Y * b.Z) - (Z * b.Y), (Z * b.X) - (X * b.Z), (X * b.Y) - (Y * b.X));

        public double Distance(Vector3D b)
            => Math.Sqrt((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z));

        public bool IsPerpendicular(Vector3D b)
            => DotProduct(b) == 0;

        public static Vector3D Max(Vector3D a, Vector3D b)
            => (a >= b) ? a : b;

        public static Vector3D Min(Vector3D a, Vector3D b)
            => (a <= b) ? a : b;

        public Vector3D Rotate(Vector3D axis, double angle)
        {
            axis = axis.Normalize();
            Vector3D parallel = axis * DotProduct(axis);
            Vector3D perpendicular = this - parallel;
            Vector3D mutualPerpendicular = axis.CrossProduct(perpendicular);
            Vector3D rotatePerpendicular = (perpendicular * Math.Cos(angle)) + (mutualPerpendicular * Math.Sin(angle));
            return rotatePerpendicular + parallel;
        }

        public Vector3D Reflect(Vector3D normal)
        {
            double dot = DotProduct(normal);
            return new Vector3D(
                X - ((dot * 2.0f) * normal.X),
                Y - ((dot * 2.0f) * normal.Y),
                Z - ((dot * 2.0f) * normal.Z));
        }

        public Vector3D NearestAxis()
        {
            Vector3D b = Zero.Clone();
            double xabs = Math.Abs(X);
            double yabs = Math.Abs(Y);
            double zabs = Math.Abs(Z);

            if (xabs >= yabs && xabs >= zabs) b.X = (X > 0.0) ? 1.0 : -1.0;
            else if (yabs >= zabs) b.Y = (Y > 0.0) ? 1.0 : -1.0;
            else b.Z = (Z > 0.0) ? 1.0 : -1.0;

            return b;
        }

        public override int GetHashCode()
        {
            return (int)((X + Y + Z) % int.MaxValue);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3D other && this == other;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }

        public Vector3D Clone()
        {
            return new Vector3D(X, Y, Z);
        }

        public Point3D ToPoint()
        {
            return new Point3D(X, Y, Z);
        }

        public double[] ToArray()
        {
            return new double[] { X, Y, Z };
        }

        #endregion

        #region Operators

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3D operator *(Vector3D a, double b)
        {
            return new Vector3D(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3D operator *(Vector3D a, float b)
        {
            return new Vector3D(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3D operator *(Vector3D a, int b)
        {
            return new Vector3D(a.X * b, a.Y * b, a.Z * b);
        }

        public static double operator *(Vector3D a, Vector3D b)
            => a.DotProduct(b);

        public static double operator *(Vector3D a, Point3D b)
            => a.DotProduct(b);

        public static Vector3D operator *(double a, Vector3D b)
        {
            return b * a;
        }

        public static Vector3D operator *(float a, Vector3D b)
        {
            return b * a;
        }

        public static Vector3D operator *(int a, Vector3D b)
        {
            return b * a;
        }

        public static Vector3D operator /(Vector3D a, double b)
        {
            return new Vector3D(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator /(Vector3D a, float b)
        {
            return new Vector3D(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator /(Vector3D a, int b)
        {
            return new Vector3D(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator -(Vector3D a)
        {
            return new Vector3D(-a.X, -a.Y, -a.Z);
        }

        public static Vector3D operator +(Vector3D a)
        {
            return new Vector3D(+a.X, +a.Y, +a.Z);
        }

        public static bool operator <(Vector3D a, Vector3D b)
        {
            return a.DotProduct(a) < b.DotProduct(b);
        }

        public static bool operator >(Vector3D a, Vector3D b)
        {
            return a.DotProduct(a) > b.DotProduct(b);
        }

        public static bool operator <=(Vector3D a, Vector3D b)
        {
            return a.DotProduct(a) <= b.DotProduct(b);
        }

        public static bool operator >=(Vector3D a, Vector3D b)
        {
            return a.DotProduct(a) >= b.DotProduct(b);
        }

        public static bool operator ==(Vector3D a, Vector3D b)
        {
            if (ReferenceEquals(a, b)) return true;

            if ((a is null) || (b is null)) return false;

            return (a.X - b.X).IsNearlyZero() && (a.Y - b.Y).IsNearlyZero()
                   && (a.Z - b.Z).IsNearlyZero();
        }

        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return !(a == b);
        }

        #endregion
    }

    public class Point3D
    {
        #region Constants

        public static readonly Point3D Zero = new Point3D(0, 0, 0);

        #endregion

        #region Public Constructors

        public Point3D()
        {
        }

        public Point3D(Point3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(double[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3D(double[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(float[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3D(float[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(int[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3D(int[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        #endregion

        #region Public Methods

        public double Distance(Point3D b)
        {
            return Math.Sqrt((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z));
        }

        public Point3D Move(Vector3D axis, double distance)
        {
            return this + (axis.Normalize() * distance);
        }

        public Point3D Clone()
        {
            return new Point3D(X, Y, Z);
        }

        public Vector3D ToVector()
        {
            return new Vector3D(X, Y, Z);
        }

        public double[] ToArray()
        {
            return new double[] { X, Y, Z };
        }

        #endregion

        #region Operators

        public static Point3D operator +(Point3D p, Vector3D v)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Vector3D operator - (Point3D a, Point3D b)
        {
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            return (a.X - b.X).IsNearlyZero() && (a.Y - b.Y).IsNearlyZero()
                   && (a.Z - b.Z).IsNearlyZero();
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (int)((X + Y + Z) % int.MaxValue);
        }

        public override bool Equals(object obj)
        {
            return obj is Point3D other && this == other;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }

        #endregion
    }

    public class Line3D
    {

        #region Public Constructors

        public Line3D()
        {
            Point = Point3D.Zero.Clone();
            Vector = Vector3D.Zero.Clone();
        }

        public Line3D(Point3D p, Vector3D v)
        {
            Point = p.Clone();
            Vector = v.Clone();
        }

        public Line3D(Point3D p1, Point3D p2)
        {
            Point = p1.Clone();
            Vector = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        }

        public Line3D(Line3D line)
        {
            Point = line.Point.Clone();
            Vector = line.Vector.Clone();
        }

        #endregion

        #region Public Properties

        public Point3D Point { get; set; }

        public Vector3D Vector { get; set; }

        #endregion

        #region Public Members

        public Point3D ClosestPoint(Point3D point)
        {
            double n = (point.ToVector() - Point.ToVector()).DotProduct(Vector);
            double d = Vector.Length();
            return Point + (Vector * (n / d));
        }

        public bool ClosestPoints(Line3D b, out Point3D pa, out Point3D pb)
        {
            pa = null;
            pb = null;

            if (Vector == b.Vector || Vector == -b.Vector) return false;

            Vector3D p0 = Point.ToVector();
            Vector3D p1 = b.Point.ToVector();
            Vector3D d0 = Vector;
            Vector3D d1 = b.Vector;
            Vector3D d0n = d0.Normalize();

            Vector3D c = new Vector3D();
            Vector3D d = new Vector3D();

            d.X = d1.X - d0n.X * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.X = p1.X - p0.X + d0n.X * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Y = d1.Y - d0n.Y * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Y = p1.Y - p0.Y + d0n.Y * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Z = d1.Z - d0n.Z * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Z = p1.Z - p0.Z + d0n.Z * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            double t = -(c.X * d.X + c.Y * d.Y + c.Z * d.Z) / (d.X * d.X + d.Y * d.Y + d.Z * d.Z);

            pb = b.Point + (b.Vector * t);
            pa = ClosestPoint(pb);

            return true;
        }

        #endregion
    }

    public class Segment3D
    {

        #region Public Constructors

        public Segment3D()
        {
            A = Point3D.Zero.Clone();
            B = Point3D.Zero.Clone();
        }

        public Segment3D(Point3D a, Point3D b)
        {
            A = a.Clone();
            B = b.Clone();
        }

        #endregion

        #region Public Properties

        public Point3D A { get; set; }

        public Point3D B { get; set; }

        public double Length => A.Distance(B);

        public Vector3D Vector => new Vector3D(B.X - A.X, B.Y - A.Y, B.Z - A.Z);

        public Vector3D NormalVector => Vector.Normalize();

        #endregion
    }

    public class Plane3D
    {

        #region Public Constructors

        public Plane3D(Vector3D normal, Point3D point)
        {
            Normal = normal;
            Point = point;
        }

        public Plane3D(Point3D a, Point3D b, Point3D c)
        {
            Vector3D av = a.ToVector();
            Vector3D bv = b.ToVector();
            Vector3D cv = c.ToVector();

            Normal = (bv - av).CrossProduct(cv - av).Normalize();
            Point = a;
        }

        #endregion

        #region Public Properties

        public Vector3D Normal { get; set; }

        public Point3D Point { get; set; }

        public double Distance => Point.Distance(Point3D.Zero);

        #endregion

        #region Public Members

        public bool IsParallel(Line3D line)
        {
            return line.Vector.DotProduct(Normal) == 0.0;
        }

        public bool IsParallel(Plane3D plane)
        {
            return Normal == plane.Normal;
        }

        public bool Intersect(Line3D line, out Point3D intersection)
        {
            if (IsParallel(line))
            {
                intersection = null;
                return false;
            }
            double t = (Distance - Normal.DotProduct(line.Point.ToVector())) / Normal.DotProduct(line.Vector);
            intersection = line.Point + (t * line.Vector);
            return true;
        }

        public bool Intersect(Plane3D b, out Line3D intersection)
        {
            intersection = null;

            if (IsParallel(b)) return false;

            Point3D p;
            Vector3D v1 = Normal.CrossProduct(b.Normal);
            Vector3D v2 = new Vector3D(v1.X * v1.X, v1.Y * v1.Y, v1.Z * v1.Z);
            double w1 = -Distance;
            double w2 = -b.Distance;
            double id;

            if ((v2.Z > v2.Y) && (v2.Z > v2.X) && (v2.Z > Constants.Epsilon))
            {
                // point on XY plane
                id = 1.0 / v1.Z;
                p = new Point3D(Normal.Y * w2 - b.Normal.Y * w1, b.Normal.X * w1 - Normal.X * w2, 0.0);
            }
            else if ((v2.Y > v2.X) && (v2.Y > Constants.Epsilon))
            {
                // point on XZ plane
                id = -1.0 / v1.Y;
                p = new Point3D(Normal.Z * w2 - b.Normal.Z * w1, 0.0, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else if (v2.X > Double.Epsilon)
            {
                // point on YZ plane
                id = 1.0 / v1.X;
                p = new Point3D(0.0, Normal.Z * w2 - b.Normal.Z * w1, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else return false;

            p = (p.ToVector() * id).ToPoint();
            id = 1.0 / Math.Sqrt(v2.X + v2.Y + v2.Z);
            v1 *= id;

            intersection = new Line3D(p, p.ToVector() + v1);

            return true;
        }

        public Point3D ClosestPoint(Point3D point)
        {
            Vector3D pv = point.ToVector();
            double d = Normal.DotProduct(pv - Point.ToVector());
            return (pv - (Normal * d)).ToPoint();
        }

        #endregion
    }

    public class Slice3D
    {
        #region Private Members

        private readonly Vector3D _normal;

        private readonly Point3D _topLeft;

        private readonly Point3D _topRight;

        private readonly Point3D _bottomLeft;

        private readonly Point3D _bottomRight;

        private readonly double _width;

        private readonly double _height;

        private readonly Plane3D _plane;

        #endregion

        #region Public Constructors

        public Slice3D(Vector3D normal, Point3D topLeft, double width, double height)
        {
            Vector3D right = normal.Rotate(Vector3D.AxisY, -90.0);
            Vector3D down = normal.Rotate(Vector3D.AxisX, -90.0);

            _topLeft = topLeft;
            _topRight = _topLeft + (right * width);
            _bottomLeft = _topLeft + (down * height);
            _bottomRight = _bottomLeft + (right * width);

            _normal = normal;
            _width = width;
            _height = height;
            _plane = new Plane3D(normal, _topLeft);
        }

        #endregion

        #region Public Properties

        public Vector3D Normal => _normal;

        public Plane3D Plane => _plane;

        public Point3D TopLeft => _topLeft;

        public Point3D TopRight => _topRight;

        public Point3D BottomLeft => _bottomLeft;

        public Point3D BottomRight => _bottomRight;

        public double Width => _width;

        public double Height => _height;

        #endregion

        #region Public Methods

        public Point3D Project(Point3D point)
        {
            throw new NotImplementedException();
        }

        public Segment3D Project(Segment3D segment)
        {
            return new Segment3D(Project(segment.A), Project(segment.B));
        }

        public bool Intersect(Slice3D b, out Segment3D intersection)
        {
            // todo: check. this always returns false????
            intersection = null;
            if (!Plane.Intersect(b.Plane, out var line)) return false;
            return false;
        }

        #endregion
    }

    public class Orientation3D
    {
        #region Private Members

        private Vector3D _forward;

        private Vector3D _down;

        #endregion

        #region Public Constructors

        public Orientation3D()
        {
            _forward = new Vector3D(1.0, 0.0, 0.0);
            _down = new Vector3D(0.0, 0.0, 1.0);
        }

        public Orientation3D(Vector3D forward, Vector3D down)
        {
            _forward = forward;
            _down = down;
        }

        public Orientation3D(Orientation3D orientation)
        {
            _forward = orientation.Forward.Clone();
            _down = orientation.Down.Clone();
        }

        #endregion

        #region Public Properties

        public Vector3D Forward => _forward;

        public Vector3D Backward => -Forward;

        public Vector3D Left => -Right;

        public Vector3D Right => Down.CrossProduct(Forward);

        public Vector3D Up => -Down;

        public Vector3D Down => _down;

        #endregion

        #region Public Methods

        public void Pitch(double angle)
        {
            Vector3D right = Right;
            _forward = _forward.Rotate(right, angle);
            _down = _down.Rotate(right, angle);
        }

        public void Roll(double angle)
        {
            _down = _down.Rotate(_forward, angle);
        }

        public void Yaw(double angle)
        {
            _forward = _forward.Rotate(_down, angle);
        }

        #endregion
    }
}
