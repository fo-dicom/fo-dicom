// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1264
    {
        [Fact]
        public void OpenDicomFileFromStream()
        {
            // Arrange
            var inputFile = new FileInfo(TestData.Resolve("10200904.dcm"));
            var outputFile = new FileInfo("./GH1264.dcm");

            outputFile.Delete();

            using var fs = File.OpenRead(inputFile.FullName);
            using var ms = new MemoryStream();

            fs.CopyTo(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var fileByteDicomFile = DicomFile.Open(inputFile.FullName);
            var streamByteDicomFile = DicomFile.Open(ms);

            // Act
            streamByteDicomFile.Save(outputFile.FullName);

            var reopenedDicomFile = DicomFile.Open(outputFile.FullName);

            // Assert
            var fileBytePixelData = DicomPixelData.Create(fileByteDicomFile.Dataset);
            var streamBytePixelData = DicomPixelData.Create(streamByteDicomFile.Dataset);
            var reopenedPixelData = DicomPixelData.Create(reopenedDicomFile.Dataset);

            Assert.Equal(79, fileBytePixelData.NumberOfFrames);
            Assert.Equal(79, streamBytePixelData.NumberOfFrames);
            Assert.Equal(79, reopenedPixelData.NumberOfFrames);

            var fileByteFrame1 = fileBytePixelData.GetFrame(0);
            var streamByteFrame1 = streamBytePixelData.GetFrame(0);
            var reopenedFrame1 = reopenedPixelData.GetFrame(0);

            Assert.Equal(fileByteFrame1.Size, streamByteFrame1.Size);
            Assert.Equal(fileByteFrame1.Size, reopenedFrame1.Size);

            var fileByteLastItem = fileByteDicomFile.Dataset.Last();
            var streamByteLastItem = streamByteDicomFile.Dataset.Last();
            var reopenedLastItem = reopenedDicomFile.Dataset.Last();

            Assert.Equal(fileByteLastItem.Tag, streamByteLastItem.Tag);
            Assert.Equal(fileByteLastItem.Tag, reopenedLastItem.Tag);
        }

        [Fact]
        public async Task OpenDicomFileFromStreamAsync()
        {
            // Arrange
            var inputFile = new FileInfo(TestData.Resolve("10200904.dcm"));
            var outputFile = new FileInfo("./GH1264.dcm");

            outputFile.Delete();

            using var fs = File.OpenRead(inputFile.FullName);
            using var ms = new MemoryStream();

            await fs.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var fileByteDicomFile = await DicomFile.OpenAsync(inputFile.FullName);
            var streamByteDicomFile = await DicomFile.OpenAsync(ms);

            // Act
            await streamByteDicomFile.SaveAsync(outputFile.FullName);

            var reopenedDicomFile = await DicomFile.OpenAsync(outputFile.FullName);

            // Assert
            var fileBytePixelData = DicomPixelData.Create(fileByteDicomFile.Dataset);
            var streamBytePixelData = DicomPixelData.Create(streamByteDicomFile.Dataset);
            var reopenedPixelData = DicomPixelData.Create(reopenedDicomFile.Dataset);

            Assert.Equal(79, fileBytePixelData.NumberOfFrames);
            Assert.Equal(79, streamBytePixelData.NumberOfFrames);
            Assert.Equal(79, reopenedPixelData.NumberOfFrames);

            var fileByteFrame1 = fileBytePixelData.GetFrame(0);
            var streamByteFrame1 = streamBytePixelData.GetFrame(0);
            var reopenedFrame1 = reopenedPixelData.GetFrame(0);

            Assert.Equal(fileByteFrame1.Size, streamByteFrame1.Size);
            Assert.Equal(fileByteFrame1.Size, reopenedFrame1.Size);

            var fileByteLastItem = fileByteDicomFile.Dataset.Last();
            var streamByteLastItem = streamByteDicomFile.Dataset.Last();
            var reopenedLastItem = reopenedDicomFile.Dataset.Last();

            Assert.Equal(fileByteLastItem.Tag, streamByteLastItem.Tag);
            Assert.Equal(fileByteLastItem.Tag, reopenedLastItem.Tag);
        }
    }
}
