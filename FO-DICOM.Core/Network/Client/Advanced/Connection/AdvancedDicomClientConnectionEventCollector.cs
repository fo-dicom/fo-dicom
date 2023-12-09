// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// This interface is responsible for collecting all events via the callback based API that is used by DicomService
    /// and exposing those events as an asynchronous enumerable of event instances 
    /// </summary>
    internal interface IAdvancedDicomClientConnectionEventCollector
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

    internal class AdvancedDicomClientConnectionEventCollector : IAdvancedDicomClientConnectionEventCollector
    {
        private readonly AdvancedDicomClientConnectionRequestHandlers _requestHandlers;
        private readonly Channel<IAdvancedDicomClientConnectionEvent> _events;
        private long _isConnectionClosed;

        public AdvancedDicomClientConnectionEventCollector(AdvancedDicomClientConnectionRequestHandlers requestHandlers)
        {
            _requestHandlers = requestHandlers;
            _events = Channel.CreateUnbounded<IAdvancedDicomClientConnectionEvent>(new UnboundedChannelOptions
            {
                SingleReader = true, SingleWriter = false, AllowSynchronousContinuations = false
            });
        }

        private bool IsConnectionClosed => Interlocked.Read(ref _isConnectionClosed) == 1;

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

        public async Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new DicomAssociationAcceptedEvent(association)).ConfigureAwait(false);
        }

        public async Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new DicomAssociationRejectedEvent(result, source, reason)).ConfigureAwait(false);
        }

        public async Task OnReceiveAssociationReleaseResponseAsync()
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(DicomAssociationReleasedEvent._instance).ConfigureAwait(false);
        }

        public async Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new DicomAbortedEvent(source, reason)).ConfigureAwait(false);
        }

        public async Task OnConnectionClosedAsync(Exception exception)
        {
            if (Interlocked.CompareExchange(ref _isConnectionClosed, 1, 0) != 0)
            {
                // Already closed
                return;
            }

            var @event = exception == null
                ? new ConnectionClosedEvent()
                : new ConnectionClosedEvent(exception);

            await _events.Writer.WriteAsync(@event).ConfigureAwait(false);

            _events.Writer.Complete();
        }

        public async Task OnSendQueueEmptyAsync()
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(SendQueueEmptyEvent._instance).ConfigureAwait(false);
        }

        public async Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new RequestCompletedEvent(request, response)).ConfigureAwait(false);
        }

        public async Task OnRequestPendingAsync(DicomRequest request, DicomResponse response)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new RequestPendingEvent(request, response)).ConfigureAwait(false);
        }

        public async Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
        {
            if (IsConnectionClosed)
            {
                return;
            }

            await _events.Writer.WriteAsync(new RequestTimedOutEvent(request, timeout)).ConfigureAwait(false);
        }

        public async Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            var onCStoreRequest = _requestHandlers?.OnCStoreRequest;

            if (onCStoreRequest == null)
            {
                throw new DicomNetworkException("This DICOM client did not provide a request handler for incoming C-STORE requests (typically following a C-GET request)");
            }

            return await onCStoreRequest(request).ConfigureAwait(false);
        }

        public async Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
        {
            var onNEventReportRequest = _requestHandlers?.OnNEventReportRequest;

            if (onNEventReportRequest == null)
            {
                throw new DicomNetworkException("This DICOM client did not provide a request handler for incoming N-EVENTREPORT requests");
            }

            return await onNEventReportRequest(request).ConfigureAwait(false);
        }
    }
}