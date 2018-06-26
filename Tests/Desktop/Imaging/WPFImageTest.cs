// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Drawing;
    using System.Windows.Media.Imaging;

    using Dicom.IO;

    using Xunit;

    [Collection("General")]
    public class WPFImageTest
    {
        #region Unit tests

        [Fact]
        public void As_BitmapSource_ReturnsBitmapSource()
        {
            var image = new WPFImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.IsAssignableFrom<BitmapSource>(image.As<BitmapSource>());
        }

        [Fact]
        public void As_Bitmap_Throws()
        {
            var image = new WPFImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.Throws<DicomImagingException>(() => image.As<Bitmap>());
        }

        #endregion
    }
}
