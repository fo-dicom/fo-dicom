using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Log {
	public class VoidLogger : DicomLogger {
		public readonly static DicomLogger Instance = new VoidLogger();

		private VoidLogger() {
		}

		public override void Log(DicomLogLevel level, string message) {
		}

		public override void Log(DicomLogLevel level, string format, params object[] args) {
		}
	}
}
