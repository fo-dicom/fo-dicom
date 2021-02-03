// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Log
{

    /// <summary>
    /// Logger for output to the <see cref="Console"/>.
    /// </summary>
    public class ConsoleLogger : TextWriterLogger
    {
        /// <summary>
        /// Singleton instance of the <see cref="ConsoleLogger"/>.
        /// </summary>
        public static readonly Logger Instance = new ConsoleLogger();

        private readonly object _lock = new object();

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLogger"/>.
        /// </summary>
        private ConsoleLogger() : base(Console.Out)
        {
        }

        /// <summary>
        /// Log a message to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public override void Log(LogLevel level, string msg, params object[] args)
        {
            lock (_lock)
            {
                var previous = ConsoleForegroundColor;
                ConsoleForegroundColor = level switch
                {
                    LogLevel.Debug => ConsoleColor.Blue,
                    LogLevel.Info => ConsoleColor.White,
                    LogLevel.Warning => ConsoleColor.Yellow,
                    LogLevel.Error => ConsoleColor.Red,
                    LogLevel.Fatal => ConsoleColor.Magenta,
                    _ => throw new ArgumentOutOfRangeException(nameof(level), level, null),
                };
                base.Log(level, msg, args);
                ConsoleForegroundColor = previous;
            }
        }

        /// <summary>
        /// Gets or sets the console foreground color.
        /// </summary>
        /// <remarks>Dispatcher method to allow for platform-specific handling.</remarks>
        private static ConsoleColor ConsoleForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }
    }

    /// <summary>
    /// Manager for logging to the console.
    /// </summary>
    public class ConsoleLogManager : LogManager
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLogManager"/>.
        /// </summary>
        public ConsoleLogManager()
        {
        }

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected override Logger GetLoggerImpl(string name)
        {
            return ConsoleLogger.Instance;
        }
    }
}
