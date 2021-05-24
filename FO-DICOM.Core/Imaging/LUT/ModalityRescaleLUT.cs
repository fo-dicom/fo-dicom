// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Modality Rescale LUT implementation of <seealso cref="IModalityLUT"/> and <seealso cref="ILUT"/>
    /// </summary>
    public class ModalityRescaleLUT : IModalityLUT
    {
        #region Private Members

        private readonly GrayscaleRenderOptions _renderOptions;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <seealso cref="ModalityRescaleLUT"/> using the specified slope and intercept parameters
        /// </summary>
        /// <param name="options">Render options</param>
        public ModalityRescaleLUT(GrayscaleRenderOptions options)
        {
            _renderOptions = options;
            MinimumOutputValue = this[options.BitDepth.MinimumValue];
            MaximumOutputValue = this[options.BitDepth.MaximumValue];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The modality rescale slope
        /// </summary>
        public double RescaleSlope => _renderOptions.RescaleSlope;

        /// <summary>
        /// The modality rescale intercept
        /// </summary>
        public double RescaleIntercept => _renderOptions.RescaleIntercept;

        public bool IsValid => true;

        public double MinimumOutputValue { get; }

        public double MaximumOutputValue { get; }

        public double this[double value] => (value * RescaleSlope) + RescaleIntercept;

        #endregion

        #region Public Methods

        public void Recalculate()
        {
        }

        #endregion
    }
}
