using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Mathematics {
	public static class Extensions {
		public static bool IsOdd(this byte v) {
			return (v & 1) == 1;
		}
		public static bool IsOdd(this short v) {
			return (v & 1) == 1;
		}
		public static bool IsOdd(this ushort v) {
			return (v & 1) == 1;
		}
		public static bool IsOdd(this int v) {
			return (v & 1) == 1;
		}
		public static bool IsOdd(this uint v) {
			return (v & 1) == 1;
		}

		public static bool IsEven(this byte v) {
			return (v & 1) == 0;
		}
		public static bool IsEven(this short v) {
			return (v & 1) == 0;
		}
		public static bool IsEven(this ushort v) {
			return (v & 1) == 0;
		}
		public static bool IsEven(this int v) {
			return (v & 1) == 0;
		}
		public static bool IsEven(this uint v) {
			return (v & 1) == 0;
		}
	}
}
