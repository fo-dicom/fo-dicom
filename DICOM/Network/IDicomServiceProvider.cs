using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public interface IDicomServiceProvider {
		void OnReceiveAssociationRequest(DicomAssociation association);
		void OnReceiveAssociationReleaseRequest();
		void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);
		void OnConnectionClosed(int errorCode);
	}
}
