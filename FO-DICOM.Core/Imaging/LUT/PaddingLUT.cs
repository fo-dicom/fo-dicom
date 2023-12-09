// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    public class PaddingLUT : ILUT
    {
        #region Private Members

        private int _paddingValue;

        private int _minValue;

        private int _maxValue;

        #endregion

        #region Public Constructors

        public PaddingLUT(int minValue, int maxValue, int paddingValue)
        {
            _paddingValue = paddingValue;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        #endregion

        #region Public Properties

        public double PixelPaddingValue => _paddingValue;

        public double MinimumOutputValue => _minValue;

        public double MaximumOutputValue => _maxValue;

        public bool IsValid => true;

        public double this[double value]
        {
            get
            {
                if (value == _paddingValue) return _minValue;
                return value;
            }
        }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
        }

        #endregion
    }
}
