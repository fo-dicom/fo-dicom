using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public class DicomFileException : DicomDataException {
		public DicomFileException(DicomFile file, string message) : base(message) {
			File = file;
		}

		public DicomFileException(DicomFile file, string format, params object[] args) : base(format, args) {
			File = file;
		}

		public DicomFileException(DicomFile file, string message, Exception innerException) : base(message, innerException) {
			File = file;
		}

		public DicomFile File {
			get;
			private set;
		}
	}
}
