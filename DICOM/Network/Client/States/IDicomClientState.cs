using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    public interface IDicomClientState
    {
        /// <summary>
        /// Is called when entering this state
        /// </summary>
        Task OnEnter(CancellationToken cancellationToken);

        /// <summary>
        /// Is called when entering this state
        /// </summary>
        Task OnExit(CancellationToken cancellationToken);

        /// <summary>
        /// Enqueues a new DICOM request for execution.
        /// </summary>
        /// <param name="dicomRequest">The DICOM request to send</param>
        void AddRequest(DicomRequest dicomRequest);

        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        Task SendAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        Task OnReceiveAssociationAccept(DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        Task OnReceiveAssociationReleaseResponse();

        /// <summary>
        /// Callback on receiving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        Task OnConnectionClosed(Exception exception);

        /// <summary>
        /// Callback when there are no new requests to send and no existing requests in process (waiting for reply)
        /// </summary>
        /// <returns>Awaitable task</returns>
        Task OnSendQueueEmpty();
    }
}
