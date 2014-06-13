using System;
using System.IO;

namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Output LUT implementation of <seealso cref="ILUT"/> used to map grayscale images to RGB grays, or colorize grayscale 
	/// images using custom color map
	/// </summary>
	public class OutputLUT : ILUT {
		#region Private Members
		private Color32[] _lut;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="OutputLUT"/> 
		/// </summary>
		/// <param name="lut">The color palette map to use for the output</param>
		public OutputLUT(Color32[] lut) {
			ColorMap = lut;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The color map
		/// </summary>
		public Color32[] ColorMap {
			get { return _lut; }
			set {
				if (value == null || value.Length != 256)
					throw new DicomImagingException("Expected 256 entry color map");
				_lut = value;
			}
		}

		public int MinimumOutputValue {
			get { return int.MinValue; }
		}

		public int MaximumOutputValue {
			get { return int.MaxValue; }
		}

		public bool IsValid {
			get { return _lut != null; }
		}

		public int this[int value] {
			get {
				unchecked {
					if (value < 0)
						return _lut[0].Value;
					if (value > 255)
						return _lut[255].Value;
					return _lut[value].Value;
				}
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
