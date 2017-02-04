using DICOM.Shared.Log;
using System.Text;
using Xunit;

namespace Dicom.Serialization
{

    [Collection("General")]
    public class XmlDicomConverterTest
    {

        [Fact]
        public void TestSimpleXmlSerialization()
        {
            var dataset = new DicomDataset();
            dataset.AddOrUpdate(DicomTag.StudyInstanceUID, "1.2.345");
            dataset.AddOrUpdate(DicomTag.PatientName, "Test^Name");
            var finalXml = new StringBuilder();
            finalXml.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            finalXml.AppendLine(@"<NativeDicomModel>");
            finalXml.AppendLine(@"<DicomAttribute tag=""00100010"" vr=""PN"" keyword=""PatientName"">");
            finalXml.AppendLine(@"<Value number=""1"">Test^Name</Value>");
            finalXml.AppendLine(@"</DicomAttribute>");
            finalXml.AppendLine(@"<DicomAttribute tag=""0020000D"" vr=""UI"" keyword=""StudyInstanceUID"">");
            finalXml.AppendLine(@"<Value number=""1"">1.2.345</Value>");
            finalXml.AppendLine(@"</DicomAttribute>");
            finalXml.AppendLine(@"</NativeDicomModel>");
            var x = new DicomXML(dataset);
            string xml = x.XmlString;            
            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.Equal(finalXml.ToString().Trim(), xml.Trim());
        }

        [Fact]
        public void TestSequenceXmlSerialization()
        {
            var file = DicomFile.Open(@".\Test Data\DICOMDIR");
            var xml = (new DicomXML(file.Dataset)).XmlString;
            var finalXml1 = new StringBuilder();
            finalXml1.AppendLine(@"<Item number=""44"">");
            finalXml1.AppendLine(@"<DicomAttribute tag=""00041400"" vr=""UL"" keyword=""OffsetOfTheNextDirectoryRecord"">");
            finalXml1.AppendLine(@"<Value number=""1"">0</Value>");
            finalXml1.AppendLine(@"</DicomAttribute>");
            var finalXml2 = new StringBuilder();
            finalXml2.AppendLine(@"<DicomAttribute tag=""00041500"" vr=""CS"" keyword=""ReferencedFileID"">");
            finalXml2.AppendLine(@"<Value number=""1"">IMAGES</Value>");
            finalXml2.AppendLine(@"<Value number=""2"">RLE</Value>");
            finalXml2.AppendLine(@"<Value number=""3"">VL3_RLE</Value>");
            finalXml2.AppendLine(@"</DicomAttribute>");
            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.True(xml.Contains(finalXml1.ToString()));
            Assert.True(xml.Contains(finalXml2.ToString()));
        }
    }
}
