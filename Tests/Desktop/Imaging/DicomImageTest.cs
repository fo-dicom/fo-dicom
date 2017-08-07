// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Imaging
{
    using System.Drawing;
    using System.Windows.Media;

    using Xunit;

    [Collection("Imaging")]
    public class DicomImageTest
    {
        #region Fields

        private readonly object @lock = new object();

        #endregion

        #region Unit tests

        [Fact]
        public void RenderImage_WinFormsManager_AsReturnsImage()
        {
            lock (this.@lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<Image>(image.As<Image>());
            }
        }

        public void RenderImage_WinFormsManager_AsReturnsBitmap()
        {
            lock (this.@lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<Bitmap>(image.As<Bitmap>());
            }
        }

        [Fact]
        public void RenderImage_WPFManager_AsReturnsImageSource()
        {
            lock (this.@lock)
            {
                ImageManager.SetImplementation(WPFImageManager.Instance);
                var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle").RenderImage();
                Assert.IsAssignableFrom<ImageSource>(image.As<ImageSource>());
            }
        }

        [Fact]
        public void Scale_MultithreadedAccess_ShouldNotThrow()
        {
            var width = 0;
            var image = new DicomImage(@".\Test Data\CT-MONO2-16-ankle");
            var exception = Record.Exception(() =>
            {
                Parallel.For(0, 100, i =>
                {
                    image.Scale = 0.999;
                    image.RenderImage().AsBitmap();
                    width = image.Width;
                });
            });

            Assert.Null(exception);
            Assert.True(width > 0);
        }

        #endregion
    }
}