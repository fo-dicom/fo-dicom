using System;
using System.Collections.Generic;
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
        Task SendAsync(CancellationToken cancellationToken = default);

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

        public abstract Task SendAsync(CancellationToken cancellationToken = default);
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

        private async Task TransitionToConnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientConnectState.InitialisationParameters();

            var state = new DicomClientConnectState(_dicomClient, parameters);

            await _dicomClient.Transition(state, cancellationToken);
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
            {
                await TransitionToConnectState(cancellationToken);
                return;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug("Cancellation requested, staying in idle state");
            }
            else
            {
                _dicomClient.Logger.Debug("No requests to send, staying in idle state");
            }
        }

        public Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public async Task SendAsync(CancellationToken cancellationToken = default)
        {
            await TransitionToConnectState(cancellationToken);
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
        }

        public DicomClientConnectState(DicomClient dicomClient, InitialisationParameters initialisationParameters)
        {
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        private async Task TransitionToRequestAssociationState(IDicomClientConnection connection, Task listenerTask, CancellationToken cancellationToken)
        {
            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection, listenerTask);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            await _dicomClient.Transition(requestAssociationState, cancellationToken).ConfigureAwait(false);
        }

        private async Task TransitionToIdleState(CancellationToken cancellationToken)
        {
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient), cancellationToken);
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn("Cancellation requested, won't connect");
                await TransitionToIdleState(cancellationToken).ConfigureAwait(false);
                return;
            }

            var host = _dicomClient.Host;
            var port = _dicomClient.Port;
            var useTls = _dicomClient.UseTls;
            var millisecondsTimeout = _dicomClient.AssociationRequestTimeoutInMs;
            var noDelay = _dicomClient.Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = _dicomClient.Options?.IgnoreSslPolicyErrors ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;
            var networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

            var dicomClientConnection = new DicomClientConnection(_dicomClient, networkStream);

            if (_dicomClient.Options != null)
                dicomClientConnection.Options = _dicomClient.Options;

            var listenerTask = Task.Run(() => dicomClientConnection.RunAsync(), cancellationToken);

            await TransitionToRequestAssociationState(dicomClientConnection, listenerTask, cancellationToken);
        }

        public Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're already connecting
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
            var host = _dicomClient.Host;
            var port = _dicomClient.Port;
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
            public IDicomClientConnection Connection { get; set; }
            public Task ListenerTask { get; set; }

            public InitialisationParameters(IDicomClientConnection connection, Task listenerTask)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
                ListenerTask = listenerTask ?? throw new ArgumentNullException(nameof(listenerTask));
            }
        }

        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationRequestTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAssociation> _onAssociationAcceptedTaskCompletionSource;

        private readonly TaskCompletionSource<(DicomRejectResult Result, DicomRejectSource Source, DicomRejectReason Reason)>
            _onAssociationRejectedTaskCompletionSource;

        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onConnectionAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly List<IDisposable> _disposables;

        public DicomClientRequestAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationRequestTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationAcceptedTaskCompletionSource = new TaskCompletionSource<DicomAssociation>();
            _onAssociationRejectedTaskCompletionSource =
                new TaskCompletionSource<(DicomRejectResult Result, DicomRejectSource Source, DicomRejectReason Reason)>();
            _onConnectionAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _disposables = new List<IDisposable>();
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            if (_onAssociationAcceptedTaskCompletionSource.TrySetResult(association))
            {
                _dicomClient.Logger.Debug("Received association accept response");
                _dicomClient.NotifyAssociationAccepted(new AssociationAcceptedEventArgs(association));

                if (!_onAssociationRejectedTaskCompletionSource.TrySetCanceled())
                {
                    _dicomClient.Logger.Warn(
                        "Association reject handling could not be canceled after receiving association accept response, this could lead to unpredictable behavior.");
                }
            }
            else
            {
                _dicomClient.Logger.Warn("Received association accept but it's already too late");
            }

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _associationRequestTimeoutCancellationTokenSource.Cancel();

            if (_onAssociationRejectedTaskCompletionSource.TrySetResult((result, source, reason)))
            {
                _dicomClient.Logger.Debug("Received association reject response");
                _dicomClient.NotifyAssociationRejected(new AssociationRejectedEventArgs(result, source, reason));

                if (!_onAssociationAcceptedTaskCompletionSource.TrySetCanceled())
                {
                    _dicomClient.Logger.Warn(
                        "Association accept handling could not be canceled after receiving association reject response, this could lead to unpredictable behavior.");
                }
            }
            else
            {
                _dicomClient.Logger.Warn("Received association reject but it's already too late");
            }

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn("Received association release response but we're still making a new association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            if (_onConnectionAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task SendAssociationRequest()
        {
            var associationToRequest = new DicomAssociation(_dicomClient.CallingAe, _dicomClient.CalledAe)
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
        }

        private async Task TransitionToSendingRequestsState(DicomAssociation association, CancellationToken cancellationToken)
        {
            var initialisationParameters = new DicomClientSendingRequestsState.InitialisationParameters(
                association, _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var newState = new DicomClientSendingRequestsState(_dicomClient, initialisationParameters);
            await _dicomClient.Transition(newState, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn("Cancellation requested before association request was made, going to disconnect now");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
                return;
            }

            await SendAssociationRequest();

            _disposables.Add(cancellationToken.Register(() => _associationRequestTimeoutCancellationTokenSource.Cancel()));

            var associationIsAccepted = _onAssociationAcceptedTaskCompletionSource.Task;
            var associationIsRejected = _onAssociationRejectedTaskCompletionSource.Task;
            var connectionIsAborted = _onConnectionAbortedTaskCompletionSource.Task;
            var connectionIsClosed = _onConnectionClosedTaskCompletionSource.Task;
            var associationRequestTimesOut = Task.Delay(_dicomClient.AssociationRequestTimeoutInMs, _associationRequestTimeoutCancellationTokenSource.Token);

            var sendRequestsWhenAssociationIsAccepted = associationIsAccepted.ContinueWith(
                async associationAcceptedTask =>
                    await TransitionToSendingRequestsState(associationAcceptedTask.Result, cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            var disconnectWhenAssociationIsRejected = associationIsRejected.ContinueWith(
                async associationRejectedTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            var disconnectWhenAborting = connectionIsAborted.ContinueWith(
                async abortTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            var cleanupWhenDisconnected = connectionIsClosed.ContinueWith(
                async disconnectTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            var disconnectWhenAssociationRequestTimesOut = associationRequestTimesOut.ContinueWith(
                async associationRequestTimeoutTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            await Task.WhenAny(
                sendRequestsWhenAssociationIsAccepted,
                disconnectWhenAssociationIsRejected,
                disconnectWhenAborting,
                cleanupWhenDisconnected,
                disconnectWhenAssociationRequestTimesOut
            ).ConfigureAwait(false);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're already busy trying to send
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onConnectionAbortedTaskCompletionSource.TrySetCanceled();
            _onAssociationAcceptedTaskCompletionSource.TrySetCanceled();
            _onAssociationRejectedTaskCompletionSource.TrySetCanceled();
            _associationRequestTimeoutCancellationTokenSource?.Dispose();
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }

        public override string ToString()
        {
            var connection = _initialisationParameters.Connection;
            var callingAe = _dicomClient.CallingAe;
            var calledAe = _dicomClient.CalledAe;
            var remoteHost = connection.NetworkStream.RemoteHost;
            var remotePort = connection.NetworkStream.RemotePort;
            return $"Requesting association from {callingAe} to {calledAe} at {remoteHost}:{remotePort}";
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
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onConnectionAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onSendQueueEmptyTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;

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
            _onSendQueueEmptyTaskCompletionSource = new TaskCompletionSource<bool>();
            _onConnectionAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _disposables = new List<IDisposable>();
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're already sending
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
            // Stop sending requests ASAP
            _sendRequestsCancellationTokenSource.Cancel();

            if (_onConnectionAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // Stop sending requests ASAP
            _sendRequestsCancellationTokenSource.Cancel();

            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            if (_onSendQueueEmptyTaskCompletionSource.TrySetResult(true))
            {
                _dicomClient.Logger.Debug("Send queue is empty");
            }
            else
            {
                _dicomClient.Logger.Warn("Send queue is now empty but it's already too late to handle that now");
            }

            return Task.FromResult(0);
        }

        private async Task TransitionToLingerState(CancellationToken cancellationToken)
        {
            var lingerParameters = new DicomClientLingeringState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var lingerState = new DicomClientLingeringState(_dicomClient, lingerParameters);
            await _dicomClient.Transition(lingerState, cancellationToken);
        }

        private async Task TransitionToReleaseAssociationState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientReleaseAssociationState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var state = new DicomClientReleaseAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        private async Task SendRequests()
        {
            while (!_sendRequestsCancellationTokenSource.IsCancellationRequested &&
                   _dicomClient.QueuedRequests.TryDequeue(out StrongBox<DicomRequest> queuedItem))
            {
                var dicomRequest = queuedItem.Value;
                await Connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);
                queuedItem.Value = null;
            }
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn("Cancellation is requested before requests could be sent, releasing association ...");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
                return;
            }

            _disposables.Add(cancellationToken.Register(() => _sendRequestsCancellationTokenSource.Cancel()));

            _dicomClient.Logger.Debug("Sending queued DICOM requests");

            await SendRequests().ConfigureAwait(false);

            _dicomClient.Logger.Debug("Requests are sent, waiting for next event now");

            var sendQueueIsEmpty = _onSendQueueEmptyTaskCompletionSource.Task;
            var onReceiveAbort = _onConnectionAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;

            var lingerWhenSendQueueIsEmpty = sendQueueIsEmpty.ContinueWith(
                async sendQueueIsEmptyTask => await TransitionToLingerState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var disconnectWhenReceiveAbort = onReceiveAbort.ContinueWith(
                async abortTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var cleanupWhenDisconnected = onDisconnect.ContinueWith(
                async disconnectTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            await Task.WhenAny(
                    lingerWhenSendQueueIsEmpty,
                    disconnectWhenReceiveAbort,
                    cleanupWhenDisconnected)
                .ConfigureAwait(false);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }

            _sendRequestsCancellationTokenSource?.Dispose();
            _onSendQueueEmptyTaskCompletionSource.TrySetCanceled();
            _onConnectionAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
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
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onConnectionAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onRequestAddedTaskCompletionSource;

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
            _onConnectionAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _onRequestAddedTaskCompletionSource = new TaskCompletionSource<bool>();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we will automatically send again if there are requests
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
            // Give up on linger
            _lingerTimeoutCancellationTokenSource.Cancel();

            if (_onConnectionAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // Give up on linger
            _lingerTimeoutCancellationTokenSource.Cancel();

            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToSendingRequestsState(CancellationToken cancellationToken)
        {
            var sendRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var sendRequestsState = new DicomClientSendingRequestsState(_dicomClient, sendRequestsParameters);
            await _dicomClient.Transition(sendRequestsState, cancellationToken);
        }

        private async Task TransitionToReleaseAssociationState(CancellationToken cancellationToken)
        {
            var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(
                _initialisationParameters.Association, _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var releaseAssociationState = new DicomClientReleaseAssociationState(_dicomClient, releaseAssociationParameters);

            await _dicomClient.Transition(releaseAssociationState, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn("Cancellation is requested, releasing association immediately");
                await TransitionToReleaseAssociationState(cancellationToken);
                return;
            }

            var onRequestIsAdded = _onRequestAddedTaskCompletionSource.Task;
            var onReceiveAbort = _onConnectionAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onLingerTimeout = Task.Delay(_dicomClient.AssociationLingerTimeoutInMs, _lingerTimeoutCancellationTokenSource.Token);

            var sendRequestsWhenRequestIsAdded = onRequestIsAdded.ContinueWith(
                async _ => await TransitionToSendingRequestsState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var releaseAssociationWhenLingerTimeoutExpires = onLingerTimeout.ContinueWith(
                async lingerTimeoutTask => await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var disconnectWhenReceiveAbort = onReceiveAbort.ContinueWith(
                async abortTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var cleanupWhenDisconnected = onDisconnect.ContinueWith(
                async disconnectTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            await Task.WhenAny(
                    sendRequestsWhenRequestIsAdded,
                    releaseAssociationWhenLingerTimeoutExpires,
                    disconnectWhenReceiveAbort,
                    cleanupWhenDisconnected)
                .ConfigureAwait(false);
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _lingerTimeoutCancellationTokenSource.Cancel();

            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            if (_onRequestAddedTaskCompletionSource.TrySetResult(true))
            {
                _dicomClient.Logger.Debug("Received new DICOM request during association linger, same association will be reused");
            }
            else
            {
                _dicomClient.Logger.Info("Received new DICOM request during association but it's already too late, a new association will be created");
            }
        }

        public void Dispose()
        {
            _lingerTimeoutCancellationTokenSource?.Dispose();
            _onConnectionAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"Keeping existing association open (lingering) for {_dicomClient.AssociationLingerTimeoutInMs}ms";
        }
    }

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onConnectionAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAssociationReleasedTaskCompletionSource;

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
            _onAssociationReleasedTaskCompletionSource = new TaskCompletionSource<bool>();
            _onConnectionAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we will automatically send again when there are requests
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

            if (_onAssociationReleasedTaskCompletionSource.TrySetResult(true))
            {
                _dicomClient.Logger.Debug("Received association release response");
            }
            else
            {
                _dicomClient.Logger.Warn("Received association release response but it's too late now");
            }

            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // Give up association release request
            _associationReleaseTimeoutCancellationTokenSource.Cancel();

            if (_onConnectionAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            // Give up association release request
            _associationReleaseTimeoutCancellationTokenSource.Cancel();

            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToRequestAssociationState(CancellationToken cancellationToken)
        {
            var parameters =
                new DicomClientRequestAssociationState.InitialisationParameters(_initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            var state = new DicomClientRequestAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(
                _initialisationParameters.Connection, _initialisationParameters.ListenerTask);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            // Regardless whether or not cancellation is requested, we'll try to cleanup the association the proper way
            await Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

            var onAssociationRelease = _onAssociationReleasedTaskCompletionSource.Task;
            var onAssociationReleaseTimeout = Task.Delay(_dicomClient.AssociationReleaseTimeoutInMs, _associationReleaseTimeoutCancellationTokenSource.Token);
            var onReceiveAbort = _onConnectionAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;

            var cleanupOrRestartWhenAssociationReleased = onAssociationRelease.ContinueWith(
                async associationReleasedTask =>
                {
                    if (!cancellationToken.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
                    {
                        await TransitionToRequestAssociationState(cancellationToken);
                    }
                    else
                    {
                        await TransitionToDisconnectState(cancellationToken);
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            var disconnectWhenAssociationReleaseTimesOut = onAssociationReleaseTimeout.ContinueWith(
                async associationTimeoutTask => await TransitionToDisconnectState(cancellationToken),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var disconnectWhenReceiveAbort = onReceiveAbort.ContinueWith(
                async abortTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            var cleanupWhenDisconnected = onDisconnect.ContinueWith(
                async disconnectTask => await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            await Task.WhenAny(
                    cleanupOrRestartWhenAssociationReleased,
                    disconnectWhenAssociationReleaseTimesOut,
                    disconnectWhenReceiveAbort,
                    cleanupWhenDisconnected)
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

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're aborting now
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

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore
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

        private async Task TransitionToIdleState(CancellationToken cancellationToken)
        {
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient), cancellationToken);
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

            await TransitionToIdleState(cancellationToken);
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
