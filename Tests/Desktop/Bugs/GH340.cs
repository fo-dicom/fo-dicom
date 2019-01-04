// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging;
using Dicom.Imaging.Render;

namespace Dicom.Bugs
{
    using Xunit;

    [Collection("General")]
    public class GH340
    {
        #region Unit tests

        [Fact]
        public void Open_BitsAllocated12_NonZeroPixelsInLastQuarter()
        {
            var file = DicomFile.Open(@"Test Data\GH340.dcm");

            // Loop over last quarter of pixels; if one is non-zero test passes.
            var pixelData = PixelDataFactory.Create(DicomPixelData.Create(file.Dataset), 0);
            for (var y = 3 * pixelData.Height / 4; y < pixelData.Height; ++y)
            {
                for (var x = 0; x < pixelData.Width; ++x)
                {
                    if (pixelData.GetPixel(x, y) != 0.0)
                    {
                        Assert.True(true);
                        return;
                    }
                }
            }

            Assert.True(false);
        }

        #endregion
    }
}
