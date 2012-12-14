using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNDeleteResponse : DicomResponse {
		public DicomNDeleteResponse(DicomDataset command) : base(command) {
		}

		public DicomNDeleteResponse(DicomNDeleteRequest request, DicomStatus status) : base(request, status) {
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}
	}
}
