using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
{
    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAssociationReleasedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAbortRequestedTaskCompletionSource;
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

        public DicomClientReleaseAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationReleaseTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationReleasedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onAbortRequestedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we will automatically send again when there are requests
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _onAssociationReleasedTaskCompletionSource.TrySetResult(true);

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

        private async Task TransitionToRequestAssociationState(DicomClientCancellation cancellation)
        {
            var parameters =
                new DicomClientRequestAssociationState.InitialisationParameters(_initialisationParameters.Connection);
            var state = new DicomClientRequestAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellation);
        }

        private async Task TransitionToAbortState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellation);
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

        public override async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            // We can mostly ignore the cancellation token in this state, unless the cancellation mode is set to "Immediately abort"
            if (cancellation.Token.IsCancellationRequested && cancellation.Mode == DicomClientCancellationMode.ImmediatelyAbortAssociation)
            {
                await TransitionToAbortState(cancellation).ConfigureAwait(false);
                return;
            }

            await _initialisationParameters.Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

            if (cancellation.Mode == DicomClientCancellationMode.ImmediatelyAbortAssociation)
            {
                _disposables.Add(cancellation.Token.Register(() => _onAbortRequestedTaskCompletionSource.TrySetResult(true)));
            }

            var onAssociationRelease = _onAssociationReleasedTaskCompletionSource.Task;
            var onAssociationReleaseTimeout = Task.Delay(_dicomClient.AssociationReleaseTimeoutInMs, _associationReleaseTimeoutCancellationTokenSource.Token);
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onAbort = _onAbortRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                onAssociationRelease,
                onAssociationReleaseTimeout,
                onReceiveAbort,
                onDisconnect,
                onAbort)
                .ConfigureAwait(false);

            if (winner == onAssociationRelease)
            {
                _dicomClient.NotifyAssociationReleased();

                if (!cancellation.Token.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
                {
                    _dicomClient.Logger.Debug($"[{this}] More requests need to be sent after association release, creating new association");
                    await TransitionToRequestAssociationState(cancellation).ConfigureAwait(false);
                }
                else
                {
                    _dicomClient.Logger.Debug(cancellation.Token.IsCancellationRequested
                        ? $"[{this}] Cancellation requested, disconnecting..."
                        : $"[{this}] No more requests to be sent, disconnecting...");
                    await TransitionToCompletedState(cancellation).ConfigureAwait(false);
                }
            }
            else if (winner == onAssociationReleaseTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Association release timed out, aborting...");
                await TransitionToAbortState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Association aborted, disconnecting...");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellation).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected during association release, cleaning up...");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested during association release, immediately aborting association");
                await TransitionToAbortState(cancellation);
            }
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return Task.FromResult(0);
        }

        public override string ToString()
        {
            return $"RELEASING ASSOCIATION";
        }

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            _associationReleaseTimeoutCancellationTokenSource.Dispose();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
            _onAssociationReleasedTaskCompletionSource.TrySetCanceled();
            _onAbortRequestedTaskCompletionSource.TrySetCanceled();
        }
    }
}
