using MetroLog;

// ReSharper disable CheckNamespace
namespace Dicom.Log {
// ReSharper restore CheckNamespace

	/// <summary>
	/// LogManager for the MetroLog logging framework.
	/// </summary>
	/// <example>
	/// LogManager.Default = new MetroLogManager();
	/// </example>
	public class MetroLogManager : LogManager {
		public override Logger GetLogger(string name) {
			return new MetroLogger(LogManagerFactory.DefaultLogManager.GetLogger(name));
		}

		private class MetroLogger : Logger {
			private readonly ILogger _logger;

			public MetroLogger(ILogger logger) {
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
