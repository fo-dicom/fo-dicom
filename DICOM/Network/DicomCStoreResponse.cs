using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCStoreResponse : DicomResponse {
		public DicomCStoreResponse(DicomDataset command) : base(command) {
		}

		public DicomCStoreResponse(DicomCStoreRequest request, DicomStatus status) : base(request, status) {
		}
	}
}
