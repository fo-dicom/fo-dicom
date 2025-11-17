// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.NativeCodec;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection(TestCollections.ImageSharp)]
    public class ImageSharpRenderingTests
    {
        public ImageSharpRenderingTests()
        {
            // Setup ImageSharp as the image manager for these tests
            // IMPORTANT: Must include NativeTranscoderManager to avoid breaking codec tests
            var services = new ServiceCollection();
            services.AddFellowOakDicom()
                .AddTranscoderManager<NativeTranscoderManager>()
                .AddImageManager<ImageSharpImageManager>();
            var serviceProvider = services.BuildServiceProvider();
            DicomSetupBuilder.UseServiceProvider(serviceProvider);
        }

#if NET462
        [Fact(Skip = "Re-enable when ImageSharp strong names their assemblies")] // TODO re-enable this
#else
        [Fact]
#endif
        public void TestRenderImage()
        {
            var image = ImageManager.CreateImage(100, 100);
            image.Render(4, false, false, 0);
            Assert.IsType<ImageSharpImage>(image);
        }

#if NET462
        [Fact(Skip = "Re-enable when ImageSharp strong names their assemblies")] // TODO re-enable this
#else
        [Fact]
#endif
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
