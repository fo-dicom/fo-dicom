using System;

namespace Dicom.Log {
	public class ConsoleLogger : DicomLogger {
		public readonly static DicomLogger Instance = new ConsoleLogger();
		private readonly static ConsoleColor[] Colors;

		static ConsoleLogger() {
			Instance = new ConsoleLogger();

			Colors = new ConsoleColor[4];
			Colors[(int)DicomLogLevel.Debug] = ConsoleColor.Gray;
			Colors[(int)DicomLogLevel.Info] = ConsoleColor.White;
			Colors[(int)DicomLogLevel.Warning] = ConsoleColor.Yellow;
			Colors[(int)DicomLogLevel.Error] = ConsoleColor.Red;
		}

		private ConsoleLogger() {
		}

		public override void Log(DicomLogLevel level, string message) {
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = Colors[(int)level];
			Console.WriteLine(message);
			Console.ForegroundColor = color;
		}

		public override void Log(DicomLogLevel level, string format, params object[] args) {
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = Colors[(int)level];
			Console.WriteLine(format, args);
			Console.ForegroundColor = color;
		}
	}
}
