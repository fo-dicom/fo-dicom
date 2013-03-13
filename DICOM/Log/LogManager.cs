using System;

namespace Dicom.Log {
	public abstract class LogManager {
		static LogManager() {
			Default = new ConsoleLogManager();
		}

		public static LogManager Default {
			get;
			set;
		}

		public abstract Logger GetLogger(string name);
	}
}
