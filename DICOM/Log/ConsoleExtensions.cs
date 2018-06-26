// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Extension methods for dumping DICOM data to console.
    /// </summary>
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Write DICOM dataset to console.
        /// </summary>
        /// <param name="dataset">DICOM dataset to write.</param>
        public static void WriteToConsole(this IEnumerable<DicomItem> dataset)
        {
            var log = new StringBuilder();
            var dumper = new DicomDatasetDumper(log, 80, 60);
            new DicomDatasetWalker(dataset).Walk(dumper);
            Console.WriteLine(log);
        }

        /// <summary>
        /// Write DICOM file to console.
        /// </summary>
        /// <param name="file">DICOM file to write.</param>
        public static void WriteToConsole(this DicomFile file)
        {
            var log = new StringBuilder();
            var dumper = new DicomDatasetDumper(log, 80, 60);
            new DicomDatasetWalker(file.FileMetaInfo).Walk(dumper);
            new DicomDatasetWalker(file.Dataset).Walk(dumper);
            Console.WriteLine(log);
        }
    }
}
