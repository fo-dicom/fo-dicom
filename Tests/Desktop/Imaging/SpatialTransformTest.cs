// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Dicom.Imaging.Mathematics;

    using Xunit;

    [Collection("General")]
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

        #endregion
    }
}
