using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Log;

namespace Dicom.Network
{
    public interface IDicomClient
    {
        /// <summary>
        /// Gets or sets the logger that will be used by this DicomClient
        /// </summary>
        Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        DicomServiceOptions Options { get; set; }

        /// <summary>
        /// Gets or sets additional presentation contexts to negotiate with association.
        /// </summary>
        List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        /// <summary>
        /// Gets or sets the fallback encoding.
        /// </summary>
        Encoding FallbackEncoding { get; set; }

        /// <summary>
        /// Gets the state of this DICOM client
        /// </summary>
        IDicomClientState State { get; }

        /// <summary>
        /// Enqueues a new DICOM request for execution.
        /// </summary>
        /// <param name="dicomRequest">The DICOM request to send</param>
        void AddRequest(DicomRequest dicomRequest);

        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe, int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs);
    }

    public class DicomClient : IDicomClient
    {
        private readonly object _lock = new object();
        private readonly ConcurrentQueue<StrongBox<DicomRequest>> _requestsToSend;

        internal IDicomClientState State { get; private set; }

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        public DicomClient()
        {
            _requestsToSend = new ConcurrentQueue<StrongBox<DicomRequest>>();
        }

        internal async Task Transition(IDicomClientState newState)
        {
            State = newState ?? throw new ArgumentNullException(nameof(newState));
            await State.Initialize();
        }

        public Logger Logger { get; set; }

        public DicomServiceOptions Options{ get; set; }

        public List<DicomPresentationContext> AdditionalPresentationContexts{ get; set; }

        public Encoding FallbackEncoding { get; set; }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _requestsToSend.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe, int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            return State.SendAsync(host, port, useTls, callingAe, calledAe, millisecondsTimeout);
        }
    }

    public static class DicomClientDefaults
    {
        public const int DefaultLingerInMs = 50;

        public const int DefaultAssociationRequestTimeoutInMs = 5000;

        public const int DefaultAssociationReleaseTimeoutInMs = 10000;
    }

    public interface IDicomClientState
    {
        /// <summary>
        /// Runs the necessary initialization tasks for the current state
        /// </summary>
        /// <returns></returns>
        Task Initialize(CancellationToken cancellationToken);

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

        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe, int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs);

    }

    public class DicomClientIdleState : IDicomClientState
    {
        private DicomClient DicomClient { get; }

        public DicomClientIdleState(DicomClient dicomClient)
        {
            DicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        public Task Initialize(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            // Ignore
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            // Ignore
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            // Ignore
            return Task.FromResult(0);
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // Ignore
            return Task.FromResult(0);
        }

        public Task OnConnectionClosed(Exception exception)
        {
            // Ignore
            return Task.FromResult(0);
        }

        public Task OnSendQueueEmpty()
        {
            // Ignore
            return Task.FromResult(0);
        }

        public async Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe, int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            var noDelay = DicomClient.Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = DicomClient.Options?.IgnoreSslPolicyErrors ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;
            var networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

            var associationToRequest = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = 1,
                MaxAsyncOpsPerformed = 1,
                RemoteHost = host,
                RemotePort = port
            };

            var dicomClientConnection = new DicomClientConnection(DicomClient, networkStream);

            var dicomClientConnectionListenerTask = dicomClientConnection.RunAsync();

            var assocationRequestingState = new DicomClientAssociationRequestingState(DicomClient, dicomClientConnection, dicomClientConnectionListenerTask, associationToRequest, millisecondsTimeout);

            await DicomClient.Transition(assocationRequestingState);
        }
    }

    public class DicomClientAssociationRequestingState : IDicomClientState
    {
        private readonly IDicomClient _dicomClient;
        private readonly IDicomClientConnection _dicomClientConnection;
        private readonly Task _dicomClientConnectionListenerTask;
        private readonly DicomAssociation _dicomAssociationToRequest;
        private readonly int _associationRequestTimeoutInMs;

        public DicomClientAssociationRequestingState(IDicomClient dicomClient,
            IDicomClientConnection dicomClientConnection,
            Task dicomClientConnectionListenerTask,
            DicomAssociation dicomAssociationToRequest,
            int associationRequestTimeoutInMs)
        {
            if (associationRequestTimeoutInMs <= 0) throw new ArgumentOutOfRangeException(nameof(associationRequestTimeoutInMs));
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _dicomClientConnection = dicomClientConnection ?? throw new ArgumentNullException(nameof(dicomClientConnection));
            _dicomClientConnectionListenerTask = dicomClientConnectionListenerTask;
            _dicomAssociationToRequest = dicomAssociationToRequest ?? throw new ArgumentNullException(nameof(dicomAssociationToRequest));
            _associationRequestTimeoutInMs = associationRequestTimeoutInMs;
        }

        public async Task Initialize(CancellationToken cancellationToken)
        {
            var timeout = Task.Delay(_associationRequestTimeoutInMs, cancellationToken);
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            throw new NotImplementedException();
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            throw new NotImplementedException();
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            throw new NotImplementedException();
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            throw new NotImplementedException();
        }

        public Task OnConnectionClosed(Exception exception)
        {
            throw new NotImplementedException();
        }

        public Task OnSendQueueEmpty()
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            // TODO wait for everything to complete, then start new association with these parameters
            throw new NotImplementedException();
        }
    }

    public interface IDicomClientConnection
    {
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
    }

    public class DicomClientConnection : DicomService, IDicomClientConnection
    {
        private IDicomClient DicomClient { get; }
        private INetworkStream NetworkStream { get; }

        public DicomClientConnection(IDicomClient dicomClient, INetworkStream networkStream)
            : base(networkStream, dicomClient.FallbackEncoding, dicomClient.Logger)
        {
            DicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            NetworkStream = networkStream;
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
