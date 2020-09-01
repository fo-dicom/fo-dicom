using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Dicom.Imaging
{
    [Collection("Imaging")]
    public class ImageSharpRenderingTests
    {
        [Fact]
        public void TestRenderImage()
        {
            ImageManager.SetImplementation(new ImageSharpImageManager());
            var image = ImageManager.CreateImage(100, 100);
            image.Render(4, false, false, 0);
            Assert.IsType<ImageSharpImage>(image);
        }

        [Fact]
        public void TestActualRendering()
        {
            ImageManager.SetImplementation(new ImageSharpImageManager());
            var dcmImage = new DicomImage(@"Test Data\CR-MONO1-10-chest");
            var image = dcmImage.RenderImage();
            Assert.IsAssignableFrom<Image<Bgra32>>(image.AsSharpImage());
            var sharpImage = image.AsSharpImage();
            Assert.Equal(dcmImage.Width, sharpImage.Width);
            Assert.Equal(dcmImage.Height, sharpImage.Height);
        }

    }
}
