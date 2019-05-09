using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// When we have an active association, but no more DICOM requests to send (and all responses for previous requests have already arrived)
    /// we will keep the association open for just a little longer in case we get any more requests
    /// </summary>
    public class DicomClientLingeringState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _lingerTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onRequestAddedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAbortRequestedCompletionSource;

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
            _onAbortReceivedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
            _onRequestAddedTaskCompletionSource = new TaskCompletionSource<bool>();
            _onAbortRequestedCompletionSource = new TaskCompletionSource<bool>();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we will automatically send again if there are requests
            return Task.FromResult(0);
        }

        public override Task AbortAsync()
        {
            _onAbortRequestedCompletionSource.TrySetResult(true);

            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we already have an active association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release but we did not expect this!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAbortReceivedTaskCompletionSource.TrySetResult(new DicomAbortedEvent(source, reason));

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

        private async Task TransitionToSendingRequestsState(CancellationToken cancellationToken)
        {
            var sendRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var sendRequestsState = new DicomClientSendingRequestsState(_dicomClient, sendRequestsParameters);
            await _dicomClient.Transition(sendRequestsState, cancellationToken);
        }

        private async Task TransitionToReleaseAssociationState(CancellationToken cancellationToken)
        {
            var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(_initialisationParameters.Association, _initialisationParameters.Connection);
            var releaseAssociationState = new DicomClientReleaseAssociationState(_dicomClient, releaseAssociationParameters);

            await _dicomClient.Transition(releaseAssociationState, cancellationToken);
        }

        private async Task TransitionToCompletedState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, CancellationToken cancellationToken)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToAbortState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation is requested, releasing association immediately");
                await TransitionToReleaseAssociationState(cancellationToken);
                return;
            }

            var onRequestIsAdded = _onRequestAddedTaskCompletionSource.Task;
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onAbortRequested = _onAbortRequestedCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onLingerTimeout = Task.Delay(_dicomClient.AssociationLingerTimeoutInMs, _lingerTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(onRequestIsAdded, onReceiveAbort, onAbortRequested, onDisconnect, onLingerTimeout)
                .ConfigureAwait(false);

            if (winner == onRequestIsAdded)
            {
                _dicomClient.Logger.Debug($"[{this}] A new request was added before linger timeout of {_dicomClient.AssociationLingerTimeoutInMs}ms, reusing association");
                await TransitionToSendingRequestsState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onLingerTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Linger timed out after {_dicomClient.AssociationLingerTimeoutInMs}ms, releasing association");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association was aborted while lingering the association");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Warn($"[{this}] Disconnected while lingering the association");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onAbortRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Abort requested, immediately aborting association");
                await TransitionToAbortState(cancellationToken).ConfigureAwait(false);
            }
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            _onRequestAddedTaskCompletionSource.TrySetResult(true);
        }

        public void Dispose()
        {
            _lingerTimeoutCancellationTokenSource.Cancel();
            _lingerTimeoutCancellationTokenSource.Dispose();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAbortRequestedCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"LINGERING ASSOCIATION";
        }
    }
}
