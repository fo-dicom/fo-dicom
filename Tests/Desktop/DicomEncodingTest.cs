// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;

using Xunit;

namespace Dicom
{
    [Collection("General")]
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

        [Fact]
        public void GetEncoding_GB18030_Desktop() //https://github.com/fo-dicom/fo-dicom/issues/481
        {
            var expected = Encoding.GetEncoding("GB18030").CodePage;
            var actual = DicomEncoding.GetEncoding("GB18030").CodePage;
            Assert.Equal(expected, actual);
        }
    }
}
