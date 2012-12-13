using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNDeleteRequest : DicomRequest {
		public DicomNDeleteRequest(DicomDataset command) : base(command) {
		}

		public DicomNDeleteRequest(DicomUID requestedClassUid, DicomUID requestedInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.NDeleteRequest, requestedClassUid, priority) {
			SOPInstanceUID = requestedInstanceUid;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID); }
			private set { Command.Add(DicomTag.RequestedSOPInstanceUID, value); }
		}

		public delegate void ResponseDelegate(DicomNDeleteRequest request, DicomNDeleteResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomNDeleteResponse)response);
			} catch {
			}
		}
	}
}
