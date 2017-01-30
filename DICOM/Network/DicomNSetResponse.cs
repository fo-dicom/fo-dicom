// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNSetResponse : DicomResponse
    {
        public DicomNSetResponse(DicomDataset command)
            : base(command)
        {
        }

        public DicomNSetResponse(DicomNSetRequest request, DicomStatus status)
            : base(request, status)
        {
            SOPInstanceUID = request.SOPInstanceUID;
        }

        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID);
            }
            private set
            {
                Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
            }
        }
    }
}
