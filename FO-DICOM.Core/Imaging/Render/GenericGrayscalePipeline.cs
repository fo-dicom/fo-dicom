// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// Grayscale color pipeline implementation of <seealso cref="IPipeline"/> interface
    /// </summary>
    public class GenericGrayscalePipeline : IPipeline
    {
        #region Private Members

        private CompositeLUT _lut;

        private readonly IModalityLUT _modalityLut;

        private readonly VOISequenceLUT _voiSequenceLut;

        private readonly VOILUT _voiLut;

        private readonly OutputLUT _outputLut;

        private readonly InvertLUT _invertLut;

        private readonly GrayscaleRenderOptions _options;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initialize new instance of <seealso cref="GenericGrayscalePipeline"/> which consist of the following sequence
        /// Rescale (Modality) LUT -> VOI LUT -> Output LUT and optionally Invert LUT if specified by grayscale options
        /// </summary>
        /// <param name="options">Grayscale options to use in the pipeline</param>
        public GenericGrayscalePipeline(GrayscaleRenderOptions options)
        {
            _options = options;
            if (options.ModalityLUTSequence != null)
                _modalityLut = new ModalitySequenceLUT(_options);
            else if (_options.RescaleSlope != 1.0 || _options.RescaleIntercept != 0.0)
                _modalityLut = new ModalityRescaleLUT(_options);
            if(_options.VOILUTSequence != null) _voiSequenceLut = new VOISequenceLUT(_options);
            _voiLut = VOILUT.Create(_options);
            _outputLut = new OutputLUT(_options);
            if (_options.Invert) _invertLut = new InvertLUT(_outputLut.MinimumOutputValue, _outputLut.MaximumOutputValue);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get <seealso cref="CompositeLUT"/> of LUTs available in this pipeline instance
        /// </summary>
        public ILUT LUT
        {
            get
            {
                if (_lut == null)
                {
                    CompositeLUT composite = new CompositeLUT();
                    if (_modalityLut != null) composite.Add(_modalityLut);
                    if (_voiSequenceLut != null) composite.Add(_voiSequenceLut);
                    composite.Add(_voiLut);
                    composite.Add(_outputLut);
                    if (_invertLut != null) composite.Add(_invertLut);
                    _lut = composite;
                }
                return new PrecalculatedLUT(_lut, _options.BitDepth.MinimumValue, _options.BitDepth.MaximumValue);
            }
        }

        #endregion
    }
}
