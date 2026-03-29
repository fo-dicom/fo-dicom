// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.StructuredReport;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.StructuredReport
{

    [Collection(TestCollections.General)]
    public class DicomStructuredReportTest
    {

        [Fact]
        public void CreateAndSaveSR()
        {
            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.BasicTextSRStorage},
                { DicomTag.SOPInstanceUID,  DicomUID.Generate()},
                { DicomTag.StudyInstanceUID, DicomUID.Generate()},
                { DicomTag.SeriesInstanceUID, DicomUID.Generate()},
                { DicomTag.MediaStorageSOPClassUID,  DicomUID.MediaStorageDirectoryStorage },
                { DicomTag.MediaStorageSOPInstanceUID,  DicomUID.Generate() },
                { DicomTag.TransferSyntaxUID, DicomTransferSyntax.ExplicitVRLittleEndian },
                { DicomTag.ImplementationClassUID,  DicomImplementation.ClassUID },
                { DicomTag.ImplementationVersionName,  DicomImplementation.Version },
                { DicomTag.Modality, "SR" },
            };

            // Create the root content item which is the title
            var titleCode = new DicomCodeItem("121144", "DCM", "Document Title");

            // Prepare the one content item 
            var textCode = new DicomCodeItem("111412", "DCM", "Narrative Summary");
            var textItem = new DicomContentItem(textCode, DicomRelationship.Contains, DicomValueType.Text, "Hello World");

            // Now we can create a special content item called a structured report
            var report = new DicomStructuredReport(titleCode, textItem);

            // We need to add the dataset with all the patient, study, series stuff to the report.
            report.Dataset.Add(dataset);

            var stream = new MemoryStream();

            // Save the SR to a stream
            report.Save(stream);

            // Ensure writer buffer is flushed before checking length
            stream.Flush();

            // Verify file size is reasonable (accommodates full range of UID length variation)
            // UIDs from DicomUID.Generate() vary in length (2.25.{random-128bit-number})
            Assert.InRange(stream.Length, 744, 934);

            stream.Position = 0;
            var report2 = DicomStructuredReport.Open(stream);

            Assert.Equal(report.Code, report2.Code);
            Assert.Equal(report.Dataset.GetString(DicomTag.SOPInstanceUID), report2.Dataset.GetString(DicomTag.SOPInstanceUID));
        }
    }
}
