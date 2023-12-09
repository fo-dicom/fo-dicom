// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Imaging.LUT
{

    public class CompositeLUT : ILUT
    {
        #region Private Members

        private readonly List<ILUT> _luts = new List<ILUT>();

        #endregion

        #region Public Properties

        public ILUT FinalLUT => _luts.LastOrDefault();

        #endregion

        #region Public Constructor

        public CompositeLUT()
        {
        }

        #endregion

        #region Public Members

        public void Add(ILUT lut) => _luts.Add(lut);

        #endregion

        #region ILUT Members

        public double MinimumOutputValue => FinalLUT?.MinimumOutputValue ?? 0;

        public double MaximumOutputValue => FinalLUT?.MaximumOutputValue ?? 0;

        public bool IsValid => _luts.All(l => l.IsValid);

        public double this[double value]
        {
            get
            {
                foreach (ILUT lut in _luts)
                {
                    value = lut[value];
                }

                return value;
            }
        }

        public void Recalculate()
        {
            foreach (ILUT lut in _luts)
            {
                lut.Recalculate();
            }
        }

        #endregion
    }
}
