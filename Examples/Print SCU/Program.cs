// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Print_SCU
{
    using System;
    using System.Drawing;
    using System.Threading;
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

            while(true)
            {
                TestPrintSCU();
                Thread.Sleep(10000);
            }

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.Elapsed);
        }

        private static void TestPrintSCU()
        {
            var printJob = new PrintJob("DICOM PRINT JOB")
            {
                RemoteAddress = "localhost",
                RemotePort = 14311,
                CallingAE = "PRINTSCU",
                CalledAE = "PRINTSCP"
            };

            printJob.FilmSession.IsColor = false; //set to true to print in color

            printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");


            //greyscale
            var dicomImage = new DicomImage(@"Data\cc_BREAST_SF_MLO_L.denOut_skinLineOff.dcm");

            //color
            //var dicomImage = new DicomImage(@"Data\US-RGB-8-epicard.dcm");

            var bitmap = dicomImage.RenderImage().As<Bitmap>();

            printJob.AddImage(bitmap, 0);

            bitmap.Dispose();

            printJob.EndFilmBox();




            printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");


            //greyscale
            dicomImage = new DicomImage(@"Data\cc_BREAST_SF_MLO_L.denOut_skinLineOff.dcm");

            //color
            //var dicomImage = new DicomImage(@"Data\US-RGB-8-epicard.dcm");

            bitmap = dicomImage.RenderImage().As<Bitmap>();

            printJob.AddImage(bitmap, 0);

            bitmap.Dispose();

            printJob.EndFilmBox();


            printJob.Print();
        }
    }
}
