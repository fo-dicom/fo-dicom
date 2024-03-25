// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.LUT;
using System.Linq;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// Grayscale color pipeline implementation of <see cref="IPipeline"/> interface
    /// </summary>
    public class GenericGrayscalePipeline : IPipeline
    {
        #region Private Members

        private readonly object _lutLock = new object();

        private CompositeLUT _lut;

        private readonly IModalityLUT _modalityLut;

        private readonly VOISequenceLUT _voiSequenceLut;

        private readonly GrayscaleRenderOptions _options;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initialize new instance of <see cref="GenericGrayscalePipeline"/> which consist of the following sequence
        /// Rescale (Modality) LUT -> VOI LUT -> Output LUT and optionally Invert LUT if specified by grayscale options
        /// </summary>
        /// <param name="options">Grayscale options to use in the pipeline</param>
        public GenericGrayscalePipeline(GrayscaleRenderOptions options)
        {
            _options = options;
            if (options.ModalityLUT != null)
            {
                _modalityLut = options.ModalityLUT;
            }
            else if (_options.RescaleSlope != 1.0 || _options.RescaleIntercept != 0.0)
            {
                _modalityLut = new ModalityRescaleLUT(_options);
            }

            if (options.VOILUTSequence != null)
            {
                _voiSequenceLut = new VOISequenceLUT(_options.VOILUTSequence.First());
            }
        }

        #endregion

        #region Public Properties

        public double WindowWidth
        {
            get => _options.WindowWidth;
            set
            {
                if (value != _options.WindowWidth)
                {
                    _options.WindowWidth = value;
                    ResetLut();
                }
            }
        }

        public double WindowCenter
        {
            get => _options.WindowCenter;
            set
            {
                if (value != _options.WindowCenter)
                {
                    _options.WindowCenter = value;
                    ResetLut();
                }
            }
        }

        public bool UseVOILUT
        {
            get => _options.UseVOILUT;
            set
            {
                if (value != _options.UseVOILUT)
                {
                    _options.UseVOILUT = value;
                    ResetLut();
                }
            }
        }

        public bool Invert
        {
            get => _options.Invert;
            set
            {
                if (value != _options.Invert)
                {
                    _options.Invert = value;
                    ResetLut();
                }
            }
        }

        public Color32[] GrayscaleColorMap
        {
            get => _options.ColorMap;
            set
            {
                _options.ColorMap = value;
                ResetLut();
            }

        }

        /// <summary>
        /// Get <see cref="FellowOakDicom.Imaging.LUT.CompositeLUT"/> of LUTs available in this pipeline instance
        /// </summary>
        public ILUT LUT
        {
            get
            {
                lock (_lutLock)
                {
                    if (_lut == null)
                    {
                        var composite = new CompositeLUT();
                        if (_modalityLut != null)
                        {
                            composite.Add(_modalityLut);
                        }

                        if (_voiSequenceLut != null)
                        {
                            composite.Add(_voiSequenceLut);
                        }

                        if (_options.UseVOILUT && _voiSequenceLut != null)
                        {
                            var voiLut = new VOILinearLUT(GrayscaleRenderOptions.CreateLinearOption(_options.BitDepth,
                                _voiSequenceLut.MinimumOutputValue, _voiSequenceLut.MaximumOutputValue));
                            composite.Add(voiLut);
                        }
                        else
                        {
                            var voiLut = VOILUT.Create(_options);
                            composite.Add(voiLut);
                        }

                        var outputLut = new OutputLUT(_options);
                        composite.Add(outputLut);

                        if (_options.Invert)
                        {
                            var invertLut = new InvertLUT(outputLut.MinimumOutputValue, outputLut.MaximumOutputValue);
                            composite.Add(invertLut);
                        }

                        _lut = composite;
                    }
                    
                    return new PrecalculatedLUT(_lut, _options.BitDepth.MinimumValue, _options.BitDepth.MaximumValue);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void ClearCache() => ResetLut();

        #endregion

        #region Private Methods

        private void ResetLut()
        {
            lock (_lutLock)
            {
                _lut = null;
            }
        }

        #endregion

    }
}
