using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    public interface IDicomClientConnection
    {
        /// <summary>
        /// Gets the network stream of this connection
        /// </summary>
        INetworkStream NetworkStream { get; }

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
        /// Send request from service.
        /// </summary>
        /// <param name="request">Request to send.</param>
        Task SendRequestAsync(DicomRequest request);

        /// <summary>
        /// Send association request.
        /// </summary>
        /// <param name="association">DICOM association.</param>
        Task SendAssociationRequestAsync(DicomAssociation association);

        /// <summary>
        /// Send association release request.
        /// </summary>
        Task SendAssociationReleaseRequestAsync();
    }

    public class DicomClientConnection : DicomService, IDicomClientConnection
    {
        private IDicomClient DicomClient { get; }
        public INetworkStream NetworkStream { get; }

        public DicomClientConnection(IDicomClient dicomClient, INetworkStream networkStream)
            : base(networkStream, dicomClient.FallbackEncoding, dicomClient.Logger)
        {
            DicomClient = dicomClient;
            NetworkStream = networkStream;
        }

        public new Task SendAssociationRequestAsync(DicomAssociation association)
        {
            return base.SendAssociationRequestAsync(association);
        }

        public new Task SendAssociationReleaseRequestAsync()
        {
            return base.SendAssociationReleaseRequestAsync();
        }

        protected override async Task OnSendQueueEmptyAsync()
        {
            await base.OnSendQueueEmptyAsync();
            await DicomClient.State.OnSendQueueEmpty();
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            return DicomClient.State.OnReceiveAssociationAccept(association);
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return DicomClient.State.OnReceiveAssociationReject(result, source, reason);
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            return DicomClient.State.OnReceiveAssociationReleaseResponse();
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            return DicomClient.State.OnReceiveAbort(source, reason);
        }

        public Task OnConnectionClosed(Exception exception)
        {
            return DicomClient.State.OnConnectionClosed(exception);
        }
    }
}
