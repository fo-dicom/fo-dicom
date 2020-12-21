using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Events;
using FellowOakDicom.Network.Client.States;
using System;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClient
    {
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(OpenAssociationRequest request, CancellationToken cancellationToken);
    }

    public class AdvancedDicomClient : IAdvancedDicomClient
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly AdvancedDicomClientOptions _advancedDicomClientOptions;

        public AdvancedDicomClient(
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory,
            AdvancedDicomClientOptions advancedDicomClientOptions
        )
        {
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _advancedDicomClientOptions = advancedDicomClientOptions ?? throw new ArgumentNullException(nameof(advancedDicomClientOptions));
        }

        public async Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(OpenAssociationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IAdvancedDicomClientConnection connection = null;
            bool disposeConnection = true;
            try
            {
                connection = await _advancedDicomClientConnectionFactory.ConnectAsync(request.ConnectionToOpen, cancellationToken).ConfigureAwait(false);

                await connection.SendAssociationRequestAsync(request.AssociationToOpen).ConfigureAwait(false);

                using var timeoutCts = new CancellationTokenSource(_advancedDicomClientOptions.AssociationRequestTimeout);
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

                await foreach (var @event in connection.Callbacks.GetEvents(cts.Token))
                {
                    switch (@event)
                    {
                        case DicomAssociationAcceptedEvent dicomAssociationAcceptedEvent:
                            disposeConnection = false;
                            return new AdvancedDicomClientAssociation(_advancedDicomClientOptions, connection, dicomAssociationAcceptedEvent.Association);
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
    }
}