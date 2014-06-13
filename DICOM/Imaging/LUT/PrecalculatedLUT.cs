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
		public PrecalculatedLUT(ILUT lut, int minValue, int maxValue) {
			_minValue = minValue;
			_maxValue = maxValue;
			_offset = -_minValue;
			_table = new int[_maxValue - _minValue + 1];
			_lut = lut;
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
			get {
				unchecked {
					int p = value + _offset;
					if (p < 0)
						return _table[0];
					if (p >= _table.Length)
						p = _table[_table.Length - 1];
					return _table[p];
				}
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
			if (IsValid)
				return;

			_lut.Recalculate();

			for (int i = _minValue; i <= _maxValue; i++) {
				_table[i + _offset] = _lut[i];
			}
		}
		#endregion
	}
}
