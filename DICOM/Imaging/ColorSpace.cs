using System;
using System.Text;

namespace Dicom {
	public class ColorSpace {
		public ColorSpace(string name, params Component[] components) {
			Name = name;
			Components = components;
		}

		public string Name {
			get;
			private set;
		}

		public Component[] Components {
			get;
			private set;
		}

		public static bool operator ==(ColorSpace a, ColorSpace b) {
			if ((object)a == null || (object)b == null) return false;
			return a.Name == b.Name;
		}
		public static bool operator !=(ColorSpace a, ColorSpace b) {
			if ((object)a == null || (object)b == null) return true;
			return a.Name != b.Name;
		}

		protected bool Equals(ColorSpace other) {
			return String.Equals(Name, other.Name);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			var other = obj as ColorSpace;
			return other != null && Equals(other);
		}

		public override int GetHashCode() {
			return (Name != null ? Name.GetHashCode() : 0);
		}

		public override string ToString() {
			return Name;
		}

		public class Component {
			public Component(string name, int subSampleX, int subSampleY) {
				Name = name;
				SubSampleX = subSampleX;
				SubSampleY = subSampleY;
			}

			public string Name {
				get;
				private set;
			}

			public int SubSampleX {
				get;
				private set;
			}

			public int SubSampleY {
				get;
				private set;
			}
		}

		public readonly static ColorSpace OneBit = new ColorSpace("1-bit", new Component("Value", 1, 1));
		public readonly static ColorSpace Grayscale = new ColorSpace("Grayscale", new Component("Value", 1, 1));
		public readonly static ColorSpace Indexed = new ColorSpace("Indexed", new Component("Value", 1, 1));
		public readonly static ColorSpace RGB = new ColorSpace("RGB", new Component("Red", 1, 1), new Component("Green", 1, 1), new Component("Blue", 1, 1));
		public readonly static ColorSpace BGR = new ColorSpace("BGR", new Component("Blue", 1, 1), new Component("Green", 1, 1), new Component("Red", 1, 1));
		public readonly static ColorSpace RGBA = new ColorSpace("RGBA", new Component("Red", 1, 1), new Component("Green", 1, 1), new Component("Blue", 1, 1), new Component("Alpha", 1, 1));
		public readonly static ColorSpace YCbCrJPEG = new ColorSpace("Y'CbCr 4:4:4 (JPEG)", new Component("Luminance", 1, 1), new Component("Blue Chroma", 1, 1), new Component("Red Chroma", 1, 1));
	}
}
