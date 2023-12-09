// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Render;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH340
    {
        #region Unit tests

        [Fact]
        public void Open_BitsAllocated12_NonZeroPixelsInLastQuarter()
        {
            var file = DicomFile.Open(TestData.Resolve("GH340.dcm"));

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
