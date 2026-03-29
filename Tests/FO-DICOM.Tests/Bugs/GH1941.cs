// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1941
    {
        [Fact]
        public void DicomFile_ContainingVOILUTFunctionWithEmptyValue_GrayscaleRenderOptionsMustPass()
        {
            var file = DicomFile.Open(TestData.Resolve("GH1941.dcm"));
            Assert.True(file.Dataset.Contains(DicomTag.BitsStored));

            var dicomImage = new DicomImage(file.Dataset);
            IImage image = dicomImage.RenderImage(0); // It threw an exception at this point
            Assert.NotNull(image);
        }
    }
}
