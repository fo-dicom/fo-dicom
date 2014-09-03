using System;
using Dicom;
using Xunit;
using Newtonsoft.Json;

namespace DICOM__Unit_Tests_
{
    /// <summary>
    ///     This is a test class for DicomTagTest and is intended
    ///     to contain all DicomTagTest Unit Tests
    /// </summary>
    public class DicomTagTest
    {
        /// <summary>
        ///     A test for ToString
        /// </summary>
        [Fact]
        public void ToJsonStringTest()
        {
            const ushort @group = 0x7FE0;
            const ushort element = 0x00FF;
            var target = new DicomTag(group, element);
            const string format = "J";
            IFormatProvider formatProvider = null;
            const string expected = "7FE000FF";
            string actual = string.Empty;
            actual = target.ToString(format, formatProvider);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToJson()
        {
            var target = new DicomTag(0x7fe0, 0x00ff);
            Console.WriteLine(JsonConvert.SerializeObject(target, Formatting.Indented));
        }
    }
}