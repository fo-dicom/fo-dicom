// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1986
    {
        [Fact]
        public void GrayscaleRenderOptions_FromMinMax_WithModalityLut_ShouldIgnoreRescaleSlopeIntercept()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;

            var result = GrayscaleRenderOptions.FromMinMax(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(511.5, result.WindowCenter);
            Assert.Equal(1023, result.WindowWidth);
        }
        
        [Fact]
        public void GrayscaleRenderOptions_FromBitRange_WithModalityLut_ShouldIgnoreRescaleSlopeIntercept()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;

            var result = GrayscaleRenderOptions.FromBitRange(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(511.5, result.WindowCenter);
            Assert.Equal(1023, result.WindowWidth);
        }

        [Fact]
        public void GrayscaleRenderOptions_FromImagePixelValueTags()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;
            dataset.AddOrUpdate(DicomTag.SmallestImagePixelValue, (ushort)0);
            dataset.AddOrUpdate(DicomTag.LargestImagePixelValue, (ushort)1023);

            var result = GrayscaleRenderOptions.FromImagePixelValueTags(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(511.5, result.WindowCenter);
            Assert.Equal(1023, result.WindowWidth);
        }

        [Fact]
        public void GrayscaleRenderOptions_FromWindowLevel()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;
            dataset.AddOrUpdate(DicomTag.WindowCenter, 500.0);
            dataset.AddOrUpdate(DicomTag.WindowWidth, 512.0);

            var result = GrayscaleRenderOptions.FromWindowLevel(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(500, result.WindowCenter);
            Assert.Equal(512, result.WindowWidth);
        }

        [Fact]
        public void GrayscaleRenderOptions_FromFunctionalWindowLevel()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;
            var referencedImageSequence = new DicomDataset { ValidateItems = false };
            referencedImageSequence.Add(DicomTag.WindowCenter, 500.0); 
            referencedImageSequence.Add(DicomTag.WindowWidth, 512.0); 
            var sharedFunctionalGroups = new DicomDataset { ValidateItems = false };
            sharedFunctionalGroups.Add(new DicomSequence(DicomTag.RenderedImageReferenceSequence, referencedImageSequence));
            dataset.AddOrUpdate(new DicomSequence(DicomTag.SharedFunctionalGroupsSequence, sharedFunctionalGroups));

            var result = GrayscaleRenderOptions.FromFunctionalWindowLevel(dataset, 0);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(500, result.WindowCenter);
            Assert.Equal(512, result.WindowWidth);
        }

        [Fact]
        public void GrayscaleRenderOptions_FromHistogram()
        {
            var dcmFile = DicomFile.Open(TestData.Resolve("GH1986.dcm"));
            var dataset = dcmFile.Dataset;

            var result = GrayscaleRenderOptions.FromHistogram(dataset);
            
            Assert.Equal(0, result.RescaleIntercept);
            Assert.Equal(1, result.RescaleSlope);
            Assert.NotNull(result.ModalityLUT);
            Assert.Equal(511.5, result.WindowCenter);
            Assert.Equal(1023, result.WindowWidth);
        }
    }
}