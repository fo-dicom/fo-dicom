using System;

namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Invert LUT implementation of <seealso cref="ILUT"/> to invert grayscale images
	/// </summary>
	public class InvertLUT : ILUT {
		#region Private Members
		private int _minValue;
		private int _maxValue;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="InvertLUT"/> 
		/// </summary>
		/// <param name="minValue">Miniumum input value</param>
		/// <param name="maxValue">Maximum output value</param>
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
