// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Invert LUT implementation of <see cref="ILUT"/> to invert grayscale images
    /// </summary>
    public class InvertLUT : ILUT
    {
        #region Private Members

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="InvertLUT"/> 
        /// </summary>
        /// <param name="minValue">Miniumum input value</param>
        /// <param name="maxValue">Maximum output value</param>
        public InvertLUT(double minValue, double maxValue)
        {
            MinimumOutputValue = minValue;
            MaximumOutputValue = maxValue;
        }

        #endregion

        #region Public Properties

        public bool IsValid => true;

        public double MinimumOutputValue { get; }

        public double MaximumOutputValue { get; }

        public double this[double value] => MaximumOutputValue - value;

        #endregion

        #region Public Methods

        public void Recalculate()
        {
        }

        #endregion
    }
}
