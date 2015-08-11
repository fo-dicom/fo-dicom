// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    public interface IDicomServiceProvider
    {
        void OnReceiveAssociationRequest(DicomAssociation association);

        void OnReceiveAssociationReleaseRequest();

        void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        void OnConnectionClosed(Exception exception);
    }
}
