// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection("Imaging")]
    public class ImageManagerTest
    {
        #region Fields

        private readonly object _lock = new object();

        #endregion

        #region Unit tests

        [Fact]
        public void SetImplementation_RawImage_ImageManagerUsesRawImageImplementation()
        {
            lock (_lock)
            {
                var image = ImageManager.CreateImage(100, 100);
                image.Render(4, false, false, 0);
                Assert.IsType<RawImage>(image);
            }
        }

        #endregion
    }
}
