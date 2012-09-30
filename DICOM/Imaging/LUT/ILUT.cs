namespace Dicom.Imaging.LUT {
	/// <summary>
	/// Interface for Lookup table definition
	/// </summary>
	public interface ILUT {
		/// <summary>
		/// Returns true if the lookup table is valid
		/// </summary>
		bool IsValid {
			get;
		}

		/// <summary>
		/// Get the minimum output value
		/// </summary>
		int MinimumOutputValue {
			get;
		}

		/// <summary>
		/// Get the maximum output value
		/// </summary>
		int MaximumOutputValue {
			get;
		}

		/// <summary>
		/// Indexer to taransfrom input value into output value
		/// </summary>
		/// <param name="input">Input value</param>
		/// <returns>Output value</returns>
		int this[int input] {
			get;
		}

		/// <summary>
		/// Forct the recalculation of LUT
		/// </summary>
		void Recalculate();
	}
}
