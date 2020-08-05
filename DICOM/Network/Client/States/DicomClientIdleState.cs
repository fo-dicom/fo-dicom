// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is doing nothing. Call SendAsync to begin
    /// </summary>
    public class DicomClientIdleState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private int _sendCalled;

        public DicomClientIdleState(DicomClient dicomClient)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
        }

        public async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            if (!cancellation.Token.IsCancellationRequested
                && _dicomClient.QueuedRequests.TryPeek(out StrongBox<DicomRequest> _)
                && Interlocked.CompareExchange(ref _sendCalled, 1, 0) == 0)
            {
                _dicomClient.Logger.Debug($"[{this}] More requests to send (and no cancellation requested yet), automatically opening new association");
                return await _dicomClient.TransitionToConnectState(cancellation).ConfigureAwait(false);
            }

            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Debug($"[{this}] Cancellation requested, staying idle");
                return this;
            }

            _dicomClient.Logger.Debug($"[{this}] No requests to send, staying idle");
            return this;
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
            await _dicomClient.TransitionToConnectState(cancellation).ConfigureAwait(false);
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

        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
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
