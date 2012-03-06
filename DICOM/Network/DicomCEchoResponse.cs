using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCEchoResponse : DicomResponse {
		public DicomCEchoResponse(DicomDataset command) : base(command) {
		}

		public DicomCEchoResponse(DicomCEchoRequest request, DicomStatus status) : base(request, status) {
		}
	}
}
