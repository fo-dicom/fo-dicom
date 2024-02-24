// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1442
    {
        [Fact]
        public void ModalitySequenceLUT_ShouldBeNull()
        {
            // Arrange
            var testFile = DicomFile.Open("./Test Data/GH1442.dcm");

            // Act
            var grayScaleRenderOptions = GrayscaleRenderOptions.FromDataset(testFile.Dataset, 0);

            // Assert
            Assert.Null(grayScaleRenderOptions.ModalityLUT);
        }
    }
}
