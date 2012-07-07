using System;

namespace Dicom.Imaging.LUT {
	public class RescaleLUT : ILUT {
		#region Private Members
		private double _rescaleSlope;
		private double _rescaleIntercept;

		private int _minValue;
		private int _maxValue;
		#endregion

		#region Public Constructors
		public RescaleLUT(int minValue, int maxValue, double slope, double intercept) {
			_rescaleSlope = slope;
			_rescaleIntercept = intercept;
			_minValue = this[minValue];
			_maxValue = this[maxValue];
		}
		#endregion

		#region Public Properties
		public double RescaleSlope {
			get { return _rescaleSlope; }
		}

		public double RescaleIntercept {
			get { return _rescaleIntercept; }
		}

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
			get {
				return unchecked((int)((value * _rescaleSlope) + _rescaleIntercept));
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
