using System;

namespace Dicom.Network {
	public class DicomAssociationAbortedException : DicomNetworkException {
		public DicomAssociationAbortedException(DicomAbortSource source, DicomAbortReason reason) : base("Association Abort [source: {0}; reason: {1}]", source, reason) {
			Source = source;
			Reason = reason;
		}

		public DicomAbortSource Source {
			get;
			private set;
		}

		public DicomAbortReason Reason {
			get;
			private set;
		}
	}
}
