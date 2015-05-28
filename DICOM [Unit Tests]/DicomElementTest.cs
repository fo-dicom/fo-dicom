namespace Dicom
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DicomElementTest
    {
        [TestMethod]
        public void DicomSignedShort_Array_GetDefaultValue()
        {
            DicomSignedShort element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.AreEqual((short)5, element.Get<short>());
        }

        [TestMethod]
        public void DicomSignedShortAsDicomElement_Array_GetDefaultValue()
        {
            DicomElement element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.AreEqual((short)5, element.Get<short>());
        }
    }
}