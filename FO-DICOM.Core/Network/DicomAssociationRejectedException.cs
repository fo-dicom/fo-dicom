// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    public class DicomAssociationRejectedException : DicomNetworkException
    {
        public DicomAssociationRejectedException(
            DicomRejectResult result,
            DicomRejectSource source,
            DicomRejectReason reason)
            : base($"Association rejected [result: {result}; source: {source}; reason: {reason}]")
        {
            RejectResult = result;
            RejectSource = source;
            RejectReason = reason;
        }

        public DicomRejectResult RejectResult { get; private set; }

        public DicomRejectSource RejectSource { get; private set; }

        public DicomRejectReason RejectReason { get; private set; }
    }
}
