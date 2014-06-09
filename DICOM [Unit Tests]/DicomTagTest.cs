using System;
using Dicom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DICOM__Unit_Tests_
{
    /// <summary>
    ///     This is a test class for DicomTagTest and is intended
    ///     to contain all DicomTagTest Unit Tests
    /// </summary>
    [TestClass]
    public class DicomTagTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        /// <summary>
        ///     A test for ToString
        /// </summary>
        [TestMethod]
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
            Assert.AreEqual(expected, actual);
        }
    }
}