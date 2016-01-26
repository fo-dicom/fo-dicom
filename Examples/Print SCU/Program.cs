// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Print_SCU
{
    using System;
    using System.Drawing;

    using Dicom;
    using Dicom.Imaging;
    using Dicom.Log;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Initialize log manager.
            LogManager.SetImplementation(ConsoleLogManager.Instance);

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            var printJob = new PrintJob("DICOM PRINT JOB")
                               {
                                   RemoteAddress = "localhost",
                                   RemotePort = 8000,
                                   CallingAE = "PRINTSCU",
                                   CalledAE = "PRINTSCP"
                               };

            printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");

            printJob.FilmSession.IsColor = false; //set to true to print in color

            //greyscale
            var dicomImage = new DicomImage(@"Data\1.3.51.5155.1353.20020423.1100947.1.0.0.dcm");

            //color
            //var dicomImage = new DicomImage(@"Data\US-RGB-8-epicard.dcm");

            var bitmap = dicomImage.RenderImage().As<Bitmap>();

            printJob.AddImage(bitmap, 0);

            bitmap.Dispose();

            printJob.EndFilmBox();

            printJob.Print();

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
