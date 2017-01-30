// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    /// <summary>
    /// Common interface for DICOM service users and providers.
    /// </summary>
    public interface IDicomService
    {
        /// <summary>
        /// Callback on recieving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        void OnConnectionClosed(Exception exception);
    }

    /// <summary>
    /// Interface for DICOM service providers.
    /// </summary>
    public interface IDicomServiceProvider : IDicomService
    {
        /// <summary>
        /// Callback to invoke when receiving an association request.
        /// </summary>
        /// <param name="association">DICOM association corresponding to the request.</param>
        void OnReceiveAssociationRequest(DicomAssociation association);

        /// <summary>
        /// Callback to invoke when receiving an association release request.
        /// </summary>
        void OnReceiveAssociationReleaseRequest();
    }
}
