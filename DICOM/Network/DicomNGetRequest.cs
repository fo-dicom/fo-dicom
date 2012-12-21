using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNGetRequest : DicomRequest {
		public DicomNGetRequest(DicomDataset command) : base(command) {
		}

		public DicomNGetRequest(DicomUID requestedClassUid, DicomUID requestedInstanceUid, DicomTag[] attributes, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.NGetRequest, requestedClassUid, priority) {
			SOPInstanceUID = requestedInstanceUid;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID); }
			private set { Command.Add(DicomTag.RequestedSOPInstanceUID, value); }
		}

		public DicomTag[] Attributes {
			get { return Command.Get<DicomTag[]>(DicomTag.AttributeIdentifierList); }
			private set { Command.Add(DicomTag.AttributeIdentifierList, value); }
		}

		public delegate void ResponseDelegate(DicomNGetRequest request, DicomNGetResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomNGetResponse)response);
			} catch {
			}
		}
	}
}
