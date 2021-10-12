// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced.Events
{
    public class RequestCompletedEvent : IAdvancedDicomClientConnectionEvent
    {
        public DicomRequest Request { get; }
        public DicomResponse Response { get; }

        public RequestCompletedEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
}