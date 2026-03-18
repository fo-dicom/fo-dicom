// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Reconstruction;
using FellowOakDicom.Imaging.Render;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        [Fact]
        public async Task CalculationInVolumeDataShouldBeCorrectly()
        {
            var testFile = TestData.Resolve("GH1876.dcm");
            var dicomFile = await DicomFile.OpenAsync(testFile, FileReadOption.ReadAll);

            // Act
            var volumeData = new VolumeData(dicomFile.Dataset);
            var stack = new Stack(volumeData, StackType.Axial, 1.73m, 17);

            // Assert
            Assert.NotNull(volumeData);

            Assert.NotEqual(volumeData.BoundingMin, volumeData.BoundingMax);

            var rawLastSlice = stack.Slices.Last().RenderRawData(dicomFile.Dataset.GetSingleValue<int>(DicomTag.BitsAllocated) / 8);

            Assert.Equal(dicomFile.Dataset.GetSingleValue<int>(DicomTag.NumberOfFrames), stack.Slices.Count);
            Assert.Contains(rawLastSlice, b => b != 0);
        }

        [Fact]
        public async Task CalcualationInStack()
        {
            var studyInstanceUID = DicomUID.Generate();
            var seriesInstanceUID = DicomUID.Generate();
            var frameOfReferenceUID = DicomUID.Generate();

            var files = new List<DicomDataset>(200);
            decimal z = 29.7m;

            for (var i = 0; i < 200; i++)
            {
                files.Add(CreateAxialImage($"-45\\-45\\{ z.ToString(CultureInfo.InvariantCulture) }", "0.3\\0.3"));
                z += 0.3m;
            }

            // Act
            var volumeData = new VolumeData(files.Select(d => new ImageData(d)));
            var stack = new Stack(volumeData, StackType.Axial, 0.3m, 0.3m);

            // Assert
            var bounding = volumeData.BoundingMin;
            Assert.Equal(29.7m, bounding.Z);
            Assert.Equal(200, stack.Slices.Count);
            Assert.Equal(0.3m, volumeData.SliceSpaces.Center);
            Assert.Equal(29.7m, stack.Slices.Last().TopLeft.Z);

            DicomDataset CreateAxialImage(string positionPatient, string pixelSpacing)
            {
                return new DicomDataset
                {
                    {DicomTag.StudyInstanceUID, studyInstanceUID },
                    {DicomTag.SeriesInstanceUID, seriesInstanceUID },
                    {DicomTag.FrameOfReferenceUID, frameOfReferenceUID },
                    { DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value },
                    { DicomTag.BitsAllocated, (ushort)8 },
                    { DicomTag.BitsStored, (ushort) 8},
                    {DicomTag.HighBit, (ushort)7 },
                    {DicomTag.Columns, (ushort)100 },
                    {DicomTag.Rows, (ushort)100 },
                    {DicomTag.PixelData, new byte[100*100]  },
                    {DicomTag.ImageOrientationPatient, @"1\0\0\0\1\0" },
                    {DicomTag.ImagePositionPatient, positionPatient },
                    {DicomTag.PixelSpacing, pixelSpacing }
                };
            }

        }

    }
}