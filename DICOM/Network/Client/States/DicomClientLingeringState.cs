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
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
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
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
            _onRequestAddedTaskCompletionSource = new TaskCompletionSource<bool>();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we will automatically send again if there are requests
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

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation is requested, releasing association immediately");
                await TransitionToReleaseAssociationState(cancellationToken);
                return;
            }

            var onRequestIsAdded = _onRequestAddedTaskCompletionSource.Task;
            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onLingerTimeout = Task.Delay(_dicomClient.AssociationLingerTimeoutInMs, _lingerTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(onRequestIsAdded, onReceiveAbort, onDisconnect, onLingerTimeout)
                .ConfigureAwait(false);

            if (winner == onRequestIsAdded)
            {
                _dicomClient.Logger.Debug($"[{this}] A new request was added, reusing association within linger timeout");
                await TransitionToSendingRequestsState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onLingerTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Linger timed out, releasing association");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association was aborted during linger");
                var associationAbortedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Warn($"[{this}] Disconnected during linger");
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

            _onRequestAddedTaskCompletionSource.TrySetResult(true);
        }

        public void Dispose()
        {
            _lingerTimeoutCancellationTokenSource.Cancel();
            _lingerTimeoutCancellationTokenSource.Dispose();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"LINGERING ASSOCIATION";
        }
    }
}
