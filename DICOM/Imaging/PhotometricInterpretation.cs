using System;
using System.Collections.Generic;

using Dicom;

namespace Dicom.Imaging {
	/// <summary>
	/// Photometric Interpretation
	/// </summary>
	public class PhotometricInterpretation : DicomParseable {
		#region Constructor
		private PhotometricInterpretation() {
		}
		#endregion

		#region Public Properties
		public string Value {
			get;
			private set;
		}

		public string Description {
			get;
			private set;
		}

		public bool IsColor {
			get;
			private set;
		}

		public bool IsPalette {
			get;
			private set;
		}

		public bool IsYBR {
			get;
			private set;
		}

		public ColorSpace ColorSpace {
			get;
			private set;
		}
		#endregion

		#region Public Methods
		public override bool Equals(object obj) {
			if (ReferenceEquals(this, obj))
				return true;
			if (!(obj is PhotometricInterpretation))
				return false;
			return (obj as PhotometricInterpretation).Value == Value;
		}

		public override int GetHashCode() {
			return Value.GetHashCode();
		}

		public override string ToString() {
			return Description;
		}
		#endregion

		#region Static Methods
		public static PhotometricInterpretation Parse(string photometricInterpretation) {
			switch (photometricInterpretation.Trim(' ', '\0')) {
				case "MONOCHROME1": return Monochrome1;
				case "MONOCHROME":
				case "MONOCHROME2": return Monochrome2;
				case "PALETTE COLOR": return PaletteColor;
				case "RGB": return Rgb;
				case "YBR_FULL": return YbrFull;
				case "YBR_FULL_422": return YbrFull422;
				case "YBR_PARTIAL_422": return YbrPartial422;
				case "YBR_PARTIAL_420": return YbrPartial420;
				case "YBR_ICT": return YbrIct;
				case "YBR_RCT": return YbrRct;
				default:
					break;
			}
			throw new DicomImagingException("Unknown Photometric Interpretation [{0}]", photometricInterpretation);
		}

		public static bool operator ==(PhotometricInterpretation a, PhotometricInterpretation b) {
			if (((object)a == null) && ((object)b == null))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a.Value == b.Value;
		}
		public static bool operator !=(PhotometricInterpretation a, PhotometricInterpretation b) {
			return !(a == b);
		}
		#endregion

		/// <summary>
		/// Pixel data represent a single monochrome image plane. The minimum sample value is intended 
		/// to be displayed as white after any VOI gray scale transformations have been performed. See 
		/// PS 3.4. This value may be used only when Samples per Pixel (0028,0002) has a value of 1.
		/// </summary>
		public readonly static PhotometricInterpretation Monochrome1 = new PhotometricInterpretation() {
			Value = "MONOCHROME1",
			Description = "Monochrome 1",
			IsColor = false,
			IsPalette = false,
			IsYBR = false,
			ColorSpace = ColorSpace.Grayscale
		};

		/// <summary>
		/// Pixel data represent a single monochrome image plane. The minimum sample value is intended 
		/// to be displayed as black after any VOI gray scale transformations have been performed. See 
		/// PS 3.4. This value may be used only when Samples per Pixel (0028,0002) has a value of 1.
		/// </summary>
		public readonly static PhotometricInterpretation Monochrome2 = new PhotometricInterpretation() {
			Value = "MONOCHROME2",
			Description = "Monochrome 2",
			IsColor = false,
			IsPalette = false,
			IsYBR = false,
			ColorSpace = ColorSpace.Grayscale
		};

		/// <summary>
		/// Pixel data describe a color image with a single sample per pixel (single image plane). The 
		/// pixel value is used as an index into each of the Red, Blue, and Green Palette Color Lookup 
		/// Tables (0028,1101-1103&1201-1203). This value may be used only when Samples per Pixel (0028,0002) 
		/// has a value of 1. When the Photometric Interpretation is Palette Color; Red, Blue, and Green 
		/// Palette Color Lookup Tables shall be present.
		/// </summary>
		public readonly static PhotometricInterpretation PaletteColor = new PhotometricInterpretation() {
			Value = "PALETTE COLOR",
			Description = "Palette Color",
			IsColor = true,
			IsPalette = true,
			IsYBR = false,
			ColorSpace = ColorSpace.Indexed
		};

		/// <summary>
		/// Pixel data represent a color image described by red, green, and blue image planes. The minimum 
		/// sample value for each color plane represents minimum intensity of the color. This value may be 
		/// used only when Samples per Pixel (0028,0002) has a value of 3.
		/// </summary>
		public readonly static PhotometricInterpretation Rgb = new PhotometricInterpretation() {
			Value = "RGB",
			Description = "RGB",
			IsColor = true,
			IsPalette = false,
			IsYBR = false,
			ColorSpace = ColorSpace.RGB
		};

		/// <summary>
		/// Pixel data represent a color image described by one luminance (Y) and two chrominance planes 
		/// (Cb and Cr). This photometric interpretation may be used only when Samples per Pixel (0028,0002) 
		/// has a value of 3. Black is represented by Y equal to zero. The absence of color is represented 
		/// by both Cb and Cr values equal to half full scale.
		/// 
		/// In the case where Bits Allocated (0028,0100) has a value of 8 then the following equations convert 
		/// between RGB and YCBCR Photometric Interpretation:
		/// Y  = + .2990R + .5870G + .1140B
		/// Cb = - .1687R - .3313G + .5000B + 128
		/// Cr = + .5000R - .4187G - .0813B + 128
		/// </summary>
		public readonly static PhotometricInterpretation YbrFull = new PhotometricInterpretation() {
			Value = "YBR_FULL",
			Description = "YBR Full",
			IsColor = true,
			IsPalette = false,
			IsYBR = true,
			ColorSpace = ColorSpace.YCbCrJPEG
		};

