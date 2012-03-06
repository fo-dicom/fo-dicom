using System;

namespace Dicom.Network {
	public class DicomAssociationAbortedException : DicomNetworkException {
		public DicomAssociationAbortedException(DicomAbortSource source, DicomAbortReason reason) : base("Association Abort [source: {0}; reason: {1}]", source, reason) {
			AbortSource = source;
			AbortReason = reason;
		}

		public DicomAbortSource AbortSource {
			get;
			private set;
		}

		public DicomAbortReason AbortReason {
			get;
			private set;
		}
	}
}
