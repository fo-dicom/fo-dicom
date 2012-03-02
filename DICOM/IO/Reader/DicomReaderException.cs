using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Reader {
	public class DicomReaderException : DicomException {
		public DicomReaderException(string message) : base(message) {
		}

		public DicomReaderException(string format, params object[] args) : base(format, args) {
		}

		public DicomReaderException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
