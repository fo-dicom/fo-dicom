// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.States
{

    public interface IDicomClientState : IDisposable
    {
        /// <summary>
        /// Is called when entering this state, and is used to move forward in the state transition flow
        /// </summary>
        Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation);

        /// <summary>
        /// Enqueues a new DICOM request for execution.
        /// </summary>
        /// <param name="dicomRequest">The DICOM request to send</param>
        Task AddRequestAsync(DicomRequest dicomRequest);

        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        Task SendAsync(DicomClientCancellation cancellation);

        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        Task OnReceiveAssociationAcceptAsync(DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        Task OnReceiveAssociationReleaseResponseAsync();

        /// <summary>
        /// Callback on receiving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        Task OnConnectionClosedAsync(Exception exception);

        /// <summary>
        /// Callback when there are no new requests to send and no existing requests in process (waiting for reply)
        /// </summary>
        /// <returns>Awaitable task</returns>
        Task OnSendQueueEmptyAsync();

        /// <summary>
        /// Callback when a request has been completed (a final response was received, causing it to be removed from the pending queue)
        /// </summary>
        /// <param name="request">The original request that was sent, which has now been fulfilled.</param>
        /// <param name="response">The final response from the DICOM server</param>
        Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response);

        /// <summary>
        /// Callback when a request has timed out (no final response was received, but the timeout was exceeded and the request has been removed from the pending queue)
        /// </summary>
        /// <param name="request">The original request that was sent, which could not be fulfilled</param>
        /// <param name="timeout">The timeout duration that has been exceeded</param>
        Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout);
    }
}
