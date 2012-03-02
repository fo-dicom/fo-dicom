using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCFindRequest : DicomRequest {
		public DicomCFindRequest(DicomDataset command) : base(command) {
		}

		public DicomCFindRequest(DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CFindRequest, DicomUID.VerificationSOPClass, priority) {
		}

		public delegate void ResponseDelegate(DicomCFindRequest request, DicomCFindResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCFindResponse)response);
			} catch {
			}
		}
	}
}
