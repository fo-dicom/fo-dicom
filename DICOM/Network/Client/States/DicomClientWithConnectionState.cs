using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    public abstract class DicomClientWithConnectionState : IDicomClientState
    {
        public IDicomClientConnection Connection { get; }

        protected DicomClientWithConnectionState(IInitialisationWithConnectionParameters parameters)
        {
            Connection = parameters.Connection ?? throw new ArgumentNullException(nameof(IInitialisationWithConnectionParameters.Connection));
        }

        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        public abstract Task OnReceiveAssociationAccept(DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        public abstract Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        public abstract Task OnReceiveAssociationReleaseResponse();

        /// <summary>
        /// Callback on receiving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        public abstract Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        public abstract Task OnConnectionClosed(Exception exception);

        /// <summary>
        /// Callback when there are no new requests to send and no existing requests in process (waiting for reply)
        /// </summary>
        /// <returns>Awaitable task</returns>
        public abstract Task OnSendQueueEmpty();

        public abstract Task OnEnter(CancellationToken cancellationToken);

        public abstract Task OnExit(CancellationToken cancellationToken);

        public abstract void AddRequest(DicomRequest dicomRequest);

        public abstract Task SendAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
