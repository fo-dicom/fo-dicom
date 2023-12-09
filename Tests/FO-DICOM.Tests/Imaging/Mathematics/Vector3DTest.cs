// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Mathematics
{

    [Collection(TestCollections.General)]
    public class Vector3DTests
    {


        [Fact]
        public void TestAdd()
        {
            var v1 = new Vector3D(2.2F, 6.1F, 7.4F);
            var v2 = new Vector3D(3.8F, 3.7F, 4.1F);
            var result = new Vector3D(6F, 9.8F, 11.5F);

            Assert.Equal(result, v1 + v2, new Vector3DComparer());

            v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            v2 = new Vector3D(-3.8F, 3.7F, -4.1F);
            result = new Vector3D(-1.6F, -2.4F, 3.3F);

            Assert.Equal(result, v1 + v2, new Vector3DComparer());
        }


        [Fact]
        public void TestSubtract()
        {
            var v1 = new Vector3D(2.2F, 6.1F, 7.4F);
            var v2 = new Vector3D(3.8F, 3.7F, 4.1F);
            var result = new Vector3D(-1.6F, 2.4F, 3.3F);

            Assert.Equal(result, v1 - v2, new Vector3DComparer());

            v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            v2 = new Vector3D(-3.8F, 3.7F, -4.1F);
            result = new Vector3D(6F, -9.8F, 11.5F);

            Assert.Equal(result, v1 - v2, new Vector3DComparer());
        }


        [Fact]
        public void TestMultiply()
        {
            var v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            var result = new Vector3D(6.82F, -18.91F, 22.94f);

            Assert.Equal(result, 3.1F * v1, new Vector3DComparer());
            Assert.Equal(result, v1 * 3.1F, new Vector3DComparer());
        }


        [Fact]
        public void TestDivide()
        {
            var result = new Vector3D(2.2F, -6.1F, 7.4F);
            var v1 = new Vector3D(6.82F, -18.91F, 22.94f);

            Assert.Equal(result, v1 / 3.1F, new Vector3DComparer());
        }


        [Fact]
        public void TestNormalize()
        {
            var v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            Assert.Equal(9.8392072851F, (float)v1.Magnitude());

            var normalized = v1.Normalize();
            Assert.Equal(1.0F, (float)normalized.Magnitude());
        }


        [Fact]
        public void TestDot()
        {
            var v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            var v2 = new Vector3D(3.8F, 3.7F, 4.1F);

            Assert.Equal(16.13F, (float)v1.DotProduct(v2));
        }


        [Fact]
        public void TestCross()
        {
            var v1 = new Vector3D(2.2F, -6.1F, 7.4F);
            var v2 = new Vector3D(-3.8F, 3.7F, 4.1F);
            var result = new Vector3D(-52.39F, -37.14F, -15.04F);

            Assert.Equal(result, v1.CrossProduct(v2), new Vector3DComparer());
        }

    }


    public class Vector3DComparer : IEqualityComparer<Vector3D>
    {

        public double Epsilon { get; set; } = 0.000001;


        public bool Equals(Vector3D x, Vector3D y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if ((x == null && y != null) || (x != null && y == null))
            {
                return false;
            }
            else

            {
                return Math.Abs(x.X - y.X) < Epsilon &&
                    Math.Abs(x.Y - y.Y) < Epsilon &&
                    Math.Abs(x.Z - y.Z) < Epsilon;
            }
        }


        public int GetHashCode(Vector3D obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

    }
}
