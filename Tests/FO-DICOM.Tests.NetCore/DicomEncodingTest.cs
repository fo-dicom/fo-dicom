// Copyright (c) 2012-2019 fo-dicom contributors.
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
        public void GetEncoding_GB18030() //https://github.com/fo-dicom/fo-dicom/issues/481
        {
            int codePage = 0;
            var exception = Record.Exception(() => { codePage = DicomEncoding.GetEncoding("GB18030").CodePage; });
            Assert.Null(exception);
            Assert.Equal(54936, codePage);
        }
    }
}
