using System;
using System.Collections.Generic;

namespace Dicom.Imaging.LUT {
	public class CompositeLUT : ILUT {
		#region Private Members
		private List<ILUT> _luts = new List<ILUT>();
		#endregion

		#region Public Properties
		public ILUT FinalLUT {
			get {
				if (_luts.Count > 0)
					return _luts[_luts.Count - 1];
				return null;
			}
		}
		#endregion

		#region Public Constructor
		public CompositeLUT() {
		}
		#endregion

		#region Public Members
		public void Add(ILUT lut) {
			_luts.Add(lut);
		}
		#endregion

		#region ILUT Members
		public int MinimumOutputValue {
			get {
				ILUT lut = FinalLUT;
				if (lut != null)
					return lut.MinimumOutputValue;
				return 0;
			}
		}

		public int MaximumOutputValue {
			get {
				ILUT lut = FinalLUT;
				if (lut != null)
					return lut.MaximumOutputValue;
				return 255;
			}
		}

		public bool IsValid {
			get {
				foreach (ILUT lut in _luts) {
					if (!lut.IsValid)
						return false;
				}
				return true;
			}
		}

		public int this[int value] {
			get {
				foreach (ILUT lut in _luts)
					value = lut[value];
				return value;
			}
		}

		public void Recalculate() {
			foreach (ILUT lut in _luts)
				lut.Recalculate();
		}
		#endregion
	}
}
