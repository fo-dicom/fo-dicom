﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using System;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection("Network")]
    public class PDVTest
    {
        #region Unit tests

        [Fact]
        public void Constructor_EvenLengthValue_Unmodified()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6 };
            var expected = new byte[6];
            Array.Copy(bytes, expected, bytes.Length);

            using var pdv = new PDV(1, bytes, bytes.Length, false, true, true);
            var actual = pdv.Value.Take(pdv.ValueLength).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_OddLengthValue_ThrowsArgumentException()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5 };
            var expected = new byte[6];
            Array.Copy(bytes, expected, bytes.Length);

            Assert.Throws<ArgumentException>(() => new PDV(1, bytes, bytes.Length, false, true, true));
        }

        #endregion
    }
}
