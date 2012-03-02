using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging {
	public class DicomImagingException : DicomException {
		public DicomImagingException(string message) : base(message) {
		}

		public DicomImagingException(string format, params object[] args) : base(format, args) {
		}

		public DicomImagingException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
