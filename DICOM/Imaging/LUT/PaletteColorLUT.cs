using System;
using System.IO;

namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Paletter color LUT implementation of <seealso cref="ILUT"/> maps PALETTE COLOR images
	/// </summary>
	public class PaletteColorLUT : ILUT {
		#region Private Members
		private int _first;
		private Color32[] _lut;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="PaletteColorLUT"/>
		/// </summary>
		/// <param name="firstEntry">The first entry (minium value)</param>
		/// <param name="lut">The palette color LUT</param>
		public PaletteColorLUT(int firstEntry, Color32[] lut) {
			_first = firstEntry;
			ColorMap = lut;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The color map
		/// </summary>
		public Color32[] ColorMap {
			get { return _lut; }
			set { _lut = value; }
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
				return _lut[value + _first].Value;
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
