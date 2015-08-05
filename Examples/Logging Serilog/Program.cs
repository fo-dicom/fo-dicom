// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using Dicom.Log;

using Serilog;
using Serilog.Enrichers;

namespace Dicom.Demo.SerilogDemo
{
    internal class Program
    {

        //Set this to false if Seq (http://getseq.net) is not present
        private static bool useSeq = true;

        private static void Main(string[] args)
        {

            //SPECIFIC LOGGER VERSUS GLOBAL LOGGER
            //UseSpecificSerilogLogger();
            //ALTERNATE
            UseGlobalSerilogLogger();

            //Do some DICOM work
            var file = DicomFile.Open(@"..\..\..\DICOM Media\Data\Patient1\2.dcm");

            //Example of logging a dicom dataset
            //file.Dataset.WriteToLog(LogManager.Default.GetLogger("dumpedDataset"), LogLevel.Info);

            //Other logging using fo-dicom's log abstraction
            Dicom.Log.Logger foDicomLogger = LogManager.Default.GetLogger("testLog");
            foDicomLogger.Fatal("A fatal message at {dateTime}", DateTime.Now);
            foDicomLogger.Debug("A debug for file {filename} - info: {@metaInfo}", file.File.Name, file.FileMetaInfo);

            Console.WriteLine("Finished - hit enter to exit");
            Console.ReadLine();


        }


        private static void UseSpecificSerilogLogger()
        {
            //Get a Serilog logger instance
            var logger = ConfigureLogging();

            //Wrap it in some extra context as an example
            logger = logger.ForContext("Purpose", "Demonstration");

            //Configure fo-dicom & Serilog
            LogManager.Default = new SerilogManager(logger);

        }

        private static void UseGlobalSerilogLogger()
        {
            //Configure logging
            ConfigureLogging();

            //Configure fo-dicom & Serilog
            LogManager.Default = new SerilogManager();

        }


        /// <summary>
        /// Create and return a serilog ILogger instance.  
        /// For convenience this also sets the global Serilog.Log instance
        /// </summary>
        /// <returns></returns>
        public static ILogger ConfigureLogging()
        {
            var loggerConfig = new LoggerConfiguration()
                //Enrich each log message with the machine name
                .Enrich.With<MachineNameEnricher>()
                //Accept verbose output  (there is effectively no filter)
                .MinimumLevel.Verbose()
                //Write out to the console using the "Literate" console sink (colours the text based on the logged type)
                .WriteTo.LiterateConsole()
                //Also write out to a file based on the date and restrict these writes to warnings or worse (warning, error, fatal)
                .WriteTo.RollingFile(@"Warnings_{Date}.txt", global::Serilog.Events.LogEventLevel.Warning);

            if (useSeq)
            {
                //Send events to a default installation of Seq on the local computer
                loggerConfig = loggerConfig.WriteTo.Seq("http://localhost:5341");
            }

            var logger = loggerConfig
                //Take all of that configuration and make a logger
                .CreateLogger();

            //Stash the logger in the global Log instance for convenience
            global::Serilog.Log.Logger = logger;

            return logger;
        }
    }
}
