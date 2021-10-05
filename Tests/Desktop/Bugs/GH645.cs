// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging;
using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH645
    {
        [Fact]
        public void UncompressedFrameSize_ShouldBeCorrect()
        {
            // Arrange
            var dicomFile = DicomFile.Open(@"Test Data\GH645.dcm");

            // Act
            var pixelData = DicomPixelData.Create(dicomFile.Dataset);
            var uncompressedFrameSize = pixelData.UncompressedFrameSize;

            // Assert
            Assert.Equal(525312, uncompressedFrameSize);
        }

    }
}
