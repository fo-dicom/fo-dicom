using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is connected and has an active DICOM association. Every queued request will be sent one by one.
    /// </summary>
    public class DicomClientSendingRequestsState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _sendRequestsCancellationTokenSource;
        private readonly CancellationTokenSource _keepTryingToSendRequestsCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<SendQueueEmptyEvent> _onSendQueueEmptyTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onCancellationTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAbortRequestedTaskCompletionSource;
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
            _keepTryingToSendRequestsCancellationTokenSource = new CancellationTokenSource();
            _onSendQueueEmptyTaskCompletionSource = new TaskCompletionSource<SendQueueEmptyEvent>();
            _onAbortReceivedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
            _onCancellationTaskCompletionSource = new TaskCompletionSource<bool>();
            _onAbortRequestedTaskCompletionSource = new TaskCompletionSource<bool>();
            _disposables = new List<IDisposable>();
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we're already sending
            return Task.FromResult(0);
        }

        public override Task AbortAsync()
        {
            _onAbortRequestedTaskCompletionSource.TrySetResult(true);

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
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we did not expect this!");
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
            _onSendQueueEmptyTaskCompletionSource.TrySetResult(new SendQueueEmptyEvent());

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

        private async Task KeepTryingToSendRequests()
        {
            while (!_keepTryingToSendRequestsCancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(1000).ConfigureAwait(false);
                await Connection.SendNextMessageAsync().ConfigureAwait(false);
            }
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation is requested before requests could be sent, releasing association ...");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
                return;
            }

            _disposables.Add(cancellationToken.Register(() => _onCancellationTaskCompletionSource.TrySetResult(true)));
            _disposables.Add(cancellationToken.Register(() => _sendRequestsCancellationTokenSource.Cancel()));
            _disposables.Add(cancellationToken.Register(() => _keepTryingToSendRequestsCancellationTokenSource.Cancel()));

            _dicomClient.Logger.Debug($"[{this}] Sending queued DICOM requests");

            var onAbortRequested = _onAbortRequestedTaskCompletionSource.Task;
            var sendRequests = SendRequests().ContinueWith(_ => KeepTryingToSendRequests(), TaskContinuationOptions.OnlyOnRanToCompletion);

            var sendOrAbort = await Task.WhenAny(onAbortRequested, sendRequests).ConfigureAwait(false);

            if (sendOrAbort == onAbortRequested)
            {
                _sendRequestsCancellationTokenSource.Cancel();
                _keepTryingToSendRequestsCancellationTokenSource.Cancel();
            }

            _dicomClient.Logger.Debug($"[{this}] Requests are sent, waiting for next event now");

            var sendQueueIsEmpty = _onSendQueueEmptyTaskCompletionSource.Task;
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onCancellation = _onCancellationTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                    sendQueueIsEmpty,
                    onReceiveAbort,
                    onDisconnect,
                    onCancellation,
                    onAbortRequested
                )
                .ConfigureAwait(false);

            if (winner == sendQueueIsEmpty)
            {
                _dicomClient.Logger.Debug($"[{this}] DICOM client send queue is empty, going to linger association now...");
                await TransitionToLingerState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onCancellation)
            {
                _dicomClient.Logger.Debug($"[{this}] DICOM client cancellation requested while sending requests, releasing association...");
                await TransitionToReleaseAssociationState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted while sending requests, cleaning up...");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] DICOM client disconnected while sending requests, cleaning up...");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onAbortRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Abort requested while sending requests, immediately aborting association");
                await TransitionToAbortState(cancellationToken).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }

            _sendRequestsCancellationTokenSource.Cancel();
            _sendRequestsCancellationTokenSource.Dispose();
            _keepTryingToSendRequestsCancellationTokenSource.Cancel();
            _keepTryingToSendRequestsCancellationTokenSource.Dispose();

            _onCancellationTaskCompletionSource.TrySetCanceled();
            _onSendQueueEmptyTaskCompletionSource.TrySetCanceled();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAbortRequestedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"SENDING REQUESTS";
        }
    }
}
