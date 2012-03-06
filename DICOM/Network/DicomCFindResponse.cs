using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCFindResponse : DicomResponse {
		public DicomCFindResponse(DicomDataset command) : base(command) {
		}

		public DicomCFindResponse(DicomCFindRequest request, DicomStatus status) : base(request, status) {
		}

		public int Remaining {
			get { return Command.Get<ushort>(DicomTag.NumberOfRemainingSuboperations, 0); }
			set { Command.Add(DicomTag.NumberOfRemainingSuboperations, (ushort)value); }
		}

		public int Completed {
			get { return Command.Get<ushort>(DicomTag.NumberOfCompletedSuboperations, 0); }
			set { Command.Add(DicomTag.NumberOfCompletedSuboperations, (ushort)value); }
		}

		public int Warnings {
			get { return Command.Get<ushort>(DicomTag.NumberOfWarningSuboperations, 0); }
			set { Command.Add(DicomTag.NumberOfWarningSuboperations, (ushort)value); }
		}

		public int Failures {
			get { return Command.Get<ushort>(DicomTag.NumberOfFailedSuboperations, 0); }
			set { Command.Add(DicomTag.NumberOfFailedSuboperations, (ushort)value); }
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
			if (Completed != 0)
				sb.AppendFormat("\n\t\tCompleted:	{0}", Completed);
			if (Remaining != 0)
				sb.AppendFormat("\n\t\tRemaining:	{0}", Remaining);
			if (Warnings != 0)
				sb.AppendFormat("\n\t\tWarnings:	{0}", Warnings);
			if (Failures != 0)
				sb.AppendFormat("\n\t\tFailures:	{0}", Failures);
			if (Status.State != DicomState.Pending && Status.State != DicomState.Success) {
				if (!String.IsNullOrEmpty(Status.ErrorComment))
					sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
			}
			return sb.ToString();
		}
	}
}
