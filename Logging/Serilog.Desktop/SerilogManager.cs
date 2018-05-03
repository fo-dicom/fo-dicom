// Copyright (c) 2012-2018 fo-dicom contributors.
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
        /// <param name="serilogLogger">Specific Serilog logger to use.</param>
        public SerilogManager(ILogger serilogLogger)
        {
            _serilogLogger = serilogLogger;
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            var serilogLogger = (_serilogLogger ?? Serilog.Log.Logger).ForContext("fo-DICOM", name);
            return new SerilogLoggerAdapter(serilogLogger);
        }
    }
}
