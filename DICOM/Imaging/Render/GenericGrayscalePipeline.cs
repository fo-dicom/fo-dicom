using System;

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	public class GenericGrayscalePipeline : IPipeline {
		#region Private Members
		private CompositeLUT _lut;
		private ModalityLUT _rescaleLut;
		private VOILUT _voiLut;
		private OutputLUT _outputLut;
		private InvertLUT _invertLut;
		#endregion

		#region Public Constructor
		public GenericGrayscalePipeline(GrayscaleRenderOptions options) {
			if (options.RescaleSlope != 1.0 || options.RescaleIntercept != 0.0)
				_rescaleLut = new ModalityLUT(options.BitDepth.MinimumValue, options.BitDepth.MaximumValue, 
											 options.RescaleSlope, options.RescaleIntercept);
			_voiLut = VOILUT.Create(options.VOILUTFunction, options.WindowCenter, options.WindowWidth);
			_outputLut = new OutputLUT(options.Monochrome1 ? ColorTable.Monochrome1 : ColorTable.Monochrome2);
			if (options.Invert)
				_invertLut = new InvertLUT(_outputLut.MinimumOutputValue, _outputLut.MaximumOutputValue);
		}
		#endregion

		#region Public Properties
		public ILUT LUT {
			get {
				if (_lut == null) {
					CompositeLUT composite = new CompositeLUT();
					if (_rescaleLut != null)
						composite.Add(_rescaleLut);
					composite.Add(_voiLut);
					composite.Add(_outputLut);
					if (_invertLut != null)
						composite.Add(_invertLut);
					_lut = composite;
				}
				return _lut;
			}
		}
		#endregion
	}
}
