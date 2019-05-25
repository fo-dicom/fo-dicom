using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
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

        private async Task<IDicomClientState> TransitionToRequestAssociationState(IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            return await _dicomClient.Transition(requestAssociationState, cancellation).ConfigureAwait(false);
        }

        private async Task<IDicomClientState> TransitionToIdleState(DicomClientCancellation cancellation)
        {
            return await _dicomClient.Transition(new DicomClientIdleState(_dicomClient), cancellation);
        }

        private async Task<IDicomClientState> TransitionToCompletedWithoutErrorState(IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(null)
                : new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(connection);
            return await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task<IDicomClientState> TransitionToCompletedWithErrorState(Exception exception, IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception)
                : new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, connection);
            return await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task<IDicomClientConnection> Connect(DicomClientCancellation cancellation)
        {
            return await Task.Run<IDicomClientConnection>(() =>
            {
                var host = _dicomClient.Host;
                var port = _dicomClient.Port;
                var useTls = _dicomClient.UseTls;
                var millisecondsTimeout = _dicomClient.AssociationRequestTimeoutInMs;
                var noDelay = _dicomClient.Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
                var ignoreSslPolicyErrors = _dicomClient.Options?.IgnoreSslPolicyErrors ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;

                var networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

                var connection = new DicomClientConnection(_dicomClient, networkStream);

                if (_dicomClient.Options != null)
                    connection.Options = _dicomClient.Options;

                if (!cancellation.Token.IsCancellationRequested)
                {
                    connection.StartListener();
                }

                return connection;
            }, cancellation.Token).ConfigureAwait(false);
        }

        public async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested, won't connect");
                return await TransitionToIdleState(cancellation).ConfigureAwait(false);
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

                    return await TransitionToRequestAssociationState(connection, cancellation).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return await TransitionToCompletedWithErrorState(e, connection, cancellation).ConfigureAwait(false);
                }
            }

            if(winner != cancel)
                throw new Exception("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);

            // Cancellation or abort was triggered but wait for the connection anyway, because we need to dispose of it properly
            try
            {
                var connection = await connect.ConfigureAwait(false);
                return await TransitionToCompletedWithoutErrorState(connection, cancellation).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return await TransitionToCompletedWithErrorState(e, null, cancellation).ConfigureAwait(false);
            }
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return CompletedTaskProvider.CompletedTask;
        }

        public Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we're already connecting
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

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _cancellationRequestedTaskCompletionSource.TrySetCanceledAsynchronously();
        }

        public override string ToString()
        {
            return $"CONNECTING";
        }
    }
}
