using System;

namespace Dicom.Imaging.LUT {
	public class VOILinearLUT : ILUT {
		#region Private Members
		private double _windowCenter;
		private double _windowWidth;

		private double _windowCenterMin05;
		private double _windowWidthMin1;
		private double _windowWidthDiv2;
		private int _windowStart;
		private int _windowEnd;

		private bool _valid;
		#endregion

		#region Public Constructors
		public VOILinearLUT(double windowCenter, double windowWidth) {
			WindowCenter = windowCenter;
			WindowWidth = windowWidth;
		}
		#endregion

		#region Public Properties
		public double WindowCenter {
			get { return _windowCenter; }
			set {
				_windowCenter = value;
				_valid = false;
			}
		}

		public double WindowWidth {
			get { return _windowWidth; }
			set {
				_windowWidth = value;
				_valid = false;
			}
		}

		public bool IsValid {
			get { return _valid; }
		}

		public int MinimumOutputValue {
			get { return 0; }
		}

		public int MaximumOutputValue {
			get { return 255; }
		}

		public int this[int value] {
			get {
				if (value <= _windowStart)
					return 0;
				if (value > _windowEnd)
					return 255;
				unchecked {
					double scale = ((value - _windowCenterMin05) / _windowWidthMin1) + 0.5;
					return (int)(scale * 255);
				}
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
			if (!_valid) {
				_windowCenterMin05 = _windowCenter - 0.5;
				_windowWidthMin1 = _windowWidth - 1;
				_windowWidthDiv2 = _windowWidthMin1 / 2;
				_windowStart = (int)(_windowCenterMin05 - _windowWidthDiv2);
				_windowEnd = (int)(_windowCenterMin05 + _windowWidthDiv2);
				_valid = true;
			}
		}
		#endregion
	}
}
