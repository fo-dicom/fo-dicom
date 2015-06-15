// Copyright (c) 2011-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Text;

    using Xunit;

    public class DicomEncodingTest
    {
        [Fact]
        public void Default_Getter_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.Default.CodePage;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_NonMatchingCharset_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.GetEncoding("GBK").CodePage;
            Assert.Equal(expected, actual);
        }
    }
}