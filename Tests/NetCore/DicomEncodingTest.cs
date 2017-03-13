// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Text;

    using Xunit;

    [Collection("General")]
    public class DicomEncodingTest
    {       
        [Fact]
        public void GetEncoding_GB18030_NetCore() //https://github.com/fo-dicom/fo-dicom/issues/481
        {
            var actual = DicomEncoding.GetEncoding("GB18030").CodePage; //note that if you put the call to expected before actual, it won't work because the GB18030 code page isn't loaded explicitly in the test project
            var expected = Encoding.GetEncoding("GB18030").CodePage;           
            Assert.Equal(expected, actual);
        }
    }
}