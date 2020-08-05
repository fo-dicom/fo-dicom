// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

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
                var nlogLevel = GetNLogLevel(level);

                if (args.Length >= 1 && args[0] is Exception)
                {
                    this.logger.Log(nlogLevel, (Exception)args[0], ordinalFormattedMessage, args);
                }
                else
                {
                    this.logger.Log(nlogLevel, ordinalFormattedMessage, args);
                }
            }

            /// <summary>
            /// Converts <see cref="LogLevel"/> enumeration to <see cref="NLog.LogLevel"/> equivalent.
            /// </summary>
            /// <param name="level"><see cref="LogLevel"/> enumeration subject to conversion.</param>
            /// <returns><see cref="NLog.LogLevel"/> equivalent.</returns>
            private static NLog.LogLevel GetNLogLevel(LogLevel level)
            {
                switch (level)
                {
                    case LogLevel.Info:
                        return NLog.LogLevel.Info;
                    case LogLevel.Warning:
                        return NLog.LogLevel.Warn;
                    case LogLevel.Error:
                        return NLog.LogLevel.Error;
                    case LogLevel.Fatal:
                        return NLog.LogLevel.Fatal;
                    case LogLevel.Debug:
                    default:
                        return NLog.LogLevel.Debug;
                }
            }
        }
    }
}
