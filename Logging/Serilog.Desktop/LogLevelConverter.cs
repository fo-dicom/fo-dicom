// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Serilog.Events;

namespace Dicom.Log
{
    /// <summary>
    /// Support for converting between <see cref="LogEventLevel"/> and <see cref="LogLevel"/>.
    /// </summary>
    internal static class LogLevelConverter
    {
        /// <summary>
        /// Convert from <see cref="LogLevel"/> to <see cref="LogEventLevel"/>.
        /// </summary>
        /// <param name="dicomLogLevel">DICOM log level.</param>
        /// <returns>Serilog log event level.</returns>
        public static LogEventLevel ToSerilog(this LogLevel dicomLogLevel)
        {
            switch (dicomLogLevel)
            {
                case LogLevel.Debug:
                    return LogEventLevel.Debug;
                case LogLevel.Info:
                    return LogEventLevel.Information;
                case LogLevel.Warning:
                    return LogEventLevel.Warning;
                case LogLevel.Error:
                    return LogEventLevel.Error;
                case LogLevel.Fatal:
                    return LogEventLevel.Fatal;
                default:
                    //pathological case - shouldn't occur
                    return LogEventLevel.Verbose;
            }

        }
    }
}
