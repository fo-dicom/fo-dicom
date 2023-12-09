// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Paletter color LUT implementation of <see cref="ILUT"/> maps PALETTE COLOR images
    /// </summary>
    public class PaletteColorLUT : ILUT
    {
        #region Private Members

        private int _first;

        private Color32[] _lut;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="PaletteColorLUT"/>
        /// </summary>
        /// <param name="firstEntry">The first entry (minium value)</param>
        /// <param name="lut">The palette color LUT</param>
        public PaletteColorLUT(int firstEntry, Color32[] lut)
        {
            _first = firstEntry;
            ColorMap = lut;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The color map
        /// </summary>
        public Color32[] ColorMap
        {
            get => _lut;
            set => _lut = value;
        }

        public double MinimumOutputValue => int.MinValue;

        public double MaximumOutputValue => int.MaxValue;

        public bool IsValid => _lut != null;

        public double this[double value] => _lut[(value - _first) > 0 ? unchecked((int)(value - _first)) : 0].Value;

        #endregion

        #region Public Methods

        public void Recalculate()
        {
        }

        #endregion
    }
}
