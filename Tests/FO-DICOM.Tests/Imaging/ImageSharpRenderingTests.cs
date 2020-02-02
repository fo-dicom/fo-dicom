using FellowOakDicom.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection("ImageSharp")]
    public class ImageSharpRenderingTests
    {
        [Fact]
        public void TestRenderImage()
        {
            var image = ImageManager.CreateImage(100, 100);
            image.Render(4, false, false, 0);
            Assert.IsType<ImageSharpImage>(image);
        }

        [Fact]
        public void TestActualRendering()
        {
            var dcmImage = new DicomImage(TestData.Resolve("CT-MONO2-16-ankle"));
            var image = dcmImage.RenderImage();
            Assert.IsAssignableFrom<Image<Bgra32>>(image.AsSharpImage());
            var sharpImage = image.AsSharpImage();
            Assert.Equal(dcmImage.Width, sharpImage.Width);
            Assert.Equal(dcmImage.Height, sharpImage.Height);
        }

    }
}
