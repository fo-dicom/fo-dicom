// Copyright (c) 2012-2015 fo-dicom contributors.
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
            var image = new WinFormsImage(100, 100, 3, false, false, 0, new PinnedIntArray(100 * 100));
            Assert.IsAssignableFrom<Image>(image.As<Image>());
        }

        [Fact]
        public void As_ImageSource_Throws()
        {
            var image = new WinFormsImage(100, 100, 3, false, false, 0, new PinnedIntArray(100 * 100));
            Assert.Throws(typeof(DicomImagingException), () => image.As<ImageSource>());
        }

        #endregion
    }
}