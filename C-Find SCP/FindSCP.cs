using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom.Network;
using Dicom;
using System.IO;
using Dicom.Log;

namespace C_Find_SCP
{
    public class FindSCP : DicomService, IDicomServiceProvider, IDicomCFindProvider, IDicomCEchoProvider
    {
        private static DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[] {
				DicomTransferSyntax.ExplicitVRLittleEndian,
				DicomTransferSyntax.ExplicitVRBigEndian,
				DicomTransferSyntax.ImplicitVRLittleEndian
			};

        private static DicomTransferSyntax[] AcceptedImageTransferSyntaxes = new DicomTransferSyntax[] {
				// Lossless
				DicomTransferSyntax.JPEGLSLossless,
				DicomTransferSyntax.JPEG2000Lossless,
				DicomTransferSyntax.JPEGProcess14SV1,
				DicomTransferSyntax.JPEGProcess14,
				DicomTransferSyntax.RLELossless,
			
				// Lossy
				DicomTransferSyntax.JPEGLSNearLossless,
				DicomTransferSyntax.JPEG2000Lossy,
				DicomTransferSyntax.JPEGProcess1,
				DicomTransferSyntax.JPEGProcess2_4,

				// Uncompressed
				DicomTransferSyntax.ExplicitVRLittleEndian,
				DicomTransferSyntax.ExplicitVRBigEndian,
				DicomTransferSyntax.ImplicitVRLittleEndian
			};

        public FindSCP(Stream stream, Logger log)
            : base(stream, log)
        {
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            if (association.CalledAE != "FINDSCP")
            {
                SendAssociationReject(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser, DicomRejectReason.CalledAENotRecognized);
                return;
            }

            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification)
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None)
                    pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);
                if (pc.AbstractSyntax == DicomUID.StudyRootQueryRetrieveInformationModelFIND ||
                    pc.AbstractSyntax == DicomUID.PatientRootQueryRetrieveInformationModelFIND)
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
            }

            SendAssociationAccept(association);
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public void OnConnectionClosed(int errorCode)
        {
        }

        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            IList<DicomCFindResponse> responses=new List<DicomCFindResponse>();
            //固定返回C-FIND-RSP给客户端
            for (int i = 0; i < 2; ++i)
            {
                DicomCFindResponse response = new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = request.Dataset };
                response.Completed = i+100;
                responses.Add(response);
            }
            DicomCFindResponse rsp = new DicomCFindResponse(request, DicomStatus.Success) { Dataset = request.Dataset };
            rsp.Completed = 2234;
            responses.Add(rsp);
            return responses;
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
