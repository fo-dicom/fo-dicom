// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// Log manager for the MetroLog logging framework.
    /// </summary>
    /// <example>
    /// LogManager.SetImplementation(MetroLogManager.Instance);
    /// </example>
    public class MetroLogManager : LogManager
    {
        /// <summary>
        /// Singleton instance of the <see cref="MetroLogManager"/>.
        /// </summary>
        public static readonly LogManager Instance = new MetroLogManager();

        /// <summary>
        /// Initializes an instance of the <see cref="MetroLogManager"/>.
        /// </summary>
        private MetroLogManager()
        {
        }

        /// <summary>
        /// Returns a logger for the specified (type) name.
        /// </summary>
        /// <param name="name">Name of logger, typically type or namespace name.</param>
        /// <returns>Logger for the specified name.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            return new MetroLogger(MetroLog.LogManagerFactory.DefaultLogManager.GetLogger(name));
        }

        /// <summary>
        /// Implementation of the <see cref="Logger"/> class for MetroLog framework.
        /// </summary>
        private class MetroLogger : Logger
        {
            /// <summary>
            /// MetroLog logger instance.
            /// </summary>
            private readonly MetroLog.ILogger logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="MetroLogger"/> class. 
            /// </summary>
            /// <param name="logger">
            /// MetroLog logger.
            /// </param>
            internal MetroLogger(MetroLog.ILogger logger)
            {
                this.logger = logger;
            }

            /// <summary>
            /// Enter log message.
            /// </summary>
            /// <param name="level">Log level.</param>
            /// <param name="msg">Formatted text message.</param>
            /// <param name="args">Array of arguments associated with the text message.</param>
            public override void Log(LogLevel level, string msg, params object[] args)
            {
                var ordinalFormattedMessage = NameFormatToPositionalFormat(msg);

                switch (level)
                {
                    case LogLevel.Debug:
                        this.logger.Debug(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Info:
                        this.logger.Info(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Warning:
                        this.logger.Warn(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Error:
                        this.logger.Error(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Fatal:
                        this.logger.Fatal(ordinalFormattedMessage, args);
                        break;
                    default:
                        this.logger.Info(ordinalFormattedMessage, args);
                        break;
                }
            }
        }
    }
}
