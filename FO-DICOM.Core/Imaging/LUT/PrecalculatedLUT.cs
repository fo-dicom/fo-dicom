// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    public class PrecalculatedLUT : ILUT
    {
        #region Private Members

        private readonly ILUT _lut;

        private readonly int _minValue;

        private readonly int _maxValue;

        private readonly int[] _table;

        private readonly int _offset;

        #endregion

        #region Public Constructor

        public PrecalculatedLUT(ILUT lut, int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _offset = -_minValue;
            _table = new int[_maxValue - _minValue + 1];
            _lut = lut;
        }

        #endregion

        #region Public Properties

        public bool IsValid => _lut.IsValid;

        public double MinimumOutputValue => _lut.MinimumOutputValue;

        public double MaximumOutputValue => _lut.MaximumOutputValue;

        public double this[double value]
        {
            get
            {
                unchecked
                {
                    int p = (int)value + _offset;
                    if (p < 0) return _table[0];
                    if (p >= _table.Length) return _table[_table.Length - 1];
                    return _table[p];
                }
            }
        }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
            if (IsValid) return;

            _lut.Recalculate();

            for (int i = _minValue; i <= _maxValue; i++)
            {
                _table[i + _offset] = (int)_lut[i];
            }
        }

        #endregion
    }
}
