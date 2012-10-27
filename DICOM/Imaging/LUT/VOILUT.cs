using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.LUT {

	/// <summary>
	/// Abstract VOI LUT implementation of <seealso cref="ILUT"/>
	/// </summary>
	public abstract class VOILUT : ILUT {
		#region Private Members
		private GrayscaleRenderOptions _renderOptions;

		private double _windowCenter;
		private double _windowWidth;

		private double _windowCenterMin05;
		private double _windowWidthMin1;
		private double _windowWidthDiv2;
		private int _windowStart;
		private int _windowEnd;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="VOUTLUT"/>
		/// </summary>
		/// <param name="options">Render options</param>
		public VOILUT(GrayscaleRenderOptions options) {
			_renderOptions = options;
			Recalculate();
		}
		#endregion

		#region Public Properties
		protected double WindowCenter {
			get { return _windowCenter; }
		}

		protected double WindowWidth {
			get { return _windowWidth; }
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

		public int MinimumOutputValue {
			get { return 0; }
		}

		public int MaximumOutputValue {
			get { return 255; }
		}

		public bool IsValid {
			get {
				// always recalculate
				return false;
			}
		}

		public abstract int this[int value] {
			get;
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
			if (_renderOptions.WindowWidth != _windowWidth || _renderOptions.WindowCenter != _windowCenter) {
				_windowWidth = _renderOptions.WindowWidth;
				_windowCenter = _renderOptions.WindowCenter;
				_windowCenterMin05 = _windowCenter - 0.5;
				_windowWidthMin1 = _windowWidth - 1;
				_windowWidthDiv2 = _windowWidthMin1 / 2;
				_windowStart = (int)(_windowCenterMin05 - _windowWidthDiv2);
				_windowEnd = (int)(_windowCenterMin05 + _windowWidthDiv2);
			}
		}
		#endregion

		#region Factory Methods
		/// <summary>
		/// Create a new VOILUT according to <paramref name="function"/>, <paramref name=" windowCenter"/>
		/// and <paramref name="windowWidth"/>
		/// </summary>
		/// <param name="options">Render options</param>
		/// <returns></returns>
		public static VOILUT Create(GrayscaleRenderOptions options) {
			switch (options.VOILUTFunction.ToUpper()) {
			case "SIGMOID":
				return new VOISigmoidLUT(options);
			default:
				break;
			}

			return new VOILinearLUT(options);
		}
		#endregion
	}

	/// <summary>
	/// LINEAR VOI LUT
	/// </summary>
	public class VOILinearLUT : VOILUT {
		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="VOILinearLUT"/>
		/// </summary>
		/// <param name="options">Render options</param>
		public VOILinearLUT(GrayscaleRenderOptions options) : base(options) {
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

	/// <summary>
	/// SIGMOID VOI LUT
	/// </summary>
	public class VOISigmoidLUT : VOILUT {
		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="VOISigmoidLUT"/>
		/// </summary>
		/// <param name="options">Render options</param>
		public VOISigmoidLUT(GrayscaleRenderOptions options) : base(options) {
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
