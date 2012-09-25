using System;
using System.IO;

namespace Dicom.Imaging.LUT {
	public class PaletteColorLUT : ILUT {
		#region Private Members
		private int _first;
		private Color32[] _lut;
		#endregion

		#region Public Constructors
		public PaletteColorLUT(int firstEntry, Color32[] lut) {
			_first = firstEntry;
			ColorMap = lut;
		}
		#endregion

		#region Public Properties
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
