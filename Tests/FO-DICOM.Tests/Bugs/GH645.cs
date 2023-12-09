// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH645
    {
        [Fact]
        public void UncompressedFrameSize_ShouldBeCorrect()
        {
            // Arrange
            var dicomFile = DicomFile.Open(TestData.Resolve("GH645.dcm"));

            // Act
            var pixelData = DicomPixelData.Create(dicomFile.Dataset);
            var uncompressedFrameSize = pixelData.UncompressedFrameSize;

            // Assert
            Assert.Equal(525312, uncompressedFrameSize);
        }

    }
}
