// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.States
{

    public abstract class DicomClientWithConnectionState : IDicomClientState
    {
        protected DicomClientWithConnectionState(IInitialisationWithConnectionParameters parameters)
        {
            Connection = parameters.Connection ?? throw new ArgumentNullException(nameof(IInitialisationWithConnectionParameters.Connection));
        }

        public IDicomClientConnection Connection { get; }

        public abstract Task OnReceiveAssociationAcceptAsync(DicomAssociation association);

        public abstract Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        public abstract Task OnReceiveAssociationReleaseResponseAsync();

        public abstract Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason);

        public abstract Task OnConnectionClosedAsync(Exception exception);

        public abstract Task OnSendQueueEmptyAsync();

        public abstract Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response);
        
        public abstract Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout);

        public abstract Task<IDicomClientState> GetNextStateAsync(DicomClientCancellation cancellation);

        public abstract Task AddRequestAsync(DicomRequest dicomRequest);

        public abstract Task SendAsync(DicomClientCancellation cancellation);

        public abstract void Dispose();
    }
}
