using System;

namespace Dicom.Network {
	public class DicomAssociationRejectedException : DicomNetworkException {
		public DicomAssociationRejectedException(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) : base("Association rejected [result: {0}; source: {1}; reason: {2}]", result, source, reason) {
			Result = result;
			Source = source;
			Reason = reason;
		}

		public DicomRejectResult Result {
			get;
			private set;
		}

		public DicomRejectSource Source {
			get;
			private set;
		}

		public DicomRejectReason Reason {
			get;
			private set;
		}
	}
}
