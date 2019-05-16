using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is connecting to the DICOM server.
    /// </summary>
    public class DicomClientConnectState : IDicomClientState, IDisposable
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

        private async Task TransitionToRequestAssociationState(IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            await _dicomClient.Transition(requestAssociationState, cancellation).ConfigureAwait(false);
        }

        private async Task TransitionToIdleState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientIdleState.InitialisationParameters();
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToCompletedWithoutErrorState(IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(null)
                : new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, IDicomClientConnection connection, DicomClientCancellation cancellation)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception)
                : new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellation);
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

        public async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested, won't connect");
                await TransitionToIdleState(cancellation).ConfigureAwait(false);
                return;
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

                    await TransitionToRequestAssociationState(connection, cancellation).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await TransitionToCompletedWithErrorState(e, connection, cancellation).ConfigureAwait(false);
                }
            }
            else
            {
                // Cancellation or abort was triggered but wait for the connection anyway, because we need to dispose of it properly
                try
                {
                    var connection = await connect.ConfigureAwait(false);
                    if (winner == cancel)
                    {
                        await TransitionToCompletedWithoutErrorState(connection, cancellation).ConfigureAwait(false);
                    }
                }
                catch(Exception e)
                {
                    await TransitionToCompletedWithErrorState(e, null, cancellation).ConfigureAwait(false);
                }
            }
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we're already connecting
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAssociationReleaseResponseAsync()
        {
            return Task.FromResult(0);
        }

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            return Task.FromResult(0);
        }

        public Task OnConnectionClosedAsync(Exception exception)
        {
            return Task.FromResult(0);
        }

        public Task OnSendQueueEmptyAsync()
        {
            return Task.FromResult(0);
        }

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _cancellationRequestedTaskCompletionSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"CONNECTING";
        }
    }
}
