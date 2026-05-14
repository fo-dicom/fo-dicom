// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Media;
using System;
using Xunit;

namespace FellowOakDicom.Tests.Media
{
    [Collection(TestCollections.General)]
    public class DicomDirectoryIconGenerationTests
    {
        #region DicomIconImageSequenceBuilder Tests

        [Fact]
        public void Build_ValidInput_ProducesCorrectSequence()
        {
            // Arrange
            var width = 64;
            var height = 64;
            var pixelData = new byte[width * height];
            Array.Fill(pixelData, (byte)128);

            // Act
            var sequence = DicomIconImageSequenceBuilder.Build(width, height, pixelData);

            // Assert
            Assert.NotNull(sequence);
            Assert.Equal(DicomTag.IconImageSequence, sequence.Tag);
            Assert.Single(sequence.Items);

            var iconDataset = sequence.Items[0];
            Assert.Equal((ushort)width, iconDataset.GetSingleValue<ushort>(DicomTag.Columns));
            Assert.Equal((ushort)height, iconDataset.GetSingleValue<ushort>(DicomTag.Rows));
            Assert.Equal((ushort)1, iconDataset.GetSingleValue<ushort>(DicomTag.SamplesPerPixel));
            Assert.Equal((ushort)8, iconDataset.GetSingleValue<ushort>(DicomTag.BitsAllocated));
            Assert.Equal((ushort)8, iconDataset.GetSingleValue<ushort>(DicomTag.BitsStored));
            Assert.Equal((ushort)7, iconDataset.GetSingleValue<ushort>(DicomTag.HighBit));
            Assert.Equal((ushort)0, iconDataset.GetSingleValue<ushort>(DicomTag.PixelRepresentation));
            Assert.Equal("MONOCHROME2", iconDataset.GetSingleValue<string>(DicomTag.PhotometricInterpretation));
            Assert.Equal("1", iconDataset.GetSingleValue<string>(DicomTag.NumberOfFrames));

            var embeddedPixelData = iconDataset.GetValues<byte>(DicomTag.PixelData);
            Assert.Equal(pixelData.Length, embeddedPixelData.Length);
            Assert.Equal(pixelData, embeddedPixelData);
        }

        [Fact]
        public void Build_MaxDimensions_Succeeds()
        {
            // Arrange
            var width = 128;
            var height = 128;
            var pixelData = new byte[width * height];

            // Act
            var sequence = DicomIconImageSequenceBuilder.Build(width, height, pixelData);

            // Assert
            Assert.NotNull(sequence);
            var iconDataset = sequence.Items[0];
            Assert.Equal((ushort)128, iconDataset.GetSingleValue<ushort>(DicomTag.Columns));
            Assert.Equal((ushort)128, iconDataset.GetSingleValue<ushort>(DicomTag.Rows));
        }

