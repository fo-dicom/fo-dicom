using System;
using System.Collections.Generic;
using System.Linq;

using NLog;

namespace Dicom.Log {
	public static class Extensions {
		public static void WriteToLog(this IEnumerable<DicomItem> dataset, Logger log, LogLevel level) {
			var logger = new DicomDatasetLogger(log, level);
			new DicomDatasetWalker(dataset).Walk(logger);
		}

		public static void WriteToLog(this DicomFile file, Logger log, LogLevel level) {
			var logger = new DicomDatasetLogger(log, level);
			new DicomDatasetWalker(file.FileMetaInfo).Walk(logger);
			new DicomDatasetWalker(file.Dataset).Walk(logger);
		}
	}
}
