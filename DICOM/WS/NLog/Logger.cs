using System;
using MetroLog;

// ReSharper disable CheckNamespace
namespace NLog
// ReSharper restore CheckNamespace
{
    public class Logger : ILogger
    {
        private readonly ILogger _logger;

        internal Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void Trace(string message, Exception ex = null)
        {
            _logger.Trace(message, ex);
        }

        public void Trace(string message, params object[] ps)
        {
            _logger.Trace(message, ps);
        }

        public void Debug(string message, Exception ex = null)
        {
            _logger.Debug(message, ex);
        }

        public void Debug(string message, params object[] ps)
        {
            _logger.Debug(message, ps);
        }

        public void Info(string message, Exception ex = null)
        {
            _logger.Info(message, ex);
        }

        public void Info(string message, params object[] ps)
        {
            _logger.Info(message, ps);
        }

        public void Warn(string message, Exception ex = null)
        {
            _logger.Warn(message, ex);
        }

        public void Warn(string message, params object[] ps)
        {
            _logger.Warn(message, ps);
        }

        public void Error(string message, Exception ex = null)
        {
            _logger.Error(message, ex);
        }

        public void Error(string message, params object[] ps)
        {
            _logger.Error(message, ps);
        }

        public void Fatal(string message, Exception ex = null)
        {
            _logger.Fatal(message, ex);
        }

        public void Fatal(string message, params object[] ps)
        {
            _logger.Fatal(message, ps);
        }

        public void Log(MetroLog.LogLevel logLevel, string message, Exception ex)
        {
            _logger.Log(logLevel, message, ex);
        }

        public void Log(MetroLog.LogLevel logLevel, string message, params object[] ps)
        {
            _logger.Log(logLevel, message, ps);
        }

        public bool IsEnabled(MetroLog.LogLevel level)
        {
            return _logger.IsEnabled(level);
        }

        public string Name
        {
            get { return _logger.Name; }
        }

        public bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        internal void Log(LogLevel logLevel, string message, Exception ex)
        {
            _logger.Log(logLevel.ToMetroLogLevel(), message, ex);
        }

        internal void Log(LogLevel logLevel, string message, params object[] ps)
        {
            _logger.Log(logLevel.ToMetroLogLevel(), message, ps);
        }
    }
}