using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
{
    public class DicomClientAbortState : DicomClientWithConnectionState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly CancellationTokenSource _associationAbortTimeoutCancellationTokenSource;

        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection;
            }
        }

        public DicomClientAbortState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _associationAbortTimeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we're aborting now
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
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

        private async Task TransitionToCompletedState(DicomClientCancellation cancellation)
        {
            var initialisationParameters = new DicomClientCompletedState
                .DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, initialisationParameters), cancellation);
        }

        public override async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            var sendAbort = _initialisationParameters.Connection.SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onTimeout = Task.Delay(100, _associationAbortTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(sendAbort, onReceiveAbort, onDisconnect, onTimeout).ConfigureAwait(false);

            if (winner == sendAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Abort notification sent to server. Disconnecting...");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
            if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Received abort while aborting. Neat.");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected while aborting. Perfect.");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
            else if (winner == onTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Abort notification timed out. Disconnecting...");
                await TransitionToCompletedState(cancellation).ConfigureAwait(false);
            }
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override string ToString()
        {
            return $"ABORTING ASSOCIATION";
        }

        public void Dispose()
        {
            _associationAbortTimeoutCancellationTokenSource.Cancel();
            _associationAbortTimeoutCancellationTokenSource.Dispose();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }
    }
}
