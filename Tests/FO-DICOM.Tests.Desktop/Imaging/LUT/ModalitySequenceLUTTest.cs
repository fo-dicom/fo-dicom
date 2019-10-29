// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    [Collection("General")]
    public class ModalitySequenceLUTTest
    {
        #region Unit tests

        [Fact]
        public void ModalitySequenceLutReturnsCorrectMinimumValue()
        {
            var file = DicomFile.Open(@".\Test Data\CR-ModalitySequenceLUT.dcm");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalitySequenceLUT(options);
            Assert.Equal(0, lut.MinimumOutputValue);
        }

        [Fact]
        public void ModalitySequenceLutReturnsCorrectMaximumValue()
        {
            var file = DicomFile.Open(@".\Test Data\CR-ModalitySequenceLUT.dcm");
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalitySequenceLUT(options);
            Assert.Equal(1023, lut.MaximumOutputValue);
        }

        #endregion
    }
}
