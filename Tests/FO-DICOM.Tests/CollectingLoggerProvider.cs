using Microsoft.Extensions.Logging;

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

        }

        public ILogger CreateLogger(string categoryName)
        {
            return CollectingLogger;
        }
    }
}
