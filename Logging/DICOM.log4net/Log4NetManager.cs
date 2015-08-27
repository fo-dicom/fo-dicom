// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// Log manager for the log4net logging framework.
    /// </summary>
    /// <example>
    /// LogManager.Default = new Log4NetLogger();
    /// </example>
    public sealed class Log4NetManager : LogManager
    {
        /// <summary>
        /// Get a log4net based logger.
        /// </summary>
        /// <param name="name">
        /// Type or namespace name.
        /// </param>
        /// <returns>
        /// A log4net based <see cref="Logger"/>.
        /// </returns>
        public override Logger GetLogger(string name)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(name));
        }

        /// <summary>
        /// Private class providing the connection to the log4net framework.
        /// </summary>
        private class Log4NetLogger : Logger
        {
            /// <summary>
            /// log4net logger instance.
            /// </summary>
            private readonly log4net.ILog logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
            /// </summary>
            /// <param name="logger">
            /// The logger.
            /// </param>
            public Log4NetLogger(log4net.ILog logger)
            {
                this.logger = logger;
            }

            /// <summary>
            /// Log a message.
            /// </summary>
            /// <param name="level">
            /// Log level.
            /// </param>
            /// <param name="msg">
            /// Formatted message string.
            /// </param>
            /// <param name="args">
            /// Message arguments.
            /// </param>
            public override void Log(LogLevel level, string msg, params object[] args)
            {
                var ordinalFormattedMessage = NameFormatToPositionalFormat(msg);

                switch (level)
                {
                    case LogLevel.Debug:
                        this.logger.DebugFormat(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Info:
                        this.logger.InfoFormat(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Warning:
                        this.logger.WarnFormat(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Error:
                        this.logger.ErrorFormat(ordinalFormattedMessage, args);
                        break;
                    case LogLevel.Fatal:
                        this.logger.FatalFormat(ordinalFormattedMessage, args);
                        break;
                    default:
                        this.logger.InfoFormat(ordinalFormattedMessage, args);
                        break;
                }
            }
        }
    }
}
