// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// Main class for logging management.
    /// </summary>
    public abstract class LogManager
    {
        #region FIELDS

        private static LogManager implementation;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="LogManager"/>.
        /// </summary>
        static LogManager()
        {
            SetImplementation(NullLoggerManager.Instance);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Set the log manager implementation to use for logging.
        /// </summary>
        /// <param name="impl"></param>
        public static void SetImplementation(LogManager impl)
        {
            implementation = impl ?? NullLoggerManager.Instance;
        }

        /// <summary>
        /// Get logger.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        public static Logger GetLogger(string name)
        {
            return implementation.GetLoggerImpl(name);
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected abstract Logger GetLoggerImpl(string name);

        #endregion

        #region INNER TYPES

        /// <summary>
        /// Manager for null ("do nothing") loggers.
        /// </summary>
        private class NullLoggerManager : LogManager
        {
            /// <summary>
            /// Singleton instance of null logger manager.
            /// </summary>
            public static readonly LogManager Instance;

            /// <summary>
            /// Initializes the static fields.
            /// </summary>
            static NullLoggerManager()
            {
                Instance = new NullLoggerManager();
            }

            /// <summary>
            /// Initializes an instance of the null logger manager.
            /// </summary>
            private NullLoggerManager()
            {
            }

            /// <summary>
            /// Get logger from the current log manager implementation.
            /// </summary>
            /// <param name="name">Classifier name, typically namespace or type name.</param>
            /// <returns>Logger from the current log manager implementation.</returns>
            protected override Logger GetLoggerImpl(string name)
            {
                return NullLogger.Instance;
            }
        }

        /// <summary>
        /// Null logger, provides a no-op logger implementation.
        /// </summary>
        private class NullLogger : Logger
        {
            /// <summary>
            /// Singleton instance of the <see cref="NullLogger"/> class.
            /// </summary>
            public static readonly Logger Instance;

            /// <summary>
            /// Initializes the static fields of <see cref="NullLogger"/>.
            /// </summary>
            static NullLogger()
            {
                Instance = new NullLogger();
            }

            /// <summary>
            /// Initializes an instance of <see cref="NullLogger"/>.
            /// </summary>
            private NullLogger()
            {
            }

            /// <summary>
            /// Dispatch a log message.
            /// </summary>
            /// <param name="level">Log level.</param>
            /// <param name="msg">Log message format string.</param>
            /// <param name="args">Arguments corresponding to the <paramref name="msg">log message</paramref>.</param>
            /// <remarks>The <see cref="NullLogger"/> Log method overloads do nothing.</remarks>
            public override void Log(LogLevel level, string msg, params object[] args)
            {
                // Do nothing
            }
        }

        #endregion
    }
}
