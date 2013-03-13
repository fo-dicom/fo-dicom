using System;

namespace Dicom.Log {
	public class ConsoleLogger : Logger {
		public readonly static Logger Instance = new ConsoleLogger();
		private object _lock = new object();

		private ConsoleLogger() {
		}

		public override void Log(LogLevel level, string msg, params object[] args) {
			lock (_lock) {
				var previous = Console.ForegroundColor;
				switch (level) {
				case LogLevel.Debug:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case LogLevel.Info:
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case LogLevel.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case LogLevel.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case LogLevel.Fatal:
					Console.ForegroundColor = ConsoleColor.Magenta;
					break;
				default:
					break;
				}
				Console.WriteLine(msg, args);
				Console.ForegroundColor = previous;
			}
		}
	}

	public class ConsoleLogManager : LogManager {
		public override Logger GetLogger(string name) {
			return ConsoleLogger.Instance;
		}
	}
}
