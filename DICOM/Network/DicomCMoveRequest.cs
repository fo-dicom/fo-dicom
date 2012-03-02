using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCMoveRequest : DicomRequest {
		public DicomCMoveRequest(DicomDataset command) : base(command) {
		}

		public DicomCMoveRequest(DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.VerificationSOPClass, priority) {
		}

		public delegate void ResponseDelegate(DicomCMoveRequest request, DicomCMoveResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCMoveResponse)response);
			} catch {
			}
		}
	}
}
