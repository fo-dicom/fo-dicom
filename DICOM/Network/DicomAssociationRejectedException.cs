using System;

namespace Dicom.Network {
	public class DicomAssociationRejectedException : DicomNetworkException {
		public DicomAssociationRejectedException(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) : base("Association rejected [result: {0}; source: {1}; reason: {2}]", result, source, reason) {
			RejectResult = result;
			RejectSource = source;
			RejectReason = reason;
		}

		public DicomRejectResult RejectResult {
			get;
			private set;
		}

		public DicomRejectSource RejectSource {
			get;
			private set;
		}

		public DicomRejectReason RejectReason {
			get;
			private set;
		}
	}
}
