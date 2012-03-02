using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNetworkException : DicomException {
		public DicomNetworkException(string message) : base(message) {
		}

		public DicomNetworkException(string format, params object[] args) : base(format, args) {
		}

		public DicomNetworkException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
