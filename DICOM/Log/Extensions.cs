// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    using DICOM.Shared.Log;
    using System.Collections.Generic;
    using System.Text;

    public static class Extensions
    {
        public static void WriteToLog(this IEnumerable<DicomItem> dataset, Logger log, LogLevel level)
        {
            var logger = new DicomDatasetLogger(log, level);
            new DicomDatasetWalker(dataset).Walk(logger);
        }

        public static void WriteToLog(this DicomFile file, Logger log, LogLevel level)
        {
            var logger = new DicomDatasetLogger(log, level);
            new DicomDatasetWalker(file.FileMetaInfo).Walk(logger);
            new DicomDatasetWalker(file.Dataset).Walk(logger);
        }

        public static string WriteToString(this IEnumerable<DicomItem> dataset)
        {
            var log = new StringBuilder();
            var dumper = new DicomDatasetDumper(log);
            new DicomDatasetWalker(dataset).Walk(dumper);
            return log.ToString();
        }

        public static string WriteToString(this DicomFile file)
        {
            var log = new StringBuilder();
            var dumper = new DicomDatasetDumper(log);
            new DicomDatasetWalker(file.FileMetaInfo).Walk(dumper);
            new DicomDatasetWalker(file.Dataset).Walk(dumper);
            return log.ToString();
        }

        public static string WriteToXml(this DicomDataset dataset)
        {
            var dicomXml = new DicomXML(dataset);
            return dicomXml.XmlString;
        }

        public static string WriteToXml(this DicomFile file)
        {
            var dicomXml = new DicomXML(file.Dataset);
            return dicomXml.XmlString;
        }

    }
}
