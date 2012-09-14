using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public interface IDicomServiceUser {
		void OnReceiveAssociationAccept(DicomAssociation association);
		void OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);
		void OnReceiveAssociationReleaseResponse();
		void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);
		void OnConnectionClosed(int errorCode);
	}
}
