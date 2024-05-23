﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Tests
{
    public class CollectingLoggerProvider : ILoggerProvider
    {
        public static readonly CollectingLoggerProvider Instance = new CollectingLoggerProvider();

        public readonly CollectingLogger CollectingLogger;

        public CollectingLoggerProvider()
        {
            CollectingLogger = new CollectingLogger();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return CollectingLogger;
        }
    }
}
