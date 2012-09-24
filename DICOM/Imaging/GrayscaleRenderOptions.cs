using System;

using Dicom.Imaging.Render;

namespace Dicom.Imaging {
	public class GrayscaleRenderOptions {
		public GrayscaleRenderOptions(BitDepth bits) {
			BitDepth = bits;
			RescaleSlope = 1.0;
			RescaleIntercept = 0.0;
			VOILUTFunction = "LINEAR";
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

		public string VOILUTFunction {
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
			} else if (dataset.Contains(DicomTag.SmallestImagePixelValue) && dataset.Contains(DicomTag.LargestImagePixelValue)) {
				var smallElement = dataset.Get<DicomElement>(DicomTag.SmallestImagePixelValue);
				var largeElement = dataset.Get<DicomElement>(DicomTag.LargestImagePixelValue);

				int smallValue = 0;
				int largeValue = 0;

				if (smallElement.ValueRepresentation == DicomVR.US) {
					smallValue = smallElement.Get<ushort>(0);
					largeValue = largeElement.Get<ushort>(0);
				} else {
					smallValue = smallElement.Get<short>(0);
					largeValue = largeElement.Get<short>(0);
				}

				if (smallValue != 0 || largeValue != 0) {
					options.WindowWidth = largeValue - smallValue;
					options.WindowCenter = (largeValue + smallValue) / 2.0;
				}
			} else {
				int padding = bits.MinimumValue;

				var element = dataset.Get<DicomElement>(DicomTag.PixelPaddingValue);
				if (element != null) {
					if (element.ValueRepresentation == DicomVR.US)
						padding = element.Get<ushort>(0);
					else
						padding = element.Get<short>(0);
				}

				var pixelData = DicomPixelData.Create(dataset);
				var pixels = PixelDataFactory.Create(pixelData, 0);
				var range = pixels.GetMinMax(padding);

				range.Maximum = (int)((range.Maximum * options.RescaleSlope) + options.RescaleIntercept);
				range.Minimum = (int)((range.Minimum * options.RescaleSlope) + options.RescaleIntercept);

				options.WindowWidth = range.Maximum - range.Minimum;
				options.WindowCenter = (range.Maximum + range.Minimum) / 2.0;
			}

			options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
			options.Monochrome1 = dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation) == PhotometricInterpretation.Monochrome1;

			return options;
		}
	}
}
