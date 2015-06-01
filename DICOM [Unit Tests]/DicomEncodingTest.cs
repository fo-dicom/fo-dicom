namespace Dicom
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DicomEncodingTest
    {
        [TestMethod]
        public void Default_Getter_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.Default.CodePage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEncoding_NonMatchingCharset_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.GetEncoding("GBK").CodePage;
            Assert.AreEqual(expected, actual);
        }
    }
}