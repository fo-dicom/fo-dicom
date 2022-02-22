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
