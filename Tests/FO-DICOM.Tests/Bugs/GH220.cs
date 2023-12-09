// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH220
    {
        [Theory]
        [InlineData("GH220.dcm")]
        public void DicomFile_Open_DoesNotThrow(string fileName)
        {
            var e = Record.Exception(() => DicomFile.Open(TestData.Resolve(fileName)));
            Assert.Null(e);
        }
    }
}
