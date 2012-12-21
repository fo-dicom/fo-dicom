using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNCreateRequest : DicomRequest {
		public DicomNCreateRequest(DicomDataset command) : base(command) {
		}

		public DicomNCreateRequest(DicomUID affectedClassUid, DicomUID affectedInstanceUid, ushort eventTypeId, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.NCreateRequest, affectedClassUid, priority) {
			SOPInstanceUID = affectedInstanceUid;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}

		public delegate void ResponseDelegate(DicomNCreateRequest request, DicomNCreateResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomNCreateResponse)response);
			} catch {
			}
		}
	}
}
