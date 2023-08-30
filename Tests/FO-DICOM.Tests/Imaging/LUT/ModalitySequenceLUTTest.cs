// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.LUT
{

    [Collection(TestCollections.General)]
    public class ModalitySequenceLUTTest
    {
        #region Unit tests

        [Fact]
        public void ModalitySequenceLutReturnsCorrectMinimumValue()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset, 0);
            Assert.NotNull(options.ModalityLUT);
            Assert.Equal(0, options.ModalityLUT.MinimumOutputValue);
        }

        [Fact]
        public void ModalitySequenceLutReturnsCorrectMaximumValue()
        {
            var file = DicomFile.Open(TestData.Resolve("CR-ModalitySequenceLUT.dcm"));
            var options = GrayscaleRenderOptions.FromDataset(file.Dataset, 0);
            Assert.NotNull(options.ModalityLUT);
            Assert.Equal(1023, options.ModalityLUT.MaximumOutputValue);
        }

        #endregion
    }
}
