// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Modality Rescale LUT implementation of <see cref="IModalityLUT"/> and <see cref="ILUT"/>
    /// </summary>
    public class ModalityRescaleLUT : IModalityLUT
    {
        #region Private Members

        private readonly GrayscaleRenderOptions _renderOptions;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="ModalityRescaleLUT"/> using the specified slope and intercept parameters
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
        public decimal RescaleSlope => _renderOptions.RescaleSlope;

        /// <summary>
        /// The modality rescale intercept
        /// </summary>
        public decimal RescaleIntercept => _renderOptions.RescaleIntercept;

        public bool IsValid => true;

        public decimal MinimumOutputValue { get; }

        public decimal MaximumOutputValue { get; }

        public decimal this[decimal value] => (value * RescaleSlope) + RescaleIntercept;

        #endregion

        #region Public Methods

        public void Recalculate()
        {
        }

        #endregion
    }
}
