// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    public static class Constantsm
    {

        public static readonly decimal Epsilon = 0.000000001m; // the epsilon in mm to check if a value is quasi zero

    }

    public class Vector3M
    {
        #region Constants

        public static readonly Vector3M Zero = new Vector3M(0m, 0m, 0m);

        public static readonly Vector3M Epsilon = new Vector3M(Constantsm.Epsilon, Constantsm.Epsilon, Constantsm.Epsilon);

        public static readonly Vector3M MinValue = new Vector3M(decimal.MinValue, decimal.MinValue, decimal.MinValue);

        public static readonly Vector3M MaxValue = new Vector3M(decimal.MaxValue, decimal.MaxValue, decimal.MaxValue);

        public static readonly Vector3M AxisX = new Vector3M(1m, 0m, 0m);

        public static readonly Vector3M AxisY = new Vector3M(0m, 1m, 0m);

        public static readonly Vector3M AxisZ = new Vector3M(0m, 0m, 1m);

        #endregion

        #region Public Constructors

        public Vector3M()
        {
        }

        public Vector3M(Vector3M v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector3M(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3M(decimal[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3M(decimal[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Vector3M(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3M(int[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Vector3M(int[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }

        #endregion

        #region Public Methods

        public bool IsZero
            => X.IsNearlyZero() && Y.IsNearlyZero() && Z.IsNearlyZero();

        public decimal Length()
            => (decimal)Math.Sqrt((double)((X * X) + (Y * Y) + (Z * Z)));

        public Vector3M Round()
            => new Vector3M(Math.Round(X), Math.Round(Y), Math.Round(Z));

        public decimal Magnitude()
            => (decimal)Math.Sqrt((double)DotProduct(this));

        public Vector3M Normalize()
            => this * (1 / Magnitude());

        public decimal DotProduct(Vector3M b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public decimal DotProduct(Point3M b)
            => (X * b.X) + (Y * b.Y) + (Z * b.Z);

        public Vector3M CrossProduct(Vector3M b)
            => new Vector3M((Y * b.Z) - (Z * b.Y), (Z * b.X) - (X * b.Z), (X * b.Y) - (Y * b.X));

        public decimal Distance(Vector3M b)
            => (decimal)Math.Sqrt((double)((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z)));

        public bool IsPerpendicular(Vector3M b)
            => DotProduct(b) == 0;

        public static Vector3M Max(Vector3M a, Vector3M b)
            => (a >= b) ? a : b;

        public static Vector3M Min(Vector3M a, Vector3M b)
            => (a <= b) ? a : b;

        public Vector3M Rotate(Vector3M axis, decimal angle)
        {
            axis = axis.Normalize();
            Vector3M parallel = axis * DotProduct(axis);
            Vector3M perpendicular = this - parallel;
            Vector3M mutualPerpendicular = axis.CrossProduct(perpendicular);
            Vector3M rotatePerpendicular = (perpendicular * (decimal)Math.Cos((double)angle)) + (mutualPerpendicular * (decimal)Math.Sin((double)angle));
            return rotatePerpendicular + parallel;
        }

        public Vector3M Reflect(Vector3M normal)
        {
            decimal dot = DotProduct(normal);
            return new Vector3M(
                X - ((dot * 2m) * normal.X),
                Y - ((dot * 2m) * normal.Y),
                Z - ((dot * 2m) * normal.Z));
        }

        public Vector3M NearestAxis()
        {
            var b = Zero.Clone();
            decimal xabs = Math.Abs(X);
            decimal yabs = Math.Abs(Y);
            decimal zabs = Math.Abs(Z);

            if (xabs >= yabs && xabs >= zabs)
            {
                b.X = (X > 0m) ? 1.0m : -1.0m;
            }
            else if (yabs >= zabs)
            {
                b.Y = (Y > 0.0m) ? 1.0m : -1.0m;
            }
            else
            {
                b.Z = (Z > 0.0m) ? 1.0m : -1.0m;
            }

            return b;
        }

        public override int GetHashCode() => (int)((X + Y + Z) % int.MaxValue);

        public override bool Equals(object obj) => obj is Vector3M other && this == other;

        public override string ToString() => $"({X}, {Y}, {Z})";

        public Vector3M Clone() => new Vector3M(X, Y, Z);

        public Point3M ToPoint() => new Point3M(X, Y, Z);

        public decimal[] ToArray() => new decimal[] { X, Y, Z };

        #endregion

        #region Operators

        public static Vector3M operator +(Vector3M a, Vector3M b)
        {
            return new Vector3M(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3M operator -(Vector3M a, Vector3M b)
        {
            return new Vector3M(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3M operator *(Vector3M a, decimal b)
        {
            return new Vector3M(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3M operator *(Vector3M a, int b)
        {
            return new Vector3M(a.X * b, a.Y * b, a.Z * b);
        }

        public static decimal operator *(Vector3M a, Vector3M b)
            => a.DotProduct(b);

        public static decimal operator *(Vector3M a, Point3M b)
            => a.DotProduct(b);

        public static Vector3M operator *(decimal a, Vector3M b)
        {
            return b * a;
        }

        public static Vector3M operator *(int a, Vector3M b)
        {
            return b * a;
        }

        public static Vector3M operator /(Vector3M a, decimal b)
        {
            return new Vector3M(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3M operator /(Vector3M a, int b)
        {
            return new Vector3M(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3M operator -(Vector3M a)
        {
            return new Vector3M(-a.X, -a.Y, -a.Z);
        }

        public static Vector3M operator +(Vector3M a)
        {
            return new Vector3M(+a.X, +a.Y, +a.Z);
        }

        public static bool operator <(Vector3M a, Vector3M b)
        {
            return a.DotProduct(a) < b.DotProduct(b);
        }

        public static bool operator >(Vector3M a, Vector3M b)
        {
            return a.DotProduct(a) > b.DotProduct(b);
        }

        public static bool operator <=(Vector3M a, Vector3M b)
        {
            return a.DotProduct(a) <= b.DotProduct(b);
        }

        public static bool operator >=(Vector3M a, Vector3M b)
        {
            return a.DotProduct(a) >= b.DotProduct(b);
        }

        public static bool operator ==(Vector3M a, Vector3M b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((a is null) || (b is null))
            {
                return false;
            }

            return (a.X - b.X).IsNearlyZero() && (a.Y - b.Y).IsNearlyZero()
                   && (a.Z - b.Z).IsNearlyZero();
        }

        public static bool operator !=(Vector3M a, Vector3M b)
        {
            return !(a == b);
        }

        #endregion
    }

    public class Point3M
    {
        #region Constants

        public static readonly Point3M Zero = new Point3M(0, 0, 0);

        #endregion

        #region Public Constructors

        public Point3M()
        {
        }

        public Point3M(Point3M v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Point3M(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3M(decimal[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3M(decimal[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        public Point3M(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3M(int[] v)
        {
            X = v[0];
            Y = v[1];
            Z = v[2];
        }

        public Point3M(int[] v, int start)
        {
            X = v[start];
            Y = v[start + 1];
            Z = v[start + 2];
        }

        #endregion

        #region Public Properties

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }

        #endregion

        #region Public Methods

        public decimal Distance(Point3M b)
            => (decimal)Math.Sqrt((double)((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z)));

        public Point3M Move(Vector3M axis, decimal distance) => this + (axis.Normalize() * distance);

        public Point3M Clone() => new Point3M(X, Y, Z);

        public Vector3M ToVector() => new Vector3M(X, Y, Z);

        public decimal[] ToArray() => new decimal[] { X, Y, Z };

        #endregion

        #region Operators

        public static Point3M operator +(Point3M p, Vector3M v)
        {
            return new Point3M(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Vector3M operator - (Point3M a, Point3M b)
        {
            return new Vector3M(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static bool operator ==(Point3M a, Point3M b)
        {
            return (a.X - b.X).IsNearlyZero() && (a.Y - b.Y).IsNearlyZero()
                   && (a.Z - b.Z).IsNearlyZero();
        }

        public static bool operator !=(Point3M a, Point3M b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
            => (int)((X + Y + Z) % int.MaxValue);

        public override bool Equals(object obj)
            => obj is Point3M other && this == other;

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        #endregion
    }

    public class Line3M
    {

        #region Public Constructors

        public Line3M()
        {
            Point = Point3M.Zero.Clone();
            Vector = Vector3M.Zero.Clone();
        }

        public Line3M(Point3M p, Vector3M v)
        {
            Point = p.Clone();
            Vector = v.Clone();
        }

        public Line3M(Point3M p1, Point3M p2)
        {
            Point = p1.Clone();
            Vector = new Vector3M(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        }

        public Line3M(Line3M line)
        {
            Point = line.Point.Clone();
            Vector = line.Vector.Clone();
        }

        #endregion

        #region Public Properties

        public Point3M Point { get; set; }

        public Vector3M Vector { get; set; }

        #endregion

        #region Public Members

        public Point3M ClosestPoint(Point3M point)
        {
            decimal n = (point.ToVector() - Point.ToVector()).DotProduct(Vector);
            decimal d = Vector.Length();
            return Point + (Vector * (n / d));
        }

        public bool ClosestPoints(Line3M b, out Point3M pa, out Point3M pb)
        {
            pa = null;
            pb = null;

            if (Vector == b.Vector || Vector == -b.Vector)
            {
                return false;
            }

            Vector3M p0 = Point.ToVector();
            Vector3M p1 = b.Point.ToVector();
            Vector3M d0 = Vector;
            Vector3M d1 = b.Vector;
            Vector3M d0n = d0.Normalize();

            var c = new Vector3M();
            var d = new Vector3M();

            d.X = d1.X - d0n.X * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.X = p1.X - p0.X + d0n.X * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Y = d1.Y - d0n.Y * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Y = p1.Y - p0.Y + d0n.Y * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            d.Z = d1.Z - d0n.Z * (d0.X * d1.X + d0.Y * d1.Y + d0.Z * d1.Z);
            c.Z = p1.Z - p0.Z + d0n.Z * (d0.X * p0.X + d0.Y * p0.Y + d0.Z * p0.Z);

            decimal t = -(c.X * d.X + c.Y * d.Y + c.Z * d.Z) / (d.X * d.X + d.Y * d.Y + d.Z * d.Z);

            pb = b.Point + (b.Vector * t);
            pa = ClosestPoint(pb);

            return true;
        }

        #endregion
    }

    public class Segment3M
    {

        #region Public Constructors

        public Segment3M()
        {
            A = Point3M.Zero.Clone();
            B = Point3M.Zero.Clone();
        }

        public Segment3M(Point3M a, Point3M b)
        {
            A = a.Clone();
            B = b.Clone();
        }

        #endregion

        #region Public Properties

        public Point3M A { get; set; }

        public Point3M B { get; set; }

        public decimal Length => A.Distance(B);

        public Vector3M Vector => new Vector3M(B.X - A.X, B.Y - A.Y, B.Z - A.Z);

        public Vector3M NormalVector => Vector.Normalize();

        #endregion
    }

    public class Plane3M
    {

        #region Public Constructors

        public Plane3M(Vector3M normal, Point3M point)
        {
            Normal = normal;
            Point = point;
        }

        public Plane3M(Point3M a, Point3M b, Point3M c)
        {
            Vector3M av = a.ToVector();
            Vector3M bv = b.ToVector();
            Vector3M cv = c.ToVector();

            Normal = (bv - av).CrossProduct(cv - av).Normalize();
            Point = a;
        }

        #endregion

        #region Public Properties

        public Vector3M Normal { get; set; }

        public Point3M Point { get; set; }

        public decimal Distance => Point.Distance(Point3M.Zero);

        #endregion

        #region Public Members

        public bool IsParallel(Line3M line)
            => line.Vector.DotProduct(Normal) == 0.0m;

        public bool IsParallel(Plane3M plane)
            => Normal == plane.Normal;

        public bool Intersect(Line3M line, out Point3M intersection)
        {
            if (IsParallel(line))
            {
                intersection = null;
                return false;
            }
            decimal t = (Distance - Normal.DotProduct(line.Point.ToVector())) / Normal.DotProduct(line.Vector);
            intersection = line.Point + (t * line.Vector);
            return true;
        }

        public bool Intersect(Plane3M b, out Line3M intersection)
        {
            intersection = null;

            if (IsParallel(b))
            {
                return false;
            }

            Point3M p;
            var v1 = Normal.CrossProduct(b.Normal);
            var v2 = new Vector3M(v1.X * v1.X, v1.Y * v1.Y, v1.Z * v1.Z);
            decimal w1 = -Distance;
            decimal w2 = -b.Distance;
            decimal id;

            if ((v2.Z > v2.Y) && (v2.Z > v2.X) && (v2.Z > Constantsm.Epsilon))
            {
                // point on XY plane
                id = 1.0m / v1.Z;
                p = new Point3M(Normal.Y * w2 - b.Normal.Y * w1, b.Normal.X * w1 - Normal.X * w2, 0.0m);
            }
            else if ((v2.Y > v2.X) && (v2.Y > Constantsm.Epsilon))
            {
                // point on XZ plane
                id = -1.0m / v1.Y;
                p = new Point3M(Normal.Z * w2 - b.Normal.Z * w1, 0.0m, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else if (v2.X > Constantsm.Epsilon)
            {
                // point on YZ plane
                id = 1.0m / v1.X;
                p = new Point3M(0.0m, Normal.Z * w2 - b.Normal.Z * w1, b.Normal.Y * w1 - Normal.Y * w2);
            }
            else
            {
                return false;
            }

            p = (p.ToVector() * id).ToPoint();
            id = 1.0m / (decimal)Math.Sqrt((double)(v2.X + v2.Y + v2.Z));
            v1 *= id;

            intersection = new Line3M(p, p.ToVector() + v1);

            return true;
        }

        public Point3M ClosestPoint(Point3M point)
        {
            var pv = point.ToVector();
            decimal d = Normal.DotProduct(pv - Point.ToVector());
            return (pv - (Normal * d)).ToPoint();
        }

        #endregion
    }

    public class Slice3M
    {
        #region Public Constructors

        public Slice3M(Vector3M normal, Point3M topLeft, decimal width, decimal height)
        {
            Vector3M right = normal.Rotate(Vector3M.AxisY, -90.0m);
            Vector3M down = normal.Rotate(Vector3M.AxisX, -90.0m);

            TopLeft = topLeft;
            TopRight = TopLeft + (right * width);
            BottomLeft = TopLeft + (down * height);
            BottomRight = BottomLeft + (right * width);

            Normal = normal;
            Width = width;
            Height = height;
            Plane = new Plane3M(normal, TopLeft);
        }

        #endregion

        #region Public Properties

        public Vector3M Normal { get; }

        public Plane3M Plane { get; }

        public Point3M TopLeft { get; }

        public Point3M TopRight { get; }

        public Point3M BottomLeft { get; }

        public Point3M BottomRight { get; }

        public decimal Width { get; }

        public decimal Height { get; }

        #endregion

        #region Public Methods

        public Point3M Project(Point3M point)
        {
            throw new NotImplementedException();
        }

        public Segment3M Project(Segment3M segment)
        {
            return new Segment3M(Project(segment.A), Project(segment.B));
        }

        public bool Intersect(Slice3M b, out Segment3M intersection)
        {
            // todo: check. this always returns false????
            intersection = null;
            if (!Plane.Intersect(b.Plane, out var _))
            {
                return false;
            }

            return false;
        }

        #endregion
    }

    public class Orientation3M
    {
        #region Public Constructors

        public Orientation3M()
        {
            Forward = new Vector3M(1.0m, 0.0m, 0.0m);
            Down = new Vector3M(0.0m, 0.0m, 1.0m);
        }

        public Orientation3M(Vector3M forward, Vector3M down)
        {
            Forward = forward;
            Down = down;
        }

        public Orientation3M(Orientation3M orientation)
        {
            Forward = orientation.Forward.Clone();
            Down = orientation.Down.Clone();
        }

        #endregion

        #region Public Properties

        public Vector3M Forward { get; private set; }

        public Vector3M Backward => -Forward;

        public Vector3M Left => -Right;

        public Vector3M Right => Down.CrossProduct(Forward);

        public Vector3M Up => -Down;

        public Vector3M Down { get; private set; }

        #endregion

        #region Public Methods

        public void Pitch(decimal angle)
        {
            Vector3M right = Right;
            Forward = Forward.Rotate(right, angle);
            Down = Down.Rotate(right, angle);
        }

        public void Roll(decimal angle)
        {
            Down = Down.Rotate(Forward, angle);
        }

        public void Yaw(decimal angle)
        {
            Forward = Forward.Rotate(Down, angle);
        }

        #endregion
    }
}
