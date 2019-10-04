// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;

using Xunit;


namespace Dicom.Imaging
{
    [Collection("Imaging")]
    public class DicomImageTest
    {
        #region Fields

        private readonly object _lock = new object();

        #endregion

        #region Unit tests

        [Fact]
        public void RenderImage_WinFormsManager_AsReturnsImage()
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<Image>(image.As<Image>());
            }
        }

        [Fact]
        public void RenderImage_WinFormsManager_AsReturnsBitmap()
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<Bitmap>(image.As<Bitmap>());
            }
        }

        [Fact]
        public void RenderImage_WPFManager_AsReturnsImageSource()
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WPFImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<ImageSource>(image.As<ImageSource>());
            }
        }

        [Fact]
        public void ManipulatedImage_MultithreadedAccess_ShouldNotThrow()
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);

                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle");
                var exception = Record.Exception(() =>
                {
                    Parallel.For(0, 1000, i =>
                    {
                        image.RenderImage().AsBitmap();
                        image.Scale = 0.999;
                    });
                });

                Assert.Null(exception);
            }
        }


        [Fact]
        public void RenderImage_ColorPalette()
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var file = new DicomImage(@".\Test Data\10200904.dcm");
                var image = file.RenderImage(0);
                Assert.IsAssignableFrom<Bitmap>(image.As<Bitmap>());
            }
        }

        [Theory]
        [InlineData("TestPattern_Palette.dcm")]
        [InlineData("TestPattern_Palette_16.dcm")]
        [InlineData("TestPattern_RGB.dcm")]
        public void RenderImage_ColorPaletteWithOffset(string filename)
        {
            lock (_lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var file = new DicomImage(@".\Test Data\" + filename);
                var image = file.RenderImage(0);
                Assert.IsAssignableFrom<Bitmap>(image.As<Bitmap>());
            }

        }


        #endregion
    }
}
