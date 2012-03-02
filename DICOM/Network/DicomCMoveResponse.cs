using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCMoveResponse : DicomResponse {
		public DicomCMoveResponse(DicomDataset command) : base(command) {
		}

		public DicomCMoveResponse(DicomCMoveResponse request, DicomStatus status) : base(request, status) {
		}
	}
}
