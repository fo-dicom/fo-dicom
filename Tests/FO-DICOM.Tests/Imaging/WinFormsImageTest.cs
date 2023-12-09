// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Drawing;
using FellowOakDicom.Imaging;
using Xunit;
using Image = SixLabors.ImageSharp.Image;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection(TestCollections.General)]
    public class WinFormsImageTest
    {
#region Unit tests

        [Fact(Skip = "Re-enable when ImageSharp strong names their assemblies")] // TODO re-enable this
        public void As_Image_ReturnsImage()
        {
            var image = new WinFormsImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.IsAssignableFrom<Image>(image.As<Image>());
        }

        [Fact]
        public void As_RawImage_Throws()
        {
            var image = new WinFormsImage(100, 100);
            image.Render(3, false, false, 0);
            Assert.Throws<DicomImagingException>(() => image.As<RawImage>());
        }


        /// <summary>
        /// cloned bitmap should be good after disposing WinFormsImage
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_WinFormsImage_DoesNot_Harm_Cloned_Bitmap()
        {
            Bitmap bitmap;

            using (var image = new WinFormsImage(100, 100))
            {
                //  render and acquire cloned bitmap
                image.Render(3, false, false, 0);
                bitmap = image.AsClonedBitmap();
            }

            //  cloned bitmap should be in good shape after disposing WinFormsImage.
            Assert.Equal(100, bitmap.Width);
        }

        /// <summary>
        /// WinFormsImage should be good after disposing cloned bitmap.
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_Cloned_Bitmap_DoesNot_Harm_WinFormsImage()
        {
            Bitmap bitmap;

            using (var image = new WinFormsImage(100, 100))
            {
                image.Render(3, false, false, 0);

                //  acquire cloned bitmap #1, dispose immediately.
                using (image.AsClonedBitmap()) { }

                //  acquire cloned bitmap #2. must not fail.
                bitmap = image.AsClonedBitmap();
            }

            //  bitmap #2 should be in good shape.
            Assert.Equal(100, bitmap.Width);
        }

        /// <summary>
        /// Bitmap does not crash each other.
        /// see issue #634.
        /// </summary>
        [Fact]
        public void Disposing_Cloned_Bitmap_DoesNot_Harm_Cloned_Bitmap()
        {
            Bitmap bitmap1;
            Bitmap bitmap2;

            using (var image = new WinFormsImage(100, 100))
            {
                image.Render(3, false, false, 0);

                //  acquire cloned bitmap #1.
                bitmap1 = image.AsClonedBitmap();

                //  acquire cloned bitmap #2.
                bitmap2 = image.AsClonedBitmap();

            }

            //  cloned bitmap #1 should be in good shape.
            Assert.Equal(100, bitmap1.Width);

            //  cloned bitmap #2 should be in good shape.
            Assert.Equal(100, bitmap2.Width);

            //  dispose cloned bitmap #1
            bitmap1.Dispose();

            //  cloned bitmap #2 should be still in good shape.
            Assert.Equal(100, bitmap2.Width);
        }

#endregion
    }
}
