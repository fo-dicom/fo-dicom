namespace Dicom
{
    using System;

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

        [TestMethod]
        public void AttributeTagAsDicomElement_Array_GetDefaultValue()
        {
            var expected = DicomTag.ALinePixelSpacing;
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, DicomTag.ALinePixelSpacing);
            var actual = element.Get<DicomTag>();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DicomUnsignedShort_Array_ExplicitMinus1InterpretAs0()
        {
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, 1, 2, 3, 4, 5);
            Assert.AreEqual(element.Get<ushort>(-1), element.Get<ushort>(0));
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DicomUnsignedShort_Array_ExplicitMinus2Throws()
        {
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, 1, 2, 3, 4, 5);
            Console.WriteLine(element.Get<ushort>(-2));
        }
    }
}