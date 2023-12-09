// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1376
    {
        [Fact]
        public async Task ShouldParseFileMetaInfoFromFirstMetaGroupAndTreatSecondMetaGroupAsData()
        {
            // Arrange
            var file = "Test Data\\GH1376.dcm";
            var dicomFile = await DicomFile.OpenAsync(file);

            // Act
            var implementationVersionNameInFileMetaInfo = dicomFile.FileMetaInfo.ImplementationVersionName;
            var transferSyntaxInFileMetaInfo = dicomFile.FileMetaInfo.TransferSyntax;
            var implementationVersionNameInDataSet = dicomFile.Dataset.GetSingleValueOrDefault<string>(DicomTag.ImplementationVersionName, null);
            var transferSyntaxInDataset = dicomFile.Dataset.GetSingleValueOrDefault<DicomTransferSyntax>(DicomTag.TransferSyntaxUID, null);

            // Assert
            Assert.NotEqual(implementationVersionNameInFileMetaInfo, implementationVersionNameInDataSet);
            Assert.NotEqual(transferSyntaxInFileMetaInfo, transferSyntaxInDataset);

            Assert.Equal(DicomTransferSyntax.JPEGProcess14SV1, transferSyntaxInFileMetaInfo);
            Assert.Equal(DicomTransferSyntax.ExplicitVRLittleEndian, transferSyntaxInDataset);
            Assert.Equal("OFFIS_DCMTK_366", implementationVersionNameInFileMetaInfo);
            Assert.Equal("DCF 3.3.12c", implementationVersionNameInDataSet);
        }

        [Fact]
        public async Task ShouldBeAbleToRender()
        {
            // Arrange
            var file = "Test Data\\GH1376.dcm";
            var dicomFile = await DicomFile.OpenAsync(file);

            // Act
            var renderException = Record.Exception(() =>
            {
                var image = new DicomImage(dicomFile.Dataset);
                using var renderedImage = image.RenderImage();
            });

            // Assert
            Assert.Null(renderException);
        }

    }
}
