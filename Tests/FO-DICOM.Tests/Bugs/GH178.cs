// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH178
    {
        [Theory]
        [InlineData("GH178.dcm")]
        [InlineData("GH184.dcm")]
        public void DicomFile_Open_DoesNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(TestData.Resolve(fileName)));
            Assert.Null(e);
        }
    }
}