		/// <summary>
		/// The same as YBR_FULL except that the Cb and Cr values are sampled horizontally at half the Y rate 
		/// and as a result there are half as many Cb and Cr values as Y values.
		/// 
		/// This Photometric Interpretation is only allowed with Planar Configuration (0028,0006) equal to 0.  
		/// Two Y values shall be stored followed by one Cb and one Cr value. The Cb and Cr values shall be 
		/// sampled at the location of the first of the two Y values. For each Row of Pixels, the first Cb and 
		/// Cr samples shall be at the location of the first Y sample. The next Cb and Cr samples shall be 
		/// at the location of the third Y sample etc.
		/// </summary>
		public readonly static PhotometricInterpretation YbrFull422 = new PhotometricInterpretation() {
			Value = "YBR_FULL_422",
			Description = "YBR Full 4:2:2",
			IsColor = true,
			IsPalette = false,
			IsYBR = true
		};

		/// <summary>
		/// The same as YBR_FULL_422 except that:
		/// <list type="number">
		/// <item>black corresponds to Y = 16</item>
		/// <item>Y is restricted to 220 levels (i.e. the maximum value is 235)</item>
		/// <item>Cb and Cr each has a minimum value of 16</item>
		/// <item>Cb and Cr are restricted to 225 levels (i.e. the maximum value is 240)</item>
		/// <item>lack of color is represented by Cb and Cr equal to 128</item>
		/// </list>
		/// 
		/// In the case where Bits Allocated (0028,0100) has value of 8 then the following equations convert 
		/// between RGB and YBR_PARTIAL_422 Photometric Interpretation:
		/// Y  = + .2568R + .5041G + .0979B + 16
		/// Cb = - .1482R - .2910G + .4392B + 128
		/// Cr = + .4392R - .3678G - .0714B + 128
		/// </summary>
		public readonly static PhotometricInterpretation YbrPartial422 = new PhotometricInterpretation() {
			Value = "YBR_PARTIAL_422",
			Description = "YBR Partial 4:2:2",
			IsColor = true,
			IsPalette = false,
			IsYBR = true
		};

		/// <summary>
		/// The same as YBR_PARTIAL_422 except that the Cb and Cr values are sampled horizontally and vertically 
		/// at half the Y rate and as a result there are four times less Cb and Cr values than Y values, versus 
		/// twice less for YBR_PARTIAL_422.
		/// 
		/// This Photometric Interpretation is only allowed with Planar Configuration (0028,0006) equal to 0.  
		/// The Cb and Cr values shall be sampled at the location of the first of the two Y values. For the first 
		/// Row of Pixels (etc.), the first Cb and Cr samples shall be at the location of the first Y sample.  The 
		/// next Cb and Cr samples shall be at the location of the third Y sample etc. The next Rows of Pixels 
		/// containing Cb and Cr samples (at the same locations than for the first Row) will be the third etc.
		/// </summary>
		public readonly static PhotometricInterpretation YbrPartial420 = new PhotometricInterpretation() {
			Value = "YBR_PARTIAL_420",
			Description = "YBR Partial 4:2:0",
			IsColor = true,
			IsPalette = false,
			IsYBR = true
		};

		/// <summary>
		/// Pixel data represent a color image described by one luminance (Y) and two chrominance planes 
		/// (Cb and Cr). This photometric interpretation may be used only when Samples per Pixel (0028,0002) has 
		/// a value of 3. Black is represented by Y equal to zero. The absence of color is represented by both 
		/// Cb and Cr values equal to zero.
		/// 
		/// Regardless of the value of Bits Allocated (0028,0100), the following equations convert between RGB 
		/// and YCbCr Photometric Interpretation:
		/// Y  = + .29900R + .58700G + .11400B
		/// Cb = - .16875R - .33126G + .50000B
		/// Cr = + .50000R - .41869G - .08131B
		/// </summary>
		public readonly static PhotometricInterpretation YbrIct = new PhotometricInterpretation() {
			Value = "YBR_ICT",
			Description = "YBR Irreversible Color Transformation (JPEG 2000)",
			IsColor = true,
			IsPalette = false,
			IsYBR = true
		};

		/// <summary>
		/// Pixel data represent a color image described by one luminance (Y) and two chrominance planes 
		/// (Cb and Cr). This photometric interpretation may be used only when Samples per Pixel (0028,0002) 
		/// has a value of 3. Black is represented by Y equal to zero. The absence of color is represented 
		/// by both Cb and Cr values equal to zero.
		/// 
		/// Regardless of the value of Bits Allocated (0028,0100), the following equations convert between 
		/// RGB and YBR_RCT Photometric Interpretation:
		/// Y  = floor((R + 2G +B) / 4)
		/// Cb = B - G
		/// Cr = R - G
		/// 
		/// The following equations convert between YBR_RCT and RGB Photometric Interpretation:
		/// R = Cr + G
		/// G = Y – floor((Cb + Cr) / 4)
		/// B = Cb + G
		/// </summary>
		public readonly static PhotometricInterpretation YbrRct = new PhotometricInterpretation() {
			Value = "YBR_RCT",
			Description = "YBR Reversible Color Transformation (JPEG 2000)",
			IsColor = true,
			IsPalette = false,
			IsYBR = true
		};
	}
}
