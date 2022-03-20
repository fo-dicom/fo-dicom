// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client.Events;
using FellowOakDicom.Network.Client.Tasks;

namespace FellowOakDicom.Network.Client.States
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
            _onAssociationAcceptedTaskCompletionSource.TrySetResult(new DicomAssociationAcceptedEvent(association));

            return Task.CompletedTask;
        }

        public override Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            _onAssociationRejectedTaskCompletionSource.TrySetResult(new DicomAssociationRejectedEvent(result, source, reason));

            return Task.CompletedTask;
        }

        public override Task OnReceiveAssociationReleaseResponseAsync()
        {
            _dicomClient.Logger.Warn($"[{this}] Received association release response but we're still making a new association!");
            return Task.CompletedTask;
        }

        public override Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            _onAbortReceivedTaskCompletionSource.TrySetResult(new DicomAbortedEvent(source, reason));

            return Task.CompletedTask;
        }

        public override Task OnConnectionClosedAsync(Exception exception)
        {
            _onConnectionClosedTaskCompletionSource.TrySetResult(new ConnectionClosedEvent(exception));

            return Task.CompletedTask;
        }

        public override Task OnSendQueueEmptyAsync() => Task.CompletedTask;

        public override Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => Task.CompletedTask;

        public override Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => Task.CompletedTask;

        private async Task SendAssociationRequest()
        {
            var associationToRequest = new DicomAssociation(_dicomClient.CallingAe, _dicomClient.CalledAe)
            {
                MaxAsyncOpsInvoked = _dicomClient.AsyncInvoked,
                MaxAsyncOpsPerformed = _dicomClient.AsyncPerformed,
                RemoteHost = _initialisationParameters.Connection.NetworkStream.RemoteHost,
                RemotePort = _initialisationParameters.Connection.NetworkStream.RemotePort,
                Options = _dicomClient.ServiceOptions
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

            _disposables.Add(cancellation.Token.Register(() => _onCancellationRequestedTaskCompletionSource.TrySetResult(true)));

            var associationIsAccepted = _onAssociationAcceptedTaskCompletionSource.Task;
            var associationIsRejected = _onAssociationRejectedTaskCompletionSource.Task;
            var abortReceived = _onAbortReceivedTaskCompletionSource.Task;
            var onDisconnect = _onConnectionClosedTaskCompletionSource.Task;
            var associationRequestTimeoutInMs = _dicomClient.ClientOptions.AssociationRequestTimeoutInMs;
            var associationRequestTimesOut = Task.Delay(associationRequestTimeoutInMs, _associationRequestTimeoutCancellationTokenSource.Token);
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
                _dicomClient.NumberOfConsecutiveTimedOutAssociationRequests = 0;
                _dicomClient.Logger.Debug($"[{this}] Association is accepted");
                var association = associationIsAccepted.Result.Association;
                _dicomClient.NotifyAssociationAccepted(new EventArguments.AssociationAcceptedEventArgs(association));
                return await _dicomClient.TransitionToSendingRequestsState(_initialisationParameters, association, cancellation).ConfigureAwait(false);
            }

            if (winner == associationIsRejected)
            {
                _dicomClient.NumberOfConsecutiveTimedOutAssociationRequests = 0;
                _dicomClient.Logger.Warn($"[{this}] Association is rejected");
                
                var associationRejectedResult = associationIsRejected.Result;
                var result = associationRejectedResult.Result;
                var source = associationRejectedResult.Source;
                var reason = associationRejectedResult.Reason;
                _dicomClient.NotifyAssociationRejected(new EventArguments.AssociationRejectedEventArgs(result, source, reason));
                var exception = new DicomAssociationRejectedException(result, source, reason);
                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
            }

            if (winner == abortReceived)
            {
                _dicomClient.NumberOfConsecutiveTimedOutAssociationRequests = 0;
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

                return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, connectionClosedEvent.Exception, cancellation).ConfigureAwait(false);
            }

            if (winner == associationRequestTimesOut)
            {
                int numberOfConsecutiveTimedOutAssociationRequests = ++_dicomClient.NumberOfConsecutiveTimedOutAssociationRequests;
                int maximumNumberOfConsecutiveTimedOutAssociationRequests = _dicomClient.ClientOptions.MaximumNumberOfConsecutiveTimedOutAssociationRequests;
                _dicomClient.Logger.Warn($"[{this}] Association request timeout (Attempt {numberOfConsecutiveTimedOutAssociationRequests} / {maximumNumberOfConsecutiveTimedOutAssociationRequests})");
                
                _dicomClient.NotifyAssociationRequestTimedOut(new EventArguments.AssociationRequestTimedOutEventArgs(
                    associationRequestTimeoutInMs, numberOfConsecutiveTimedOutAssociationRequests, maximumNumberOfConsecutiveTimedOutAssociationRequests
                ));
                if (numberOfConsecutiveTimedOutAssociationRequests >= maximumNumberOfConsecutiveTimedOutAssociationRequests)
                {
                    _dicomClient.NumberOfConsecutiveTimedOutAssociationRequests = 0;
                    
                    var exception = new DicomAssociationRequestTimedOutException(associationRequestTimeoutInMs, numberOfConsecutiveTimedOutAssociationRequests);
                    return await _dicomClient.TransitionToCompletedWithErrorState(_initialisationParameters, exception, cancellation).ConfigureAwait(false);
                }

                return await _dicomClient.TransitionToCompletedState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            if (winner == onCancellationRequested)
            {
                _dicomClient.NumberOfConsecutiveTimedOutAssociationRequests = 0;
                _dicomClient.Logger.Warn($"[{this}] Cancellation requested while requesting association, aborting now...");
                return await _dicomClient.TransitionToAbortState(_initialisationParameters, cancellation).ConfigureAwait(false);
            }

            throw new DicomNetworkException("Unknown winner of Task.WhenAny in DICOM client, this is likely a bug: " + winner);
        }

        public override Task AddRequestAsync(DicomRequest dicomRequest)
        {
            _dicomClient.QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            return Task.CompletedTask;
        }

        // Ignore, we're already busy trying to send
        public override Task SendAsync(DicomClientCancellation cancellation) => Task.CompletedTask;

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _onConnectionClosedTaskCompletionSource.TrySetCanceled();
            _onAbortReceivedTaskCompletionSource.TrySetCanceled();
            _onAssociationAcceptedTaskCompletionSource.TrySetCanceled();
            _onAssociationRejectedTaskCompletionSource.TrySetCanceled();
            _onCancellationRequestedTaskCompletionSource.TrySetCanceled();
            _associationRequestTimeoutCancellationTokenSource.Cancel();
            _associationRequestTimeoutCancellationTokenSource.Dispose();
        }

        public override string ToString() => $"REQUESTING ASSOCIATION";
    }
}
