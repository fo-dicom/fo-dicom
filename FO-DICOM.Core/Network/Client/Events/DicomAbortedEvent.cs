// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.Events
{

    internal class DicomAbortedEvent
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
