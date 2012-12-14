using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomResponse : DicomMessage {
		public DicomResponse(DicomDataset command) : base(command) {
		}

		public DicomResponse(DicomMessage request, DicomStatus status) : base() {
			Type = (DicomCommandField)(0x8000 | (int)request.Type);
			SOPClassUID = request.SOPClassUID;
			RequestMessageID = request.MessageID;
			Status = status;
		}

		public ushort RequestMessageID {
			get { return Command.Get<ushort>(DicomTag.MessageIDBeingRespondedTo); }
			set { Command.Add(DicomTag.MessageIDBeingRespondedTo, value); }
		}

		public DicomStatus Status {
			get {
				var status = DicomStatus.Lookup(Command.Get<ushort>(DicomTag.Status));
				var comment = Command.Get<string>(DicomTag.ErrorComment, null);
				if (comment != null)
					return new DicomStatus(status, comment);
				return status;
			}
			set {
				Command.Add(DicomTag.Status, value.Code);
				Command.Add(DicomTag.ErrorComment, value.ErrorComment);
			}
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
			if (Status.State != DicomState.Pending && Status.State != DicomState.Success) {
				if (!String.IsNullOrEmpty(Status.ErrorComment))
					sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
				if (Command.Contains(DicomTag.OffendingElement)) {
					string[] tags = Command.Get<string[]>(DicomTag.OffendingElement);
					if (tags.Length > 0) {
						sb.Append("\n\t\tTags:		");
						foreach (var tag in tags)
							sb.AppendFormat(" {0}", tag);
					}
				}
			}
			return sb.ToString();
		}
	}
}
