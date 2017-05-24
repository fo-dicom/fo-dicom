// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    using System;

    /// <summary>
    /// Logger for output to the <see cref="Console"/>.
    /// </summary>
    public class ConsoleLogger : Logger
    {
        /// <summary>
        /// Singleton instance of the <see cref="ConsoleLogger"/>.
        /// </summary>
        public static readonly Logger Instance = new ConsoleLogger();

        private readonly object @lock = new object();

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLogger"/>.
        /// </summary>
        private ConsoleLogger()
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
            lock (this.@lock)
            {
                var previous = ConsoleForegroundColor;
                switch (level)
                {
                    case LogLevel.Debug:
                        ConsoleForegroundColor = ConsoleColor.Blue;
                        break;
                    case LogLevel.Info:
                        ConsoleForegroundColor = ConsoleColor.White;
                        break;
                    case LogLevel.Warning:
                        ConsoleForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogLevel.Error:
                        ConsoleForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Fatal:
                        ConsoleForegroundColor = ConsoleColor.Magenta;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("level", level, null);
                }
                Console.WriteLine(NameFormatToPositionalFormat(msg), args);
                ConsoleForegroundColor = previous;
            }
        }

        /// <summary>
        /// Gets or sets the console foreground color.
        /// </summary>
        /// <remarks>Dispatcher method to allow for platform-specific handling.</remarks>
        private static ConsoleColor ConsoleForegroundColor
        {
            get
            {
#if __IOS__ || __ANDROID__ || NET35
                return ConsoleColor.Black;
#else
                return Console.ForegroundColor;
#endif
            }
            set
            {
#if !__IOS__ && !__ANDROID__ && !NET35
                Console.ForegroundColor = value;
#endif
            }
        }
    }

    /// <summary>
    /// Manager for logging to the console.
    /// </summary>
    public class ConsoleLogManager : LogManager
    {
        /// <summary>
        /// Singleton instance of the <see cref="ConsoleLogManager"/>.
        /// </summary>
        public static readonly LogManager Instance = new ConsoleLogManager();

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLogManager"/>.
        /// </summary>
        private ConsoleLogManager()
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
