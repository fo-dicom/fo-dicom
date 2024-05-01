// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection(TestCollections.Imaging)]
    public class DicomImageTest
    {
        #region Unit tests

        [Fact]
        public void RenderImage_RawImageManager_AsReturnsRawImage()
        {
            var image = new DicomImage(TestData.Resolve("CT-MONO2-16-ankle")).RenderImage();
            Assert.IsAssignableFrom<byte[]>(image.As<byte[]>());
        }

        // [Fact(Skip = "Flaky test. Looks like the raw image manager might not be threadsafe. To investigate...")]
        [Fact()]
        public void ManipulatedImage_MultithreadedAccess_ShouldNotThrow()
        {
            var image = new DicomImage(TestData.Resolve("CT-MONO2-16-ankle"));
            var exception = Record.Exception(() =>
            {
                Parallel.For(0, 1000, i =>
                {
                    image.RenderImage().AsBytes();
                    image.Scale = 0.999;
                });
            });

            Assert.Null(exception);
        }


        [Fact]
        public void RenderImage_ColorPalette()
        {
            var file = new DicomImage(TestData.Resolve("10200904.dcm"));
            var image = file.RenderImage(0);
            Assert.IsAssignableFrom<byte[]>(image.As<byte[]>());
        }

        [Theory]
        [InlineData("TestPattern_Palette.dcm")]
        [InlineData("TestPattern_Palette_16.dcm")]
        [InlineData("TestPattern_RGB.dcm")]
        public void RenderImage_ColorPaletteWithOffset(string filename)
        {
            var file = new DicomImage(TestData.Resolve(filename));
            var image = file.RenderImage(0);
            Assert.IsAssignableFrom<byte[]>(image.As<byte[]>());
        }

        #endregion
    }
}