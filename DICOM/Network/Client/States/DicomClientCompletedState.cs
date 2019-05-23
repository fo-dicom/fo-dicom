using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client has completed its work. If no errors happened, it will transition back to 'idle', otherwise it will stop here.
    /// </summary>
    public class DicomClientCompletedState : IDicomClientState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private int _sendCalled;

        public abstract class InitialisationParameters
        {

        }

        public class DicomClientCompletedWithoutErrorInitialisationParameters : InitialisationParameters
        {
            public IDicomClientConnection Connection { get; }

            public DicomClientCompletedWithoutErrorInitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public class DicomClientCompletedWithErrorInitialisationParameters : InitialisationParameters
        {
            public IDicomClientConnection Connection { get; }
            public Exception ExceptionToThrow { get; }

            public DicomClientCompletedWithErrorInitialisationParameters(Exception exceptionToThrow)
            {
                ExceptionToThrow = exceptionToThrow ?? throw new ArgumentNullException(nameof(exceptionToThrow));
            }

            public DicomClientCompletedWithErrorInitialisationParameters(Exception exceptionToThrow, IDicomClientConnection connection)
                : this(exceptionToThrow)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        public DicomClientCompletedState(DicomClient dicomClient, InitialisationParameters initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
        }

        private async Task Cleanup(IDicomClientConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            var listener = connection.Listener;
            try
            {
                connection.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn($"[{this}] DicomService could not be disposed properly: " + e);
            }

            try
            {
                connection.NetworkStream?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn($"[{this}] NetworkStream could not be disposed properly: " + e);
            }

            // wait until listener task realizes connection is gone
            if (listener != null)
            {
                await listener.ConfigureAwait(false);
            }
        }

        private async Task TransitionToIdleState(DicomClientCancellation cancellation)
        {
            var parameters = new DicomClientIdleState.InitialisationParameters();
            await _dicomClient.Transition(new DicomClientIdleState(_dicomClient, parameters), cancellation).ConfigureAwait(false);
        }

        public async Task OnEnterAsync(DicomClientCancellation cancellation)
        {
            switch (_initialisationParameters)
            {
                case DicomClientCompletedWithoutErrorInitialisationParameters parameters:
                {
                    _dicomClient.Logger.Debug($"[{this}] DICOM client completed without errors");

                    if (parameters.Connection != null)
                    {
                        _dicomClient.Logger.Debug($"[{this}] We still have an active connection, cleaning that up now");
                        await Cleanup(parameters.Connection).ConfigureAwait(false);
                    }

                    await TransitionToIdleState(cancellation);

                    break;
                }

                case DicomClientCompletedWithErrorInitialisationParameters parameters:
                {
                    _dicomClient.Logger.Debug($"[{this}] DICOM client completed with an error");

                    if (parameters.Connection != null)
                    {
                        _dicomClient.Logger.Debug($"[{this}] An error occurred while we had an active connection, cleaning that up first");
                        await Cleanup(parameters.Connection).ConfigureAwait(false);
                    }
                    else
                    {
                        _dicomClient.Logger.Warn($"[{this}] An error occurred and no active connection was detected, so no cleanup will happen!");
                    }

                    throw parameters.ExceptionToThrow;
                }
            }
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return CompletedTaskProvider.CompletedTask;
        }

        public async Task SendAsync(DicomClientCancellation cancellation)
        {
            if (!(_initialisationParameters is DicomClientCompletedWithErrorInitialisationParameters))
            {
                _dicomClient.Logger.Warn($"[{this}] Called SendAsync during COMPLETED (without errors) state but this is unnecessary, the DicomClient will automatically" +
                    "transition back to IDLE in a moment");
                return;
            }

            if (Interlocked.CompareExchange(ref _sendCalled, 1, 0) != 0)
            {
                _dicomClient.Logger.Warn($"[{this}] Called SendAsync more than once, ignoring subsequent calls");
                return;
            }

            await TransitionToIdleState(cancellation).ConfigureAwait(false);
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
            return $"COMPLETED";
        }

        public void Dispose()
        {
        }
    }
}
