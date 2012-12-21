using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNGetResponse : DicomResponse {
		public DicomNGetResponse(DicomDataset command) : base(command) {
		}

		public DicomNGetResponse(DicomNGetRequest request, DicomStatus status) : base(request, status) {
			SOPInstanceUID = request.SOPInstanceUID;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}
	}
}
