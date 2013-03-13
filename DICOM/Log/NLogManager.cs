using System;

namespace Dicom.Log {
	/// <summary>
	/// LogManager for the NLog logging framework.
	/// </summary>
	/// <example>
	/// LogManager.Default = new NLogManager();
	/// </example>
	public class NLogManager : LogManager {
		public override Logger GetLogger(string name) {
			return new NLogger(NLog.LogManager.GetLogger(name));
		}

		private class NLogger : Logger {
			private NLog.Logger _logger;

			public NLogger(NLog.Logger logger) {
				_logger = logger;
			}

			public override void Log(LogLevel level, string msg, params object[] args) {
				switch (level) {
				case LogLevel.Debug:
					_logger.Debug(msg, args);
					break;
				case LogLevel.Info:
					_logger.Info(msg, args);
					break;
				case LogLevel.Warning:
					_logger.Warn(msg, args);
					break;
				case LogLevel.Error:
					_logger.Error(msg, args);
					break;
				case LogLevel.Fatal:
					_logger.Fatal(msg, args);
					break;
				default:
					_logger.Info(msg, args);
					break;
				}
			}
		}
	}
}
