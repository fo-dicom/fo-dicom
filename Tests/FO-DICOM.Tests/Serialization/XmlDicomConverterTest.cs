// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Serialization;
using System;
using System.Text;
using System.Xml;
using Xunit;

namespace FellowOakDicom.Tests.Serialization
{

    [Collection(TestCollections.General)]
    public class XmlDicomConverterTest
    {
        [Fact]
        public void TestSimpleXmlSerialization()
        {
            var dataset = new DicomDataset();
            dataset.AddOrUpdate(DicomTag.StudyInstanceUID, "1.2.345");
            dataset.AddOrUpdate(DicomTag.PatientName, "Test^Name");
            string[] patientCommentsValue = null;
            dataset.AddOrUpdate(DicomTag.PatientComments, patientCommentsValue);
            var finalXml = new StringBuilder();
            finalXml.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            finalXml.AppendLine(@"<NativeDicomModel>");
            finalXml.AppendLine(@"<DicomAttribute tag=""00100010"" vr=""PN"" keyword=""PatientName"">");
            finalXml.AppendLine(@"<PersonName number=""1"">");
            finalXml.AppendLine(@"<Alphabetic>");
            finalXml.AppendLine(@"<FamilyName>Test</FamilyName>");
            finalXml.AppendLine(@"<GivenName>Name</GivenName>");
            finalXml.AppendLine(@"</Alphabetic>");
            finalXml.AppendLine(@"</PersonName>");
            finalXml.AppendLine(@"</DicomAttribute>");
            finalXml.AppendLine(@"<DicomAttribute tag=""00104000"" vr=""LT"" keyword=""PatientComments"">");
            finalXml.AppendLine(@"<Value number=""1""></Value>");
            finalXml.AppendLine(@"</DicomAttribute>");
            finalXml.AppendLine(@"<DicomAttribute tag=""0020000D"" vr=""UI"" keyword=""StudyInstanceUID"">");
            finalXml.AppendLine(@"<Value number=""1"">1.2.345</Value>");
            finalXml.AppendLine(@"</DicomAttribute>");
            finalXml.AppendLine(@"</NativeDicomModel>");
            string xml = DicomXML.ConvertDicomToXML(dataset);
            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.Equal(finalXml.ToString().Trim(), xml.Trim());
        }

        [Fact]
        public void TestSequenceXmlSerialization()
        {
            var file = DicomFile.Open(TestData.Resolve("DICOMDIR"));
            var xml = DicomXML.ConvertDicomToXML(file.Dataset);
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
            Assert.Contains(finalXml1.ToString(), xml);
            Assert.Contains(finalXml2.ToString(), xml);
        }


        [Fact]
        public void TestDicomFragmentSequenceXmlSerialization()
        {
            // Test File HasFragmentSequence with DicomFragmentSequence (total count is 96)
            var file = DicomFile.Open(TestData.Resolve("HasFragmentSequence.dcm"));
            var xml = DicomXML.ConvertDicomToXML(file.Dataset);

            var PixelDataStart = @"<DicomAttribute tag=""7FE00010"" vr=""OB"" keyword=""PixelData"">";

            Assert.True(!string.IsNullOrEmpty(xml));
            Assert.Contains(PixelDataStart, xml);

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList Fragments = doc.DocumentElement.SelectNodes("DicomAttribute/Fragment");

            Assert.True(Fragments.Count == 96);
        }
    }
}
