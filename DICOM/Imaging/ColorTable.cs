using System;
using System.IO;

namespace Dicom.Imaging {
	public static class ColorTable {
		public readonly static Color32[] Monochrome1 = InitGrayscaleLUT(true);
		public readonly static Color32[] Monochrome2 = InitGrayscaleLUT(false);

		private static Color32[] InitGrayscaleLUT(bool reverse) {
			Color32[] LUT = new Color32[256];
			int i;
			byte b;
			if (reverse) {
				for (i = 0, b = 255; i < 256; i++, b--) {
					LUT[i] = new Color32(0xff, b, b, b);
				}
			} else {
				for (i = 0, b = 0; i < 256; i++, b++) {
					LUT[i] = new Color32(0xff, b, b, b);
				}
			}
			return LUT;
		}

		public static Color32[] Reverse(Color32[] lut) {
			Color32[] clone = new Color32[lut.Length];
			Array.Copy(lut, clone, clone.Length);
			Array.Reverse(clone);
			return clone;
		}

		public static Color32[] LoadLUT(string file) {
			try {
				byte[] data = File.ReadAllBytes(file);
				if (data.Length != (256 * 3))
					return null;

				Color32[] LUT = new Color32[256];
				for (int i = 0; i < 256; i++) {
					LUT[i] = new Color32(0xff, data[i], data[i + 256], data[i + 512]);
				}
				return LUT;
			} catch {
				return null;
			}
		}

		public static void SaveLUT(string file, Color32[] lut) {
			if (lut.Length != 256) return;
			FileStream fs = new FileStream(file, FileMode.Create);
			for (int i = 0; i < 256; i++) fs.WriteByte(lut[i].R);
			for (int i = 0; i < 256; i++) fs.WriteByte(lut[i].G);
			for (int i = 0; i < 256; i++) fs.WriteByte(lut[i].B);
			fs.Close();
		}
	}
}

