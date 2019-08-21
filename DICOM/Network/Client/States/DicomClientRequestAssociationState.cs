// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Network.Client.Events;
using Dicom.Network.Client.Tasks;

namespace Dicom.Network.Client.States
{
    /// <summary>
    /// The DICOM client is connected to the server and requires an association. When transitioning into this state, a new association request will be sent
    /// </summary>
    public class DicomClientRequestAssociationState : DicomClientWithConnectionState
    {
        public class InitialisationParameters : IInitialisationWithConnectionParameters
        {
            public IDicomClientConnection Connection { get; set; }

            public InitialisationParameters(IDicomClientConnection connection)
            {
                Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            }
        }

        private readonly DicomClient _dicomClient;
        private readonly InitialisationParameters _initialisationParameters;
        private readonly CancellationTokenSource _associationRequestTimeoutCancellationTokenSource;
        private readonly TaskCompletionSource<DicomAssociationAcceptedEvent> _onAssociationAcceptedTaskCompletionSource;
        private readonly TaskCompletionSource<DicomAssociationRejectedEvent> _onAssociationRejectedTaskCompletionSource;
        private readonly TaskCompletionSource<DicomAbortedEvent> _onAbortReceivedTaskCompletionSource;
        private readonly TaskCompletionSource<ConnectionClosedEvent> _onConnectionClosedTaskCompletionSource;
        private readonly TaskCompletionSource<bool> _onCancellationRequestedTaskCompletionSource;
        private readonly IList<IDisposable> _disposables;

        public DicomClientRequestAssociationState(DicomClient dicomClient, InitialisationParameters initialisationParameters) : base(initialisationParameters)
        {
            _dicomClient = dicomClient ?? throw new ArgumentNullException(nameof(dicomClient));
            _initialisationParameters = initialisationParameters ?? throw new ArgumentNullException(nameof(initialisationParameters));
            _associationRequestTimeoutCancellationTokenSource = new CancellationTokenSource();
            _onAssociationAcceptedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAssociationAcceptedEvent>();
            _onAssociationRejectedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAssociationRejectedEvent>();
            _onAbortReceivedTaskCompletionSource = TaskCompletionSourceFactory.Create<DicomAbortedEvent>();
            _onConnectionClosedTaskCompletionSource = TaskCompletionSourceFactory.Create<ConnectionClosedEvent>();
            _onCancellationRequestedTaskCompletionSource = TaskCompletionSourceFactory.Create<bool>();
            _disposables = new List<IDisposable>();
        }

        public override Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            _onAssociationAcceptedTaskCompletionSource.TrySetResultAsynchronously(new DicomAssociationAcceptedEvent(association));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _onAssociationRejectedTaskCompletionSource.TrySetResultAsynchronously(new DicomAssociationRejectedEvent(result, source, reason));

            return CompletedTaskProvider.CompletedTask;
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we're still making a new association!");
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

        private async Task SendAssociationRequest()
        {
            var associationToRequest = new DicomAssociation(_dicomClient.CallingAe, _dicomClient.CalledAe)
            {
                MaxAsyncOpsInvoked = _dicomClient.AsyncInvoked,
                MaxAsyncOpsPerformed = _dicomClient.AsyncPerformed,
                RemoteHost = _initialisationParameters.Connection.NetworkStream.RemoteHost,
                RemotePort = _initialisationParameters.Connection.NetworkStream.RemotePort,
                MaximumPDULength = _dicomClient.Options?.MaxPDULength ?? DicomServiceOptions.Default.MaxPDULength
            };

            foreach (var queuedItem in _dicomClient.QueuedRequests.ToList())
            {
                var dicomRequest = queuedItem.Value;
                associationToRequest.PresentationContexts.AddFromRequest(dicomRequest);
                associationToRequest.ExtendedNegotiations.AddFromRequest(dicomRequest);
            }

            foreach (var context in _dicomClient.AdditionalPresentationContexts)
            {
                associationToRequest.PresentationContexts.Add(
                    context.AbstractSyntax,
                    context.UserRole,
                    context.ProviderRole,
                    context.GetTransferSyntaxes().ToArray());
            }
            
            foreach (var extendedNegotiation in _dicomClient.AdditionalExtendedNegotiations)
            {
                associationToRequest.ExtendedNegotiations.AddOrUpdate(
                    extendedNegotiation.SopClassUid,
                    extendedNegotiation.RequestedApplicationInfo,
                    extendedNegotiation.ServiceClassUid,
                    extendedNegotiation.RelatedGeneralSopClasses.ToArray());
            }

            await Connection.SendAssociationRequestAsync(associationToRequest).ConfigureAwait(false);
        }

