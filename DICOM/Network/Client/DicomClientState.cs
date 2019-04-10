using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    public interface IDicomClientState
    {
        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);
    }

    public interface IDicomClientState<in TTransitionParameters> : IDicomClientState
    {
        /// <summary>
        /// Runs the necessary initialization tasks for the current state
        /// </summary>
        /// <returns></returns>
        Task OnTransition(TTransitionParameters parameters, CancellationToken cancellationToken);
    }

    public abstract class DicomClientWithConnectionState<TTransitionParameters> : IDicomClientState<TTransitionParameters>
    {
        /// <summary>
        /// Gets the connection between the client and the server
        /// </summary>
        public IDicomClientConnection Connection { get; }

        /// <summary>
        /// Gets the long-running task that is listening for incoming DICOM communication from the server
        /// </summary>
        public Task ListenerTask { get; }

        protected DicomClientWithConnectionState(IDicomClientConnection connection, Task listenerTask)
        {
            Connection = connection;
            ListenerTask = listenerTask;
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

        public abstract Task OnTransition(TTransitionParameters transitionParameters, CancellationToken cancellationToken);

        public abstract Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// The DICOM client is doing nothing. Call SendAsync to trigger sending of requests
    /// </summary>
    public class DicomClientIdleState : IDicomClientState<DicomClientIdleState.TransitionParameters>
    {
        private DicomClient DicomClient { get; }

        public DicomClientIdleState(DicomClient dicomClient)
        {
            DicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        public Task OnTransition(TransitionParameters transitionParameters, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public async Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            var noDelay = DicomClient.Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = DicomClient.Options?.IgnoreSslPolicyErrors ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;
            var networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

            var dicomClientConnection = new DicomClientConnection(DicomClient, networkStream)
            {
                Options = DicomClient.Options,
            };

            var dicomClientConnectionListenerTask = dicomClientConnection.RunAsync();

            var requestAssociationState = new DicomClientRequestAssociationState(DicomClient, dicomClientConnection, dicomClientConnectionListenerTask);

            var transitionParameters = new DicomClientRequestAssociationState.TransitionParameters
            {
                Host = host,
                Port = port,
                CallingA = callingAe,
                CalledAe = calledAe
            };

            await DicomClient.Transition(requestAssociationState, transitionParameters, cancellationToken);
        }

        public class TransitionParameters
        {
        }
    }

    /// <summary>
    /// The DICOM client is connected to the server and requires an association. When transitioning into this state, a new association request will be sent
    /// </summary>
    public class DicomClientRequestAssociationState : DicomClientWithConnectionState<DicomClientRequestAssociationState.TransitionParameters>, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly CancellationTokenSource _timeoutCancellationTokenSource;
        private CancellationTokenSource _combinedCancellationTokenSource;

        public DicomClientRequestAssociationState(DicomClient dicomClient,
            IDicomClientConnection dicomClientConnection,
            Task dicomClientConnectionListenerTask) : base(dicomClientConnection, dicomClientConnectionListenerTask)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _timeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            // TODO transition to AssociationRejectedState
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            // Ignore, we're making a new association now
            return Task.FromResult(0);
            ;
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // TODO transition to AbortedState
            return Task.FromResult(0);
            ;
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // TODO transition to DisconnectedState
            return Task.FromResult(0);
            ;
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
            ;
        }

        public override async Task OnTransition(TransitionParameters transitionParameters, CancellationToken cancellationToken)
        {
            var associationToRequest = new DicomAssociation(transitionParameters.CallingA, transitionParameters.CalledAe)
            {
                MaxAsyncOpsInvoked = 1,
                MaxAsyncOpsPerformed = 1,
                RemoteHost = transitionParameters.Host,
                RemotePort = transitionParameters.Port
            };

            foreach (var request in _dicomClient.QueuedRequests)
            {
                associationToRequest.PresentationContexts.AddFromRequest(request);
            }

            foreach (var context in _dicomClient.AdditionalPresentationContexts)
            {
                associationToRequest.PresentationContexts.Add(
                    context.AbstractSyntax,
                    context.UserRole,
                    context.ProviderRole,
                    context.GetTransferSyntaxes().ToArray());
            }

            await Connection.SendAssociationRequestAsync(associationToRequest);

            _combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _timeoutCancellationTokenSource.Token);

            await Task.Delay(transitionParameters.AssociationRequestTimeoutInMs, _combinedCancellationTokenSource.Token)
                .ContinueWith(_ =>
                {
                    // TODO transition to another state when association request times out. Perhaps re-enter this one again, with a max retry count..
                }, TaskContinuationOptions.OnlyOnCanceled);
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public class TransitionParameters
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string CallingA { get; set; }
            public string CalledAe { get; set; }
            public int AssociationRequestTimeoutInMs { get; set; }
        }

        public void Dispose()
        {
            _timeoutCancellationTokenSource?.Dispose();
            _combinedCancellationTokenSource?.Dispose();
        }
    }
}
