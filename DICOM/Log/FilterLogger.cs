using System;

namespace Dicom.Log {
	public class FilterLogger : DicomLogger {
		private DicomLogLevel _minLevel;
		private DicomLogger _logger;

		public FilterLogger(DicomLogLevel minimumLogLevel, DicomLogger logger) {
			_minLevel = minimumLogLevel;
			_logger = logger;
		}

		public override void Log(DicomLogLevel level, string message) {
			if (level >= _minLevel)
				_logger.Log(level, message);
		}

		public override void Log(DicomLogLevel level, string format, params object[] args) {
			if (level >= _minLevel)
				_logger.Log(level, format, args);
		}
	}
}
