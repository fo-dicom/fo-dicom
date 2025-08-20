// Copyright (c) Medicim NV. All rights reserved.
// Confidential and for internal use only. The content of this document constitutes proprietary
// information of the Nobel Biocare group of companies. Any disclosure, copying, distribution or use of
// any parts of the content of this document by unauthorized parties is strictly prohibited.

using FellowOakDicom.Imaging.Reconstruction;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Reconstruction
{
    public class VolumeDataMultiFrameDatasetTests
    {
        [Fact]
        public async Task VolumeData_GivenVolumeDataConstructedFromMultiFrameFile_ShouldReadCorrectly()
        {
            var testFile = TestData.Resolve("GH1876.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            // Act
            var volumeData = new VolumeData(dicomFile.Dataset);

            // Assert
            Assert.NotNull(volumeData);
            
            Assert.NotEqual(volumeData.BoundingMin, volumeData.BoundingMax);
        }
        
        [Fact]
        public async Task VolumeData_GivenVolumeDataConstructedFromNonMultiFrameFile_ShouldThrowException()
        {
            var testFile = TestData.Resolve("TestPattern_Palette.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            Assert.Throws<DicomDataException>(() => new VolumeData(dicomFile.Dataset));
        }
    }
}