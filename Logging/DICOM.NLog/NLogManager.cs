// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// LogManager for the NLog logging framework.
    /// </summary>
    /// <example>
    /// LogManager.SetImplementation(NLogManager.Instance);
    /// </example>
    public class NLogManager : LogManager
    {
        /// <summary>
        /// Singleton instance of <see cref="NLogManager"/>.
        /// </summary>
        public static readonly LogManager Instance = new NLogManager();

        /// <summary>
        /// Initializes an instance of <see cref="NLogManager"/>.
        /// </summary>
        private NLogManager()
        {
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            return new NLogger(NLog.LogManager.GetLogger(name));
        }

        /// <summary>
        /// Implementation of a NLog logger.
        /// </summary>
        private class NLogger : Logger
        {
            private readonly NLog.Logger logger;

            /// <summary>
            /// Initializes an instance of <see cref="NLogger"/>.
            /// </summary>
            /// <param name="logger"></param>
            public NLogger(NLog.Logger logger)
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
