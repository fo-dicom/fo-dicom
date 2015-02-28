// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Log {
	public class ConsoleLogger : Logger {
		public readonly static Logger Instance = new ConsoleLogger();
		private readonly object _lock = new object();

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