        public override async Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation)
        {
            if (cancellation.Token.IsCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested before association request was made, going to disconnect now");
                return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            await SendAssociationRequest().ConfigureAwait(false);

            _disposables.Add(cancellation.Token.Register(() => _onCancellationRequestedTaskCompletionSource.TrySetResultAsynchronously(true)));

            var associationIsAccepted = _onAssociationAcceptedTaskCompletionSource.Task;
            var associationIsRejected = _onAssociationRejectedTaskCompletionSource.Task;
            var abortReceived = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var associationRequestTimesOut = Task.Delay(_dicomClient.AssociationRequestTimeoutInMs, _associationRequestTimeoutCancellationTokenSource.Token);
            var onCancellationRequested = _onCancellationRequestedTaskCompletionSource.Task;

            var winner = await Task.WhenAny(
                associationIsAccepted,
                associationIsRejected,
                abortReceived,
                onDisconnect,
                associationRequestTimesOut,
                onCancellationRequested
            ).ConfigureAwait(false);

            if (winner == associationIsAccepted)
            {
                _dicomClient.Logger.Debug($"[{this}] Association is accepted");
                var association = associationIsAccepted.Result.Association;
                _dicomClient.NotifyAssociationAccepted(new EventArguments.AssociationAcceptedEventArgs(association));
                return await _dicomClient.TransitionToSendingRequestsState(_initialisationParameters, association, cancellation).ConfigureAwait(false);
            }

            if (winner == associationIsRejected)
            {
                var associationRejectedResult = associationIsRejected.Result;

                _dicomClient.Logger.Warn($"[{this}] Association is rejected");
                var result = associationRejectedResult.Result;
                var source = associationRejectedResult.Source;
                var reason = associationRejectedResult.Reason;
                _dicomClient.NotifyAssociationRejected(new EventArguments.AssociationRejectedEventArgs(result, source, reason));
                var exception = new DicomAssociationRejectedException(result, source, reason);
                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
            }

            if (winner == abortReceived)
            {
                _dicomClient.Logger.Warn($"[{this}] Association is aborted");
                var abortReceivedResult = abortReceived.Result;
                var exception = new DicomAssociationAbortedException(abortReceivedResult.Source, abortReceivedResult.Reason);
                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
            }

            if (winner == onDisconnect)
            {
                _dicomClient.Logger.Warn($"[{this}] Disconnected");
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

            if (winner == associationRequestTimesOut)
            {
                _dicomClient.Logger.Warn($"[{this}] Association request timeout");
                return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            if (winner == onCancellationRequested)
            {
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested while requesting association, aborting now...");
                return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            throw new DicomNetworkException("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return CompletedTaskProvider.CompletedTask;
        }

        public override Task SendAsync(DicomClientCancellation cancellation)
        {
            // Ignore, we're already busy trying to send
            return CompletedTaskProvider.CompletedTask;
        }

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _onConnectionClosedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAbortReceivedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAssociationAcceptedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onAssociationRejectedTaskCompletionSource.TrySetCanceledAsynchronously();
            _onCancellationRequestedTaskCompletionSource.TrySetCanceledAsynchronously();
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            _associationRequestTimeoutCancellationTokenSource.Dispose();
        }

        public override string ToString()
        {
            return $"REQUESTING ASSOCIATION";
        }
    }
}
