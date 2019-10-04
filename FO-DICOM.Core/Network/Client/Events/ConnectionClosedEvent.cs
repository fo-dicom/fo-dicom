// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network.Client.Events
{
    internal class ConnectionClosedEvent
    {
        public Exception Exception { get; }

        public ConnectionClosedEvent(Exception exception)
        {
            Exception = exception;
        }
    }
}
