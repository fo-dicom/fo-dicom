using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Codec {
	public class DicomCodecException : DicomException {
		public DicomCodecException(string message) : base(message) {
		}

		public DicomCodecException(string format, params object[] args) : base(format, args) {
		}

		public DicomCodecException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
