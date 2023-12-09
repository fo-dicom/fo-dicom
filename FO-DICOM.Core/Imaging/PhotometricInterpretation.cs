// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Photometric Interpretation.
    /// </summary>
    public class PhotometricInterpretation : DicomParseable
    {
        #region Constructor

        private PhotometricInterpretation(string value, string description, bool isColor, bool isPalette, bool isYBR,
            ColorSpace colorSpace)
        {
            Value = value;
            Description = description;
            IsColor = isColor;
            IsPalette = isPalette;
            IsYBR = isYBR;
            ColorSpace = colorSpace;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the identifier value, corresponding with a DICOM defined term for tag (0028, 0004).
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the description of the photometric interpretation.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets whether or not the photometric interpretation represents color (true) or grayscale (false).
        /// </summary>
        public bool IsColor { get; }

        /// <summary>
        /// Gets whether or not the photometric interpretation is represented by a palette of colors.
        /// </summary>
        public bool IsPalette { get; }

        /// <summary>
        /// Gets whether or not the photometric interpretation represents an YBR color scheme.
        /// </summary>
        public bool IsYBR { get; }

        /// <summary>
        /// Gets the color space of the photometric interpretation, or <code>null</code> if <see cref="IsPalette"/> is <code>false</code>.
        /// </summary>
        public ColorSpace ColorSpace { get; }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is PhotometricInterpretation)) return false;
            return ((PhotometricInterpretation)obj).Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Description;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Parse the photometric interpretation based on a string, typically obtained by reading DICOM tag (0028,0004).
        /// </summary>
        /// <param name="photometricInterpretation">String to be parsed.</param>
        /// <returns><see cref="PhotometricInterpretation"/> object corresponding to the parsed <paramref name="photometricInterpretation"/> string.</returns>
        /// <exception cref="DicomImagingException">Thrown when the parsed string cannot be matched with the known photometric interpretations.</exception>
        public static PhotometricInterpretation Parse(string photometricInterpretation)
        {
            switch (photometricInterpretation.Trim(' ', '\0'))
            {
                case "MONOCHROME1":
                    return Monochrome1;
                case "MONOCHROME":
                case "MONOCHROME2":
                    return Monochrome2;
                case "PALETTE COLOR":
                    return PaletteColor;
                case "RGB":
                    return Rgb;
                case "YBR_FULL":
                    return YbrFull;
                case "YBR_FULL_422":
                    return YbrFull422;
                case "YBR_PARTIAL_422":
                    return YbrPartial422;
                case "YBR_PARTIAL_420":
                    return YbrPartial420;
                case "YBR_ICT":
                    return YbrIct;
                case "YBR_RCT":
                    return YbrRct;
            }

            throw new DicomImagingException($"Unknown Photometric Interpretation [{photometricInterpretation}]");
        }

        /// <summary>
        /// Equivalence operator for <see cref="PhotometricInterpretation"/> class.
        /// </summary>
        /// <param name="a">Left-hand side object to compare for equivalence.</param>
        /// <param name="b">Right-hand side object to compare for equivalence.</param>
        /// <returns>True if both objects are <code>null</code> or <see cref="PhotometricInterpretation"/> objects with the same <see cref="Value"/>,
        /// false otherwise.</returns>
        public static bool operator ==(PhotometricInterpretation a, PhotometricInterpretation b)
        {
            if ((object)a == null && (object)b == null) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Value == b.Value;
        }

        /// <summary>
        /// Non-equivalence operator for <see cref="PhotometricInterpretation"/> class.
        /// </summary>
        /// <param name="a">Left-hand side object to compare for non-equivalence.</param>
        /// <param name="b">Right-hand side object to compare for none-equivalence.</param>
        /// <returns>True if exactly one object is <code>null</code> or if both are <see cref="PhotometricInterpretation"/> objects with different <see cref="Value"/>,
        /// false otherwise.</returns>
        public static bool operator !=(PhotometricInterpretation a, PhotometricInterpretation b)
        {
            return !(a == b);
        }

        #endregion

        /// <summary>
        /// Pixel data represent a single monochrome image plane. The minimum sample value is intended 
        /// to be displayed as white after any VOI gray scale transformations have been performed. See 
        /// PS 3.4. This value may be used only when Samples per Pixel (0028,0002) has a value of 1.
        /// </summary>
        public static readonly PhotometricInterpretation Monochrome1 =
            new PhotometricInterpretation("MONOCHROME1", "Monochrome 1", false, false, false, ColorSpace.Grayscale);

        /// <summary>
        /// Pixel data represent a single monochrome image plane. The minimum sample value is intended 
        /// to be displayed as black after any VOI gray scale transformations have been performed. See 
        /// PS 3.4. This value may be used only when Samples per Pixel (0028,0002) has a value of 1.
        /// </summary>
        public static readonly PhotometricInterpretation Monochrome2 =
            new PhotometricInterpretation("MONOCHROME2", "Monochrome 2", false, false, false, ColorSpace.Grayscale);

        /// <summary>
        /// Pixel data describe a color image with a single sample per pixel (single image plane). The 
        /// pixel value is used as an index into each of the Red, Blue, and Green Palette Color Lookup 
        /// Tables (0028,1101-1103 &amp; 1201-1203). This value may be used only when Samples per Pixel (0028,0002) 
        /// has a value of 1. When the Photometric Interpretation is Palette Color; Red, Blue, and Green 
        /// Palette Color Lookup Tables shall be present.
        /// </summary>
        public static readonly PhotometricInterpretation PaletteColor =
            new PhotometricInterpretation("PALETTE COLOR", "Palette Color", true, true, false, ColorSpace.Indexed);

        /// <summary>
        /// Pixel data represent a color image described by red, green, and blue image planes. The minimum 
        /// sample value for each color plane represents minimum intensity of the color. This value may be 
        /// used only when Samples per Pixel (0028,0002) has a value of 3.
        /// </summary>
        public static readonly PhotometricInterpretation Rgb =
            new PhotometricInterpretation("RGB", "RGB", true, false, false, ColorSpace.RGB);

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
        public static readonly PhotometricInterpretation YbrFull =
            new PhotometricInterpretation("YBR_FULL", "YBR Full", true, false, true, ColorSpace.YCbCrJPEG);

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
        public static readonly PhotometricInterpretation YbrFull422 =
            new PhotometricInterpretation("YBR_FULL_422", "YBR Full 4:2:2", true, false, true, null);

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
        public static readonly PhotometricInterpretation YbrPartial422 =
            new PhotometricInterpretation("YBR_PARTIAL_422", "YBR Partial 4:2:2", true, false, true, null);

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
        public static readonly PhotometricInterpretation YbrPartial420 =
            new PhotometricInterpretation("YBR_PARTIAL_420", "YBR Partial 4:2:0", true, false, true, null);

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
        public static readonly PhotometricInterpretation YbrIct =
            new PhotometricInterpretation("YBR_ICT", "YBR Irreversible Color Transformation (JPEG 2000)", true, false,
                true, null);

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
        public static readonly PhotometricInterpretation YbrRct =
            new PhotometricInterpretation("YBR_RCT", "YBR Reversible Color Transformation (JPEG 2000)", true, false,
                true, null);
    }
}
