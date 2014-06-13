using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomStructuredReportException : DicomException {
		public DicomStructuredReportException(string message) : base(message) {
		}

		public DicomStructuredReportException(string format, params object[] args) : base(format, args) {
		}

		public DicomStructuredReportException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
