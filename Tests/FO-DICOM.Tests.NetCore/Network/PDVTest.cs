// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using System;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    public class PDVTest
    {
        #region Unit tests

        [Fact]
        public void Constructor_EvenLengthValue_Unmodified()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6 };
            var expected = new byte[6];
            Array.Copy(bytes, expected, bytes.Length);

            var pdv = new PDV(1, bytes, true, true);
            var actual = pdv.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_OddLengthValue_PaddedToEvenLength()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5 };
            var expected = new byte[6];
            Array.Copy(bytes, expected, bytes.Length);

            var pdv = new PDV(1, bytes, true, true);
            var actual = pdv.Value;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
