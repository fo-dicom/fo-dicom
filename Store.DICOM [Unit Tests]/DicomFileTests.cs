using System.IO;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dicom
{
    [TestClass]
    public class DicomFileTests
    {
        private readonly string _dicomFileName = IOHelper.GetMyDocumentsPath(@"Data\DICOM\ct.0.dcm");

         [TestMethod]
         public void Open_ExistingDicomFile_Success()
         {
             var dicomFile = DicomFile.Open(_dicomFileName);
             var expected = DicomFileFormat.DICOM3;
             var actual = dicomFile.Format;
             Assert.AreEqual(expected, actual);
         }

         [TestMethod]
         public void Dataset_ExistingDicomFile_ContainsPatientId()
         {
             var dicomFile = DicomFile.Open(_dicomFileName);
             var expected = true;
             var actual = dicomFile.Dataset.Contains(DicomTag.PatientID);
             Assert.AreEqual(expected, actual);
         }

         [TestMethod]
         public void Dataset_ExistingDicomCtFile_DoesNotContainBeamSequence()
         {
             var dicomFile = DicomFile.Open(_dicomFileName);
             var expected = false;
             var actual = dicomFile.Dataset.Contains(DicomTag.BeamSequence);
             Assert.AreEqual(expected, actual);
         }
    }
}