using DICOM.Shared.Log;
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
            string finalXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<NativeDicomModel> 
<DicomAttribute tag=""00100010"" vr=""PN"" keyword=""PatientName"">
<Value number=""1"">Test^Name</Value>
</DicomAttribute>
<DicomAttribute tag=""0020000D"" vr=""UI"" keyword=""StudyInstanceUID"">
<Value number=""1"">1.2.345</Value>
</DicomAttribute>
</NativeDicomModel>
";
            var x = new DicomXML(dataset);
            string xml = x.XmlString;            
            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.Equal(finalXml.Trim(), xml.Trim());
        }

        [Fact]
        public void TestSequenceXmlSerialization()
        {
            var file = DicomFile.Open(@".\Test Data\DICOMDIR");
            var xml = (new DicomXML(file.Dataset)).XmlString;
            string finalXml1 = @"<Item number=""44"">
<DicomAttribute tag=""00041400"" vr=""UL"" keyword=""OffsetOfTheNextDirectoryRecord"">
<Value number=""1"">0</Value>
</DicomAttribute>
";
            string finalXml2 = @"<DicomAttribute tag=""00041500"" vr=""CS"" keyword=""ReferencedFileID"">
<Value number=""1"">IMAGES</Value>
<Value number=""2"">RLE</Value>
<Value number=""3"">VL3_RLE</Value>
</DicomAttribute>
";
            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.True(xml.Contains(finalXml1));
            Assert.True(xml.Contains(finalXml2));
        }
    }
}
