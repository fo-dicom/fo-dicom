using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;

namespace Dicom.Network.Client.States
{
    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAssociationAbortedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAssociationReleasedTaskCompletionSource;

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
            _onAssociationReleasedTaskCompletionSource = new TaskCompletionSource<bool>();
            _onAssociationAbortedTaskCompletionSource = new TaskCompletionSource<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = new TaskCompletionSource<ConnectionClosedEvent>();
        }

        public override Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we will automatically send again when there are requests
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we were just in the processing of releasing the association!");
            return Task.FromResult(0);
        }

        public override Task OnReceiveAssociationReleaseResponse()
        {
            _onAssociationReleasedTaskCompletionSource.TrySetResult(true);

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

        private async Task TransitionToRequestAssociationState(CancellationToken cancellationToken)
        {
            var parameters =
                new DicomClientRequestAssociationState.InitialisationParameters(_initialisationParameters.Connection);
            var state = new DicomClientRequestAssociationState(_dicomClient, parameters);
            await _dicomClient.Transition(state, cancellationToken);
        }

        private async Task TransitionToAbortState(CancellationToken cancellationToken)
        {
            var parameters = new DicomClientAbortState.InitialisationParameters(_initialisationParameters.Connection);
            await _dicomClient.Transition(new DicomClientAbortState(_dicomClient, parameters), cancellationToken);
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
            // Regardless whether or not cancellation is requested, we'll try to cleanup the association the proper way
            await _initialisationParameters.Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

            var onAssociationRelease = _onAssociationReleasedTaskCompletionSource.Task;
            var onAssociationReleaseTimeout = Task.Delay(_dicomClient.AssociationReleaseTimeoutInMs, _associationReleaseTimeoutCancellationTokenSource.Token);
            var onReceiveAbort = _onAssociationAbortedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                onAssociationRelease,
                onAssociationReleaseTimeout,
                onReceiveAbort,
                onDisconnect)
                .ConfigureAwait(false);

            if (winner == onAssociationRelease)
            {
                _dicomClient.NotifyAssociationReleased();

                if (!cancellationToken.IsCancellationRequested && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _))
                {
                    _dicomClient.Logger.Debug($"[{this}] More requests need to be sent after association release, creating new association");
                    await TransitionToRequestAssociationState(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    _dicomClient.Logger.Debug(cancellationToken.IsCancellationRequested
                        ? $"[{this}] Cancellation requested, disconnecting..."
                        : $"[{this}] No more requests to be sent, disconnecting...");
                    await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
                }
            }
            else if (winner == onAssociationReleaseTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Association release timed out, aborting...");
                await TransitionToAbortState(cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Association aborted, disconnecting...");
                var associationAbortedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(associationAbortedResult.Source, associationAbortedResult.Reason);
                await TransitionToCompletedWithErrorState(exception, cancellationToken).ConfigureAwait(false);
            }
            else if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected during association release, cleaning up...");
                await TransitionToCompletedState(cancellationToken).ConfigureAwait(false);
            }
        }

        public override void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public override string ToString()
        {
            return $"RELEASING ASSOCIATION";
        }

        public void Dispose()
        {
            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            _associationReleaseTimeoutCancellationTokenSource.Dispose();
            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAssociationAbortedTaskCompletionSource.TrySetCanceled();
            _onAssociationReleasedTaskCompletionSource.TrySetCanceled();
        }
    }
}
