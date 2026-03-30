// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Media;
using System;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    public class DesktopIconGeneratorTests : IClassFixture<InitializationFixture>
    {
        [Fact]
        public void GenerateIconImageSequence_ValidDicomImage_ProducesCorrectIcon()
        {
            // Arrange
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            var generator = new DesktopIconGenerator();

            // Act
            var iconSequence = generator.GenerateIconImageSequence(file.Dataset);

            // Assert
            Assert.NotNull(iconSequence);
            Assert.Equal(DicomTag.IconImageSequence, iconSequence.Tag);
            Assert.Single(iconSequence.Items);

            var iconDataset = iconSequence.Items[0];
            var width = iconDataset.GetSingleValue<ushort>(DicomTag.Columns);
            var height = iconDataset.GetSingleValue<ushort>(DicomTag.Rows);

            // Verify dimensions are within limits
            Assert.InRange(width, 1, 128);
            Assert.InRange(height, 1, 128);

            // Verify 8-bit grayscale format
            Assert.Equal((ushort)1, iconDataset.GetSingleValue<ushort>(DicomTag.SamplesPerPixel));
            Assert.Equal((ushort)8, iconDataset.GetSingleValue<ushort>(DicomTag.BitsAllocated));
            Assert.Equal("MONOCHROME2", iconDataset.GetSingleValue<string>(DicomTag.PhotometricInterpretation));

            // Verify pixel data size matches dimensions
            var pixelData = iconDataset.GetValues<byte>(DicomTag.PixelData);
            Assert.Equal(width * height, pixelData.Length);
        }

        [Fact]
        public void GenerateIconImageSequence_PreservesAspectRatio()
        {
            // Arrange - CT-MONO2-16-ankle is 512x512
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            var generator = new DesktopIconGenerator();

            // Act
            var iconSequence = generator.GenerateIconImageSequence(file.Dataset);

            // Assert
            var iconDataset = iconSequence.Items[0];
            var width = iconDataset.GetSingleValue<ushort>(DicomTag.Columns);
            var height = iconDataset.GetSingleValue<ushort>(DicomTag.Rows);

            // Should be 128x128 for square image
            Assert.Equal((ushort)128, width);
            Assert.Equal((ushort)128, height);
        }

        [Fact]
        public void GenerateIconImageSequence_NullDataset_ThrowsArgumentNullException()
        {
            var generator = new DesktopIconGenerator();
            Assert.Throws<ArgumentNullException>(() => generator.GenerateIconImageSequence(null!));
        }
    }
}