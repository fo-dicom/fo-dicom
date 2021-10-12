// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.Advanced.Events
{

    internal class DicomAssociationRejectedEvent : IAdvancedDicomClientConnectionEvent
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
}
