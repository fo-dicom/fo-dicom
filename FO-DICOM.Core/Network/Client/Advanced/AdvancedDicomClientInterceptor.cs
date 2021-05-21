using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClientConnectionInterceptor
    {
        /// <summary>
        /// Send association request.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="association">DICOM association.</param>
        Task SendAssociationRequestAsync(IAdvancedDicomClientConnection connection, DicomAssociation association);

        /// <summary>
        /// Send request from service.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="request">Request to send.</param>
        Task SendRequestAsync(IAdvancedDicomClientConnection connection, DicomRequest request);

        /// <summary>
        /// Send association release request.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        Task SendAssociationReleaseRequestAsync(IAdvancedDicomClientConnection connection);

        /// <summary>
        /// Send abort request.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Abort reason.</param>
        Task SendAbortAsync(IAdvancedDicomClientConnection connection, DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="association">Accepted association.</param>
        Task OnReceiveAssociationAcceptAsync(IAdvancedDicomClientConnection connection, DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        Task OnReceiveAssociationRejectAsync(IAdvancedDicomClientConnection connection, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        /// 
        Task OnReceiveAssociationReleaseResponseAsync(IAdvancedDicomClientConnection connection);

        /// <summary>
        /// Callback on receiving an abort message.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        Task OnReceiveAbortAsync(IAdvancedDicomClientConnection connection, DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        Task OnConnectionClosedAsync(IAdvancedDicomClientConnection connection, Exception exception);

        /// <summary>
        /// Callback when a request has been completed (a final response was received, causing it to be removed from the pending queue)
        /// One request can only receive one completed response
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="request">The original request that was sent, which has now been fulfilled</param>
        /// <param name="response">The final response from the DICOM server</param>
        Task OnRequestCompletedAsync(IAdvancedDicomClientConnection connection, DicomRequest request, DicomResponse response);

        /// <summary>
        /// Callback when a request has received a pending response (this is not the final response, the request is still in the pending queue)
        /// One request can receive multiple pending responses
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="request">The original request that was sent</param>
        /// <param name="response">A pending response from the DICOM server</param>
        Task OnRequestPendingAsync(IAdvancedDicomClientConnection connection, DicomRequest request, DicomResponse response);

        /// <summary>
        /// Callback when a request has timed out (no final response was received, but the timeout was exceeded and the request has been removed from the pending queue)
        /// </summary>
        /// <param name="connection">The DICOM client connection</param>
        /// <param name="request">The original request that was sent, which could not be fulfilled</param>
        /// <param name="timeout">The timeout duration that has been exceeded</param>
        Task OnRequestTimedOutAsync(IAdvancedDicomClientConnection connection, DicomRequest request, TimeSpan timeout);
    }

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