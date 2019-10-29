// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    #region Unit tests

    [Collection("General")]
    public class PrecalculatedLUTTest
    {
        [Theory(DisplayName = "Issue #219 ")]
        [InlineData(-80, -50)]
        [InlineData(0, 0)]
        [InlineData(80, 50)]
        public void Indexer_Get_AlwaysReturnValidValue(int input, int expected)
        {
            var lut = new PrecalculatedLUT(new MockLUT(-100, 100), -50, 50);
            lut.Recalculate();

            var actual = lut[input];
            Assert.Equal(expected, actual);
        }
    }

    #endregion

    #region Support classes

    internal class MockLUT : ILUT
    {
        internal MockLUT(int min, int max)
        {
            this.MinimumOutputValue = min;
            this.MaximumOutputValue = max;
            this.IsValid = false;
        }

        public bool IsValid { get; private set; }
        public int MinimumOutputValue { get; private set; }
        public int MaximumOutputValue { get; private set; }

        public int this[int input]
        {
            get
            {
                return input;
            }
        }

        public void Recalculate()
        {
            this.IsValid = true;
        }
    }

    #endregion
}
