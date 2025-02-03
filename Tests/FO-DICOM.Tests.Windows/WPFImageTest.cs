// Copyright (c) 2012-2024 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Drawing;
using System.Windows.Media.Imaging;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection("Windows")]
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
