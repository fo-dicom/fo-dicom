using Microsoft.Extensions.Logging;

namespace FellowOakDicom.AspNetCore
{
    public class DicomLogger : Log.ILogger
    {

        private readonly Microsoft.Extensions.Logging.ILogger _aspnetLogger;

        public DicomLogger(Microsoft.Extensions.Logging.ILogger logger)
        {
            _aspnetLogger = logger;
        }

        public void Debug(string msg, params object[] args)
            => _aspnetLogger.LogDebug(msg, args);

        public void Error(string msg, params object[] args)
            => _aspnetLogger.LogError(msg, args);

        public void Fatal(string msg, params object[] args)
            => _aspnetLogger.LogError(msg, args);

        public void Info(string msg, params object[] args)
            => _aspnetLogger.LogInformation(msg, args);

        public void Warn(string msg, params object[] args)
            => _aspnetLogger.LogWarning(msg, args);

        public void Log(Log.LogLevel level, string msg, params object[] args)
        {
            switch(level)
            {
                case FellowOakDicom.Log.LogLevel.Debug:
                    _aspnetLogger.LogDebug(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Error:
                case FellowOakDicom.Log.LogLevel.Fatal:
                    _aspnetLogger.LogError(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Info:
                    _aspnetLogger.LogInformation(msg, args);
                    break;
                case FellowOakDicom.Log.LogLevel.Warning:
                    _aspnetLogger.LogWarning(msg, args);
                    break;
            }
        }

    }
}
