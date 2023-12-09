// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Tests
{
    /// <summary>
    /// Console Logger that additionally collects the log messages for use in unit tests.
    /// </summary>
    public class CollectingLogger : ILogger
    {
        private readonly IList<(LogLevel, string)> _logEntries = new List<(LogLevel, string)>();

        private bool _collecting;

        public IList<(LogLevel, string)> LogEntries => _logEntries;

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

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (_collecting)
            {
                _logEntries.Add((logLevel, formatter(state, exception)));
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _collecting;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        public void StartCollecting()
        {
            _collecting = true;
        }

        public void StopCollecting()
        {
            _collecting = false;
        }
    }
}
