using System;
using System.Diagnostics;
using System.IO;

using Dicom;
using Dicom.Imaging;

namespace Jpeg2000Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = args.Length > 0 ? args[0] : @"C:\Users\ander\Documents\Data\DICOM\compsamples_j2k\IMAGES";

            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            var stopwatch = new Stopwatch();

            foreach (var file in files)
            {
                try
                {
                    stopwatch.Start();
                    var dcm = DicomFile.Open(file);
                    stopwatch.Stop();
                    var reading = stopwatch.ElapsedMilliseconds;

                    stopwatch.Reset();
                    stopwatch.Start();
                    var rendered = new DicomImage(dcm.Dataset).RenderImage();
                    stopwatch.Stop();
                    var rendering = stopwatch.ElapsedMilliseconds;

                    stopwatch.Reset();
                    stopwatch.Start();
                    var bitmap = rendered.AsBitmap();
                    stopwatch.Stop();
                    var converting = stopwatch.ElapsedMilliseconds;

                    Console.WriteLine($"{file}: {reading} ms, {rendering} ms, {converting} ms");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error for {file}: {e}");
                }
            }
        }
    }
}
