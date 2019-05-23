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
        private readonly TaskCompletionSource<AllRequestsHaveCompletedEvent> _allRequestsHaveCompletedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onCancellationTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;

        private event EventHandler TriggerSendMoreRequests;

        /// <summary>
        /// This is the little flag that could
        /// It will happily switch to 1 when this DicomClient state is actively sending requests
        /// When its hard work is -temporarily- finished (and we wait for responses to come in), it will lower the flag back to 0
        /// </summary>
        private int _sendingRequests = 0;

        /// <summary>
        /// Since SendQueueEmpty is unreliable in heavy loads, we must keep our own track of pending requests. This is rather easy to do,
        /// as we're the ones sending the requests, and we get a callback for each completed request
        /// </summary>
        private readonly ConcurrentDictionary<int, DicomRequest> _pendingRequests;

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
            _allRequestsHaveCompletedTaskCompletionSource = TaskCompletionSourceFactory.Create<AllRequestsHaveCompletedEvent>();
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onCancellationTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
            _pendingRequests = new ConcurrentDictionary<int, DicomRequest>();
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));

            TriggerSendMoreRequests?.Invoke(this, EventArgs.Empty);

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
            if (!_dicomClient.QueuedRequests.IsEmpty)
            {
                TriggerSendMoreRequests?.Invoke(this, EventArgs.Empty);
                return CompletedTaskProvider.CompletedTask;
            }

            if (_pendingRequests.IsEmpty)
            {
                _allRequestsHaveCompletedTaskCompletionSource.TrySetResultAsynchronously(new AllRequestsHaveCompletedEvent());
            }

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            _pendingRequests.TryRemove(request.MessageID, out DicomRequest _);

            if (!_dicomClient.QueuedRequests.IsEmpty)
            {
                TriggerSendMoreRequests?.Invoke(this, EventArgs.Empty);
                return CompletedTaskProvider.CompletedTask;
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
            var parameters = new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, _initialisationParameters.Connection);
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

                await Connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);

                _pendingRequests.TryAdd(dicomRequest.MessageID, dicomRequest);

                queuedItem.Value = null;
            }
        }

        private async Task SendInitialRequests()
        {
            // Don't spam the connection unnecessarily, there's locks in there!
            if (Interlocked.CompareExchange(ref _sendingRequests, 1, 0) != 0)
                return;

            try
            {
                await SendRequests().ConfigureAwait(false);
            }
            finally
            {
                Interlocked.Exchange(ref _sendingRequests, 0);
            }
        }

        /// <summary>
        /// This is an event handler that triggers in certain circumstances that require us to manually send more requests:
        ///     - More requests are added to the DicomClient
        ///     - A pending request has completed, perhaps freeing up an async operations slot on the association
        ///
        /// Because this is an event handler, it has to be async void, but that suits our use case rather neatly, because we don't want to hold hostage
        /// the places where these events come from (i.e. the listener in DicomService, or user code which adds requests)
        /// </summary>
        private async void SendMoreRequests(object sender, EventArgs eventArgs)
        {
            if (_sendRequestsCancellationTokenSource.IsCancellationRequested)
                return;

            // Don't spam the connection unnecessarily, there's locks in there!
            if (Interlocked.CompareExchange(ref _sendingRequests, 1, 0) != 0)
                return;

            try
            {
                _dicomClient.Logger.Info($"[{this}] Sending more requests");

                if (!_dicomClient.QueuedRequests.IsEmpty)
                {
                    await SendRequests().ConfigureAwait(false);
                }
                else
                {
                    // Attempt to flush existing requests from the queue
                    await Connection.SendNextMessageAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                Interlocked.Exchange(ref _sendingRequests, 0);
            }
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

            _dicomClient.Logger.Debug($"[{this}] Sending queued DICOM requests");

            await SendInitialRequests().ConfigureAwait(false);

            TriggerSendMoreRequests += SendMoreRequests;

            _dicomClient.Logger.Debug($"[{this}] Requests are all queued for sending");

            var sendQueueIsEmpty = _allRequestsHaveCompletedTaskCompletionSource.Task;
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onCancellation = _onCancellationTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                    sendQueueIsEmpty,
                    onReceiveAbort,
                    onDisconnect,
                    onCancellation
                )
                .ConfigureAwait(false);

            if (winner == sendQueueIsEmpty)
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
            TriggerSendMoreRequests -= SendMoreRequests;

            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }

            _sendRequestsCancellationTokenSource.Cancel();
            _sendRequestsCancellationTokenSource.Dispose();

            _onCancellationTaskCompletionSource.TrySetCanceledAsynchronously();
            _allRequestsHaveCompletedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortReceivedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onConnectionClosedTaskCompletionSource.TrySetCanceledAsynchronously();
        }

        public override string ToString()
        {
            return $"SENDING REQUESTS";
        }
    }
}
