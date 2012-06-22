using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCEchoRequest : DicomRequest {
		public DicomCEchoRequest(DicomDataset command) : base(command) {
		}

		public DicomCEchoRequest(DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CEchoRequest, DicomUID.Verification, priority) {
		}

		public delegate void ResponseDelegate(DicomCEchoRequest request, DicomCEchoResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCEchoResponse)response);
			} catch {
			}
		}
	}
}
