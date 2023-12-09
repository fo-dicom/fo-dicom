// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;

namespace FellowOakDicom.Log
{
    public static class Extensions
    {
        public static void WriteToLog(this DicomDataset dataset, Microsoft.Extensions.Logging.ILogger log, Microsoft.Extensions.Logging.LogLevel level)
        {
            var logger = new DicomDatasetLogger(log, level);
            new DicomDatasetWalker(dataset).Walk(logger);
        }

        public static void WriteToLog(this DicomFile file, Microsoft.Extensions.Logging.ILogger log, Microsoft.Extensions.Logging.LogLevel level)
        {
            var logger = new DicomDatasetLogger(log, level);
            new DicomDatasetWalker(file.FileMetaInfo).Walk(logger);
            new DicomDatasetWalker(file.Dataset).Walk(logger);
        }
        
        [Obsolete("Use the overload that accepts a Microsoft.Extensions.Logging.ILogger")]
        public static void WriteToLog(this DicomDataset dataset, ILogger log, LogLevel level)
        {
            var microsoftLogger = new FellowOakDicomLogger(log);
            var microsoftLogLevel = level.ToMicrosoftLogLevel();
            dataset.WriteToLog(microsoftLogger, microsoftLogLevel);
        }

        [Obsolete("Use the overload that accepts a Microsoft.Extensions.Logging.ILogger")]
        public static void WriteToLog(this DicomFile file, ILogger log, LogLevel level)
        {
            var microsoftLogger = new FellowOakDicomLogger(log);
            var microsoftLogLevel = level.ToMicrosoftLogLevel();
            file.WriteToLog(microsoftLogger, microsoftLogLevel);
        }

        public static string WriteToString(this DicomDataset dataset)
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
    }
}
