// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text.RegularExpressions;

using Xunit;

namespace Dicom
{
    public class DicomUIDGeneratorTest
    {
        #region Unit tests

        [Fact]
        public void GenerateDerivedFromUUID_MultipleValues_AllValuesContainOnlyDigitsAndDecimalDots()
        {
            for (var i = 0; i < 1000; ++i)
            {
                var uid = DicomUIDGenerator.GenerateDerivedFromUUID();
                var invalids = Regex.Replace(uid.UID, @"[0-9\.]", "");
                Assert.Equal(0, invalids.Length);
            }
        }

        #endregion
    }
}
