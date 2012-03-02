namespace Dicom.Imaging {
	public class GrayscaleRenderOptions {
		public GrayscaleRenderOptions() {
			RescaleSlope = 1.0;
			RescaleIntercept = 0.0;
			WindowWidth = double.MaxValue;
			WindowCenter = 0.0;
		}

		public GrayscaleRenderOptions(BitDepth bits) {
			RescaleSlope = 1.0;
			RescaleIntercept = 0.0;
			WindowWidth = bits.MaximumValue - bits.MinimumValue;
			WindowCenter = (bits.MaximumValue + bits.MinimumValue) / 2.0;
		}

		public double RescaleSlope {
			get;
			set;
		}

		public double RescaleIntercept {
			get;
			set;
		}

		public double WindowWidth {
			get;
			set;
		}

		public double WindowCenter {
			get;
			set;
		}
	}
}
