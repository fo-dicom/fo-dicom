﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Dicom.IO;

    using Xunit;

    [Collection("Imaging")]
    public class ImageManagerTest
    {
        #region Fields

        private readonly object @lock = new object();

        #endregion

        #region Unit tests

        [Fact]
        public void SetImplementation_WinForms_ImageManagerUsesWinFormsImplementation()
        {
            lock (@lock)
            {
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                var image = ImageManager.CreateImage(100, 100, 4, false, false, 0, new PinnedIntArray(100 * 100));
                Assert.IsType<WinFormsImage>(image);
            }
        }

        [Fact]
        public void SetImplementation_WPF_ImageManagerUsesWPFImplementation()
        {
            lock (@lock)
            {
                ImageManager.SetImplementation(WPFImageManager.Instance);
                var image = ImageManager.CreateImage(100, 100, 4, false, false, 0, new PinnedIntArray(100 * 100));
                Assert.IsType<WPFImage>(image);
            }
        }

        #endregion
    }
}