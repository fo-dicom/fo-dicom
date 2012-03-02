using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCFindResponse : DicomResponse {
		public DicomCFindResponse(DicomDataset command) : base(command) {
		}

		public DicomCFindResponse(DicomCFindResponse request, DicomStatus status) : base(request, status) {
		}
	}
}
