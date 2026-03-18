// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Mathematics
{

    [Collection(TestCollections.General)]
    public class Vector3MTests
    {


        [Fact]
        public void TestAdd()
        {
            var v1 = new Vector3M(2.2m, 6.1m, 7.4m);
            var v2 = new Vector3M(3.8m, 3.7m, 4.1m);
            var result = new Vector3M(6m, 9.8m, 11.5m);

            Assert.Equal(result, v1 + v2, new Vector3MComparer());

            v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            v2 = new Vector3M(-3.8m, 3.7m, -4.1m);
            result = new Vector3M(-1.6m, -2.4m, 3.3m);

            Assert.Equal(result, v1 + v2, new Vector3MComparer());
        }


        [Fact]
        public void TestSubtract()
        {
            var v1 = new Vector3M(2.2m, 6.1m, 7.4m);
            var v2 = new Vector3M(3.8m, 3.7m, 4.1m);
            var result = new Vector3M(-1.6m, 2.4m, 3.3m);

            Assert.Equal(result, v1 - v2, new Vector3MComparer());

            v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            v2 = new Vector3M(-3.8m, 3.7m, -4.1m);
            result = new Vector3M(6m, -9.8m, 11.5m);

            Assert.Equal(result, v1 - v2, new Vector3MComparer());
        }


        [Fact]
        public void TestMultiply()
        {
            var v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            var result = new Vector3M(6.82m, -18.91m, 22.94m);

            Assert.Equal(result, 3.1m * v1, new Vector3MComparer());
            Assert.Equal(result, v1 * 3.1m, new Vector3MComparer());
        }


        [Fact]
        public void TestDivide()
        {
            var result = new Vector3M(2.2m, -6.1m, 7.4m);
            var v1 = new Vector3M(6.82m, -18.91m, 22.94m);

            Assert.Equal(result, v1 / 3.1m, new Vector3MComparer());
        }


        [Fact]
        public void TestNormalize()
        {
            var v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            Assert.Equal(9.8392072851F, (float)v1.Magnitude());

            var normalized = v1.Normalize();
            Assert.Equal(1.0F, (float)normalized.Magnitude());
        }


        [Fact]
        public void TestDot()
        {
            var v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            var v2 = new Vector3M(3.8m, 3.7m, 4.1m);

            Assert.Equal(16.13F, (float)v1.DotProduct(v2));
        }


        [Fact]
        public void TestCross()
        {
            var v1 = new Vector3M(2.2m, -6.1m, 7.4m);
            var v2 = new Vector3M(-3.8m, 3.7m, 4.1m);
            var result = new Vector3M(-52.39m, -37.14m, -15.04m);

            Assert.Equal(result, v1.CrossProduct(v2), new Vector3MComparer());
        }

    }


    public class Vector3MComparer : IEqualityComparer<Vector3M>
    {

        private bool _checkExact;

        public Vector3MComparer(bool checkExact = true)
        {
            _checkExact = checkExact;
        }

        public decimal Epsilon { get; set; } = 0.000001m;


        public bool Equals(Vector3M x, Vector3M y)
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
                if (_checkExact)
                {
                    return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
                }
                else
                {
                    return Math.Abs(x.X - y.X) < Epsilon &&
                        Math.Abs(x.Y - y.Y) < Epsilon &&
                        Math.Abs(x.Z - y.Z) < Epsilon;
                }
            }
        }


        public int GetHashCode(Vector3M obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

    }
}
