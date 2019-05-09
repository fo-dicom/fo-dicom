using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.States
{
    public abstract class DicomClientWithConnectionState : IDicomClientState
    {
        protected DicomClientWithConnectionState(IInitialisationWithConnectionParameters parameters)
        {
            Connection = parameters.Connection ?? throw new ArgumentNullException(nameof(IInitialisationWithConnectionParameters.Connection));
        }

        public IDicomClientConnection Connection { get; }

        public abstract Task OnReceiveAssociationAccept(DicomAssociation association);

        public abstract Task OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        public abstract Task OnReceiveAssociationReleaseResponse();

        public abstract Task OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        public abstract Task OnConnectionClosed(Exception exception);

        public abstract Task OnSendQueueEmpty();

        public abstract Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response);

        public abstract Task OnEnter(CancellationToken cancellationToken);

        public abstract void AddRequest(DicomRequest dicomRequest);

        public abstract Task SendAsync(CancellationToken cancellationToken = default(CancellationToken));

        public abstract Task AbortAsync();
    }
}
