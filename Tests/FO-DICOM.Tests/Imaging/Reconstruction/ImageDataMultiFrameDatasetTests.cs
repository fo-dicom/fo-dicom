// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.Reconstruction;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Reconstruction
{
    public class ImageDataMultiFrameDatasetTests
    {
        [Fact]
        public async Task ImageData_GivenImageDataConstructedFromMultiFrameFile_ShouldReadCorrectly()
        {
            var testFile = TestData.Resolve("GH1876.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            // Act
            var firstFrameImageData = new ImageData(dicomFile.Dataset, 1);
            var secondFrameImageData = new ImageData(dicomFile.Dataset, 0);

            // Assert
            Assert.NotNull(firstFrameImageData);
            Assert.NotNull(secondFrameImageData);

            Assert.NotEqual(firstFrameImageData.Geometry.PointBottomLeft, secondFrameImageData.Geometry.PointBottomLeft);
        }

        [Fact]
        public async Task ImageData_GivenImageDataConstructedFromMultiFrameFile_WhenPixelDataIsOnlyCreatedOnce_ShouldReadCorrectly()
        {
            var testFile = TestData.Resolve("GH1876.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            var pixelData = DicomPixelData.CreateFromDataset(dicomFile.Dataset);

            // Act
            var firstFrameImageData = new ImageData(dicomFile.Dataset, pixelData, 1);
            var secondFrameImageData = new ImageData(dicomFile.Dataset, pixelData, 0);

            // Assert
            Assert.NotNull(firstFrameImageData);
            Assert.NotNull(secondFrameImageData);

            Assert.NotEqual(firstFrameImageData.Geometry.PointBottomLeft, secondFrameImageData.Geometry.PointBottomLeft);
        }

        [Fact]
        public async Task ImageData_GivenCompressedImageDataConstructedFromMultiFrameFile_WhenPixelDataIsOnlyCreatedOnce_ShouldDecompressAndReadCorrectly()
        {
            var testFile = TestData.Resolve("GH1876.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            var transcoder = new DicomTranscoder(dicomFile.Dataset.InternalTransferSyntax, DicomTransferSyntax.RLELossless);
            var rleDataset = transcoder.Transcode(dicomFile.Dataset);

            var pixelData = DicomPixelData.CreateFromDataset(rleDataset);

            // Act
            Func<ImageData> act = () => new ImageData(rleDataset, pixelData, 0);

            // Assert
            Assert.Throws<DicomDataException>(act);
        }
    }
}