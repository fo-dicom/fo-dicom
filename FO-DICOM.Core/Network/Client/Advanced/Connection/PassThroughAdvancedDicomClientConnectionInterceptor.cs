using System;
// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    public class PassThroughAdvancedDicomClientConnectionInterceptor : IAdvancedDicomClientConnectionInterceptor
    {
        public static readonly PassThroughAdvancedDicomClientConnectionInterceptor Instance = new PassThroughAdvancedDicomClientConnectionInterceptor();
        
        public Task SendAssociationRequestAsync(IAdvancedDicomClientConnection connection, DicomAssociation association)
            => connection.SendAssociationRequestAsync(association);

        public Task SendRequestAsync(IAdvancedDicomClientConnection connection, DicomRequest request)
            => connection.SendRequestAsync(request);

        public Task SendAssociationReleaseRequestAsync(IAdvancedDicomClientConnection connection)
            => connection.SendAssociationReleaseRequestAsync();

        public Task SendAbortAsync(IAdvancedDicomClientConnection connection, DicomAbortSource source, DicomAbortReason reason)
            => connection.SendAbortAsync(source, reason);

        public Task OnReceiveAssociationAcceptAsync(IAdvancedDicomClientConnection connection, DicomAssociation association)
            => connection.OnReceiveAssociationAcceptAsync(association);

        public Task OnReceiveAssociationRejectAsync(IAdvancedDicomClientConnection connection, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) 
            => connection.OnReceiveAssociationRejectAsync(result, source, reason);

        public Task OnReceiveAssociationReleaseResponseAsync(IAdvancedDicomClientConnection connection)
            => connection.OnReceiveAssociationReleaseResponseAsync();

        public Task OnReceiveAbortAsync(IAdvancedDicomClientConnection connection, DicomAbortSource source, DicomAbortReason reason)
            => connection.OnReceiveAbortAsync(source, reason);

        public Task OnConnectionClosedAsync(IAdvancedDicomClientConnection connection, Exception exception) 
            => connection.OnConnectionClosedAsync(exception);

        public Task OnRequestCompletedAsync(IAdvancedDicomClientConnection connection, DicomRequest request, DicomResponse response)
            => connection.OnRequestCompletedAsync(request, response);

        public Task OnRequestPendingAsync(IAdvancedDicomClientConnection connection, DicomRequest request, DicomResponse response)
            => connection.OnRequestPendingAsync(request, response);

        public Task OnRequestTimedOutAsync(IAdvancedDicomClientConnection connection, DicomRequest request, TimeSpan timeout)
            => connection.OnRequestTimedOutAsync(request, timeout);
    }
}