// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced.Events
{

    internal class ConnectionClosedEvent : IAdvancedDicomClientConnectionEvent
    {
        public Exception Exception { get; }

        public ConnectionClosedEvent(Exception exception)
        {
            Exception = exception;
        }
    }
}
