// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{

    [Collection("General")]
    public class TempFileBufferTest
    {
        #region Unit tests

        [Fact]
        public void Data_CompareWithInitializer_ExactMatch()
        {
            var expected = Enumerable.Range(0, 254).Select(i => (byte)i).ToArray();
            var buffer = new TempFileBuffer(expected);

            var actual = buffer.Data;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(20, 150)]
        [InlineData(0, 128)]
        [InlineData(5, 1)]
        [InlineData(30, 224)]
        [InlineData(12, 0)]
        public void GetByteRange_CompareWithInitializer_ExactMatch(int offset, int count)
        {
            var data = Enumerable.Range(0, 254).Select(i => (byte)i).ToArray();
            var buffer = new TempFileBuffer(data);

            var expected = new ArraySegment<byte>(data, offset, count);
            var actual = buffer.GetByteRange(offset, count);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
