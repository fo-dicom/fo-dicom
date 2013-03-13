// ReSharper disable CheckNamespace
namespace Dicom.Log {
// ReSharper restore CheckNamespace
	public class ConsoleLogger : Logger {
		public readonly static Logger Instance = new ConsoleLogger();
		private object _lock = new object();

		private ConsoleLogger() {
		}

		public override void Log(LogLevel level, string msg, params object[] args) {
			lock (_lock) {
				System.Diagnostics.Debug.WriteLine(level.ToString().ToUpperInvariant() + " " + msg, args);
			}
		}
	}

	public class ConsoleLogManager : LogManager {
		public override Logger GetLogger(string name) {
			return ConsoleLogger.Instance;
		}
	}
}
