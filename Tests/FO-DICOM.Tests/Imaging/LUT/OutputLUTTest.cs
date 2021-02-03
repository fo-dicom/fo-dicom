// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    [Collection("WithTranscoder")]
    public class OutputLUTTest
    {
        #region Unit tests

        [FactForNetCore]
        public void ColorMap_Monochrome2ImageOptions_ReturnsMonochrome2ColorMap()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new OutputLUT(options);
            Assert.Same(ColorTable.Monochrome2, lut.ColorMap);
        }

        #endregion
    }
}
