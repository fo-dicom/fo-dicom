// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
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
