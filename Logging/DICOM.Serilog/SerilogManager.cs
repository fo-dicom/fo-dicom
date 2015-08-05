// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Serilog;

namespace Dicom.Log
{

    /// <summary>
    /// Directs fo-dicom's logging through Serilog
    /// </summary>
    public class SerilogManager : LogManager
    {
        private readonly ILogger _serilogLogger;

        /// <summary>
        /// Instantiates fo-dicom Serilog logging relying on Serilog's Serilog.Log global static logger
        /// </summary>
        public SerilogManager()
        {
            _serilogLogger = null;
        }

        /// <summary>
        /// Instantiates fo-dicom Serilog logging facility, sending logs through the provided ILogger instance
        /// </summary>
        /// <param name="serilogLogger"></param>
        public SerilogManager(ILogger serilogLogger)
        {
            _serilogLogger = serilogLogger;
        }

        /// <summary>
        /// Returns a scoped instance of a Serilog logger with the fo-DICOM
        /// property set to the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Logger GetLogger(string name)
        {
            var serilogLogger = (_serilogLogger ?? Serilog.Log.Logger).ForContext("fo-DICOM", name);
            return new SerilogLoggerAdapter(serilogLogger);
        }
    }
}
