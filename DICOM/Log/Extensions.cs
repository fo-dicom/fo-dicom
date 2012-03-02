using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.Log {
	public static class Extensions {
		public static void WriteToLog(this IEnumerable<DicomItem> dataset, DicomLogger log, DicomLogLevel level = DicomLogLevel.Info) {
			var logger = new DicomDatasetLogger(log, level);
			new DicomDatasetWalker(dataset).Walk(logger);
		}

		public static void WriteToLog(this DicomFile file, DicomLogger log, DicomLogLevel level = DicomLogLevel.Info) {
			var logger = new DicomDatasetLogger(log, level);
			new DicomDatasetWalker(file.FileMetaInfo).Walk(logger);
			new DicomDatasetWalker(file.Dataset).Walk(logger);
		}
	}
}
