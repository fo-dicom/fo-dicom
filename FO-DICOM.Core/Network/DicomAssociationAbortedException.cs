// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    public class DicomAssociationAbortedException : DicomNetworkException
    {
        public DicomAssociationAbortedException(DicomAbortSource source, DicomAbortReason reason)
            : base($"Association Abort [source: {source}; reason: {reason}]")
        {
            AbortSource = source;
            AbortReason = reason;
        }

        public DicomAbortSource AbortSource { get; private set; }

        public DicomAbortReason AbortReason { get; private set; }
    }
}
