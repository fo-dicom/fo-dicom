// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Log
{
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public class FellowOakDicomLogger: Microsoft.Extensions.Logging.ILogger
    {
        private readonly ILogger _logger;

        public FellowOakDicomLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.None:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    _logger.Debug(formatter(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    _logger.Info(formatter(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    _logger.Warn(formatter(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    _logger.Error(formatter(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    _logger.Fatal(formatter(state, exception));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Unsupported log level");
            }   
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => throw new NotSupportedException();
    }
}