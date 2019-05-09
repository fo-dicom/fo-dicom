using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
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
        private readonly TaskCompletionSource<DicomAssociationAcceptedEvent> _onAssociationAcceptedTaskCompletionSource;
        private readonly TaskCompletionSource<DicomAssociationRejectedEvent> _onAssociationRejectedTaskCompletionSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAbortRequestedTaskCompletionSource;

        public DicomClientRequestAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationRequestTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationAcceptedTaskCompletionSource = new TaskCompletionSource<DicomAssociationAcceptedEvent>();
            _onAssociationRejectedTaskCompletionSource = new TaskCompletionSource<DicomAssociationRejectedEvent>();
            _onAbortReceivedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
            _onAbortRequestedTaskCompletionSource = new TaskCompletionSource<bool>();
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            _onAssociationAcceptedTaskCompletionSource.TrySetResult(new DicomAssociationAcceptedEvent(association));

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _onAssociationRejectedTaskCompletionSource.TrySetResult(new DicomAssociationRejectedEvent(result, source, reason));

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we're still making a new association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAbortReceivedTaskCompletionSource.TrySetResult(new DicomAbortedEvent(source, reason));

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosedAsync(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResult(new ConnectionClosedEvent(exception));

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmptyAsync()
        {
            return Task.FromResult(0);
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
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

            await Connection.SendAssociationRequestAsync(associationToRequest).ConfigureAwait(false);
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
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken).ConfigureAwait(false);
        }

        private async Task TransitionToCompletedState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection, CancellationToken.None);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken).ConfigureAwait(false);
        }

        private async Task TransitionToAbortState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnterAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested before association request was made, going to disconnect now");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
                return;
            }

            if (_onAbortRequestedTaskCompletionSource.Task.IsCompleted)
            {
                _dicomClient.Logger.Warn($"[{this}] Abort requested before association could be requested, immediately aborting now...");
                await TransitionToAbortState(cancellationToken).ConfigureAwait(false);
                return;
            }

            await SendAssociationRequest();

            var associationIsAccepted = _onAssociationAcceptedTaskCompletionSource.Task;
            var associationIsRejected = _onAssociationRejectedTaskCompletionSource.Task;
            var abortReceived = _onAbortReceivedTaskCompletionSource.Task;
            var connectionIsClosed = _onConnectionClosedTaskCompletionSource.Task;
            var associationRequestTimesOut = Task.Delay(_dicomClient.AssociationRequestTimeoutInMs, _associationRequestTimeoutCancellationTokenSource.Token);
            var onAbortRequested = _onAbortRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                associationIsAccepted,
                associationIsRejected,
                abortReceived,
                connectionIsClosed,
                associationRequestTimesOut,
                onAbortRequested
            ).ConfigureAwait(false);

            if (winner == associationIsAccepted)
            {
                _dicomClient.Logger.Debug($"[{this}] Association is accepted");
                var association = associationIsAccepted.Result.Association;
                _dicomClient.NotifyAssociationAccepted(new EventArguments.AssociationAcceptedEventArgs(association));
                await TransitionToSendingRequestsState(association, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == associationIsRejected)
            {
                var associationRejectedResult = associationIsRejected.Result;

                _dicomClient.Logger.Warn($"[{this}] Association is rejected");
                var result = associationRejectedResult.Result;
                var source = associationRejectedResult.Source;
                var reason = associationRejectedResult.Reason;
                _dicomClient.NotifyAssociationRejected(new EventArguments.AssociationRejectedEventArgs(result, source, reason));
                var exception = new DicomAssociationRejectedException(result, source, reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == abortReceived)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted");
                var abortReceivedResult = abortReceived.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
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
            else if (winner == onAbortRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Abort requested while requesting association");
                await TransitionToAbortState(cancellationToken).ConfigureAwait(false);
            }
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we're already busy trying to send
            return Task.FromResult(0);
        }

        public override Task AbortAsync()
        {
            _onAbortRequestedTaskCompletionSource.TrySetResult(true);

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
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
