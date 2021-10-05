// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Linq;
using FellowOakDicom.Log;

namespace FellowOakDicom.Tests
{
    /// <summary>
    /// Console Logger that additionally collects the log messages for use in unit tests.
    /// </summary>
    class CollectingConsoleLogger : ConsoleLogger
    {
        /// <summary>
        /// Singleton instance of the <see cref="CollectingConsoleLogger"/>.
        /// </summary>
        public new static readonly Logger Instance = new CollectingConsoleLogger();

        private readonly IList<(LogLevel, string)> _logEntries = new List<(LogLevel, string)>();

        /// <summary>
        /// Save an issued log message.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public override void Log(LogLevel level, string msg, params object[] args)
        {
            _logEntries.Add((level, string.Format(NameFormatToPositionalFormat(msg), args)));
            base.Log(level, msg, args);
        }

        /// <summary>
        /// Clear the collected log entries.
        /// </summary>
        public void Reset()
        {
            _logEntries.Clear();
        }

        /// <summary>
        /// Return the number of issued log calls.
        /// </summary>
        public int NumberOfWarnings => _logEntries.Count(e => e.Item1 == LogLevel.Warning);

        /// <summary>
        /// Return the level and message of a given log call.
        /// </summary>
        public (LogLevel, string) LogEntryAt(int index) => _logEntries[index];

        /// <summary>
        /// Return the message of a given log call and log level.
        /// <param name="index">The index of the message in all messages of the given level.</param>
        /// <param name="logLevel">Only messages of this log level are considered.</param>
        /// </summary>
        public string LogMessageAt(int index, LogLevel logLevel) =>
            _logEntries.Where(e => e.Item1 == logLevel).ElementAt(index).Item2;

        /// <summary>
        /// Returns the warning message at a given index.
        /// <param name="index">The index of the message in all warning messages.</param>
        /// </summary>
        public string WarningAt(int index) => LogMessageAt(index, LogLevel.Warning);
    }

    /// <summary>
    /// Manager for collecting log messages.
    /// </summary>
    public class CollectingConsoleLogManager : LogManager
    {
        /// <summary>
        /// Get log collector instance.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>The log collector singleton instance.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            return CollectingConsoleLogger.Instance;
        }
    }
}
