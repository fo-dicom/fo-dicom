// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface for implementations of a DICOM service as a client.
    /// </summary>
    [Obsolete("This is the synchronous API employed by the old DICOM client. Use IDicomClientConnection instead.")]
    public interface IDicomServiceUser : IDicomService
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
