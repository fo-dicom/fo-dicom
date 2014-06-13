using System;
using System.Collections.Generic;
using System.Text;

namespace Dicom.Imaging.Mathematics {
	public class Vector3D {
		#region Constants
		public static readonly Vector3D Zero = new Vector3D(0.0, 0.0, 0.0);
		public static readonly Vector3D Epsilon = new Vector3D(Double.Epsilon, Double.Epsilon, Double.Epsilon);
		public static readonly Vector3D MinValue = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);
		public static readonly Vector3D MaxValue = new Vector3D(Double.MaxValue, Double.MaxValue, Double.MaxValue);

		public static readonly Vector3D AxisX = new Vector3D(1.0, 0.0, 0.0);
		public static readonly Vector3D AxisY = new Vector3D(0.0, 1.0, 0.0);
		public static readonly Vector3D AxisZ = new Vector3D(0.0, 0.0, 1.0);
		#endregion

		#region Private Members
		private double _x;
		private double _y;
		private double _z;
		#endregion

		#region Public Constructors
		public Vector3D() {
		}

		public Vector3D(Vector3D v) {
			_x = v.X;
			_y = v.Y;
			_z = v.Z;
		}

		public Vector3D(double x, double y, double z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Vector3D(double[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Vector3D(double[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}

		public Vector3D(float x, float y, float z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Vector3D(float[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Vector3D(float[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}

		public Vector3D(int x, int y, int z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Vector3D(int[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Vector3D(int[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}
		#endregion

		#region Public Properties
		public double X {
			get { return _x; }
			set { _x = value; }
		}

		public double Y {
			get { return _y; }
			set { _y = value; }
		}

		public double Z {
			get { return _z; }
			set { _z = value; }
		}
		#endregion

		#region Public Methods
		public double Length() {
			return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
		}

		public Vector3D Round() {
			return new Vector3D(Math.Round(X), Math.Round(Y), Math.Round(Z));
		}

		public double Magnitude() {
			return Math.Sqrt(DotProduct(this));
		}

		public Vector3D Normalize() {
			return this * (1 / Magnitude());
		}

		public double DotProduct(Vector3D b) {
			return (X * b.X) + (Y * b.Y) + (Z * b.Z);
		}

		public Vector3D CrossProduct(Vector3D b) {
			return new Vector3D((Y * b.Z) - (Z * b.Y),
								(Z * b.X) - (X * b.Z),
								(X * b.Y) - (Y * b.X));
		}

		public double Distance(Vector3D b) {
			return Math.Sqrt((X - b.X) * (X - b.X) +
							 (Y - b.Y) * (Y - b.Y) +
							 (Z - b.Z) * (Z - b.Z));
		}

		public bool IsPerpendicular(Vector3D b) {
			return DotProduct(b) == 0;
		}

		public static Vector3D Max(Vector3D a, Vector3D b) {
			return (a >= b) ? a : b;
		}

		public static Vector3D Min(Vector3D a, Vector3D b) {
			return (a <= b) ? a : b;
		}

		public Vector3D Rotate(Vector3D axis, double angle) {
			axis = axis.Normalize();
			Vector3D parallel = axis * DotProduct(axis);
			Vector3D perpendicular = this - parallel;
			Vector3D mutualPerpendicular = axis.CrossProduct(perpendicular);
			Vector3D rotatePerpendicular = (perpendicular * Math.Cos(angle)) + (mutualPerpendicular * Math.Sin(angle));
			return rotatePerpendicular + parallel;
		}

		public Vector3D Reflect(Vector3D normal) {
			double dot = DotProduct(normal);
			return new Vector3D(
					X - ((dot * 2.0f) * normal.X),
					Y - ((dot * 2.0f) * normal.Y),
					Z - ((dot * 2.0f) * normal.Z)
				);
		}

		public Vector3D NearestAxis() {
			Vector3D b = Vector3D.Zero.Clone();
			double xabs = Math.Abs(X);
			double yabs = Math.Abs(Y);
			double zabs = Math.Abs(Z);

			if (xabs >= yabs && xabs >= zabs)
				b.X = (X > 0.0) ? 1.0 : -1.0;
			else if (yabs >= zabs)
				b.Y = (Y > 0.0) ? 1.0 : -1.0;
			else
				b.Z = (Z > 0.0) ? 1.0 : -1.0;

			return b;
		}

		public override int GetHashCode() {
			return (int)((X + Y + Z) % int.MaxValue);
		}
		public override bool Equals(object obj) {
			if (obj is Vector3D)
				return this == (Vector3D)obj;
			return false;
		}
		public override string ToString() {
			return String.Format("({0}, {1}, {2})", X, Y, Z);
		}

		public Vector3D Clone() {
			return new Vector3D(X, Y, Z);
		}

		public Point3D ToPoint() {
			return new Point3D(X, Y, Z);
		}
		#endregion

		#region Operators
		public static Vector3D operator +(Vector3D a, Vector3D b) {
			return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Vector3D operator -(Vector3D a, Vector3D b) {
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Vector3D operator *(Vector3D a, double b) {
			return new Vector3D(a.X * b, a.Y * b, a.Z * b);
		}
		public static Vector3D operator *(Vector3D a, float b) {
			return new Vector3D(a.X * b, a.Y * b, a.Z * b);
		}
		public static Vector3D operator *(Vector3D a, int b) {
			return new Vector3D(a.X * b, a.Y * b, a.Z * b);
		}

		public static Vector3D operator *(double a, Vector3D b) {
			return a * b;
		}
		public static Vector3D operator *(float a, Vector3D b) {
			return a * b;
		}
		public static Vector3D operator *(int a, Vector3D b) {
			return a * b;
		}

		public static Vector3D operator /(Vector3D a, double b) {
			return new Vector3D(a.X / b, a.Y / b, a.Z / b);
		}
		public static Vector3D operator /(Vector3D a, float b) {
			return new Vector3D(a.X / b, a.Y / b, a.Z / b);
		}
		public static Vector3D operator /(Vector3D a, int b) {
			return new Vector3D(a.X / b, a.Y / b, a.Z / b);
		}

		public static Vector3D operator -(Vector3D a) {
			return new Vector3D(-a.X, -a.Y, -a.Z);
		}

		public static Vector3D operator +(Vector3D a) {
			return new Vector3D(+a.X, +a.Y, +a.Z);
		}

		public static bool operator <(Vector3D a, Vector3D b) {
			return a.DotProduct(a) < b.DotProduct(b);
		}

		public static bool operator >(Vector3D a, Vector3D b) {
			return a.DotProduct(a) > b.DotProduct(b);
		}

		public static bool operator <=(Vector3D a, Vector3D b) {
			return a.DotProduct(a) <= b.DotProduct(b);
		}

		public static bool operator >=(Vector3D a, Vector3D b) {
			return a.DotProduct(a) >= b.DotProduct(b);
		}

		public static bool operator ==(Vector3D a, Vector3D b) {
			if (ReferenceEquals(a, b))
				return true;

			if (((object)a == null) || ((object)b == null))
				return false;

			return Math.Abs(a.X - b.X) <= Double.Epsilon &&
					Math.Abs(a.Y - b.Y) <= Double.Epsilon &&
					Math.Abs(a.Z - b.Z) <= Double.Epsilon;
		}

		public static bool operator !=(Vector3D a, Vector3D b) {
			return !(a == b);
		}
		#endregion
	}

	public class Point3D {
		#region Constants
		public static readonly Point3D Zero = new Point3D(0, 0, 0);
		#endregion

		#region Private Members
		private double _x;
		private double _y;
		private double _z;
		#endregion

		#region Public Constructors
		public Point3D() {
		}

		public Point3D(Point3D v) {
			_x = v.X;
			_y = v.Y;
			_z = v.Z;
		}

		public Point3D(double x, double y, double z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Point3D(double[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Point3D(double[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}

		public Point3D(float x, float y, float z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Point3D(float[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Point3D(float[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}

		public Point3D(int x, int y, int z) {
			_x = x;
			_y = y;
			_z = z;
		}
		public Point3D(int[] v) {
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}
		public Point3D(int[] v, int start) {
			_x = v[start];
			_y = v[start + 1];
			_z = v[start + 2];
		}
		#endregion

		#region Public Properties
		public double X {
			get { return _x; }
			set { _x = value; }
		}

		public double Y {
			get { return _y; }
			set { _y = value; }
		}

		public double Z {
			get { return _z; }
			set { _z = value; }
		}
		#endregion

		#region Public Methods
		public double Distance(Point3D b) {
			return Math.Sqrt((X - b.X) * (X - b.X) +
							 (Y - b.Y) * (Y - b.Y) +
							 (Z - b.Z) * (Z - b.Z));
		}

		public Point3D Move(Vector3D axis, double distance) {
			return this + (axis.Normalize() * distance);
		}

		public Point3D Clone() {
			return new Point3D(X, Y, Z);
		}
		public Vector3D ToVector() {
			return new Vector3D(X, Y, Z);
		}
		#endregion

		#region Operators
		public static Point3D operator +(Point3D p, Vector3D v) {
			return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
		}

		public static bool operator ==(Point3D a, Point3D b) {
			return Math.Abs(a.X - b.X) <= Double.Epsilon &&
				   Math.Abs(a.Y - b.Y) <= Double.Epsilon &&
				   Math.Abs(a.Z - b.Z) <= Double.Epsilon;
		}

		public static bool operator !=(Point3D a, Point3D b) {
			return !(a == b);
		}

		public override int GetHashCode() {
			return (int)((X + Y + Z) % int.MaxValue);
		}
		public override bool Equals(object obj) {
			if (obj is Point3D)
				return this == (Point3D)obj;
			return false;
		}
		public override string ToString() {
			return String.Format("({0}, {1}, {2})", X, Y, Z);
		}
		#endregion
	}

	public class Line3D {
		#region Private Members
		private Vector3D _vector;
		private Point3D _point;
		#endregion

		#region Public Constructors
		public Line3D() {
			_point = Point3D.Zero.Clone();
			_vector = Vector3D.Zero.Clone();
		}

		public Line3D(Point3D p, Vector3D v) {
			_point = p.Clone();
			_vector = v.Clone();
		}

		public Line3D(Line3D line) {
			_point = line.Point.Clone();
			_vector = line.Vector.Clone();
		}
		#endregion

		#region Public Properties
		public Point3D Point {
			get { return _point; }
			set { _point = value; }
		}

		public Vector3D Vector {
			get { return _vector; }
			set { _vector = value; }
		}
		#endregion

		#region Public Members
		public Point3D ClosestPoint(Point3D point) {
			double n = (point.ToVector() - Point.ToVector()).DotProduct(Vector);
			double d = Vector.Length();
			return Point + (Vector * (n / d));
		}

		public bool ClosestPoints(Line3D b, out Point3D pa, out Point3D pb) {
			pa = null;
			pb = null;

			if (Vector == b.Vector || Vector == -b.Vector)
				return false;

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

	public class Segment3D {
		#region Private Members
		private Point3D _a;
		private Point3D _b;
		#endregion

		#region Public Constructors
		public Segment3D() {
			_a = Point3D.Zero.Clone();
			_b = Point3D.Zero.Clone();
		}

		public Segment3D(Point3D a, Point3D b) {
			_a = a.Clone();
			_b = b.Clone();
		}
		#endregion

		#region Public Properties
		public Point3D A {
			get { return _a; }
			set { _a = value; }
		}

		public Point3D B {
			get { return _b; }
			set { _b = value; }
		}

		public double Length {
			get { return _a.Distance(_b); }
		}

		public Vector3D Vector {
			get { return new Vector3D(_b.X - _a.X, _b.Y - _a.Y, _b.Z - _a.Z); }
		}

		public Vector3D NormalVector {
			get { return Vector.Normalize(); }
		}
		#endregion
	}

	public class Plane3D {
		#region Private Members
		Vector3D _normal;
		Point3D _point;
		#endregion

		#region Public Constructors
		public Plane3D(Vector3D normal, Point3D point) {
			_normal = normal;
			_point = point;
		}

		public Plane3D(Point3D a, Point3D b, Point3D c) {
			Vector3D av = a.ToVector();
			Vector3D bv = b.ToVector();
			Vector3D cv = c.ToVector();

			_normal = (bv - av).CrossProduct(cv - av).Normalize();
			_point = a;
		}
		#endregion

		#region Public Properties
		public Vector3D Normal {
			get { return _normal; }
			set { _normal = value; }
		}

		public Point3D Point {
			get { return _point; }
			set { _point = value; }
		}

		public double Distance {
			get { return Point.Distance(Point3D.Zero); }
		}
		#endregion

		#region Public Members
		public bool IsParallel(Line3D line) {
			return line.Vector.DotProduct(Normal) == 0.0;
		}

		public bool IsParallel(Plane3D plane) {
			return Normal == plane.Normal;
		}

		public bool Intersect(Line3D line, out Point3D intersection) {
			if (IsParallel(line)) {
				intersection = null;
				return false;
			}
			double t = (Distance - Normal.DotProduct(line.Point.ToVector())) / Normal.DotProduct(line.Vector);
			intersection = line.Point + (t * line.Vector);
			return true;
		}

		public bool Intersect(Plane3D b, out Line3D intersection) {
			intersection = null;

			if (IsParallel(b))
				return false;

			Point3D p;
			Vector3D v1 = Normal.CrossProduct(b.Normal);
			Vector3D v2 = new Vector3D(v1.X * v1.X, v1.Y * v1.Y, v1.Z * v1.Z);
			double w1 = -Distance;
			double w2 = -b.Distance;
			double id;

			if ((v2.Z > v2.Y) && (v2.Z > v2.X) && (v2.Z > Double.Epsilon)) {
				// point on XY plane
				id = 1.0 / v1.Z;
				p = new Point3D(Normal.Y * w2 - b.Normal.Y * w1,
								b.Normal.X * w1 - Normal.X * w2,
								0.0);
			} else if ((v2.Y > v2.X) && (v2.Y > Double.Epsilon)) {
				// point on XZ plane
				id = -1.0 / v1.Y;
				p = new Point3D(Normal.Z * w2 - b.Normal.Z * w1,
								0.0,
								b.Normal.Y * w1 - Normal.Y * w2);
			} else if (v2.X > Double.Epsilon) {
				// point on YZ plane
				id = 1.0 / v1.X;
				p = new Point3D(0.0,
								  Normal.Z * w2 - b.Normal.Z * w1,
								b.Normal.Y * w1 - Normal.Y * w2);
			} else
				return false;

			p = (p.ToVector() * id).ToPoint();
			id = 1.0 / Math.Sqrt(v2.X + v2.Y + v2.Z);
			v1 *= id;

			intersection = new Line3D(p, p.ToVector() + v1);

			return true;
		}

		public Point3D ClosestPoint(Point3D point) {
			Vector3D pv = point.ToVector();
			double d = Normal.DotProduct(pv - Point.ToVector());
			return (pv - (Normal * d)).ToPoint();
		}
		#endregion
	}

	public class Slice3D {
		#region Private Members
		Vector3D _normal;
		Point3D _topLeft;
		Point3D _topRight;
		Point3D _bottomLeft;
		Point3D _bottomRight;
		double _width;
		double _height;
		Plane3D _plane;
		#endregion

		#region Public Constructors
		public Slice3D(Vector3D normal, Point3D topLeft, double width, double height) {
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
		public Vector3D Normal {
			get { return _normal; }
		}

		public Plane3D Plane {
			get { return _plane; }
		}

		public Point3D TopLeft {
			get { return _topLeft; }
		}

		public Point3D TopRight {
			get { return _topRight; }
		}

		public Point3D BottomLeft {
			get { return _bottomLeft; }
		}

		public Point3D BottomRight {
			get { return _bottomRight; }
		}

		public double Width {
			get { return _width; }
		}

		public double Height {
			get { return _height; }
		}
		#endregion

		#region Public Methods
		public Point3D Project(Point3D point) {
			Point3D p = Plane.ClosestPoint(point);
			//normal vector?
			throw new NotImplementedException();
			return p;
		}

		public Segment3D Project(Segment3D segment) {
			return new Segment3D(Project(segment.A), Project(segment.B));
		}

		public bool Intersect(Slice3D b, out Segment3D intersection) {
			intersection = null;

			Line3D line;
			if (!Plane.Intersect(b.Plane, out line))
				return false;

			return false;
		}
		#endregion
	}

	public class Orientation3D {
		#region Private Members
		private Vector3D _forward;
		private Vector3D _down;
		#endregion

		#region Public Constructors
		public Orientation3D() {
			_forward = new Vector3D(1.0, 0.0, 0.0);
			_down = new Vector3D(0.0, 0.0, 1.0);
		}

		public Orientation3D(Vector3D forward, Vector3D down) {
			_forward = forward;
			_down = down;
		}

		public Orientation3D(Orientation3D orientation) {
			_forward = orientation.Forward.Clone();
			_down = orientation.Down.Clone();
		}
		#endregion

		#region Public Properties
		public Vector3D Forward {
			get { return _forward; }
		}

		public Vector3D Backward {
			get { return -Forward; }
		}

		public Vector3D Left {
			get { return -Right; }
		}

		public Vector3D Right {
			get { return Down.CrossProduct(Forward); }
		}

		public Vector3D Up {
			get { return -Down; }
		}

		public Vector3D Down {
			get { return _down; }
		}
		#endregion

		#region Public Methods
		public void Pitch(double angle) {
			Vector3D right = Right;
			_forward = _forward.Rotate(right, angle);
			_down = _down.Rotate(right, angle);
		}

		public void Roll(double angle) {
			_down = _down.Rotate(_forward, angle);
		}

		public void Yaw(double angle) {
			_forward = _forward.Rotate(_down, angle);
		}
		#endregion
	}
}
