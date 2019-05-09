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
            public CancellationToken AbortToken { get; }

            public InitialisationParameters(CancellationToken abortToken)
            {
                AbortToken = abortToken;
            }
        }

        private async Task TransitionToConnectState(CancellationToken cancellationToken)
        {
            var state = new DicomClientConnectState(_dicomClient);

            await _dicomClient.Transition(state, cancellationToken).ConfigureAwait(false);
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            if (!_initialisationParameters.AbortToken.IsCancellationRequested
                && !cancellationToken.IsCancellationRequested
                && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _)
                && Interlocked.CompareExchange(ref _sendCalled, 1, 0) == 0)
            {
                _dicomClient.Logger.Debug($"[{this}] More requests to send (and no cancellation requested yet), automatically opening new association");
                await TransitionToConnectState(cancellationToken).ConfigureAwait(false);
                return;
            }

            if (_initialisationParameters.AbortToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug($"[{this}] Abort requested, staying idle");
            }
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug($"[{this}] Cancellation requested, staying idle");
            }
            else
            {
                _dicomClient.Logger.Debug($"[{this}] No requests to send, staying idle");
            }
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public async Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Interlocked.CompareExchange(ref _sendCalled, 1, 0) != 0)
            {
                _dicomClient.Logger.Warn($"[{this}] Called SendAsync more than once, ignoring subsequent calls");
                return;
            }
            await TransitionToConnectState(cancellationToken).ConfigureAwait(false);
        }

        public Task AbortAsync()
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationAccept(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReleaseResponse()
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnConnectionClosed(Exception exception)
        {
            return Task.FromResult(0);
        }

        public Task OnSendQueueEmpty()
        {
            return Task.FromResult(0);
        }

        public override string ToString()
        {
            return $"IDLE";
        }
    }
}
