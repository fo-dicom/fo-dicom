using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNEventReportRequest : DicomRequest {
		public DicomNEventReportRequest(DicomDataset command) : base(command) {
		}

		public DicomNEventReportRequest(DicomUID affectedClassUid, DicomUID affectedInstanceUid, ushort eventTypeId, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.NEventReportRequest, affectedClassUid, priority) {
			SOPInstanceUID = affectedInstanceUid;
			EventTypeID = eventTypeId;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}

		public ushort EventTypeID {
			get { return Command.Get<ushort>(DicomTag.EventTypeID); }
			private set { Command.Add(DicomTag.EventTypeID, value); }
		}

		public delegate void ResponseDelegate(DicomNEventReportRequest request, DicomNEventReportResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomNEventReportResponse)response);
			} catch {
			}
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} [{1}]", ToString(Type), MessageID);
			sb.AppendFormat("\n\t\tEvent Type:	{0:x4}", EventTypeID);
			return sb.ToString();
		}
	}
}
