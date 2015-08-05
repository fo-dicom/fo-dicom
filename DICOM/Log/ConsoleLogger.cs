// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Log
{
    public class ConsoleLogger : Logger
    {
        public static readonly Logger Instance = new ConsoleLogger();

        private object _lock = new object();

        private ConsoleLogger()
        {
        }

        public override void Log(LogLevel level, string msg, params object[] args)
        {
            lock (_lock)
            {
                var previous = Console.ForegroundColor;
                switch (level)
                {
                    case LogLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case LogLevel.Info:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case LogLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Fatal:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    default:
                        break;
                }
                Console.WriteLine(NameFormatToPositionalFormat(msg), args);
                Console.ForegroundColor = previous;
            }
        }
    }

    public class ConsoleLogManager : LogManager
    {
        public override Logger GetLogger(string name)
        {
            return ConsoleLogger.Instance;
        }
    }
}
