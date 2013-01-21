using System;

// ReSharper disable CheckNamespace
namespace NLog
// ReSharper restore CheckNamespace
{
    internal static class LogLevelExtensions
    {
        internal static MetroLog.LogLevel ToMetroLogLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return MetroLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return MetroLog.LogLevel.Debug;
                case LogLevel.Info:
                    return MetroLog.LogLevel.Info;
                case LogLevel.Warn:
                    return MetroLog.LogLevel.Warn;
                case LogLevel.Error:
                    return MetroLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return MetroLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, "Invalid log level");
            }
        }
    }
}