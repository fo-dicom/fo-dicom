using System;

namespace Dicom.Network.Client.Events
{
    public class ConnectionClosedEvent
    {
        public Exception Exception { get; }

        public ConnectionClosedEvent()
        {

        }

        public ConnectionClosedEvent(Exception exception)
        {
            Exception = exception;
        }
    }
}
