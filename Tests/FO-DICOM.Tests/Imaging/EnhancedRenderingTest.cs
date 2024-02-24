// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection("Imaging")]
    public class EnhancedRenderingTest
    {
        [Fact]
        public void EnhancedMRRenderFrames_Autoapply()
        {
            var image = new DicomImage(TestData.Resolve("MOVEKNEE.dcm"));
            Assert.Equal(19, image.NumberOfFrames);

            // render frame 0, the original windowwith from PerFrameSequence shall be applied
            _ = image.RenderImage(0);
            Assert.Equal(0, image.CurrentFrame);
            Assert.Equal(2810, image.WindowWidth);

            // render frame 2, the original windowwith from PerFrameSequence shall be applied
            _ = image.RenderImage(2);
            Assert.Equal(2, image.CurrentFrame);
            Assert.Equal(2808, image.WindowWidth);

            // disable autoapply to all frames
            image.AutoApplyLUTToAllFrames = false;

            // change the windowwith of the current frame 2, render the image and assert that the changed windowwidh
            image.WindowWidth = 2700;
            _ = image.RenderImage(2);
            Assert.Equal(2, image.CurrentFrame);
            Assert.Equal(2700, image.WindowWidth);

            // render the frame 0 and assert that still the original windowwith is applied
            _ = image.RenderImage(0);
            Assert.Equal(0, image.CurrentFrame);
            Assert.Equal(2810, image.WindowWidth);

            // now enable autoapply to all frames
            image.AutoApplyLUTToAllFrames = true;

            // again change the windowwith of current frame 0
            image.WindowWidth = 2600;
            _ = image.RenderImage(0);
            Assert.Equal(0, image.CurrentFrame);
            Assert.Equal(2600, image.WindowWidth);

            // now when rendering another frame, the changed windowwith should be applied
            _ = image.RenderImage(2);
            Assert.Equal(2, image.CurrentFrame);
            Assert.Equal(2600, image.WindowWidth);
        }

        [Fact]
        public void EnhancedMRFrameGeometry_WithPositionPerFrame()
        {
            var image = DicomFile.Open(TestData.Resolve("mr_brucker.dcm"));
            var geometryFrame1 = new FrameGeometry(image.Dataset, 0);
            var geometryFrame4 = new FrameGeometry(image.Dataset, 3);

            Assert.True(geometryFrame1.HasGeometryData);
            Assert.True(geometryFrame4.HasGeometryData);

            Assert.Equal(-6.2, geometryFrame1.PointTopLeft.Z);
            Assert.Equal(-3.2, geometryFrame4.PointTopLeft.Z);
        }

        [Fact]
        public void EnhancedMRFrameGeometry_WithPositionInShared()
        {
            var image = DicomFile.Open(TestData.Resolve("MOVEKNEE.dcm"));
            var geometryFrame1 = new FrameGeometry(image.Dataset, 0);
            var geometryFrame4 = new FrameGeometry(image.Dataset, 3);

            Assert.True(geometryFrame1.HasGeometryData);
            Assert.True(geometryFrame4.HasGeometryData);

            Assert.Equal(geometryFrame4.PointTopLeft, geometryFrame1.PointTopLeft);
        }

    }
}
