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
    /// <summary>
    /// Represents an advanced DICOM client that exposes the highest amount of manual control over the underlying DICOM communication when sending DICOM requests<br/>
    /// Using an advanced DICOM client, it is possible to manage the lifetime of a DICOM connection + association in a fine-grained manner and send any number of DICOM requests over it<br/>
    /// <br/>
    /// The regular DICOM client is completely built on top of this advanced DICOM client.<br/>
    /// Be aware that consumers of the advanced DICOM client are left to their own devices to handle the fine details of interacting with other PACS software.<br/>
    /// Here is an incomplete sample of the things the regular DICOM client will do for you:<br/>
    /// - Enforce a maximum amount of requests per association<br/>
    /// - Keep an association alive for a certain amount of time to allow more requests to be sent<br/>
    /// - Automatically open more associations while more requests are enqueued<br/>
    /// - Automatically negotiate presentation contexts based on the requests that are enqueued<br/>
    /// <br/>
    /// This advanced DICOM client is offered to expert users of the Fellow Oak DICOM library.<br/>
    /// If you do not consider yourself such an expert, please reconsider the compatibility of the regular DicomClient with your use case.
    /// </summary>
    public interface IAdvancedDicomClient
    {
        /// <summary>
        /// Opens a new TCP connection to another AE using the parameters provided in the connection <paramref name="request"/><br/>
        /// WARNING: you cannot reuse a single connection for multiple associations
        /// </summary>
        /// <param name="request">The connection request that specifies the details of the connection that should be opened</param>
        /// <param name="cancellationToken">The token that will cancel the opening of the connection</param>
        /// <returns>A new instance of <see cref="IAdvancedDicomClientConnection"/></returns>
        Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Opens a new DICOM association over an existing TCP connection
        /// </summary>
        /// <param name="connection">The open connection that will be used to send the association request</param>
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
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(IAdvancedDicomClientConnection connection, AdvancedDicomClientAssociationRequest request,
            CancellationToken cancellationToken);
    }

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
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

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
                            var result = dicomAssociationRejectedEvent.Result;
                            var source = dicomAssociationRejectedEvent.Source;
                            var reason = dicomAssociationRejectedEvent.Reason;

                            _logger.Debug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it: {Result} {Source} {Reason}",
                                request.CallingAE, request.CalledAE, request.CalledAE, result, source, reason);

                            throw new DicomAssociationRejectedException(result, source, reason);
                        }
                    case DicomAbortedEvent dicomAbortedEvent:
                        {
                            var source = dicomAbortedEvent.Source;
                            var reason = dicomAbortedEvent.Reason;

                            _logger.Debug("Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it: {Source} {Reason}",
                                request.CallingAE, request.CalledAE, request.CalledAE, source, reason);

                            throw new DicomAssociationAbortedException(source, reason);
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