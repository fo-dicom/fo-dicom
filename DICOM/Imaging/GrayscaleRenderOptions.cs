namespace Dicom.Imaging {
	public class GrayscaleRenderOptions {
		public GrayscaleRenderOptions(BitDepth bits) {
			BitDepth = bits;
			RescaleSlope = 1.0;
			RescaleIntercept = 0.0;
			WindowWidth = bits.MaximumValue - bits.MinimumValue;
			WindowCenter = (bits.MaximumValue + bits.MinimumValue) / 2.0;
			Monochrome1 = false;
			Invert = false;
		}

		public BitDepth BitDepth {
			get;
			set;
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

		public bool Monochrome1 {
			get;
			set;
		}

		public bool Invert {
			get;
			set;
		}

		public static GrayscaleRenderOptions FromDataset(DicomDataset dataset) {
			var bits = BitDepth.FromDataset(dataset);
			var options = new GrayscaleRenderOptions(bits);
			options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
			options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);
			if (dataset.Contains(DicomTag.WindowWidth) && dataset.Get<double>(DicomTag.WindowWidth) != 0.0) {
				options.WindowWidth = dataset.Get<double>(DicomTag.WindowWidth);
				options.WindowCenter = dataset.Get<double>(DicomTag.WindowCenter);
			}
			options.Monochrome1 = dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation) == PhotometricInterpretation.Monochrome1;
			return options;
		}
	}
}
