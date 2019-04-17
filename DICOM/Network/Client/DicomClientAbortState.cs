using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    public class DicomClientAbortState : DicomClientWithConnectionState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<Exception> _onConnectionClosedTaskCompletionSource;
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
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<(DicomAbortSource Source, DicomAbortReason Reason)>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<Exception>();
            _associationAbortTimeoutCancellationTokenSource = new CancellationTokenSource();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default)
        {
            // Ignore, we're aborting now
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we were just in the process of aborting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we were just in the process of aborting");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we are already in the process of aborting!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAssociationAbortedTaskCompletionSource.TrySetResult((source, reason));

            return Task.FromResult(0);
        }

        public override Task OnConnectionClosed(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResult(exception);

            return Task.FromResult(0);
        }

        public override Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        private async Task TransitionToCompletedState(CancellationToken cancellationToken)
        {
            var initialisationParameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, initialisationParameters), cancellationToken);
        }

        public override async Task OnEnter(CancellationToken cancellationToken)
        {
            await _initialisationParameters.Connection.SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified).ConfigureAwait(false);

            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onTimeout = Task.Delay(100, _associationAbortTimeoutCancellationTokenSource.Token);

            var winner = await Task.WhenAny(onReceiveAbort, onDisconnect, onTimeout).ConfigureAwait(false);

            if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Received abort after aborting. Neat.");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected after aborting. Perfect.");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Abort request timed out. Disconnecting...");
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
        }

        public override string ToString()
        {
            return $"ABORTING ASSOCIATION";
        }

        public void Dispose()
        {
            _associationAbortTimeoutCancellationTokenSource.Cancel();
            _associationAbortTimeoutCancellationTokenSource.Dispose();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
        }
    }
}
