// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.EventArguments;
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
        private readonly TaskCompletionSource<AllRequestsHaveCompletedEvent> _allRequestsHaveCompletedTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;
        private readonly Tasks.AsyncManualResetEvent _sendMoreRequests;
        private readonly ConcurrentDictionary<int, DicomRequest> _pendingRequests;
        private readonly ConcurrentBag<DicomRequest> _sentRequests;
        private readonly int _maximumNumberOfRequestsPerAssociation;

        /**
         * Safety flag that prevents parallel sending of DICOM requests
         */
        private int _sending;

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
            _allRequestsHaveCompletedTaskCompletionSource = TaskCompletionSourceFactory.Create<AllRequestsHaveCompletedEvent>();
            _disposables = new List<IDisposable>();
            _pendingRequests = new ConcurrentDictionary<int, DicomRequest>();
            _sentRequests = new ConcurrentBag<DicomRequest>();
            _sendMoreRequests = new Tasks.AsyncManualResetEvent(set: true);
            _maximumNumberOfRequestsPerAssociation = _dicomClient.MaximumNumberOfRequestsPerAssociation ?? int.MaxValue;
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

        private void RemoveRequestFromPendingList(DicomRequest request)
        {
            _pendingRequests.TryRemove(request.MessageID, out DicomRequest _);

            if (_pendingRequests.IsEmpty)
            {
                if (_dicomClient.QueuedRequests.IsEmpty || _sentRequests.Count >= _maximumNumberOfRequestsPerAssociation)
                {
                    _allRequestsHaveCompletedTaskCompletionSource.TrySetResultAsynchronously(new AllRequestsHaveCompletedEvent());
                }
                else
                {
                    _sendMoreRequests.Set();
                }
            } 
            else if (Connection.IsSendNextMessageRequired)
            {
                _sendMoreRequests.Set();
            }
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            RemoveRequestFromPendingList(request);

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
        {
            RemoveRequestFromPendingList(request);

            _dicomClient.NotifyRequestTimedOut(new RequestTimedOutEventArgs(request, timeout));

            return CompletedTaskProvider.CompletedTask;
        }

        private async Task SendRequests()
        {
            while (!_sendRequestsCancellationTokenSource.IsCancellationRequested
                   && _sentRequests.Count < _maximumNumberOfRequestsPerAssociation
                   && _dicomClient.QueuedRequests.TryDequeue(out StrongBox<DicomRequest> queuedItem))
            {
                var dicomRequest = queuedItem.Value;

                if (dicomRequest == null) continue;

                queuedItem.Value = null;

                _pendingRequests[dicomRequest.MessageID] = dicomRequest;

                _sentRequests.Add(dicomRequest);

                await Connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);
            }
        }

        private async Task SendInitialRequests()
        {
            _sending = 1;

            await SendRequests().ConfigureAwait(false);

            Interlocked.Exchange(ref _sending, 0);
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
                var allRequestsHaveCompleted = _allRequestsHaveCompletedTaskCompletionSource.Task;

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
                        if (_sentRequests.Count >= _maximumNumberOfRequestsPerAssociation)
                        {
                            _dicomClient.Logger.Debug($"[{this}] DICOM client has reached the maximum number of requests for this association and is still waiting for the sent requests to complete");
                        }
                        else
                        {
                            _dicomClient.Logger.Debug($"[{this}] DICOM client has more queued requests, sending those now...");

                            await SendRequests().ConfigureAwait(false);
                        }
                    }

                    if (Connection.IsSendNextMessageRequired)
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

        public override async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                switch (cancellation.Mode)
                {
                    case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation is requested before requests could be sent, releasing association ...");
                        return await _dicomClient.TransitionToReleaseAssociationState(_initialisationParameters, cancellation).ConfigureAwait(false);
                    case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation is requested before requests could be sent, aborting association ...");
                        return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cancellation.Mode), cancellation.Mode, "Unknown cancellation mode");
                }
            }

            _disposables.Add(cancellation.Token.Register(() => _sendRequestsCancellationTokenSource.Cancel()));
            _disposables.Add(cancellation.Token.Register(() => _onCancellationTaskCompletionSource.TrySetResultAsynchronously(true)));

            _dicomClient.Logger.Debug($"[{this}] Sending DICOM requests");

            await SendInitialRequests().ConfigureAwait(false);

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
                if (_sentRequests.Count < _maximumNumberOfRequestsPerAssociation)
                {
                    _dicomClient.Logger.Debug($"[{this}] All requests are done, going to linger association now...");
                    return await _dicomClient.TransitionToLingerState(_initialisationParameters, cancellation).ConfigureAwait(false);
                }

                _dicomClient.Logger.Debug($"[{this}] The maximum number of requests per association ({_maximumNumberOfRequestsPerAssociation}) have been sent, immediately releasing association now...");
                return await _dicomClient.TransitionToReleaseAssociationState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            if (winner == onCancellation)
            {
                switch (cancellation.Mode)
                {
                    case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation requested, releasing association...");
                        return await _dicomClient.TransitionToReleaseAssociationState(_initialisationParameters, cancellation).ConfigureAwait(false);
                    case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                        _dicomClient.Logger.Warn($"[{this}] Cancellation requested, aborting association...");
                        return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cancellation.Mode), cancellation.Mode, "Unknown cancellation mode");
                }
            }

            if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted while sending requests, cleaning up...");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
            }

            if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected while sending requests, cleaning up...");
                var connectionClosedEvent = await onDisconnect.ConfigureAwait(false);
                if (connectionClosedEvent.Exception == null)
                {
                    return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
                }
                else
                {
                    return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, connectionClosedEvent.Exception, cancellation);
                }
            }

            throw new DicomNetworkException("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);
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
            _allRequestsHaveCompletedTaskCompletionSource.TrySetCanceledAsynchronously();

            _sendMoreRequests.Dispose();
        }

        public override string ToString()
        {
            return $"SENDING REQUESTS";
        }
    }
}
