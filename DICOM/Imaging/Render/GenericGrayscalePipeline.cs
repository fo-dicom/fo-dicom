using System;

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// Grayscale color pipeline implementation of <seealso cref="IPipeline"/> interface
	/// </summary>
	public class GenericGrayscalePipeline : IPipeline {
		#region Private Members
		private CompositeLUT _lut;
		private ModalityLUT _rescaleLut;
		private VOILUT _voiLut;
		private OutputLUT _outputLut;
		private InvertLUT _invertLut;
		private GrayscaleRenderOptions _options;
		#endregion

		#region Public Constructor
		/// <summary>
		/// Initialize new instance of <seealso cref="GenericGrayscalePipeline"/> which consist of the following sequence
		/// Rescale (Modality) LUT -> VOI LUT -> Output LUT and optionally Invert LUT if specified by grayscale options
		/// </summary>
		/// <param name="options">Grayscale options to use in the pipeline</param>
		public GenericGrayscalePipeline(GrayscaleRenderOptions options) {
			_options = options;
			if (_options.RescaleSlope != 1.0 || _options.RescaleIntercept != 0.0)
				_rescaleLut = new ModalityLUT(_options);
			_voiLut = VOILUT.Create(_options);
			_outputLut = new OutputLUT(_options.Monochrome1 ? ColorTable.Monochrome1 : ColorTable.Monochrome2);
			if (_options.Invert)
				_invertLut = new InvertLUT(_outputLut.MinimumOutputValue, _outputLut.MaximumOutputValue);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Get <seealso cref="CompositeLUT"/> of avaliavble LUTs available in this pipeline instance
		/// </summary>
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
				return new PrecalculatedLUT(_lut, _options.BitDepth.MinimumValue, _options.BitDepth.MaximumValue);
			}
		}
		#endregion
	}
}
