// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// LogManager for the NLog logging framework.
    /// </summary>
    /// <example>
    /// LogManager.Default = new NLogManager();
    /// </example>
    public class NLogManager : LogManager
    {
        public override Logger GetLogger(string name)
        {
            return new NLogger(NLog.LogManager.GetLogger(name));
        }

        private class NLogger : Logger
        {
            private readonly NLog.Logger _logger;

            public NLogger(NLog.Logger logger)
            {
                _logger = logger;
            }

            public override void Log(LogLevel level, string msg, params object[] args)
            {
                var ordinalFormattedMessage = NameFormatToPositionalFormat(msg);

                switch (level)
                {
                    case LogLevel.Debug:
                        _logger.Debug(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Info:
                        _logger.Info(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Warning:
                        _logger.Warn(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Error:
                        _logger.Error(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Fatal:
                        _logger.Fatal(ordinalFormattedMessage, args);
                        break;
                    default:
                        _logger.Info(ordinalFormattedMessage, args);
                        break;
                }
            }
        }
    }
}
