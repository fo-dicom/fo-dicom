using System;
using System.Collections.Generic;

namespace Dicom.Network {
	public sealed class DicomAssociation {
		public DicomAssociation() {
			PresentationContexts = new DicomPresentationContextCollection();
		}

		public DicomAssociation(string callingAe, string calledAe, uint maxPduLength = 16384) : this() {
			CallingAE = callingAe;
			CalledAE = calledAe;
			MaximumPDULength = maxPduLength;
		}

		public string CallingAE {
			get;
			internal set;
		}

		public string CalledAE {
			get;
			internal set;
		}

		public DicomUID RemoteImplemetationClassUID {
			get;
			internal set;
		}

		public string RemoteImplementationVersion {
			get;
			internal set;
		}

		public uint MaximumPDULength {
			get;
			internal set;
		}

		public DicomPresentationContextCollection PresentationContexts {
			get;
			private set;
		}
	}
}
