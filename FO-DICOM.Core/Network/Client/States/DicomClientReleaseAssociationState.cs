// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client.Events;
using FellowOakDicom.Network.Client.Tasks;

namespace FellowOakDicom.Network.Client.States
{

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState
    {
        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationReleaseTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAssociationReleasedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onAbortRequestedTaskCompletionSource;
        private readonly List<IDisposable> _disposables;

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
            _onAssociationReleasedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onAbortRequestedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we will automatically send again when there are requests
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association accept but we were just in the processing of releasing the association!");
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _dicomClient.Logger.Warn($"[{this}] Received association reject but we were just in the processing of releasing the association!");
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _onAssociationReleasedTaskCompletionSource.TrySetResultAsynchronously(true);

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAbortReceivedTaskCompletionSource.TrySetResultAsynchronously(new DicomAbortedEvent(source, reason));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnConnectionClosedAsync(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResultAsynchronously(new ConnectionClosedEvent(exception));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnSendQueueEmptyAsync()
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
        {
            return CompletedTaskProvider.CompletedTask;
        }

        public override async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            // We can mostly ignore the cancellation token in this state, unless the cancellation mode is set to "Immediately abort"
            if (cancellation.Token.IsCancellationRequested && cancellation.Mode == DicomClientCancellationMode.ImmediatelyAbortAssociation)
            {
                return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            await _initialisationParameters.Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

            if (cancellation.Mode == DicomClientCancellationMode.ImmediatelyAbortAssociation)
            {
                _disposables.Add(cancellation.Token.Register(() => _onAbortRequestedTaskCompletionSource.TrySetResultAsynchronously(true)));
            }

            var onAssociationRelease = _onAssociationReleasedTaskCompletionSource.Task;
            var onAssociationReleaseTimeout = Task.Delay(_dicomClient.AssociationReleaseTimeoutInMs, _associationReleaseTimeoutCancellationTokenSource.Token);
            var onReceiveAbort = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var onAbort = _onAbortRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                onAssociationRelease,
                onAssociationReleaseTimeout,
                onReceiveAbort,
                onDisconnect,
                onAbort)
                .ConfigureAwait(false);

            if (winner == onAssociationRelease)
            {
                _dicomClient.NotifyAssociationReleased();

                _dicomClient.Logger.Debug($"[{this}] Association release response received, disconnecting...");
                return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            if (winner == onAssociationReleaseTimeout)
            {
                _dicomClient.Logger.Debug($"[{this}] Association release timed out, aborting...");
                return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            if (winner == onReceiveAbort)
            {
                _dicomClient.Logger.Debug($"[{this}] Association aborted, disconnecting...");
                var abortReceivedResult = onReceiveAbort.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
            }

            if (winner == onDisconnect)
            {
                _dicomClient.Logger.Debug($"[{this}] Disconnected during association release, cleaning up...");

                var connectionClosedEvent = await onDisconnect.ConfigureAwait(false);
                if (connectionClosedEvent.Exception == null)
                {
                    return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
                }
                else
                {
                    return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, connectionClosedEvent.Exception, cancellation);
                }
            }

            if (winner == onAbort)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested during association release, immediately aborting association");
                return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation);
            }

            throw new DicomNetworkException("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return CompletedTaskProvider.CompletedTask;
        }

        public override string ToString()
        {
            return $"RELEASING ASSOCIATION";
        }

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _associationReleaseTimeoutCancellationTokenSource.Cancel();
            _associationReleaseTimeoutCancellationTokenSource.Dispose();
            _onConnectionClosedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortReceivedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAssociationReleasedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortRequestedTaskCompletionSource.TrySetCanceledAsynchronously();
        }
    }
}
