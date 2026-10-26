// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

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
            // A negative rescale slope (PS3.3 C.11.1.1.2) maps the minimum stored value to the maximum output value.
            var atMinimumStoredValue = this[options.BitDepth.MinimumValue];
            var atMaximumStoredValue = this[options.BitDepth.MaximumValue];
            MinimumOutputValue = Math.Min(atMinimumStoredValue, atMaximumStoredValue);
            MaximumOutputValue = Math.Max(atMinimumStoredValue, atMaximumStoredValue);
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