        [Fact]
        public void Build_NullPixelData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                DicomIconImageSequenceBuilder.Build(64, 64, null!));
        }

        [Theory]
        [InlineData(0, 64)]
        [InlineData(64, 0)]
        [InlineData(-1, 64)]
        [InlineData(64, -1)]
        public void Build_InvalidDimensions_ThrowsArgumentException(int width, int height)
        {
            var pixelData = new byte[Math.Abs(width * height)];
            Assert.Throws<ArgumentException>(() =>
                DicomIconImageSequenceBuilder.Build(width, height, pixelData));
        }

        [Theory]
        [InlineData(129, 64)]
        [InlineData(64, 129)]
        [InlineData(200, 200)]
        public void Build_DimensionsExceed128_ThrowsArgumentException(int width, int height)
        {
            var pixelData = new byte[width * height];
            Assert.Throws<ArgumentException>(() =>
                DicomIconImageSequenceBuilder.Build(width, height, pixelData));
        }

        [Fact]
        public void Build_MismatchedPixelDataLength_ThrowsArgumentException()
        {
            var width = 64;
            var height = 64;
            var pixelData = new byte[100]; // Wrong size

            Assert.Throws<ArgumentException>(() =>
                DicomIconImageSequenceBuilder.Build(width, height, pixelData));
        }

        #endregion

        #region DicomDirectory Integration Tests

        [Fact]
        public void AddFile_WithoutIconGenerator_WorksAsExpected()
        {
            // Arrange
            var dicomDir = new DicomDirectory();
            var dataset = CreateTestDicomDataset();
            var file = new DicomFile(dataset);

            // Act
            var entry = dicomDir.AddFile(file, "TEST\\IMAGE001");

            // Assert
            Assert.NotNull(entry);
            Assert.NotNull(entry.InstanceRecord);
            Assert.False(entry.InstanceRecord.Contains(DicomTag.IconImageSequence));
        }

        [Fact]
        public void AddFile_WithIconGenerator_AddsIconToImageRecord()
        {
            // Arrange
            var dicomDir = new DicomDirectory();
            var dataset = CreateTestDicomDataset();
            var file = new DicomFile(dataset);

            dicomDir.GenerateImageIcons = true;
            dicomDir._iconGenerator = new MockIconGenerator
            {
                ReturnValue = CreateTestIconSequence()
            };

            // Act
            var entry = dicomDir.AddFile(file, "TEST\\IMAGE001");

            // Assert
            Assert.NotNull(entry);
            Assert.NotNull(entry.InstanceRecord);
            Assert.True(entry.InstanceRecord.Contains(DicomTag.IconImageSequence));

            var iconSequence = entry.InstanceRecord.GetSequence(DicomTag.IconImageSequence);
            Assert.NotNull(iconSequence);
            Assert.Single(iconSequence.Items);
        }

        [Fact]
        public void AddFile_IconGeneratorReturnsNull_DoesNotBreakDicomDir()
        {
            // Arrange
            var dicomDir = new DicomDirectory();
            var dataset = CreateTestDicomDataset();
            var file = new DicomFile(dataset);

            dicomDir.GenerateImageIcons = true;
            dicomDir._iconGenerator = new MockIconGenerator
            {
                ReturnValue = null
            };

            // Act
            var entry = dicomDir.AddFile(file, "TEST\\IMAGE001");

            // Assert
            Assert.NotNull(entry);
            Assert.NotNull(entry.InstanceRecord);
            Assert.False(entry.InstanceRecord.Contains(DicomTag.IconImageSequence));
        }

        [Fact]
        public void AddFile_IconGeneratorThrows_DoesNotBreakDicomDir()
        {
            // Arrange
            var dicomDir = new DicomDirectory();
            var dataset = CreateTestDicomDataset();
            var file = new DicomFile(dataset);

            dicomDir.GenerateImageIcons = true;
            dicomDir._iconGenerator = new MockIconGenerator
            {
                ShouldThrow = true
            };

            // Act - Should not throw
            var entry = dicomDir.AddFile(file, "TEST\\IMAGE001");

            // Assert
            Assert.NotNull(entry);
            Assert.NotNull(entry.InstanceRecord);
            Assert.False(entry.InstanceRecord.Contains(DicomTag.IconImageSequence));
        }

        #endregion

        #region Helper Methods

        private static DicomDataset CreateTestDicomDataset()
        {
            return new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUID.Generate() },
                { DicomTag.StudyInstanceUID, DicomUID.Generate() },
                { DicomTag.SeriesInstanceUID, DicomUID.Generate() },
                { DicomTag.PatientID, "12345" },
                { DicomTag.PatientName, "Test^Patient" },
                { DicomTag.Modality, "OT" }
            };
        }

        private static DicomSequence CreateTestIconSequence()
        {
            var pixelData = new byte[64 * 64];
            Array.Fill(pixelData, (byte)128);
            return DicomIconImageSequenceBuilder.Build(64, 64, pixelData);
        }

        private class MockIconGenerator : IIconGenerator
        {
            public DicomSequence? ReturnValue { get; set; }
            public bool ShouldThrow { get; set; }

            public DicomSequence? GenerateIconImageSequence(DicomDataset dataset, int frameIndex = 0)
            {
                if (ShouldThrow)
                {
                    throw new InvalidOperationException("Test exception");
                }
                return ReturnValue;
            }
        }

        #endregion
    }
}