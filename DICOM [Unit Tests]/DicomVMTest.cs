// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

    public class DicomVMTest
    {
        #region Unit tests

        [Fact]
        public void Parse_OneToNOrOne_InterpretedAsOneToN()
        {
            var actual = DicomVM.Parse("1-n or 1");
            Assert.Equal(DicomVM.VM_1_n, actual);
        }

        #endregion
    }
}