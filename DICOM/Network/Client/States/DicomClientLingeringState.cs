using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// When we have an active association, but no more DICOM requests to send (and all responses for previous requests have already arrived)
    /// we will keep the association open for just a little longer in case we get any more requests
    /// </summary>
    public class DicomClientLingeringState : DicomClientWithAssociationState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _lingerTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onRequestAddedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onCancellationRequestedTaskCompletionSource;
        private readonly List<IDisposable> _disposables;

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
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onRequestAddedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _onCancellationRequestedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we will automatically send again if there are requests
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we already have an active association!");
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we already have an active association!");
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release but we did not expect this!");
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAbortReceivedTaskCompletionSource.TrySetResultAsynchronously(new DicomAbortedEvent(source, reason));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnConnectionClosedAsync(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResultAsynchronously(new ConnectionClosedEvent(exception));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnSendQueueEmptyAsync()
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        private async Task TransitionToSendingRequestsState(DicomClientCancellation cancellation)
        {
            var sendRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var sendRequestsState = new DicomClientSendingRequestsState(_dicomClient, sendRequestsParameters);
            await _dicomClient.Transition(sendRequestsState, cancellation);
        }

        private async Task TransitionToReleaseAssociationState(DicomClientCancellation cancellation)
        {
            var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(_initialisationParameters.Association, _initialisationParameters.Connection);
            var releaseAssociationState = new DicomClientReleaseAssociationState(_dicomClient, releaseAssociationParameters);

            await _dicomClient.Transition(releaseAssociationState, cancellation);
        }

        private async Task TransitionToCompletedState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToAbortState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellation);
        }

        private async Task OnCancel(DicomClientCancellation cancellation)
        {
            switch (cancellation.Mode)
            {
                case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                    _dicomClient.Logger.Warn($"[{this}] Cancellation requested, immediately aborting association");
                    await TransitionToAbortState(cancellation).ConfigureAwait(false);
                    break;
                case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                default:
                    _dicomClient.Logger.Warn($"[{this}] Cancellation requested, immediately releasing association");
                    await TransitionToReleaseAssociationState(cancellation).ConfigureAwait(false);
                    break;
            }
        }

        public override async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                await OnCancel(cancellation).ConfigureAwait(false);
                return;
            }

            if (_dicomClient.QueuedRequests.Any())
            {
                _dicomClient.Logger.Debug($"[{this}] DICOM client already has requests again, immediately moving back to sending requests");
                await TransitionToSendingRequestsState(cancellation).ConfigureAwait(false);
                return;
            }

            _disposables.Add(cancellation.Token.Register(() => _onCancellationRequestedTaskCompletionSource.TrySetResultAsynchronously(true)));

            var onRequestIsAdded = _onRequestAddedTaskCompletionSource.Task;
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onLingerTimeout = Task.Delay(_dicomClient.AssociationLingerTimeoutInMs, _lingerTimeoutCancellationTokenSource.Token);
            var onCancel = _onCancellationRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(onRequestIsAdded, onReceiveAbort, onDisconnect, onLingerTimeout, onCancel)
                .ConfigureAwait(false);

            if (winner == onRequestIsAdded)
            {
                _dicomClient.Logger.Debug($"[{this}] A new request was added before linger timeout of {_dicomClient.AssociationLingerTimeoutInMs}ms, reusing association");
                await TransitionToSendingRequestsState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onLingerTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Linger timed out after {_dicomClient.AssociationLingerTimeoutInMs}ms, releasing association");
                await TransitionToReleaseAssociationState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association was aborted while lingering the association");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellation).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Warn($"[{this}] Disconnected while lingering the association");
                var connectionClosedEvent = await onDisconnect.ConfigureAwait(false);
                if (connectionClosedEvent.Exception == null)
                {
                    await TransitionToCompletedState(cancellation).ConfigureAwait(false);
                }
                else
                {
                    await TransitionToCompletedWithErrorState(connectionClosedEvent.Exception, cancellation);
                }
            }
            else if (winner == onCancel)
            {
                await OnCancel(cancellation).ConfigureAwait(false);
                return;
            }
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            _onRequestAddedTaskCompletionSource.TrySetResultAsynchronously(true);

            return CompletedTaskProvider.CompletedTask;
        }

        public override void Dispose()
        {
            foreach(var disposable in _disposables)
                disposable.Dispose();

            _lingerTimeoutCancellationTokenSource.Cancel();
            _lingerTimeoutCancellationTokenSource.Dispose();
            _onRequestAddedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortReceivedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onConnectionClosedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onCancellationRequestedTaskCompletionSource.TrySetCanceledAsynchronously();
        }

        public override string ToString()
        {
            return $"LINGERING ASSOCIATION";
        }
    }
}
