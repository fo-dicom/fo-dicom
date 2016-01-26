// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Print_SCP
{
    using System;

    using Dicom;
    using Dicom.Imaging;
    using Dicom.Log;
    using Dicom.Printing;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Initialize log manager.
            LogManager.SetImplementation(ConsoleLogManager.Instance);

            //This is a simple DICOM Print SCP implementation with Print Job and Send Event Report Support
            //This sample depends on the Microsoft XPS Document Writer Printer to be installed on the system
            //You are free to use what ever printer you like by modifieing the PrintJob DoPrint method hard coded
            //printer name

            //All print jobs willbe created to the exe folder under a folder named PrintJobs

            Console.WriteLine("Starting print SCP server with AET: PRINTSCP on port 8000");

            PrintService.Start(8000, "PRINTSCP");

            Console.WriteLine("Press any key to stop the service");

            Console.Read();

            Console.WriteLine("Stopping print service");

            PrintService.Stop();

        }
    }
}
