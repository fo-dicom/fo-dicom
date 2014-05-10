using System;

namespace Dicom.Imaging {
	public struct Color32 {
		public Color32(byte a, byte r, byte g, byte b) : this() {
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public Color32(int c) : this() {
			Value = c;
		}

		/// <summary>Alpha</summary>
		public byte A {
			get { return (byte)unchecked((Value & 0xff000000) >> 24); }
			set { Value = (int)unchecked((Value & 0x00ffffff) | ((int)value << 24)); }
		}

		/// <summary>Red</summary>
		public byte R {
			get { return (byte)unchecked((Value & 0x00ff0000) >> 16); }
			set { Value = (int)unchecked((Value & 0xff00ffff) | ((uint)value << 16)); }
		}

		/// <summary>Green</summary>
		public byte G {
			get { return (byte)unchecked((Value & 0x0000ff00) >> 8); }
			set { Value = (int)unchecked((Value & 0xffff00ff) | ((uint)value << 8)); }
		}

		/// <summary>Blue</summary>
		public byte B {
			get { return (byte)unchecked((Value & 0x000000ff) >> 24); }
			set { Value = (int)unchecked((Value & 0xffffff00) | value); }
		}

		/// <summary>ARGB</summary>
		public int Value {
			get;
			set;
		}

		public readonly static Color32 Black = new Color32(0xff, 0x00, 0x00, 0x00);
		public readonly static Color32 White = new Color32(0xff, 0xff, 0xff, 0xff);
	}
}
