using System;

namespace FellowOakDicom.Network.Client.Advanced.Events
{
    public class RequestPendingEvent : IAdvancedDicomClientConnectionEvent
    {
        public DicomRequest Request { get; }
        public DicomResponse Response { get; }

        public RequestPendingEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
}