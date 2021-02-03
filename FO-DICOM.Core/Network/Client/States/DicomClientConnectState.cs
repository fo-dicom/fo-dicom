// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client.Tasks;

namespace FellowOakDicom.Network.Client.States
{

    /// <summary>
    /// The DICOM client is connecting to the DICOM server.
    /// </summary>
    public class DicomClientConnectState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private readonly TaskCompletionSource<bool> _cancellationRequestedTaskCompletionSource;
        private readonly List<IDisposable> _disposables;

        public DicomClientConnectState(DicomClient dicomClient)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _cancellationRequestedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
        }

        private async Task<IDicomClientConnection> Connect(DicomClientCancellation cancellation) =>
            await Task.Run<IDicomClientConnection>(() =>
            {
                var host = _dicomClient.Host;
                var port = _dicomClient.Port;
                var useTls = _dicomClient.UseTls;
                var millisecondsTimeout = _dicomClient.ClientOptions.AssociationRequestTimeoutInMs;
                var noDelay = _dicomClient.ServiceOptions.TcpNoDelay;
                var ignoreSslPolicyErrors = _dicomClient.ServiceOptions.IgnoreSslPolicyErrors;

                var networkStream = _dicomClient.NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

                var connection = new DicomClientConnection(_dicomClient, networkStream);

                if (_dicomClient.ServiceOptions != null)
                {
                    connection.Options = _dicomClient.ServiceOptions;
                }

                if (!cancellation.Token.IsCancellationRequested)
                {
                    connection.StartListener();
                }

                return connection;
            }, cancellation.Token).ConfigureAwait(false);

        public async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested, won't connect");
                return await _dicomClient.TransitionToIdleState(cancellation).ConfigureAwait(false);
            }

            _disposables.Add(cancellation.Token.Register(() => _cancellationRequestedTaskCompletionSource.SetResult(true)));

            var connect = Connect(cancellation);
            var cancel = _cancellationRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(connect, cancel).ConfigureAwait(false);

            if (winner == connect)
            {
                IDicomClientConnection connection = null;
                try
                {
                    connection = await connect.ConfigureAwait(false);

                    return await _dicomClient.TransitionToRequestAssociationState(connection, cancellation).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return await _dicomClient.TransitionToCompletedWithErrorState(connection, e, cancellation).ConfigureAwait(false);
                }
            }

            if (winner != cancel)
            {
                throw new DicomNetworkException("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);
            }

            // Cancellation or abort was triggered but wait for the connection anyway, because we need to dispose of it properly
            try
            {
                var connection = await connect.ConfigureAwait(false);
                return await _dicomClient.TransitionToCompletedState(connection, cancellation).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return await _dicomClient.TransitionToCompletedWithErrorState((IDicomClientConnection) null, e, cancellation).ConfigureAwait(false);
            }
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return Task.CompletedTask;
        }

        // Ignore, we're already connecting
        public Task SendAsync(DicomClientCancellation cancellation) => Task.CompletedTask;

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association) => Task.CompletedTask;

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) => Task.CompletedTask;

        public Task OnReceiveAssociationReleaseResponseAsync() => Task.CompletedTask;

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason) => Task.CompletedTask;

        public Task OnConnectionClosedAsync(Exception exception) => Task.CompletedTask;

        public Task OnSendQueueEmptyAsync() => Task.CompletedTask;

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => Task.CompletedTask;

        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => Task.CompletedTask;

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _cancellationRequestedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString() => $"CONNECTING";
    }
}
