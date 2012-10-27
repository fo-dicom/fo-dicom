using System;

namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Modalit LUT implementation of <seealso cref="ILUT"/>
	/// </summary>
	public class ModalityLUT : ILUT {
		#region Private Members
		private GrayscaleRenderOptions _renderOptions;
		private int _minValue;
		private int _maxValue;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="ModalityLUT"/> using the specified slope and intercept parameters
		/// </summary>
		/// <param name="options">Render options</param>
		public ModalityLUT(GrayscaleRenderOptions options) {
			_renderOptions = options;
			_minValue = this[options.BitDepth.MinimumValue];
			_maxValue = this[options.BitDepth.MaximumValue];
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The modality rescale slope
		/// </summary>
		public double RescaleSlope {
			get { return _renderOptions.RescaleSlope; }
		}

		/// <summary>
		/// The modality rescale intercept
		/// </summary>
		public double RescaleIntercept {
			get { return _renderOptions.RescaleIntercept; }
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
				return unchecked((int)((value * RescaleSlope) + RescaleIntercept));
			}
		}
		#endregion

		#region Public Methods
		public void Recalculate() {
		}
		#endregion
	}
}
