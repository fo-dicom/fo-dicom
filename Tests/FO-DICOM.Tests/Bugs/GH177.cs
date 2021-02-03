// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH177
    {
        [Theory]
        [InlineData(@"GH177_D_CLUNIE_CT1_IVRLE_BigEndian_undefined_length.dcm")]
        [InlineData(@"GH177_D_CLUNIE_CT1_IVRLE_BigEndian_ELE_undefinded_length.dcm")]
        public void DicomFile_Open_ShouldNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(TestData.Resolve(fileName)));
            Assert.Null(e);
        }
    }
}
