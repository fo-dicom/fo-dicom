// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;

namespace FellowOakDicom.Log
{

    /// <summary>
    /// Extension methods for dumping DICOM data to console.
    /// </summary>
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Write DICOM dataset to console.
        /// </summary>
        /// <param name="dataset">DICOM dataset to write.</param>
        public static void WriteToConsole(this DicomDataset dataset)
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
