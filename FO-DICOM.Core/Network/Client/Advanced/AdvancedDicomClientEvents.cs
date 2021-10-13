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
    
    public class ConnectionClosedEvent : IAdvancedDicomClientEvent
    {
        public static readonly ConnectionClosedEvent WithoutException = new ConnectionClosedEvent();
        public static ConnectionClosedEvent WithException(Exception exception) => new ConnectionClosedEvent(exception);
        
        public Exception Exception { get; }

        private ConnectionClosedEvent()
        {
            
        }

        private ConnectionClosedEvent(Exception exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
    }
    
    public class DicomAbortedEvent : IAdvancedDicomClientEvent
    {
        public DicomAbortSource Source { get; }
        public DicomAbortReason Reason { get; }

        public DicomAbortedEvent(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }
    }
    
    public class DicomAssociationAcceptedEvent : IAdvancedDicomClientEvent
    {
        public DicomAssociation Association { get; }

        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
    
    public class DicomAssociationRejectedEvent : IAdvancedDicomClientEvent
    {
        public DicomRejectResult Result { get; }
        public DicomRejectSource Source { get; }
        public DicomRejectReason Reason { get; }

        public DicomAssociationRejectedEvent(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            Result = result;
            Source = source;
            Reason = reason;
        }
    }

    public class DicomAssociationReleasedEvent : IAdvancedDicomClientEvent
    {
        public static readonly DicomAssociationReleasedEvent Instance = new DicomAssociationReleasedEvent();

        private DicomAssociationReleasedEvent()
        {
            
        }
    }

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
    
    public class RequestPendingEvent : IAdvancedDicomClientEvent
    {
        public DicomRequest Request { get; }
        public DicomResponse Response { get; }

        public RequestPendingEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
    
    public class RequestTimedOutEvent : IAdvancedDicomClientEvent
    {
        public DicomRequest Request { get; }
        public TimeSpan Timeout { get; }

        public RequestTimedOutEvent(DicomRequest request, TimeSpan timeout)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Timeout = timeout;
        }
    }

    public class SendQueueEmptyEvent : IAdvancedDicomClientEvent
    {
        public static readonly SendQueueEmptyEvent Instance = new SendQueueEmptyEvent();

        private SendQueueEmptyEvent() {}
    }
}