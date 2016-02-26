// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Helpers
{
    using System.Text;

    using Dicom.Log;

    public class StringLogManager : LogManager
    {
        protected override Logger GetLoggerImpl(string name)
        {
            return StringLogger.Instance;
        }

        internal class StringLogger : Logger
        {
            private readonly StringBuilder builder = new StringBuilder();

            internal static Logger Instance = new StringLogger();

            private StringLogger()
            {
            }

            public override void Log(LogLevel level, string msg, params object[] args)
            {
                var format = Logger.NameFormatToPositionalFormat(msg);
                this.builder.AppendLine(string.Format(format, args));
            }

            public override string ToString()
            {
                return this.builder.ToString();
            }
        }
    }
}