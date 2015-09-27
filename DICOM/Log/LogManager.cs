// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    /// <summary>
    /// Main class for logging management.
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Initilaizes the static fields of <see cref="LogManager"/>.
        /// </summary>
        static LogManager()
        {
            Default = new LogManager();
        }

        /// <summary>
        /// Initializes an instance of <see cref="LogManager"/>.
        /// </summary>
        protected LogManager()
        {
        }

        /// <summary>
        /// Gets or sets the default log manager implementation.
        /// </summary>
        public static LogManager Default { get; set; }

        /// <summary>
        /// Get logger from the <see cref="Default"/> log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the <see cref="Default"/> log manager implementation.</returns>
        public virtual Logger GetLogger(string name)
        {
            return NullLogger.Instance;
        }

        #region INNER TYPES

        /// <summary>
        /// Null logger, provides a no-op logger implementation.
        /// </summary>
        private class NullLogger : Logger
        {
            /// <summary>
            /// Singleton instance of the <see cref="NullLogger"/> class.
            /// </summary>
            internal static readonly Logger Instance;

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
