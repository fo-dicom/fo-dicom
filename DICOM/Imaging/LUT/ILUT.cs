namespace Dicom.Imaging.LUT {
	public interface ILUT {
		bool IsValid {
			get;
		}

		int MinimumOutputValue {
			get;
		}

		int MaximumOutputValue {
			get;
		}

		int this[int input] {
			get;
		}

		void Recalculate();
	}
}
