// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.AspNetCore
{

    public class DicomLogManager : FellowOakDicom.Log.ILogManager
    {
        private readonly Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;

        public DicomLogManager(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Log.ILogger GetLogger(string name)
        {
            return new DicomLogger(_loggerFactory.CreateLogger(name));
        }
    }


    public class DicomLogger : FellowOakDicom.Log.Logger
    {

        private readonly Microsoft.Extensions.Logging.ILogger _aspnetLogger;

        public DicomLogger(Microsoft.Extensions.Logging.ILogger logger)
        {
            _aspnetLogger = logger;
        }

        public override void Log(Log.LogLevel level, string msg, params object[] args)
        {
            switch(level)
            {
                case FellowOakDicom.Log.LogLevel.Debug:
                    _aspnetLogger.LogDebug(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Error:
                case FellowOakDicom.Log.LogLevel.Fatal:
                    _aspnetLogger.LogError(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Info:
                    _aspnetLogger.LogInformation(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Warning:
                    _aspnetLogger.LogWarning(msg, args);
                    break;
            }
        }

    }
}
