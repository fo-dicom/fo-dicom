// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.States
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
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var listener = connection.Listener;
            try
            {
                connection.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn($"[{this}] The listener for incoming DICOM communication could not be disposed properly: " + e);
            }

            try
            {
                connection.NetworkStream?.Dispose();
            }
            catch (Exception e)
            {
                _dicomClient.Logger.Warn($"[{this}] The connection network stream could not be disposed properly: " + e);
            }

            // wait until listener task realizes connection is gone
            if (listener != null)
            {
                await listener.ConfigureAwait(false);
            }
        }

        public async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            switch (_initialisationParameters)
            {
                case DicomClientCompletedWithoutErrorInitialisationParameters parameters:
                {
                    _dicomClient.Logger.Debug($"[{this}] DICOM client completed without errors");

                    if (parameters.Connection != null)
                    {
                        _dicomClient.Logger.Debug($"[{this}] Cleaning up");
                        await Cleanup(parameters.Connection).ConfigureAwait(false);
                    }

                    return await _dicomClient.TransitionToIdleState(cancellation).ConfigureAwait(false);
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

                    ExceptionDispatchInfo.Capture(parameters.ExceptionToThrow).Throw();
                    return null; // We will never get here
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(_initialisationParameters), "Unknown initialisation parameters");
            }
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return Task.CompletedTask;
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

            await _dicomClient.TransitionToIdleState(cancellation).ConfigureAwait(false);
        }

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association) => Task.CompletedTask;

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) => Task.CompletedTask;

        public Task OnReceiveAssociationReleaseResponseAsync() => Task.CompletedTask;

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason) => Task.CompletedTask;

        public Task OnConnectionClosedAsync(Exception exception) => Task.CompletedTask;

        public Task OnSendQueueEmptyAsync() => Task.CompletedTask;

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => Task.CompletedTask;

        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => Task.CompletedTask;

        public override string ToString() => $"COMPLETED";

        public void Dispose()
        {
        }
    }
}
