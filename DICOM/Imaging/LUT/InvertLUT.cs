using System;

namespace Dicom.Imaging.LUT {
	public class InvertLUT : ILUT {
		#region Private Members
		private int _minValue;
		private int _maxValue;
		#endregion

		#region Public Constructors
		public InvertLUT(int minValue, int maxValue) {
			_minValue = minValue;
			_maxValue = maxValue;
		}
		#endregion

		#region Public Properties
		public bool IsValid {
			get { return true; }
		}

		public int MinimumOutputValue {
			get { return _minValue; }
		}

		public int MaximumOutputValue {
			get { return _maxValue; }
		}

		public int this[int value] {
			get { return _maxValue - value; }
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
