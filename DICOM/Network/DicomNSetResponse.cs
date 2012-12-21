using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNSetResponse : DicomResponse {
		public DicomNSetResponse(DicomDataset command) : base(command) {
		}

		public DicomNSetResponse(DicomNSetRequest request, DicomStatus status) : base(request, status) {
			SOPInstanceUID = request.SOPInstanceUID;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}
	}
}
