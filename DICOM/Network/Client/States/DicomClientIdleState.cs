using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is doing nothing. Call SendAsync to begin
    /// </summary>
    public class DicomClientIdleState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private int _sendCalled;

        public DicomClientIdleState(DicomClient dicomClient, InitialisationParameters initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
        }

        public class InitialisationParameters
        {
        }

        private async Task TransitionToConnectState(DicomClientCancellation cancellation)
        {
            var state = new DicomClientConnectState(_dicomClient);

            await _dicomClient.Transition(state, cancellation).ConfigureAwait(false);
        }

        public async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            if (!cancellation.Token.IsCancellationRequested
                && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _)
                && Interlocked.CompareExchange(ref _sendCalled, 1, 0) == 0)
            {
                _dicomClient.Logger.Debug($"[{this}] More requests to send (and no cancellation requested yet), automatically opening new association");
                await TransitionToConnectState(cancellation).ConfigureAwait(false);
                return;
            }

            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug($"[{this}] Cancellation requested, staying idle");
            }
            else
            {
                _dicomClient.Logger.Debug($"[{this}] No requests to send, staying idle");
            }
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return CompletedTaskProvider.CompletedTask;
        }

        public async Task SendAsync(DicomClientCancellation cancellation)
        {
            if (Interlocked.CompareExchange(ref _sendCalled, 1, 0) != 0)
            {
                _dicomClient.Logger.Warn($"[{this}] Called SendAsync more than once, ignoring subsequent calls");
                return;
            }
            await TransitionToConnectState(cancellation).ConfigureAwait(false);
        }

        public Task AbortAsync()
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnReceiveAssociationReleaseResponseAsync()
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnConnectionClosedAsync(Exception exception)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnSendQueueEmptyAsync()
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public override string ToString()
        {
            return $"IDLE";
        }

        public void Dispose()
        {
        }
    }
}
