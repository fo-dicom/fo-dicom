// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    #region Unit tests

    [Collection(TestCollections.General)]
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
            MinimumOutputValue = min;
            MaximumOutputValue = max;
            IsValid = false;
        }

        public bool IsValid { get; private set; }
        public double MinimumOutputValue { get; private set; }
        public double MaximumOutputValue { get; private set; }

        public double this[double input] => input;

        public void Recalculate()
        {
            IsValid = true;
        }
    }

    #endregion
}
