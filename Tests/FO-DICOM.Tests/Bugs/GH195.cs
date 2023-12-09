// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH195
    {
        [Theory]
        [InlineData("GH195.dcm")]
        public void DicomImage_Constructor_DoesNotThrow(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName));
            var e = Record.Exception(() => new DicomImage(file.Dataset));
            Assert.Null(e);
        }
    }
}
