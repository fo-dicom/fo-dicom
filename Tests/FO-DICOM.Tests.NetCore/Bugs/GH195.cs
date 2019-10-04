// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using Dicom.Imaging;

    using Xunit;

    [Collection("General")]
    public class GH195
    {
        [Theory]
        [InlineData(@".\Test Data\GH195.dcm")]
        public void DicomImage_Constructor_DoesNotThrow(string fileName)
        {
            var file = DicomFile.Open(fileName);
            var e = Record.Exception(() => new DicomImage(file.Dataset));
            Assert.Null(e);
        }
    }
}
