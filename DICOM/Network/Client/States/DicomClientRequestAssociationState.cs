using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client
{
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
        private readonly TaskCompletionSource<DicomAssociationRejectedEvent> _onAssociationRejectedTaskCompletionSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;

        public DicomClientRequestAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationRequestTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationAcceptedTaskCompletionSource = new TaskCompletionSource<DicomAssociation>();
            _onAssociationRejectedTaskCompletionSource = new TaskCompletionSource<DicomAssociationRejectedEvent>();
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _onAssociationAcceptedTaskCompletionSource.TrySetResult(association);

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _onAssociationRejectedTaskCompletionSource.TrySetResult(new DicomAssociationRejectedEvent(result, source, reason));

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we're still making a new association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAssociationAbortedTaskCompletionSource.TrySetResult(new DicomAbortedEvent(source, reason));

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResult(new ConnectionClosedEvent(exception));

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

        private async Task TransitionToCompletedState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested before association request was made, going to disconnect now");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
                return;
            }

            await SendAssociationRequest();

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
                _dicomClient.Logger.Debug($"[{this}] Association is accepted");
                _dicomClient.NotifyAssociationAccepted(new AssociationAcceptedEventArgs(associationIsAccepted.Result));
                await TransitionToSendingRequestsState(associationIsAccepted.Result, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationIsRejected)
            {
                var associationRejectedResult = associationIsRejected.Result;

                _dicomClient.Logger.Warn($"[{this}] Association is rejected");
                var result = associationRejectedResult.Result;
                var source = associationRejectedResult.Source;
                var reason = associationRejectedResult.Reason;
                _dicomClient.NotifyAssociationRejected(new AssociationRejectedEventArgs(result, source, reason));
                var exception = new DicomAssociationRejectedException(result, source, reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationIsAborted)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted");
                var associationAbortedResult = associationIsAborted.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == connectionIsClosed)
            {
                _dicomClient.Logger.Warn($"[{this}] Disconnected");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationRequestTimesOut)
            {
                _dicomClient.Logger.Warn($"[{this}] Association request timeout");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
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
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            _associationRequestTimeoutCancellationTokenSource.Dispose();
        }

        public override string ToString()
        {
            return $"REQUESTING ASSOCIATION";
        }
    }
}
