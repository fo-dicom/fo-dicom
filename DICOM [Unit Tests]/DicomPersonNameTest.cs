using Dicom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace DICOM__Unit_Tests_
{
    
    
    /// <summary>
    ///This is a test class for DicomPersonNameTest and is intended
    ///to contain all DicomPersonNameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DicomPersonNameTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        ///A test for DicomPersonName Constructor
        ///</summary>
        [TestMethod()]
        public void DicomPersonNameConstructorTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("Last^First^Middle^Prefix^Suffix", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "", "");
            Assert.AreEqual("Last^First^Middle", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", null, "");
            Assert.AreEqual("Last^First", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "", null, "Suffix");
            Assert.AreEqual("Last^First^^^Suffix", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "", "", "", null, null);
            Assert.AreEqual("", target.Get<string>());
        }

        /// <summary>
        ///A test for DicomPersonName Constructor
        ///</summary>
        [TestMethod()]
        public void DicomPersonNameConstructorTest1()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, DicomEncoding.GetEncoding("ISO IR 144"), "Тарковский", "Андрей", "Арсеньевич");
            byte[] b = target.Buffer.GetByteRange(0, (int)target.Buffer.Size);
            byte[] c = Encoding.GetEncoding("iso-8859-5").GetBytes("Тарковский^Андрей^Арсеньевич");
            CollectionAssert.AreEqual(c, b);
            // foloowing test checks also padding with space!
            target = new DicomPersonName(DicomTag.PatientName, DicomEncoding.GetEncoding("ISO IR 144"), "Тарковский", "Андрей");
            b = target.Buffer.GetByteRange(0, (int)target.Buffer.Size);
            c = Encoding.GetEncoding("iso-8859-5").GetBytes("Тарковский^Андрей ");
            CollectionAssert.AreEqual(c, b);
        }

        /// <summary>
        ///A test for Last
        ///</summary>
        [TestMethod()]
        public void LastTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("Last", target.Last);
            target = new DicomPersonName(DicomTag.PatientName, "");
            Assert.AreEqual("", target.Last);
            target = new DicomPersonName(DicomTag.PatientName, "=Doe^John");
            Assert.AreEqual("", target.Last);
        }

        /// <summary>
        ///A test for First
        ///</summary>
        [TestMethod()]
        public void FirstTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("First", target.First);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.AreEqual("", target.First);
            target = new DicomPersonName(DicomTag.PatientName, "Last=Doe^John");
            Assert.AreEqual("", target.First);
        }

        /// <summary>
        ///A test for Middle
        ///</summary>
        [TestMethod()]
        public void MiddleTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("Middle", target.Middle);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First");
            Assert.AreEqual("", target.Middle);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First=Doe^John^Peter");
            Assert.AreEqual("", target.Middle);
        }

        /// <summary>
        ///A test for Prefix
        ///</summary>
        [TestMethod()]
        public void PrefixTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("Prefix", target.Prefix);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.AreEqual("", target.Prefix);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First^Middle=Doe^John^Peter^MD^xx");
            Assert.AreEqual("", target.Prefix);
        }

        /// <summary>
        ///A test for Suffix
        ///</summary>
        [TestMethod()]
        public void SuffixTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.AreEqual("Suffix", target.Suffix);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.AreEqual("", target.Suffix);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First^Middle^Prefix=Doe^John^Peter^MD");
            Assert.AreEqual("", target.Suffix);
        }
    }
}
