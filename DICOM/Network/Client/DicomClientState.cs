using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    public interface IDicomClientState
    {
        /// <summary>
        /// Is called when entering this state
        /// </summary>
        /// <returns></returns>
        Task OnEnter(CancellationToken cancellationToken);

        /// <summary>
        /// Is called when entering this state
        /// </summary>
        /// <returns></returns>
        Task OnExit(CancellationToken cancellationToken);

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
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);

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

    public interface IInitialisationWithConnectionParameters
    {
        IDicomClientConnection Connection { get; set; }

        Task ListenerTask { get; set; }
    }

    public interface IInitialisationWithAssociationParameters : IInitialisationWithConnectionParameters
    {
        DicomAssociation Association { get; set; }
    }

    public abstract class DicomClientWithConnectionState : IDicomClientState
    {
        /// <summary>
        /// Gets the connection between the client and the server
        /// </summary>
        public IDicomClientConnection Connection { get; }

        /// <summary>
        /// Gets the long-running task that is listening for incoming DICOM communication from the server
        /// </summary>
        public Task ListenerTask { get; }

        protected DicomClientWithConnectionState(IInitialisationWithConnectionParameters initialisationParameters)
        {
            Connection = initialisationParameters.Connection;
            ListenerTask = initialisationParameters.ListenerTask;
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

        public abstract Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);
    }

    public abstract class DicomClientWithAssociationState : DicomClientWithConnectionState
    {
        /// <summary>
        /// Gets the currently active association between the client and the server
        /// </summary>
        public DicomAssociation Association { get; set; }

        protected DicomClientWithAssociationState(IInitialisationWithAssociationParameters initialisationParameters) : base(initialisationParameters)
        {
            if (initialisationParameters == null) throw new ArgumentNullException(nameof(initialisationParameters));
            Association = initialisationParameters.Association ?? throw new ArgumentNullException(nameof(initialisationParameters.Association));
        }
    }

    /// <summary>
    /// The DICOM client is doing nothing. Call SendAsync to begin
    /// </summary>
    public class DicomClientIdleState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;

        public DicomClientIdleState(DicomClient dicomClient)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        public Task OnEnter(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public async Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            var parameters = new DicomClientConnectState.InitialisationParameters(host, port, useTls, callingAe, calledAe, millisecondsTimeout);

            var state = new DicomClientConnectState(_dicomClient, parameters);

            await _dicomClient.Transition(state, cancellationToken);
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnConnectionClosed(Exception exception)
        {
            return Task.FromResult(0);
        }

        public Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override string ToString()
        {
            return $"Idle";
        }
    }

    /// <summary>
    /// The DICOM client is connecting to the DICOM server.
    /// </summary>
    public class DicomClientConnectState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;

        public class InitialisationParameters
        {
            public string Host{ get; set; }
            public int Port{ get; set; }
            public bool UseTls{ get; set; }
            public string CallingAe{ get; set; }
            public string CalledAe { get; set; }
            public int TimeoutInMs { get; set; }

            public InitialisationParameters(string host, int port, bool useTls, string callingAe, string calledAe, int timeoutInMs)
            {
                Host = host ?? throw new ArgumentNullException(nameof(host));
                Port = port;
                UseTls = useTls;
                CallingAe = callingAe ?? throw new ArgumentNullException(nameof(callingAe));
                CalledAe = calledAe ?? throw new ArgumentNullException(nameof(calledAe));
                TimeoutInMs = timeoutInMs;
            }
        }

        public DicomClientConnectState(DicomClient dicomClient, InitialisationParameters initialisationParameters)
        {
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            var host = _initialisationParameters.Host;
            var port = _initialisationParameters.Port;
            var useTls = _initialisationParameters.UseTls;
            var millisecondsTimeout = _initialisationParameters.TimeoutInMs;
            var callingAe = _initialisationParameters.CalledAe;
            var calledAe = _initialisationParameters.CalledAe;
            var noDelay = _dicomClient.Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = _dicomClient.Options?.IgnoreSslPolicyErrors ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;
            var networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

            var dicomClientConnection = new DicomClientConnection(_dicomClient, networkStream);

            if (_dicomClient.Options != null)
                dicomClientConnection.Options = _dicomClient.Options;

            var dicomClientConnectionListenerTask = Task.Run(() => dicomClientConnection.RunAsync(), cancellationToken);

            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(callingAe, calledAe,
                DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, dicomClientConnection, dicomClientConnectionListenerTask);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            await _dicomClient.Transition(requestAssociationState, cancellationToken).ConfigureAwait(false);
        }

        public Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO what to do here? We're already connecting..
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnConnectionClosed(Exception exception)
        {
            return Task.FromResult(0);
        }

        public Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override string ToString()
        {
            var host = _initialisationParameters.Host;
            var port = _initialisationParameters.Port;
            return $"Connecting to {host}:{port}";
        }
    }

    /// <summary>
    /// The DICOM client is connected to the server and requires an association. When transitioning into this state, a new association request will be sent
    /// </summary>
    public class DicomClientRequestAssociationState : DicomClientWithConnectionState, IDisposable
    {
        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public string CallingAe { get; set; }
            public string CalledAe { get; set; }
            public int AssociationRequestTimeoutInMs { get; set; }
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(string callingAe, string calledAe, int associationRequestTimeoutInMs, IDicomClientConnection connection, Task listenerTask)
            {
                CallingAe = callingAe;
                CalledAe = calledAe;
                AssociationRequestTimeoutInMs = associationRequestTimeoutInMs;
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
                ListenerTask = listenerTask ?? throw new ArgumentNullException(nameof(listenerTask));
            }
        }

        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _timeoutCancellationTokenSource;
        private CancellationTokenSource _combinedCancellationTokenSource;

        public DicomClientRequestAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _timeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override async Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _timeoutCancellationTokenSource.Cancel();

            _dicomClient.Logger.Info("Association accepted");
            _dicomClient.NotifyAssociationAccepted(new AssociationAcceptedEventArgs(association));

            var initialisationParameters =
                new DicomClientSendingRequestsState.InitialisationParameters(association, _initialisationParameters.Connection,
                    _initialisationParameters.ListenerTask);
            var newState = new DicomClientSendingRequestsState(_dicomClient, initialisationParameters);
            await _dicomClient.Transition(newState, CancellationToken.None);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _timeoutCancellationTokenSource.Cancel();
            _dicomClient.NotifyAssociationRejected(new AssociationRejectedEventArgs(result, source, reason));
            // TODO transition to AssociationRejectedState
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release response but we're still making a new association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _timeoutCancellationTokenSource.Cancel();
            // TODO transition to AbortedState
            return Task.FromResult(0);
            ;
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            _timeoutCancellationTokenSource.Cancel();
            // TODO transition to DisconnectedState
            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            var associationToRequest = new DicomAssociation(_initialisationParameters.CallingAe, _initialisationParameters.CalledAe)
            {
                MaxAsyncOpsInvoked = _dicomClient.AsyncInvoked,
                MaxAsyncOpsPerformed = _dicomClient.AsyncPerformed,
                RemoteHost = _initialisationParameters.Connection.NetworkStream.RemoteHost,
                RemotePort = _initialisationParameters.Connection.NetworkStream.RemotePort
            };

            foreach (var queuedItem in _dicomClient.QueuedRequests.ToList())
            {
                var dicomRequest = queuedItem.Value;
                associationToRequest.PresentationContexts.AddFromRequest(dicomRequest);
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

            // TODO use custom TaskCompletionSource and fill it when AssociationAccept is received...

            await Task.Delay(_initialisationParameters.AssociationRequestTimeoutInMs, _combinedCancellationTokenSource.Token)
                .ContinueWith(_ =>
                {
                    _dicomClient.Logger.Warn("Association request timed out");
                    // TODO transition to another state when association request times out. Perhaps re-enter this one again, with a max retry count..
                }, TaskContinuationOptions.OnlyOnCanceled)
                .ConfigureAwait(false);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO check if different parameters, if so enqueue transition
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _timeoutCancellationTokenSource?.Dispose();
            _combinedCancellationTokenSource?.Dispose();
        }

        public override string ToString()
        {
            var parameters = _initialisationParameters;
            var connection = _initialisationParameters.Connection;
            return $"Requesting association from {parameters.CallingAe} to {parameters.CalledAe} at {connection.NetworkStream.RemoteHost}:{connection.NetworkStream.RemotePort}";
        }
    }

    /// <summary>
    /// The DICOM client is connected and has an active DICOM association. Every queued request will be sent one by one.
    /// </summary>
    public class DicomClientSendingRequestsState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _sendRequestsCancellationTokenSource;
        private CancellationTokenSource _combinedCancellationTokenSource;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection, Task listenerTask)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
                ListenerTask = listenerTask ?? throw new ArgumentNullException(nameof(listenerTask));
            }
        }

        public DicomClientSendingRequestsState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _sendRequestsCancellationTokenSource = new CancellationTokenSource();
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO check if these parameters match with the existing association, if not, enqueue the necessary state transitions
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn("Received association accept but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn("Received association reject but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release response but we did not expect this!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // TODO transition to aborted state
            _sendRequestsCancellationTokenSource.Cancel();
            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // TODO transition to disconnected state
            _sendRequestsCancellationTokenSource.Cancel();
            return Task.FromResult(0);
        }

        public override async Task OnSendQueueEmpty()
        {
            // TODO transition to lingering state

            var lingerParameters = new DicomClientLingeringState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var lingerState = new DicomClientLingeringState(_dicomClient, lingerParameters);
            await _dicomClient.Transition(lingerState, CancellationToken.None);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            _combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_sendRequestsCancellationTokenSource.Token, cancellationToken);

            while (!_combinedCancellationTokenSource.IsCancellationRequested
                   && _dicomClient.QueuedRequests.TryDequeue(out StrongBox<DicomRequest> queuedItem))
            {
                var dicomRequest = queuedItem.Value;
                await Connection.SendRequestAsync(dicomRequest);
            }
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _combinedCancellationTokenSource?.Dispose();
            _sendRequestsCancellationTokenSource?.Dispose();
        }

        public override string ToString()
        {
            return $"Sending queued DICOM requests";
        }
    }

    /// <summary>
    /// When we have an active association, but no more DICOM requests to send (and all responses for previous requests have already arrived)
    /// we will keep the association open for just a little longer in case we get any more requests
    /// </summary>
    public class DicomClientLingeringState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _lingerTimeoutCancellationTokenSource;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection, Task listenerTask)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
                ListenerTask = listenerTask ?? throw new ArgumentNullException(nameof(listenerTask));
            }
        }

        public DicomClientLingeringState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _lingerTimeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO check if parameters correspond with active association, take action accordingly
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn("Received association accept but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn("Received association reject but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release but we did not expect this!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // TODO transition to aborted state
            _lingerTimeoutCancellationTokenSource.Cancel();
            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // TODO transition to disconnected state
            _lingerTimeoutCancellationTokenSource.Cancel();
            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            // Start the clock
            await Task.Delay(DicomClientDefaults.DefaultLingerInMs, _lingerTimeoutCancellationTokenSource.Token)
                .ContinueWith(async lingerTask =>
                {
                    if (lingerTask.IsCanceled)
                    {
                        // A request came in, hurray!
                        var sendRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(_initialisationParameters.Association,
                            _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
                        var sendRequestsState = new DicomClientSendingRequestsState(_dicomClient, sendRequestsParameters);
                        await _dicomClient.Transition(sendRequestsState, cancellationToken);
                    }
                    else if (lingerTask.IsCompleted)
                    {
                        // No requests came in, boohoo
                        var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(
                            _initialisationParameters.Association, _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
                        var releaseAssociationState = new DicomClientReleaseAssociationState(_dicomClient, releaseAssociationParameters);

                        await _dicomClient.Transition(releaseAssociationState, cancellationToken);
                    }
                }, TaskContinuationOptions.NotOnFaulted)
                .ConfigureAwait(false);

            throw new NotImplementedException();
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            _dicomClient.Logger.Debug("Canceling linger because a new request came in");
            _lingerTimeoutCancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            _lingerTimeoutCancellationTokenSource?.Dispose();
        }

        public override string ToString()
        {
            return $"Keeping existing association open (lingering)";
        }
    }

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection, Task listenerTask)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
                ListenerTask = listenerTask ?? throw new ArgumentNullException(nameof(listenerTask));
            }
        }

        public DicomClientReleaseAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationReleaseTimeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO check if parameters correspond with active association, take action accordingly
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn("Received association accept but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn("Received association reject but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {

            // TODO transition to aborted state
            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // TODO transition to disconnected state
            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            await Connection.SendAssociationReleaseRequestAsync();

            var timeout = DicomClientDefaults.DefaultAssociationReleaseTimeoutInMs;
            await Task.Delay(timeout, _associationReleaseTimeoutCancellationTokenSource.Token)
                .ContinueWith(async associationReleaseTimeout =>
                {
                    if (associationReleaseTimeout.IsCompleted)
                    {
                        // No association release response received within timeout? Move to disconnect state anyway
                        _dicomClient.Logger.Warn($"Did not receive association release response within {timeout}ms, disconnecting now...");

                        var disconnectParameters =
                            new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection,
                                _initialisationParameters.ListenerTask);
                        var disconnectState = new DicomClientDisconnectState(_dicomClient, disconnectParameters);

                        await _dicomClient.Transition(disconnectState, cancellationToken);
                    }
                    else if (associationReleaseTimeout.IsCanceled)
                    {
                        // Association release timeout was canceled? Then we must have received an association release response
                        var disconnectParameters = new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection, _initialisationParameters.ListenerTask);
                        var disconnectState = new DicomClientDisconnectState(_dicomClient, disconnectParameters);

                        _dicomClient.NotifyAssociationReleased();

                        await _dicomClient.Transition(disconnectState, CancellationToken.None);
                    }

                }, TaskContinuationOptions.NotOnFaulted)
                .ConfigureAwait(false);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override string ToString()
        {
            var association = _initialisationParameters.Association;
            return $"Releasing association between {association.CallingAE} to {association.CalledAE} on {association.RemoteHost}:{association.RemotePort}";
        }

        public void Dispose()
        {
            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            _associationReleaseTimeoutCancellationTokenSource?.Dispose();
        }
    }

    public class DicomClientAbortState : DicomClientWithConnectionState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;

        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(IDicomClientConnection connection, Task listenerTask)
            {
                Connection = connection;
                ListenerTask = listenerTask;
            }
        }

        public DicomClientAbortState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO explicit send is requested, move back to association request state after abort
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn("Received association accept but we were just in the process of aborting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn("Received association reject but we were just in the process of aborting");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release response but we were just in the process of aborting!");
            return Task.FromResult(0);
        }

        public override async Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _dicomClient.Logger.Warn("Received abort while we were just in the process of aborting... wonderful");
            var idleState = new DicomClientIdleState(_dicomClient);
            await _dicomClient.Transition(idleState, CancellationToken.None);
        }

        public override async Task OnConnectionClosed(Exception exception)
        {
            _dicomClient.Logger.Warn("Received 'OnConnectionClosed' after abort, perfect");
            var idleState = new DicomClientIdleState(_dicomClient);
            await _dicomClient.Transition(idleState, CancellationToken.None);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            await _initialisationParameters.Connection.SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override string ToString()
        {
            var connectionNetworkStream = _initialisationParameters.Connection?.NetworkStream;
            return $"Aborting connection to {connectionNetworkStream?.RemoteHost}:{connectionNetworkStream?.RemotePort}";
        }
    }

    public class DicomClientDisconnectState : DicomClientWithConnectionState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;

        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(IDicomClientConnection connection, Task listenerTask)
            {
                Connection = connection;
                ListenerTask = listenerTask;
            }
        }

        public DicomClientDisconnectState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
        }

        public override Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            // TODO explicit send is requested, move back to association request state after disconnect
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn("Received association accept but we were just in the process of disconnecting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn("Received association reject but we were just in the process of disconnecting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release response but we were just in the process of disconnecting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _dicomClient.Logger.Warn("Received abort but we were just in the process of disconnecting!");
            // TODO transition to aborted state
            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            _dicomClient.Logger.Warn("Received on connection closed, exactly as we expected!");
            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            try
            {
                _initialisationParameters.Connection?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("DicomService could not be disposed properly: " + e);
            }

            try
            {
                _initialisationParameters.Connection?.NetworkStream?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("NetworkStream could not be disposed properly: " + e);
            }

            // wait until listener task realizes connection is gone
            await _initialisationParameters.ListenerTask.ConfigureAwait(false);

            if (_dicomClient.QueuedRequests.Any())
            {
                // TODO reconnect
            }
            else
            {
                var idleState = new DicomClientIdleState(_dicomClient);

                await _dicomClient.Transition(idleState, cancellationToken);
            }
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override string ToString()
        {
            var connectionNetworkStream = _initialisationParameters.Connection?.NetworkStream;
            return $"Disconnecting from {connectionNetworkStream?.RemoteHost}:{connectionNetworkStream?.RemotePort}";
        }
    }
}
