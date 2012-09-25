using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.LUT {
	public abstract class VOILUT : ILUT {
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
		public VOILUT(double windowCenter, double windowWidth) {
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

		protected double WindowCenterMin05 {
			get { return _windowCenterMin05; }
		}

		protected double WindowWidthMin1 {
			get { return _windowWidthMin1; }
		}

		protected double WindowWidthDiv2 {
			get { return _windowWidthDiv2; }
		}

		protected int WindowStart {
			get { return _windowStart; }
		}

		protected int WindowEnd {
			get { return _windowEnd; }
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

		public abstract int this[int value] {
			get;
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

		#region Factory Methods
		public static VOILUT Create(string function, double windowCenter, double windowWidth) {
			switch (function.ToUpper()) {
			case "SIGMOID":
				return new VOISigmoidLUT(windowCenter, windowWidth);
			default:
				break;
			}

			return new VOILinearLUT(windowCenter, windowWidth);
		}
		#endregion
	}

	public class VOILinearLUT : VOILUT {
		#region Public Constructors
		public VOILinearLUT(double windowCenter, double windowWidth) : base(windowCenter, windowWidth) {
		}
		#endregion

		#region Public Properties
		public override int this[int value] {
			get {
				if (value <= WindowStart)
					return MinimumOutputValue;
				if (value > WindowEnd)
					return MaximumOutputValue;
				unchecked {
					return (int)Math.Round((((value - WindowCenterMin05) / WindowWidthMin1) + 0.5) * 255.0);
				}
			}
		}
		#endregion
	}

	public class VOISigmoidLUT : VOILUT {
		#region Public Constructors
		public VOISigmoidLUT(double windowCenter, double windowWidth) : base(windowCenter, windowWidth) {
		}
		#endregion

		#region Public Properties
		public override int this[int value] {
			get {
				unchecked {
					return (int)Math.Round(255.0 / (1.0 + Math.Exp(-4.0 * ((value - WindowCenter) / WindowWidth))));
				}
			}
		}
		#endregion
	}
}
