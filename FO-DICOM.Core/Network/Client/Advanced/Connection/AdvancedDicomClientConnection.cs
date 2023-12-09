// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Client.Advanced.Association;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// Represents an open DICOM connection, which contains two things:<br/>
    /// 1. An open TCP socket to the port where the other AE is running<br/>
    /// 2. A DICOM listener that collects incoming DICOM messages over this socket<br/>
    /// </summary>
    public interface IAdvancedDicomClientConnection : IDicomClientConnection
    {
        /// <summary>
        /// Gets an event collector for the DICOM events that occur on this connection  
        /// </summary>
        internal IAdvancedDicomClientConnectionEventCollector EventCollector { get; }

        /// <summary>
        /// Opens a new DICOM association over an existing TCP connection
        /// </summary>
        /// <param name="request">The request that specifies how the association should be opened</param>
        /// <param name="cancellationToken">
        /// The token that will cancel the opening of the association.
        /// Depending on the timing, this may leave the connection unusable, it is safest to always reopen a new connection if cancellation occurred.
        /// </param>
        /// <returns>The opened association if the other AE accepted the association request</returns>
        /// <exception cref="DicomAssociationRejectedException">When the association is rejected</exception>
        /// <exception cref="DicomAssociationAbortedException">When the association is aborted prematurely</exception>
        /// <exception cref="DicomNetworkException">When the connection is lost without an underlying IO exception</exception>
        /// <exception cref="System.IO.IOException">When the connection is lost</exception>
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken);
    }

    /// <inheritdoc cref="IAdvancedDicomClientConnection" />
    internal class AdvancedDicomClientConnection : DicomService, IAdvancedDicomClientConnection
    {
        private readonly ILogger _logger;
        private readonly IAdvancedDicomClientConnectionEventCollector _eventCollector;
        private int _isAssociationOpened;
        public INetworkStream NetworkStream { get; }
        public Task Listener { get; private set; }
        public new bool IsSendNextMessageRequired => base.IsSendNextMessageRequired;
        IAdvancedDicomClientConnectionEventCollector IAdvancedDicomClientConnection.EventCollector => _eventCollector;

        public AdvancedDicomClientConnection(
            IAdvancedDicomClientConnectionEventCollector eventCollector,
            INetworkStream networkStream,
            Encoding fallbackEncoding,
            DicomServiceOptions dicomServiceOptions,
            ILogger logger,
            DicomServiceDependencies dependencies) : base(networkStream, fallbackEncoding, logger, dependencies)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Options = dicomServiceOptions ?? throw new ArgumentNullException(nameof(dicomServiceOptions));
            _eventCollector = eventCollector ?? throw new ArgumentNullException(nameof(eventCollector));
            NetworkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream));
        }

        public void StartListener()
        {
            if (Listener != null)
            {
                return;
            }

            Listener = Task.Run(RunAsync);
        }

        public new Task SendAssociationRequestAsync(DicomAssociation association) => base.SendAssociationRequestAsync(association);
        public new Task SendAssociationReleaseRequestAsync() => base.SendAssociationReleaseRequestAsync();
        public new Task SendAbortAsync(DicomAbortSource source, DicomAbortReason reason) => base.SendAbortAsync(source, reason);
        public new Task SendRequestAsync(DicomRequest request) => base.SendRequestAsync(request);
        public new Task SendNextMessageAsync() => base.SendNextMessageAsync();

        protected override Task OnSendQueueEmptyAsync() => _eventCollector.OnSendQueueEmptyAsync();
        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association) => _eventCollector.OnReceiveAssociationAcceptAsync(association);

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) =>
            _eventCollector.OnReceiveAssociationRejectAsync(result, source, reason);

        public Task OnReceiveAssociationReleaseResponseAsync() => _eventCollector.OnReceiveAssociationReleaseResponseAsync();
        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason) => _eventCollector.OnReceiveAbortAsync(source, reason);
        public Task OnConnectionClosedAsync(Exception exception) => _eventCollector.OnConnectionClosedAsync(exception);
        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => _eventCollector.OnRequestCompletedAsync(request, response);
        public Task OnRequestPendingAsync(DicomRequest request, DicomResponse response) => _eventCollector.OnRequestPendingAsync(request, response);
        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => _eventCollector.OnRequestTimedOutAsync(request, timeout);
        public Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request) => _eventCollector.OnCStoreRequestAsync(request);
        public Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request) => _eventCollector.OnNEventReportRequestAsync(request);

        public async Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken)
        {
            if (Interlocked.CompareExchange(ref _isAssociationOpened, 1, 0) != 0)
            {
                throw new DicomNetworkException("A connection can only be used once for one association. Create a new connection to open another association");
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.UserIdentityNegotiation != null)
            {
                request.UserIdentityNegotiation.Validate();
            }

            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogDebug("Sending association request from {CallingAE} to {CalledAE}", request.CallingAE, request.CalledAE);

            await SendAssociationRequestAsync(ToDicomAssociation(request)).ConfigureAwait(false);

            await foreach (var @event in _eventCollector.GetEvents(cancellationToken).ConfigureAwait(false))
            {
                switch (@event)
                {
                    case DicomAssociationAcceptedEvent dicomAssociationAcceptedEvent:
                        {
                            _logger.LogDebug("Association request from {CallingAE} to {CalledAE} has been accepted", request.CallingAE, request.CalledAE);

                            return new AdvancedDicomClientAssociation(this, dicomAssociationAcceptedEvent.Association, _logger);
                        }
                    case DicomAssociationRejectedEvent dicomAssociationRejectedEvent:
                        {
                            var result = dicomAssociationRejectedEvent.Result;
                            var source = dicomAssociationRejectedEvent.Source;
                            var reason = dicomAssociationRejectedEvent.Reason;

                            _logger.LogDebug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it: {Result} {Source} {Reason}",
                                request.CallingAE, request.CalledAE, request.CalledAE, result, source, reason);

                            throw new DicomAssociationRejectedException(result, source, reason);
                        }
                    case DicomAbortedEvent dicomAbortedEvent:
                        {
                            var source = dicomAbortedEvent.Source;
                            var reason = dicomAbortedEvent.Reason;

                            _logger.LogDebug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it: {Source} {Reason}",
                                request.CallingAE, request.CalledAE, request.CalledAE, source, reason);

                            throw new DicomAssociationAbortedException(source, reason);
                        }
                    case ConnectionClosedEvent connectionClosedEvent:
                        {
                            _logger.LogDebug("Association request from {CallingAE} to {CalledAE} failed because the connection was closed", request.CallingAE, request.CalledAE);

                            if (connectionClosedEvent.Exception != null)
                            {
                                ExceptionDispatchInfo.Capture(connectionClosedEvent.Exception).Throw();
                            }
                            else
                            {
                                throw new DicomNetworkException("Connection was lost before an association could be established");
                            }

                            break;
                        }
                }
            }

            throw new DicomNetworkException("Failed to open a DICOM association because the connection is already closed");
        }

        private DicomAssociation ToDicomAssociation(AdvancedDicomClientAssociationRequest request)
        {
            var dicomAssociation = new DicomAssociation(request.CallingAE, request.CalledAE)
            {
                RemoteHost = NetworkStream.RemoteHost,
                RemotePort = NetworkStream.RemotePort,
                Options = Options,
                MaxAsyncOpsInvoked = request.MaxAsyncOpsInvoked,
                MaxAsyncOpsPerformed = request.MaxAsyncOpsPerformed,
                MaximumPDULength = Options.MaxPDULength,
                UserIdentityNegotiation = request.UserIdentityNegotiation
            };

            foreach (var presentationContext in request.PresentationContexts)
            {
                dicomAssociation.PresentationContexts.Add(presentationContext);
            }

            foreach (var extendedNegotiation in request.ExtendedNegotiations)
            {
                dicomAssociation.ExtendedNegotiations.Add(extendedNegotiation);
            }

            return dicomAssociation;
        }
    }
}