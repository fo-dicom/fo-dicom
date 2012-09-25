using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	public class PaletteColorPipeline : IPipeline {
		private ILUT _lut;

		public PaletteColorPipeline(DicomPixelData pixelData) {
			var lut = pixelData.PaletteColorLUT;
			var first = pixelData.Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 1);

			_lut = new PaletteColorLUT(first, lut);
		}

		public ILUT LUT {
			get { return _lut; }
		}
	}
}
