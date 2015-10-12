// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Text;

    using Xunit;

    [Collection("General")]
    public class DicomEncodingTest
    {
        [Fact]
        public void Default_Getter_ReturnsUTF8()
        {
            var expected = Encoding.UTF8.CodePage;
            var actual = DicomEncoding.Default.CodePage;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_NonMatchingCharset_ReturnsUTF8()
        {
            var expected = Encoding.UTF8.CodePage;
            var actual = DicomEncoding.GetEncoding("GBK").CodePage;
            Assert.Equal(expected, actual);
        }
    }
}