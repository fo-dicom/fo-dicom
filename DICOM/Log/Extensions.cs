using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public static void WriteToConsole(this IEnumerable<DicomItem> dataset) {
			var log = new StringBuilder();
			var dumper = new DicomDatasetDumper(log, 80, 60);
			new DicomDatasetWalker(dataset).Walk(dumper);
			Console.WriteLine(log);
		}

		public static void WriteToConsole(this DicomFile file) {
			var log = new StringBuilder();
			var dumper = new DicomDatasetDumper(log, 80, 60);
			new DicomDatasetWalker(file.FileMetaInfo).Walk(dumper);
			new DicomDatasetWalker(file.Dataset).Walk(dumper);
			Console.WriteLine(log);
		}

		public static string WriteToString(this IEnumerable<DicomItem> dataset) {
			var log = new StringBuilder();
			var dumper = new DicomDatasetDumper(log);
			new DicomDatasetWalker(dataset).Walk(dumper);
			return log.ToString();
		}

		public static string WriteToString(this DicomFile file) {
			var log = new StringBuilder();
			var dumper = new DicomDatasetDumper(log);
			new DicomDatasetWalker(file.FileMetaInfo).Walk(dumper);
			new DicomDatasetWalker(file.Dataset).Walk(dumper);
			return log.ToString();
		}
	}
}
