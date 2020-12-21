// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.Advanced.Events
{

    internal class DicomAbortedEvent : IAdvancedDicomClientConnectionEvent
    {
        public DicomAbortSource Source { get; }
        public DicomAbortReason Reason { get; }

        public DicomAbortedEvent(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }
    }
}
