// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;

    /// <summary>
    /// Interface for implementations of a DICOM service as a client.
    /// </summary>
    public interface IDicomServiceUser
    {
        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        void OnReceiveAssociationAccept(DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        void OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        void OnReceiveAssociationReleaseResponse();

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

        /// <summary>
        /// Callback for handling a client related C-STORE request, typically emanating from the client's C-GET request.
        /// </summary>
        /// <param name="request">
        /// C-STORE request.
        /// </param>
        /// <returns>
        /// The <see cref="DicomCStoreResponse"/> related to the C-STORE <paramref name="request"/>.
        /// </returns>
        DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request);
    }
}
