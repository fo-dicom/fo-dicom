// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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

        #endregion
    }
}