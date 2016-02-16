// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;

    using Dicom.Log;

    public class DicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        public DicomCEchoProvider(INetworkStream stream, Logger log)
            : base(stream, log)
        {
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification) pc.SetResult(DicomPresentationContextResult.Accept);
                else pc.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported);
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

        public void OnConnectionClosed(Exception exception)
        {
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
