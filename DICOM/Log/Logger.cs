using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Log {
	public abstract class Logger {
		public abstract void Log(LogLevel level, string msg, params object[] args);

		public void Debug(string msg, params object[] args) {
			Log(LogLevel.Debug, msg, args);
		}

		public void Info(string msg, params object[] args) {
			Log(LogLevel.Info, msg, args);
		}

		public void Warn(string msg, params object[] args) {
			Log(LogLevel.Warning, msg, args);
		}

		public void Error(string msg, params object[] args) {
			Log(LogLevel.Error, msg, args);
		}

		public void Fatal(string msg, params object[] args) {
			Log(LogLevel.Fatal, msg, args);
		}
	}
}
