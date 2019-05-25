using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is connected and has an active DICOM association. Every queued request will be sent one by one.
    /// </summary>
    public class DicomClientSendingRequestsState : DicomClientWithAssociationState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _sendRequestsCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onCancellationTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _allPendingRequestsHaveCompletedTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;
        private readonly Tasks.AsyncManualResetEvent _sendMoreRequests;
        private readonly ConcurrentDictionary<int, DicomRequest> _pendingRequests;

        /**
         * Safety flag that prevents parallel sending of DICOM requests
         */
        private int _sending = 0;

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
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onCancellationTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _allPendingRequestsHaveCompletedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
            _pendingRequests = new ConcurrentDictionary<int, DicomRequest>();
            _sendMoreRequests = new Tasks.AsyncManualResetEvent(set: true);
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            _sendMoreRequests.Set();

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we're already sending
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
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we did not expect this!");
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
            _sendMoreRequests.Set();

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            _pendingRequests.TryRemove(request.MessageID, out DicomRequest _);

            if (_pendingRequests.IsEmpty)
            {
                if (_dicomClient.QueuedRequests.IsEmpty)
                {
                    _allPendingRequestsHaveCompletedTaskCompletionSource.TrySetResultAsynchronously(true);
                }
                else
                {
                    _sendMoreRequests.Set();
                }
            }

            return CompletedTaskProvider.CompletedTask;
        }

        private async Task TransitionToLingerState(DicomClientCancellation cancellation)
        {
            var lingerParameters = new DicomClientLingeringState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var lingerState = new DicomClientLingeringState(_dicomClient, lingerParameters);
            await _dicomClient.Transition(lingerState, cancellation);
        }

        private async Task TransitionToReleaseAssociationState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientReleaseAssociationState.InitialisationParameters(_initialisationParameters.Association,
                _initialisationParameters.Connection);
            var state = new DicomClientReleaseAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellation);
        }

        private async Task TransitionToCompletedState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, DicomClientCancellation cancellation)
        {
            var parameters =
                new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToAbortState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellation);
        }

        private async Task SendRequests()
        {
            while (!_sendRequestsCancellationTokenSource.IsCancellationRequested &&
                   _dicomClient.QueuedRequests.TryDequeue(out StrongBox<DicomRequest> queuedItem))
            {
                var dicomRequest = queuedItem.Value;

                if (dicomRequest == null) continue;

                _pendingRequests[dicomRequest.MessageID] = dicomRequest;

                await Connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);

                queuedItem.Value = null;
            }
        }

        /// <summary>
        /// Sometimes, we need to manually send more requests:
        ///     - When more requests are added to the DicomClient
        ///     - When a pending request has completed, perhaps freeing up an async operations slot on the association
        /// </summary>
        private async Task KeepSendingUntilAllRequestsHaveCompletedAsync()
        {
            var cancellationTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            using (_sendRequestsCancellationTokenSource.Token.Register(() => cancellationTaskCompletionSource.SetResult(true)))
            {
                /**
                 * This event will be triggered when the DicomClientConnection believes it has finished its work by triggering the OnSendQueueEmpty event
                 */
                var sendMoreRequests = _sendMoreRequests.WaitAsync();

                /**
                 * This event will be triggered when the CancellationToken passed into SendAsync is cancelled
                 */
                var onCancel = cancellationTaskCompletionSource.Task;

                /**
                 * This event will be triggered when the pending queue becomes empty
                 */
                var allRequestsHaveCompleted = _allPendingRequestsHaveCompletedTaskCompletionSource.Task;

                var winner = await Task.WhenAny(allRequestsHaveCompleted, sendMoreRequests, onCancel).ConfigureAwait(false);
                if (winner == allRequestsHaveCompleted || winner == onCancel)
                {
                    return;
                }
            }

            try
            {
                if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                {
                    if (!_dicomClient.QueuedRequests.IsEmpty)
                    {
                        _dicomClient.Logger.Debug($"[{this}] DICOM client has more queued requests, sending those now...");

                        await SendRequests().ConfigureAwait(false);
                    }
                    else if (Connection.IsSendNextMessageRequired)
                    {
                        _dicomClient.Logger.Debug($"[{this}] DICOM client connection still has unsent requests, sending those now...");

                        await Connection.SendNextMessageAsync().ConfigureAwait(false);
                    }

                    Interlocked.Exchange(ref _sending, 0);
                }
            }
            finally
            {
                _sendMoreRequests.Reset();
            }

            await KeepSendingUntilAllRequestsHaveCompletedAsync().ConfigureAwait(false);
        }

        public override async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                switch (cancellation.Mode)
                {
                    case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation is requested before requests could be sent, releasing association ...");
                        await TransitionToReleaseAssociationState(cancellation).ConfigureAwait(false);
                        break;
                    case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation is requested before requests could be sent, aborting association ...");
                        await TransitionToAbortState(cancellation).ConfigureAwait(false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cancellation.Mode), cancellation.Mode, "Unknown cancellation mode");
                }

                return;
            }

            _disposables.Add(cancellation.Token.Register(() => _sendRequestsCancellationTokenSource.Cancel()));
            _disposables.Add(cancellation.Token.Register(() => _onCancellationTaskCompletionSource.TrySetResultAsynchronously(true)));

            _dicomClient.Logger.Debug($"[{this}] Sending DICOM requests");

            _sending = 1;

            await SendRequests().ConfigureAwait(false);

            Interlocked.Exchange(ref _sending, 0);

            var allRequestsHaveCompleted = KeepSendingUntilAllRequestsHaveCompletedAsync();
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onCancellation = _onCancellationTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                    allRequestsHaveCompleted,
                    onReceiveAbort,
                    onDisconnect,
                    onCancellation
                )
                .ConfigureAwait(false);

            if (winner == allRequestsHaveCompleted)
            {
                _dicomClient.Logger.Debug($"[{this}] All requests are done, going to linger association now...");
                await TransitionToLingerState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onCancellation)
            {
                switch (cancellation.Mode)
                {
                    case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation requested, releasing association...");
                        await TransitionToReleaseAssociationState(cancellation).ConfigureAwait(false);
                        break;
                    case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation requested, aborting association...");
                        await TransitionToAbortState(cancellation).ConfigureAwait(false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cancellation.Mode), cancellation.Mode, "Unknown cancellation mode");
                }

                return;
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted while sending requests, cleaning up...");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellation).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected while sending requests, cleaning up...");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
        }

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }

            _sendRequestsCancellationTokenSource.Cancel();
            _sendRequestsCancellationTokenSource.Dispose();

            _onCancellationTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortReceivedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onConnectionClosedTaskCompletionSource.TrySetCanceledAsynchronously();
            _allPendingRequestsHaveCompletedTaskCompletionSource.TrySetCanceledAsynchronously();

            _sendMoreRequests.Dispose();
        }

        public override string ToString()
        {
            return $"SENDING REQUESTS";
        }
    }
}