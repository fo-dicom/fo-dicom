using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.Advanced.Events;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    public class AdvancedDicomClient : IAdvancedDicomClient
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly ILogger _logger;

        public AdvancedDicomClient(IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory, ILogger logger)
        {
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            cancellationToken.ThrowIfCancellationRequested();

            IAdvancedDicomClientConnection connection = null;
            bool disposeConnection = true;
            try
            {
                var connectionToOpen = request.Connection;
                var networkStreamOptions = connectionToOpen.NetworkStreamCreationOptions;
                
                _logger.Debug("Opening connection to {Host}:{Port}", networkStreamOptions.Host, networkStreamOptions.Port);
                
                connection = await _advancedDicomClientConnectionFactory.ConnectAsync(connectionToOpen, cancellationToken).ConfigureAwait(false);

                _logger.Debug("Sending association request from {CallingAE} to {CalledAE}", request.CallingAE, request.CalledAE);

                await connection.SendAssociationRequestAsync(ToDicomAssociation(request)).ConfigureAwait(false);

                var associationRequestTimeout = request.AssociationRequestTimeout; 

                using var timeoutCts = new CancellationTokenSource(associationRequestTimeout);
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

                await foreach (var @event in connection.Callbacks.GetEvents(cts.Token))
                {
                    switch (@event)
                    {
                        case DicomAssociationAcceptedEvent dicomAssociationAcceptedEvent:
                            disposeConnection = false;
                            return new AdvancedDicomClientAssociation(request, connection, dicomAssociationAcceptedEvent.Association);
                        case DicomAssociationRejectedEvent dicomAssociationRejectedEvent:
                            throw new DicomAssociationRejectedException(dicomAssociationRejectedEvent.Result, dicomAssociationRejectedEvent.Source,
                                dicomAssociationRejectedEvent.Reason);
                        case DicomAbortedEvent dicomAbortedEvent:
                            throw new DicomAssociationAbortedException(dicomAbortedEvent.Source, dicomAbortedEvent.Reason);
                        case ConnectionClosedEvent connectionClosedEvent:
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
            finally
            {
                if (disposeConnection)
                {
                    connection?.Dispose();
                }
            }

            throw new DicomNetworkException("Failed to open a DICOM association. That's all we know.");
        }

        private static DicomAssociation ToDicomAssociation(AdvancedDicomClientAssociationRequest request)
        {
            var dicomAssociation = new DicomAssociation(request.CallingAE, request.CalledAE)
            {
                Options = request.Connection.DicomServiceOptions,
                MaxAsyncOpsInvoked = request.MaxAsyncOpsInvoked,
                MaxAsyncOpsPerformed = request.MaxAsyncOpsPerformed,
                MaximumPDULength = request.Connection.DicomServiceOptions.MaxPDULength,
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