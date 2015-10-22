// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    public interface IDicomServiceUser
    {
        void OnReceiveAssociationAccept(DicomAssociation association);

        void OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        void OnReceiveAssociationReleaseResponse();

        void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        void OnConnectionClosed(Exception exception);
    }
}
