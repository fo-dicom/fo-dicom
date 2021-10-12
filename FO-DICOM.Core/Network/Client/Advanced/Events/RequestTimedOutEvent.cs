// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced.Events
{
    public class RequestTimedOutEvent : IAdvancedDicomClientConnectionEvent
    {
        public DicomRequest Request { get; }
        public TimeSpan Timeout { get; }

        public RequestTimedOutEvent(DicomRequest request, TimeSpan timeout)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Timeout = timeout;
        }
    }
}