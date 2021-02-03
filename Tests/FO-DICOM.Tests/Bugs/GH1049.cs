// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Render;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH1049
    {
        [Fact]
        public void Create_YbrFull422PlanarConfigurationZero_ReturnsPixelData()
        {
            // Arrange
            var dicomFile = DicomFile.Open(TestData.Resolve("GH1049_planar_0.dcm"));

            // Act
            var pixelData = PixelDataFactory.Create(DicomPixelData.Create(dicomFile.Dataset), 0);

            // Assert
            Assert.NotNull(pixelData);
        }

        [Fact]
        public void Create_YbrFull422PlanarConfigurationOne_ThrowsException()
        {
            // Arrange
            var dicomFile = DicomFile.Open(TestData.Resolve("GH1049_planar_1.dcm"));

            // Act
            Assert.Throws<DicomImagingException>(() =>
                PixelDataFactory.Create(DicomPixelData.Create(dicomFile.Dataset), 0));
        }
    }

}
