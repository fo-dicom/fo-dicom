// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.LUT;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    [Collection(TestCollections.General)]
    public class ModalityRescaleLUTTest
    {
        #region Unit tests

        [Fact]
        public void ModalityRescaleLutReturnsCorrectMinimumValue()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalityRescaleLUT(options);
            Assert.Equal(0, lut.MinimumOutputValue);
        }

        [Fact]
        public void ModalityRescaleLutReturnsCorrectMaximumValue()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalitySequenceLUT(options);
            Assert.Equal(1023, lut.MaximumOutputValue);
        }

        [Fact]
        public void ModalityRescaleLutReturnsCorrectRescaleIntercept()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalityRescaleLUT(options);
            Assert.Equal(0.0, lut.RescaleIntercept);
        }

        [Fact]
        public void ModalityRescaleLutReturnsCorrectRescaleSlope()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset);
            var lut = new ModalityRescaleLUT(options);
            Assert.Equal(1.0, lut.RescaleSlope);
        }

        #endregion
    }
}
