// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Log
{

    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    internal static class LogLevelExtensions
    {
        [Obsolete("Only use this in obsolete methods")]
        public static Microsoft.Extensions.Logging.LogLevel ToMicrosoftLogLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return Microsoft.Extensions.Logging.LogLevel.Debug;
                case LogLevel.Info:
                    return Microsoft.Extensions.Logging.LogLevel.Information;
                case LogLevel.Warning:
                    return Microsoft.Extensions.Logging.LogLevel.Warning;
                case LogLevel.Error:
                    return Microsoft.Extensions.Logging.LogLevel.Error;
                case LogLevel.Fatal:
                    return Microsoft.Extensions.Logging.LogLevel.Critical;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}
