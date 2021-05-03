using FellowOakDicom.Network.Client.Advanced.Events;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
     public interface IAdvancedDicomClientConnectionCallbacks
    {
        /// <summary>
        /// Callback when there are no new requests to send and no existing requests in process (waiting for reply)
        /// </summary>
        /// <returns>Awaitable task</returns>
        Task OnSendQueueEmptyAsync();
        
        /// <summary>
        /// Callback for handling association accept scenarios.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        Task OnReceiveAssociationAcceptAsync(DicomAssociation association);

        /// <summary>
        /// Callback for handling association reject scenarios.
        /// </summary>
        /// <param name="result">Specification of rejection result.</param>
        /// <param name="source">Source of rejection.</param>
        /// <param name="reason">Detailed reason for rejection.</param>
        Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Callback on response from an association release.
        /// </summary>
        Task OnReceiveAssociationReleaseResponseAsync();

        /// <summary>
        /// Callback on receiving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        Task OnConnectionClosedAsync(Exception exception);
        
        /// <summary>
        /// Callback when a request has been completed (a final response was received, causing it to be removed from the pending queue)
        /// One request can only receive one completed response
        /// </summary>
        /// <param name="request">The original request that was sent, which has now been fulfilled</param>
        /// <param name="response">The final response from the DICOM server</param>
        Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response);

        /// <summary>
        /// Callback when a request has received a pending response (this is not the final response, the request is still in the pending queue)
        /// One request can receive multiple pending responses
        /// </summary>
        /// <param name="request">The original request that was sent</param>
        /// <param name="response">A pending response from the DICOM server</param>
        Task OnRequestPendingAsync(DicomRequest request, DicomResponse response);

        /// <summary>
        /// Callback when a request has timed out (no final response was received, but the timeout was exceeded and the request has been removed from the pending queue)
        /// </summary>
        /// <param name="request">The original request that was sent, which could not be fulfilled</param>
        /// <param name="timeout">The timeout duration that has been exceeded</param>
        Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout);

        /// <summary>
        /// Callback for handling a client related C-STORE request, typically emanating from the client's C-GET request.
        /// </summary>
        /// <param name="request">
        /// C-STORE request.
        /// </param>
        /// <returns>
        /// The <see cref="DicomCStoreResponse"/> related to the C-STORE <paramref name="request"/>.
        /// </returns>
        Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request);

        /// <summary>
        /// Callback for handling a client related N-EVENT-REPORT-RQ request, typically emanating from the client's N-ACTION request.
        /// </summary>
        /// <param name="request">
        /// N-EVENT-REPORT-RQ request.
        /// </param>
        /// <returns>
        /// The <see cref="DicomNEventReportResponse"/> related to the N-EVENT-REPORT-RQ <paramref name="request"/>.
        /// </returns>
        Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request);

        /// <summary>
        /// Gets an IAsyncEnumerable of all the DICOM events that are provided to this callback instance
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that stops enumerating the events</param>
        IAsyncEnumerable<IAdvancedDicomClientConnectionEvent> GetEvents(CancellationToken cancellationToken);
    }
    
    public class AdvancedDicomClientConnectionCallbacks : IAdvancedDicomClientConnectionCallbacks
    {
        private readonly Channel<IAdvancedDicomClientConnectionEvent> _events;

        public AdvancedDicomClientConnectionCallbacks()
        {
            _events = Channel.CreateUnbounded<IAdvancedDicomClientConnectionEvent>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false,
                AllowSynchronousContinuations = false
            });
        }
        
        public async IAsyncEnumerable<IAdvancedDicomClientConnectionEvent> GetEvents([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (await _events.Reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                while (_events.Reader.TryRead(out IAdvancedDicomClientConnectionEvent @event))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    yield return @event;
                }
            }   
        }
        
        public async Task OnReceiveAssociationAcceptAsync(DicomAssociation association) => await _events.Writer.WriteAsync(new DicomAssociationAcceptedEvent(association)).ConfigureAwait(false);

        public async Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) => await _events.Writer.WriteAsync(new DicomAssociationRejectedEvent(result, source, reason)).ConfigureAwait(false);

        public async Task OnReceiveAssociationReleaseResponseAsync() => await _events.Writer.WriteAsync(new DicomAssociationReleasedEvent()).ConfigureAwait(false);

        public async Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason) => await _events.Writer.WriteAsync(new DicomAbortedEvent(source, reason)).ConfigureAwait(false);

        public async Task OnConnectionClosedAsync(Exception exception) => await _events.Writer.WriteAsync(new ConnectionClosedEvent(exception)).ConfigureAwait(false);

        public async Task OnSendQueueEmptyAsync() => await _events.Writer.WriteAsync(new SendQueueEmptyEvent()).ConfigureAwait(false);

        public async Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => await _events.Writer.WriteAsync(new RequestCompletedEvent(request, response)).ConfigureAwait(false);
        
        public async Task OnRequestPendingAsync(DicomRequest request, DicomResponse response) => await _events.Writer.WriteAsync(new RequestPendingEvent(request, response)).ConfigureAwait(false);

        public async Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => await _events.Writer.WriteAsync(new RequestTimedOutEvent(request, timeout)).ConfigureAwait(false);
        
        public Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request) => throw new NotImplementedException();

        public Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request) => throw new NotImplementedException();
    }
}