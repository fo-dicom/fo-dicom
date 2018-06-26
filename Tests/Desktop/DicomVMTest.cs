﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

    [Collection("General")]
    public class DicomVMTest
    {
        #region Unit tests

        [Fact]
        public void Parse_OneToNOrOne_InterpretedAsOneToN()
        {
            var actual = DicomVM.Parse("1-n or 1");
            Assert.Equal(1, actual.Minimum);
            Assert.Equal(int.MaxValue, actual.Maximum);
            Assert.Equal(1, actual.Multiplicity);
        }

        #endregion
    }
}
