using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is connecting to the DICOM server.
    /// </summary>
    public class DicomClientConnectState : IDicomClientState, IDisposable
    {
        private readonly DicomClient _dicomClient;
        private readonly TaskCompletionSource<bool> _cancellationRequested;
        private readonly List<IDisposable> _disposables;

        public DicomClientConnectState(DicomClient dicomClient)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _cancellationRequested = new TaskCompletionSource<bool>();
            _disposables = new List<IDisposable>();
        }

        private async Task TransitionToRequestAssociationState(IDicomClientConnection connection, CancellationToken cancellationToken)
        {
            var initialisationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection);

            var requestAssociationState = new DicomClientRequestAssociationState(_dicomClient, initialisationParameters);

            await _dicomClient.Transition(requestAssociationState, cancellationToken).ConfigureAwait(false);
        }

        private async Task TransitionToIdleState(CancellationToken cancellationToken)
        {
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient), cancellationToken);
        }

        private async Task TransitionToCompletedWithoutErrorState(IDicomClientConnection connection, CancellationToken cancellationToken)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(null)
                : new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        private async Task TransitionToCompletedWithErrorState(Exception exception, IDicomClientConnection connection, CancellationToken cancellationToken)
        {
            var parameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception)
                : new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, connection);
            await _dicomClient.Transition(new DicomClientCompletedState(_dicomClient, parameters), cancellationToken);
        }

        private async Task<IDicomClientConnection> Connect(CancellationToken cancellationToken)
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

                if (!cancellationToken.IsCancellationRequested)
                {
                    connection.StartListener();
                }

                return connection;
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task OnEnter(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested, won't connect");
                await TransitionToIdleState(cancellationToken).ConfigureAwait(false);
                return;
            }

            _disposables.Add(cancellationToken.Register(() => _cancellationRequested.SetResult(true)));

            var connect = Connect(cancellationToken);
            var cancel = _cancellationRequested.Task;

            var winner = await Task.WhenAny(connect, cancel).ConfigureAwait(false);

            if (winner == connect)
            {
                IDicomClientConnection connection = null;
                try
                {
                    connection = await connect.ConfigureAwait(false);

                    await TransitionToRequestAssociationState(connection, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await TransitionToCompletedWithErrorState(e, connection, cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                // Cancellation was triggered but wait for the connection anyway, because we need to dispose of it properly
                try
                {
                    var connection = await connect.ConfigureAwait(false);
                    await TransitionToCompletedWithoutErrorState(connection, cancellationToken).ConfigureAwait(false);
                }
                catch(Exception e)
                {
                    await TransitionToCompletedWithErrorState(e, null, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Ignore, we're already connecting
            return Task.FromResult(0);
        }

        public Task AbortAsync()
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _cancellationRequested.TrySetCanceled();
        }

        public override string ToString()
        {
            return $"CONNECTING";
        }
    }
}
