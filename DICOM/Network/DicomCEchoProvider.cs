using System;
using System.IO;

namespace Dicom.Network {
	public class DicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider {
		public DicomCEchoProvider(Stream stream) : base(stream) {
		}

		public void OnReceiveAssociationRequest(DicomAssociation association) {
			foreach (var pc in association.PresentationContexts) {
				if (pc.AbstractSyntax == DicomUID.Verification)
					pc.SetResult(DicomPresentationContextResult.Accept);
				else
					pc.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported);
			}
			SendAssociationAccept(association);
		}

		public void OnReceiveAssociationReleaseRequest() {
			SendAssociationReleaseResponse();
		}

		public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) {
		}

		public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request) {
			return new DicomCEchoResponse(request, DicomStatus.Success);
		}
	}
}
