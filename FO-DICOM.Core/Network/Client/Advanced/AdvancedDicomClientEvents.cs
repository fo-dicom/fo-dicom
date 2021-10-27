// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// This is an empty marker interface so it is possible to create collections of the various events that occur in our DICOM network communication
    /// </summary>
    public interface IAdvancedDicomClientEvent
    {
        
    }
    
    /// <summary>
    /// When the TCP connection with the SCP is closed
    /// </summary>
    public class ConnectionClosedEvent : IAdvancedDicomClientEvent
    {
        public static readonly ConnectionClosedEvent WithoutException = new ConnectionClosedEvent();
        public static ConnectionClosedEvent WithException(Exception exception) => new ConnectionClosedEvent(exception);
        
        /// <summary>
        /// (Optional) the exception that occured while trying to read from or write to the connection
        /// </summary>
        public Exception Exception { get; }

        private ConnectionClosedEvent()
        {
            
        }

        private ConnectionClosedEvent(Exception exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
    }
    
    /// <summary>
    /// When the DICOM association is suddenly aborted
    /// </summary>
    public class DicomAbortedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// Who initiated the ABORT
        /// </summary>
        public DicomAbortSource Source { get; }
        
        /// <summary>
        /// Why the ABORT occurred
        /// </summary>
        public DicomAbortReason Reason { get; }

        public DicomAbortedEvent(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }
    }
    
    /// <summary>
    /// When the DICOM association is accepted
    /// </summary>
    public class DicomAssociationAcceptedEvent : IAdvancedDicomClientEvent
    {
        public DicomAssociation Association { get; }

        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
    
    /// <summary>
    /// When the DICOM association is rejected
    /// </summary>
    public class DicomAssociationRejectedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// Whether the rejection is permanent or only temporary
        /// </summary>
        public DicomRejectResult Result { get; }
        
        /// <summary>
        /// Who rejected the association
        /// </summary>
        public DicomRejectSource Source { get; }
        
        /// <summary>
        /// Why the association was rejected
        /// </summary>
        public DicomRejectReason Reason { get; }

        public DicomAssociationRejectedEvent(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            Result = result;
            Source = source;
            Reason = reason;
        }
    }

    /// <summary>
    /// When the association is released
    /// </summary>
    public class DicomAssociationReleasedEvent : IAdvancedDicomClientEvent
    {
        public static readonly DicomAssociationReleasedEvent Instance = new DicomAssociationReleasedEvent();

        private DicomAssociationReleasedEvent()
        {
            
        }
    }

    /// <summary>
    /// When a DICOM request is completed and no further responses are expected
    /// </summary>
    public class RequestCompletedEvent : IAdvancedDicomClientEvent
    {
        public DicomRequest Request { get; }
        public DicomResponse Response { get; }

        public RequestCompletedEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
    
    /// <summary>
    /// When a DICOM request has been sent to the SCP and is now pending one or more responses 
    /// </summary>
    public class RequestPendingEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// The original request
        /// </summary>
        public DicomRequest Request { get; }
        
        /// <summary>
        /// The response that was received from the SCP
        /// </summary>
        public DicomResponse Response { get; }

        public RequestPendingEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
    
    /// <summary>
    /// When a DICOM request times out
    /// </summary>
    public class RequestTimedOutEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// The original request
        /// </summary>
        public DicomRequest Request { get; }
        
        /// <summary>
        /// The timeout that elapsed before receiving a response
        /// </summary>
        public TimeSpan Timeout { get; }

        public RequestTimedOutEvent(DicomRequest request, TimeSpan timeout)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Timeout = timeout;
        }
    }

    /// <summary>
    /// When the internal DicomService queue is empty, and a call to SendNextMessage will be required to send more requests
    /// </summary>
    public class SendQueueEmptyEvent : IAdvancedDicomClientEvent
    {
        public static readonly SendQueueEmptyEvent Instance = new SendQueueEmptyEvent();

        private SendQueueEmptyEvent() {}
    }
}