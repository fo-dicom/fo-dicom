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
    }

    public interface IInitialisationWithAssociationParameters : IInitialisationWithConnectionParameters
    {
        DicomAssociation Association { get; set; }
    }

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

            await _dicomClient.Transition(state, cancellationToken).ConfigureAwait(false);
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
            {
                _dicomClient.Logger.Debug("More requests to send (and no cancellation requested yet), automatically opening new association");
                await TransitionToConnectState(cancellationToken).ConfigureAwait(false);
                return;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug("Cancellation requested, staying idle");
            }
            else
            {
                _dicomClient.Logger.Debug("No requests to send, staying idle");
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
            await TransitionToConnectState(cancellationToken).ConfigureAwait(false);
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
            return $"IDLE";
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

        private async Task TransitionToRequestAssociationState(IDicomClientConnection connection, CancellationToken cancellationToken)
        {
            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            await _dicomClient.Transition(requestAssociationState, cancellationToken).ConfigureAwait(false);
        }

        private async Task TransitionToIdleState(CancellationToken cancellationToken)
        {
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, IDicomClientConnection connection, CancellationToken cancellationToken)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception)
                : new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
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

            INetworkStream networkStream;
            try
            {
                networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);
            }
            catch (Exception e)
            {
                await TransitionToCompletedWithErrorState(e, null, cancellationToken).ConfigureAwait(false);
                return;
            }

            var connection = new DicomClientConnection(_dicomClient, networkStream);

            if (_dicomClient.Options != null)
                connection.Options = _dicomClient.Options;

            try
            {
                connection.StartListener();
            }
            catch (Exception e)
            {
                await TransitionToCompletedWithErrorState(e, connection, cancellationToken).ConfigureAwait(false);
                return;
            }

            await TransitionToRequestAssociationState(connection, cancellationToken).ConfigureAwait(false);
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
            return $"CONNECTING";
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

            public InitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationRequestTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAssociation> _onAssociationAcceptedTaskCompletionSource;

        private readonly TaskCompletionSource<(DicomRejectResult Result, DicomRejectSource Source, DicomRejectReason Reason)>
            _onAssociationRejectedTaskCompletionSource;

        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
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
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _disposables = new List<IDisposable>();
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            if (_onAssociationAcceptedTaskCompletionSource.TrySetResult(association))
            {
                _dicomClient.NotifyAssociationAccepted(new AssociationAcceptedEventArgs(association));
            }
            else
            {
                _dicomClient.Logger.Warn("Received association accept but it's already too late");
            }

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            if (_onAssociationRejectedTaskCompletionSource.TrySetResult((result, source, reason)))
            {
                _dicomClient.Logger.Debug("Received association reject response");
                _dicomClient.NotifyAssociationRejected(new AssociationRejectedEventArgs(result, source, reason));
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
            if (_onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason)))
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
                association, _initialisationParameters.Connection);
            var newState = new DicomClientSendingRequestsState(_dicomClient, initialisationParameters);
            await _dicomClient.Transition(newState, cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection);
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
            var associationIsAborted = _onAssociationAbortedTaskCompletionSource.Task;
            var connectionIsClosed = _onConnectionClosedTaskCompletionSource.Task;
            var associationRequestTimesOut = Task.Delay(_dicomClient.AssociationRequestTimeoutInMs, _associationRequestTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(
                associationIsAccepted,
                associationIsRejected,
                associationIsAborted,
                connectionIsClosed,
                associationRequestTimesOut
            ).ConfigureAwait(false);

            if (winner == associationIsAccepted)
            {
                _dicomClient.Logger.Debug("[RequestAssociationState] Association is accepted");
                await TransitionToSendingRequestsState(associationIsAccepted.Result, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationIsRejected)
            {
                _dicomClient.Logger.Warn("[RequestAssociationState] Association is rejected");
                var associationRejectedResult = associationIsRejected.Result;
                var exception = new DicomAssociationRejectedException(associationRejectedResult.Result, associationRejectedResult.Source, associationRejectedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationIsAborted)
            {
                _dicomClient.Logger.Warn("[RequestAssociationState] Association is aborted");
                var associationAbortedResult = associationIsAborted.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == connectionIsClosed)
            {
                _dicomClient.Logger.Warn("[RequestAssociationState] Disconnected");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationRequestTimesOut)
            {
                _dicomClient.Logger.Warn("[RequestAssociationState] Association request timeout");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
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

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're already busy trying to send
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
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
            return $"REQUESTING ASSOCIATION";
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
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onSendQueueEmptyTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public DicomClientSendingRequestsState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _sendRequestsCancellationTokenSource = new CancellationTokenSource();
            _onSendQueueEmptyTaskCompletionSource = new TaskCompletionSource<bool>();
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
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

            if (_onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason)))
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
                _initialisationParameters.Connection);
            var lingerState = new DicomClientLingeringState(_dicomClient, lingerParameters);
            await _dicomClient.Transition(lingerState, cancellationToken);
        }

        private async Task TransitionToReleaseAssociationState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientReleaseAssociationState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var state = new DicomClientReleaseAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
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
            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onCancellation = new TaskCompletionSource<CancellationToken>();

            _disposables.Add(cancellationToken.Register(() => onCancellation.SetResult(cancellationToken)));


            var winner = await Task.WhenAny(sendQueueIsEmpty, onReceiveAbort, onDisconnect, onCancellation.Task).ConfigureAwait(false);

            if (winner == sendQueueIsEmpty)
            {
                _dicomClient.Logger.Debug("[SendingRequestsState] DICOM client send queue is empty, going to linger association now...");
                await TransitionToLingerState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onCancellation.Task)
            {
                _dicomClient.Logger.Debug("[SendingRequestsState] DICOM client cancellation requested while sending requests, releasing association...");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn("[SendingRequestsState] Association is aborted while sending requests, cleaning up...");
                var associationAbortedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug("[SendingRequestsState] DICOM client disconnected while sending requests, cleaning up...");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
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
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"SENDING REQUESTS";
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
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onRequestAddedTaskCompletionSource;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public DicomClientLingeringState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _lingerTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
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
            if (_onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            // Cleanup linger
            _lingerTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {

            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            // Cleanup linger
            _lingerTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToSendingRequestsState(CancellationToken cancellationToken)
        {
            var sendRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var sendRequestsState = new DicomClientSendingRequestsState(_dicomClient, sendRequestsParameters);
            await _dicomClient.Transition(sendRequestsState, cancellationToken);
        }

        private async Task TransitionToReleaseAssociationState(CancellationToken cancellationToken)
        {
            var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(
                _initialisationParameters.Association, _initialisationParameters.Connection);
            var releaseAssociationState = new DicomClientReleaseAssociationState(_dicomClient, releaseAssociationParameters);

            await _dicomClient.Transition(releaseAssociationState, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
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
            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onLingerTimeout = Task.Delay(_dicomClient.AssociationLingerTimeoutInMs, _lingerTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(onRequestIsAdded, onLingerTimeout, onReceiveAbort, onDisconnect).ConfigureAwait(false);

            if (winner == onRequestIsAdded)
            {
                _dicomClient.Logger.Debug("[LingeringState] A new request was added, reusing association within linger timeout");
                await TransitionToSendingRequestsState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onLingerTimeout)
            {
                _dicomClient.Logger.Debug("[LingeringState] Linger timed out, releasing association");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn("[LingeringState] Association was aborted during linger");
                var associationAbortedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Warn("[LingeringState] Disconnected during linger");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
        }

        public override Task OnExit(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            if (_onRequestAddedTaskCompletionSource.TrySetResult(true))
            {
                _dicomClient.Logger.Debug("Received new DICOM request during association linger, same association will be reused");
            }
            else
            {
                _dicomClient.Logger.Info("Received new DICOM request during association but it's already too late, a new association will be created");
            }

            _lingerTimeoutCancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            _lingerTimeoutCancellationTokenSource?.Dispose();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"LINGERING ASSOCIATION";
        }
    }

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAssociationReleasedTaskCompletionSource;

        public class InitialisationParameters : IInitialisationWithAssociationParameters
        {
            public DicomAssociation Association { get; set; }
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(DicomAssociation association, IDicomClientConnection connection)
            {
                Association = association ?? throw new ArgumentNullException(nameof(association));
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public DicomClientReleaseAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationReleaseTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationReleasedTaskCompletionSource = new TaskCompletionSource<bool>();
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
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
            if (_onAssociationReleasedTaskCompletionSource.TrySetResult(true))
            {
                _dicomClient.Logger.Debug("Received association release response");
                _dicomClient.NotifyAssociationReleased();
            }
            else
            {
                _dicomClient.Logger.Warn("Received association release response but it's too late now");
                _dicomClient.NotifyAssociationReleased();
            }

            _associationReleaseTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            if (_onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            _associationReleaseTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            _associationReleaseTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToRequestAssociationState(CancellationToken cancellationToken)
        {
            var parameters =
                new DicomClientRequestAssociationState.InitialisationParameters(_initialisationParameters.Connection);
            var state = new DicomClientRequestAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellationToken);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientDisconnectState.InitialisationParameters(
                _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            // Regardless whether or not cancellation is requested, we'll try to cleanup the association the proper way
            await _initialisationParameters.Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

            var onAssociationRelease = _onAssociationReleasedTaskCompletionSource.Task;
            var onAssociationReleaseTimeout = Task.Delay(_dicomClient.AssociationReleaseTimeoutInMs, _associationReleaseTimeoutCancellationTokenSource.Token);
            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(onAssociationReleaseTimeout,onReceiveAbort,onDisconnect)
                .ConfigureAwait(false);

            if (winner == onAssociationRelease)
            {
                if (!cancellationToken.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
                {
                    _dicomClient.Logger.Debug("More requests need to be sent after association release, creating new association");
                    await TransitionToRequestAssociationState(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    _dicomClient.Logger.Debug("No more requests to be sent, disconnecting...");
                    await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
                }
            }
            else if (winner == onAssociationReleaseTimeout)
            {
                _dicomClient.Logger.Debug("Association release timed out, disconnecting...");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug("Association aborted, disconnecting...");
                var associationAbortedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug("Disconnected during association release, cleaning up...");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
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
            return $"RELEASING ASSOCIATION";
        }

        public void Dispose()
        {
            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            _associationReleaseTimeoutCancellationTokenSource?.Dispose();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onAssociationReleasedTaskCompletionSource.TrySetCanceled();
        }
    }

    public class DicomClientAbortState : DicomClientWithConnectionState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
        private readonly CancellationTokenSource _associationAbortTimeoutCancellationTokenSource;

        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection;
            }
        }

        public DicomClientAbortState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _associationAbortTimeoutCancellationTokenSource = new CancellationTokenSource();
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

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            if (_onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason)))
            {
                _dicomClient.Logger.Debug("Received abort request");
            }
            else
            {
                _dicomClient.Logger.Warn("Received abort request but too late to handle it now");
            }

            _associationAbortTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            if (_onConnectionClosedTaskCompletionSource.TrySetResult(exception))
            {
                _dicomClient.Logger.Debug("Connection was closed");
            }
            else
            {
                _dicomClient.Logger.Warn("Connection was closed but it's already too late to handle it now");
            }

            _associationAbortTimeoutCancellationTokenSource.Cancel();

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToDisconnectState(CancellationToken cancellationToken)
        {
            var initialisationParameters =
                new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, initialisationParameters), cancellationToken);
        }

        private async Task TransitionToCompletedState(CancellationToken cancellationToken)
        {
            var initialisationParameters =
                new DicomClientDisconnectState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientDisconnectState(_dicomClient, initialisationParameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            await _initialisationParameters.Connection.SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);

            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onTimeout = Task.Delay(100, _associationAbortTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(onReceiveAbort, onDisconnect, onTimeout).ConfigureAwait(false);

            if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug("Received abort after aborting. Neat.");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug("Disconnected after aborting. Perfect.");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onTimeout)
            {
                _dicomClient.Logger.Debug("Abort request timed out. Disconnecting...");
                await TransitionToDisconnectState(cancellationToken).ConfigureAwait(false);
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
            return $"ABORTING ASSOCIATION";
        }

        public void Dispose()
        {
            _associationAbortTimeoutCancellationTokenSource.Cancel();
            _associationAbortTimeoutCancellationTokenSource?.Dispose();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }
    }

    public class DicomClientDisconnectState : DicomClientWithConnectionState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;

        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection;
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
            var connection = _initialisationParameters.Connection;
            var listener = connection?.Listener;
            try
            {
                connection?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("DicomService could not be disposed properly: " + e);
            }

            try
            {
                connection?.NetworkStream?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("NetworkStream could not be disposed properly: " + e);
            }

            // wait until listener task realizes connection is gone
            if (listener != null)
            {
                await listener.ConfigureAwait(false);
            }

            await TransitionToIdleState(cancellationToken).ConfigureAwait(false);
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
            return $"DISCONNECTING";
        }
    }

    /// <summary>
    /// The DICOM client has completed its work. If no errors happened, it will transition back to 'idle', otherwise it will stop here.
    /// </summary>
    public class DicomClientCompletedState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;

        public abstract class InitialisationParameters
        {

        }

        public class DicomClientCompletedWithoutErrorInitialisationParameters : InitialisationParameters
        {
            public DicomClientCompletedWithoutErrorInitialisationParameters()
            {

            }
        }

        public class DicomClientCompletedWithErrorInitialisationParameters : InitialisationParameters
        {
            public IDicomClientConnection Connection { get; }
            public Exception ExceptionToThrow { get; }

            public DicomClientCompletedWithErrorInitialisationParameters(Exception exceptionToThrow)
            {
                ExceptionToThrow = exceptionToThrow ?? throw new ArgumentNullException(nameof(exceptionToThrow));
            }

            public DicomClientCompletedWithErrorInitialisationParameters(Exception exceptionToThrow, IDicomClientConnection connection)
                : this(exceptionToThrow)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public DicomClientCompletedState(DicomClient dicomClient, InitialisationParameters initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
        }

        private async Task Cleanup(IDicomClientConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            var listener = connection?.Listener;
            try
            {
                connection?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("DicomService could not be disposed properly: " + e);
            }

            try
            {
                connection?.NetworkStream?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn("NetworkStream could not be disposed properly: " + e);
            }

            // wait until listener task realizes connection is gone
            if (listener != null)
            {
                await listener.ConfigureAwait(false);
            }
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            switch (_initialisationParameters)
            {
                case DicomClientCompletedWithoutErrorInitialisationParameters _:
                    _dicomClient.Logger.Debug("DICOM client completed without errors");
                    break;

                case DicomClientCompletedWithErrorInitialisationParameters parameters:
                    _dicomClient.Logger.Debug("DICOM client completed with an error");

                    if (parameters.Connection != null)
                    {
                        _dicomClient.Logger.Debug("An error occurred while we had an active connection, cleaning that up first");
                        await Cleanup(parameters.Connection).ConfigureAwait(false);
                    }
                    else
                    {
                        _dicomClient.Logger.Warn("An error occurred and no active connection was detected, so no cleanup will happen!");
                    }

                    throw parameters.ExceptionToThrow;
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

        public Task SendAsync(CancellationToken cancellationToken = default)
        {
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
            return $"COMPLETED";
        }
    }

}
