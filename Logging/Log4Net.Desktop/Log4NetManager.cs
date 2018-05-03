// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// Log manager for the log4net logging framework.
    /// </summary>
    /// <example>
    /// LogManager.SetImplementation(Log4NetManager.Instance);
    /// </example>
    public sealed class Log4NetManager : LogManager
    {
        /// <summary>
        /// Singleton instance of the <see cref="Log4NetManager"/>.
        /// </summary>
        public static readonly LogManager Instance = new Log4NetManager();

        /// <summary>
        /// Initializes an instance of <see cref="Log4NetManager"/>.
        /// </summary>
        private Log4NetManager()
        {
            // Perform dummy logging to ensure that [XmlConfigurator] attribute can be sufficiently employed (#244)
            var dummy = log4net.LogManager.Exists(string.Empty);
            dummy?.Warn("Unexpectedly found empty string logger");
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected override Logger GetLoggerImpl(string name)
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
            /// Log a message to the logger.
            /// </summary>
            /// <param name="level">Log level.</param>
            /// <param name="msg">Log message (format string).</param>
            /// <param name="args">Log message arguments.</param>
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
