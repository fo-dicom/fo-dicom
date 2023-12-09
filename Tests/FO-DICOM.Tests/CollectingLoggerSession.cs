// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Tests
{
    public class CollectingLoggerSession : IDisposable
    {
        public CollectingLogger Logger { get; }

        public CollectingLoggerSession(CollectingLogger logger)
        {
            Logger = logger;
            Logger.Reset();
            Logger.StartCollecting();
        }

        public void Dispose()
        {
            Logger.StopCollecting();
        }

        public int NumberOfWarnings => Logger.NumberOfWarnings;
        public string WarningAt(int index) => Logger.WarningAt(index);
        public void Reset() => Logger.Reset();
    }
}
