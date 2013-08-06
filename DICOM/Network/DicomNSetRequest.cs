using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNSetRequest : DicomRequest {
		public DicomNSetRequest(DicomDataset command) : base(command) {
		}

		public DicomNSetRequest(DicomUID requestedClassUid, DicomUID requestedInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.NSetRequest, requestedClassUid, priority) {
			SOPInstanceUID = requestedInstanceUid;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID); }
			private set { Command.Add(DicomTag.RequestedSOPInstanceUID, value); }
		}

		public delegate void ResponseDelegate(DicomNSetRequest request, DicomNSetResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomNSetResponse)response);
			} catch {
			}
		}
	}
}
