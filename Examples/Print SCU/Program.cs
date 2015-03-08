using Dicom;
using Dicom.Imaging;
using Dicom.IO;
using Dicom.Network;
using Dicom.Printing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print_SCU
{
    class Program
    {
        static void Main(string[] args)
        {
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
            
            var bitmap = dicomImage.RenderImage() as System.Drawing.Bitmap;
            
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
