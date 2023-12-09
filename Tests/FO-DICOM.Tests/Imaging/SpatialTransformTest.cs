// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Mathematics;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection(TestCollections.Imaging)]
    public class SpatialTransformTest
    {
        #region Unit tests

        [Fact]
        public void IsTransformed_PanNonZero_IsTrue()
        {
            var transform = new SpatialTransform { Pan = new Point2(5, -7) };
            Assert.True(transform.IsTransformed);
        }

        [Fact]
        public void IsTransformed_ScaleUnity_RotateZeroPanZero_IsFalse()
        {
            var transform = new SpatialTransform { Scale = 1.0, Rotation = 0, Pan = new Point2(0, 0) };
            Assert.False(transform.IsTransformed);
        }

        [Fact]
        public void RotateAndFlipImage()
        {
            var myDicomFile = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));
            var myDicomImage = new DicomImage(myDicomFile.Dataset);
            IImage myImg = myDicomImage.RenderImage(0);
            myImg.Render(3, true, true, 0);
        }

        #endregion
    }
}
