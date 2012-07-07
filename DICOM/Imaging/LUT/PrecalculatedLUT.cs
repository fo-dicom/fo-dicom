using System;

namespace Dicom.Imaging.LUT {
	public class PrecalculatedLUT : ILUT {
		#region Private Members
		private ILUT _lut;

		private int _minValue;
		private int _maxValue;

		private int[] _table;
		private int _offset;
		#endregion

		#region Public Constructor
		public PrecalculatedLUT(ILUT lut) {
			_minValue = lut.MinimumOutputValue;
			_maxValue = lut.MaximumOutputValue;
			_offset = -_minValue;
			_table = new int[_maxValue - _minValue + 1];
			_lut = lut;

			for (int i = _minValue; i <= _maxValue; i++) {
				_table[i + _offset] = _lut[i];
			}
		}
		#endregion

		#region Public Properties
		public bool IsValid {
			get { return _lut.IsValid; }
		}

		public int MinimumOutputValue {
			get { return _lut.MinimumOutputValue; }
		}

		public int MaximumOutputValue {
			get { return _lut.MaximumOutputValue; }
		}

		public int this[int value] {
			get { return _table[value + _offset]; }
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
			if (IsValid)
				return;

			for (int i = _minValue; i <= _maxValue; i++) {
				_table[i + _offset] = _lut[i];
			}
		}
		#endregion
	}
}
