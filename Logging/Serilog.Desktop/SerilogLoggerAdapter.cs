// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Serilog;

namespace Dicom.Log
{
    /// <summary>
    /// Implementation of <see cref="Logger"/> for Serilog.
    /// </summary>
    internal class SerilogLoggerAdapter : Logger
    {
        private readonly ILogger serilog;

        /// <summary>
        /// Initializes an instance of <see cref="SerilogLoggerAdapter"/>
        /// </summary>
        /// <param name="serilog"></param>
        public SerilogLoggerAdapter(ILogger serilog)
        {
            this.serilog = serilog;
        }

        /// <summary>
        /// Log a message to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public override void Log(LogLevel level, string msg, params object[] args)
        {
            this.serilog.Write(level.ToSerilog(), msg, args);
        }
    }
}
