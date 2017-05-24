// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Drawing;
    using System.Windows.Media;

    using Dicom.IO;

    using Xunit;

    [Collection("General")]
    public class WinFormsImageTest
    {
        #region Unit tests

        [Fact]
        public void As_Image_ReturnsImage()
        {
            var image = new WinFormsImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.IsAssignableFrom<Image>(image.As<Image>());
        }

        [Fact]
        public void As_ImageSource_Throws()
        {
            var image = new WinFormsImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.Throws(typeof(DicomImagingException), () => image.As<ImageSource>());
        }

        #endregion
    }
}