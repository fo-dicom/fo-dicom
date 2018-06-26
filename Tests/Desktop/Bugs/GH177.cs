﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using Xunit;

    public class GH177
    {
        [Theory]
        [InlineData(@".\Test Data\GH177_D_CLUNIE_CT1_IVRLE_BigEndian_undefined_length.dcm")]
        [InlineData(@".\Test Data\GH177_D_CLUNIE_CT1_IVRLE_BigEndian_ELE_undefinded_length.dcm")]
        public void DicomFile_Open_ShouldNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(fileName));
            Assert.Null(e);
        }
    }
}
