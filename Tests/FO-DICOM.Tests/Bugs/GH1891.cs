// Copyright (c) 2012-2024 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1891
    {
        [Fact]
        public void DicomFile_ContainingVOILUTFunctionWithEmptyValue_GrayscaleRenderOptionsMustPass()
        {
            var file = DicomFile.Open("./Test Data/GH1891.dcm");
            var dicomImage = new DicomImage(file.Dataset);
            IImage image = dicomImage.RenderImage(0); // It threw an exception at this point
            Assert.NotNull(image);
        }
    }
}
