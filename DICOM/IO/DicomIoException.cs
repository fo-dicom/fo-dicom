using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO {
	public class DicomIoException : DicomException {
		public DicomIoException(string message) : base(message) {
		}

		public DicomIoException(string format, params object[] args) : base(format, args) {
		}

		public DicomIoException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
