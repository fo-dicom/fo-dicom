// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1905
    {

        [Fact]
        public void RenderingRadiationDoseWithLowWindowWidth()
        {
            var file = DicomFile.Open(TestData.Resolve("GH1905.dcm"));
            var dicomImage = new DicomImage(file.Dataset);
            IImage image = dicomImage.RenderImage(0);
            Assert.NotNull(image);

            // verify some pixels, to ensure it is not completely white or black
            Assert.Equal(1280, image.Width);
            Assert.Equal(1280, image.Height);
            Assert.Equal(5, image.GetPixel(0, 0).R);
            Assert.Equal(236, image.GetPixel(600, 600).R);
            Assert.Equal(10, image.GetPixel(100, 800).R);
        }

    }
}
