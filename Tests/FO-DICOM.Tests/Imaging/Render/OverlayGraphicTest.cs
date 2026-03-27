// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Render
{
    [Collection(TestCollections.ImageSharp)]
    public class OverlayGraphicTest
    {
        [FactForNetCore]
        public void RenderOutOfBoundsOrigin()
        {
            // Arrange
            var dicomFile = DicomFile.Open("Test Data/OutOfBoundsOverlay.dcm");
            var expectedColor = Color32.White;

            // Act
            var dicomImage = new DicomImage(dicomFile.Dataset)
            {
                OverlayColor = Color32.White.Value
            };
            var image = dicomImage.RenderImage(0);

            // Assert
            var actualColor = image.GetPixel(25, 25);
            Assert.Equal(expectedColor, actualColor);

        }
    }
}
