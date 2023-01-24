// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    [Collection(TestCollections.WithTranscoder)]
    public class OutputLUTTest
    {
        #region Unit tests

        [FactForNetCore]
        public void ColorMap_Monochrome2ImageOptions_ReturnsMonochrome2ColorMap()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset, 0);
            var lut = new OutputLUT(options);
            Assert.Same(ColorTable.Monochrome2, lut.ColorMap);
        }

        #endregion
    }
}
