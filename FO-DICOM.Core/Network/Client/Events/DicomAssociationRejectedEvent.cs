// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.Events
{

    internal class DicomAssociationRejectedEvent
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
