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


        /// <summary>
        /// bitmap should be good after disposing WinFormsImage
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_WinFormsImage_DoesNot_Harm_Acquired_Bitmap()
        {
            Bitmap bitmap;

            using (var image = new WinFormsImage(100, 100))
            {
                //  render and acquire bitmap
                image.Render(3, false, false, 0);
                bitmap = image.AsBitmap();
            }

            //  bitmap should be in good shape after disposing WinFormsImage.
            Assert.Equal(100, bitmap.Width);
        }

        /// <summary>
        /// WinFormsImage should be good after disposing acquired bitmap.
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_Bitmap_DoesNot_Harm_WinFormsImage()
        {
            Bitmap bitmap;

            using (var image = new WinFormsImage(100, 100))
            {
                image.Render(3, false, false, 0);

                //  acquire bitmap #1, dispose immediately.
                using (image.AsBitmap()) { }

                //  acquire bitmap #2. must not fail.
                bitmap = image.AsBitmap();
            }

            //  bitmap #2 should be in good shape.
            Assert.Equal(100, bitmap.Width);
        }

        /// <summary>
        /// Bitmap does not crash each other.
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_Bitmap_DoesNot_Harm_Bitmap()
        {
            Bitmap bitmap1;
            Bitmap bitmap2;

            using (var image = new WinFormsImage(100, 100))
            {
                image.Render(3, false, false, 0);

                //  acquire bitmap #1.
                bitmap1 = image.AsBitmap();

                //  acquire bitmap #2.
                bitmap2 = image.AsBitmap();

            }

            //  bitmap #1 should be in good shape.
            Assert.Equal(100, bitmap1.Width);

            //  bitmap #2 should be in good shape.
            Assert.Equal(100, bitmap2.Width);

            //  dispose bitmap #1
            bitmap1.Dispose();

            //  bitmap #2 should be still in good shape.
            Assert.Equal(100, bitmap2.Width);
        }

        #endregion
    }
}