using System;

namespace Dicom.Imaging.LUT {
	public class PaddingLUT : ILUT {
		#region Private Members
		private int _paddingValue;
		private int _minValue;
		private int _maxValue;
		#endregion
		
		#region Public Constructors
		public PaddingLUT(int minValue, int maxValue, int paddingValue) {
			_paddingValue = paddingValue;
			_minValue = minValue;
			_maxValue = maxValue;
		}
		#endregion

		#region Public Properties
		public double PixelPaddingValue {
			get { return _paddingValue; }
		}

		public int MinimumOutputValue {
			get { return _minValue; }
		}

		public int MaximumOutputValue {
			get { return _maxValue; }
		}

		public bool IsValid {
			get { return true; }
		}

		public int this[int value] {
			get {
				if (value == _paddingValue)
					return _minValue;
				return value;
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
