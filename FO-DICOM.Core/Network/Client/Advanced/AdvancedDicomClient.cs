// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <inheritdoc cref="IAdvancedDicomClient"/>
    public class AdvancedDicomClient : IAdvancedDicomClient
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly ILogger _logger;

        public AdvancedDicomClient(IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory, ILogger logger)
        {
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc cref="IAdvancedDicomClient.OpenConnectionAsync"/>
        public async Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var networkStreamOptions = request.NetworkStreamCreationOptions;

            _logger.Debug("Opening connection to {Host}:{Port}", networkStreamOptions.Host, networkStreamOptions.Port);

            return await _advancedDicomClientConnectionFactory.ConnectAsync(request, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IAdvancedDicomClient.OpenAssociationAsync"/>
        public async Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(IAdvancedDicomClientConnection connection, AdvancedDicomClientAssociationRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            cancellationToken.ThrowIfCancellationRequested();

            _logger.Debug("Sending association request from {CallingAE} to {CalledAE}", request.CallingAE, request.CalledAE);

            await connection.SendAssociationRequestAsync(ToDicomAssociation(connection, request)).ConfigureAwait(false);

            await foreach (var @event in connection.Callbacks.GetEvents(cancellationToken).ConfigureAwait(false))
            {
                switch (@event)
                {
                    case DicomAssociationAcceptedEvent dicomAssociationAcceptedEvent:
                        {
                            _logger.Debug("Association request from {CallingAE} to {CalledAE} has been accepted", request.CallingAE, request.CalledAE);

                            return new AdvancedDicomClientAssociation(connection, dicomAssociationAcceptedEvent.Association, _logger);
                        }
                    case DicomAssociationRejectedEvent dicomAssociationRejectedEvent:
                        {
                            _logger.Debug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it", request.CallingAE, request.CalledAE,
                                request.CalledAE);

                            throw new DicomAssociationRejectedException(dicomAssociationRejectedEvent.Result, dicomAssociationRejectedEvent.Source,
                                dicomAssociationRejectedEvent.Reason);
                        }
                    case DicomAbortedEvent dicomAbortedEvent:
                        {
                            _logger.Debug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it", request.CallingAE, request.CalledAE,
                                request.CalledAE);

                            throw new DicomAssociationAbortedException(dicomAbortedEvent.Source, dicomAbortedEvent.Reason);
                        }
                    case ConnectionClosedEvent connectionClosedEvent:
                        {
                            _logger.Debug("Association request from {CallingAE} to {CalledAE} failed because the connection was closed", request.CallingAE, request.CalledAE);

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

            throw new DicomNetworkException("Failed to open a DICOM association. That's all we know.");
        }

        private static DicomAssociation ToDicomAssociation(IAdvancedDicomClientConnection connection, AdvancedDicomClientAssociationRequest request)
        {
            var dicomAssociation = new DicomAssociation(request.CallingAE, request.CalledAE)
            {
                Options = connection.Options,
                MaxAsyncOpsInvoked = request.MaxAsyncOpsInvoked,
                MaxAsyncOpsPerformed = request.MaxAsyncOpsPerformed,
                MaximumPDULength = connection.Options.MaxPDULength,
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