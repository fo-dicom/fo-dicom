// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace FellowOakDicom.Tests.Log
{
    internal class TextWriterLogger : ILogger, IDisposable
    {

        private readonly TextWriter _textWriter;

        /// <summary>
        /// Initializes an instance of the <see cref="TextWriterLogger"/>.
        /// </summary>
        protected internal TextWriterLogger(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new TextWriterLogger(_textWriter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _textWriter.Write($"[{logLevel.ToString().ToUpperInvariant()}] ");
            _textWriter.WriteLine(formatter(state, exception));
        }

        public void Dispose()
        {
           // nothing to dispose
        }
    }
}
