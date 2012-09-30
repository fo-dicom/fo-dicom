using System;

namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Modalit LUT implementation of <seealso cref="ILUT"/>
	/// </summary>
	public class ModalityLUT : ILUT {
		#region Private Members
		private double _rescaleSlope;
		private double _rescaleIntercept;

		private int _minValue;
		private int _maxValue;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="ModalityLUT"/> using the specified slope and intercept parameters
		/// </summary>
		/// <param name="minValue">The minimum input pixel value</param>
		/// <param name="maxValue">The maximum input pixel value</param>
		/// <param name="slope">The modality LUT rescale slope</param>
		/// <param name="intercept">The modality LUT rescale intercept</param>
		public ModalityLUT(int minValue, int maxValue, double slope, double intercept) {
			_rescaleSlope = slope;
			_rescaleIntercept = intercept;
			_minValue = this[minValue];
			_maxValue = this[maxValue];
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The modality rescale slope
		/// </summary>
		public double RescaleSlope {
			get { return _rescaleSlope; }
		}

		/// <summary>
		/// The modality rescale intercept
		/// </summary>
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
