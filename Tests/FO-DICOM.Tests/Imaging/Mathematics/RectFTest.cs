// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Mathematics
{

    [Collection(TestCollections.General)]
    public class RectFTest
    {
        #region Unit tests

        [Theory]
        [InlineData(0f, 0f, 100f, 50f, 10f, 20f, -10f, -20f, 120f, 90f)]
        [InlineData(0f, 0f, 100f, 50f, -10f, -20f, 10f, 20f, 80f, 10f)]
        [InlineData(0f, 0f, 100f, 50f, -10f, -30f, 10f, 25f, 80f, 0f)]
        [InlineData(0f, 0f, 100f, 50f, -100f, -20f, 50f, 20f, 0f, 10f)]
        public void Inflate_Various_YieldCorrectNewRectangle(
            float x0,
            float y0,
            float w0,
            float h0,
            float inflateX,
            float inflateY,
            float x1,
            float y1,
            float w1,
            float h1)
        {
            var rect = new RectF(x0, y0, w0, h0);
            rect.Inflate(inflateX, inflateY);
            Assert.Equal(x1, rect.X);
            Assert.Equal(y1, rect.Y);
            Assert.Equal(w1, rect.Width);
            Assert.Equal(h1, rect.Height);
        }

        [Fact]
        public void Assignment_ChangeInNewInstance_DoesNotAffectOldInstance()
        {
            var oldRect = new RectF(10f, 20f, 30f, 40f);
            var newRect = oldRect;
            newRect.Inflate(10f, 10f);
            Assert.Equal(10f, oldRect.X);
            Assert.Equal(20f, oldRect.Y);
            Assert.Equal(30f, oldRect.Width);
            Assert.Equal(40f, oldRect.Height);
        }

        #endregion
    }
}
