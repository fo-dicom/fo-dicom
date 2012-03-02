using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public class DicomDataException : DicomException {
		public DicomDataException(string message) : base(message) {
		}

		public DicomDataException(string format, params object[] args) : base(format, args) {
		}

		public DicomDataException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
