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
			AffectedSOPClassUID = request.AffectedSOPClassUID;
			RequestMessageID = request.MessageID;
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
	}
}
