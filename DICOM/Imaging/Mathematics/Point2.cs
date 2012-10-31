using System;
using System.Runtime.Serialization;

namespace Dicom.Imaging.Mathematics {
	/// <summary>
	/// Coordinate in 2D space
	/// </summary>
#if NETFX_CORE
    [DataContract]
    public class Point2 : IComparable<Point2>, IEquatable<Point2> {
#else
    [Serializable]
	public class Point2 : IComparable<Point2>, IEquatable<Point2>, ISerializable {
#endif
		public static readonly Point2 Origin = new Point2();

		public Point2() {
		}

		public Point2(int x, int y) {
			X = x;
			Y = y;
		}

#if !NETFX_CORE
		protected Point2(SerializationInfo info, StreamingContext context) {
			X = info.GetInt32("X");
			Y = info.GetInt32("Y");
		}
#endif

		/// <summary>Position on X axis</summary>
#if NETFX_CORE
        [DataMember]
#endif
        public int X {
			get;
			set;
		}

		/// <summary>Position on Y axis</summary>
#if NETFX_CORE
        [DataMember]
#endif
        public int Y
        {
			get;
			set;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(this, obj)) return true;
			if (obj is Point2) {
				Point2 other = obj as Point2;
				return X == other.X && Y == other.Y;
			}
			return false;
		}

		public bool Equals(Point2 other) {
			if (other == null)
				return false;
			return X == other.X && Y == other.Y;
		}

		public override int GetHashCode() {
			return X ^ Y;
		}

		/// <summary>Gets a human-readable string representing this <see cref="Dicom.Imaging.Mathematics.Point2"/> object.</summary>
		/// <returns>String representation</returns>
		public override string ToString() {
			return String.Format("({0},{1})", X, Y);
		}

		/// <summary>IComparable interface implementation</summary>
		/// <param name="other">Point to compare</param>
		/// <returns>Compare result</returns>
		public int CompareTo(Point2 other) {
			if (X < other.X) return -1;
			if (X > other.X) return 1;
			if (Y < other.Y) return -1;
			if (Y > other.Y) return 1;
			return 0;
		}

#if !NETFX_CORE
		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("X", X);
			info.AddValue("Y", Y);
		}
#endif
	}
}
