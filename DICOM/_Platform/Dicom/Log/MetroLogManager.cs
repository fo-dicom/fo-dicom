// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using MetroLog;

namespace Dicom.Log {

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
