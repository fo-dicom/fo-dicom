// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Xunit;

    public class GrayscaleRenderOptionsTest
    {
        #region Unit tests

        [Fact]
        public void ColorMap_Monochrome2ImageOptions_ReturnsMonochrome2ColorMap()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            Assert.Same(ColorTable.Monochrome2, options.ColorMap);
        }

        [Fact]
        public void ColorMap_Setter_ReturnsSetColorMap()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            options.ColorMap = ColorTable.Monochrome1;
            Assert.Same(ColorTable.Monochrome1, options.ColorMap);
        }

        #endregion
    }
}