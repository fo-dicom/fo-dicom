// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

namespace Dicom.Log
{
    using System;

    /// <summary>
    /// Logger for output to a <see cref="TextWriter"/> implmentation.
    /// </summary>
    public class TextWriterLogger : Logger
    {
        private readonly TextWriter _textWriter;

        /// <summary>
        /// Initializes an instance of the <see cref="TextWriterLogger"/>.
        /// </summary>
        protected internal TextWriterLogger(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        /// <summary>
        /// Log a message to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public override void Log(LogLevel level, string msg, params object[] args)
        {
            _textWriter.Write($"[{level.ToString().ToUpperInvariant()}] ");
            _textWriter.WriteLine(NameFormatToPositionalFormat(msg), args);
        }
    }

    /// <summary>
    /// Manager for logging to a <see cref="TextWriter"/> implementation.
    /// </summary>
    public class TextWriterLogManager : LogManager
    {
        #region FIELDS

        private readonly Logger _loggerImpl;

        #endregion

        /// <summary>
        /// Initializes an instance of the <see cref="TextWriterLogManager"/>.
        /// </summary>
        public TextWriterLogManager(TextWriter textWriter)
        {
            _loggerImpl = new TextWriterLogger(textWriter);
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            return _loggerImpl;
        }
    }
}
