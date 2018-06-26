﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH220
    {
        [Theory]
        [InlineData(@".\Test Data\GH220.dcm")]
        public void DicomFile_Open_DoesNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(fileName));
            Assert.Null(e);
        }
    }
}
