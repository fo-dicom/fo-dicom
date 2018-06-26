﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using Xunit;

    [Collection("General")]
    public class GH178
    {
        [Theory]
        [InlineData(@".\Test Data\GH178.dcm")]
        [InlineData(@".\Test Data\GH184.dcm")]
        public void DicomFile_Open_DoesNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(fileName));
            Assert.Null(e);
        }
    }
}
