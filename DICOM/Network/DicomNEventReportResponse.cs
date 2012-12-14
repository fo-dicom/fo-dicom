using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomNEventReportResponse : DicomResponse {
		public DicomNEventReportResponse(DicomDataset command) : base(command) {
		}

		public DicomNEventReportResponse(DicomNEventReportRequest request, DicomStatus status) : base(request, status) {
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}

		public ushort EventTypeID {
			get { return Command.Get<ushort>(DicomTag.EventTypeID); }
			private set { Command.Add(DicomTag.EventTypeID, value); }
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
			sb.AppendFormat("\n\t\tEvent Type:	{0:x4}", EventTypeID);
			if (Status.State != DicomState.Pending && Status.State != DicomState.Success) {
				if (!String.IsNullOrEmpty(Status.ErrorComment))
					sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
			}
			return sb.ToString();
		}
	}
}
