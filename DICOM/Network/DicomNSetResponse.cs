// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public class DicomNSetResponse : DicomResponse
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
                Command.Add(DicomTag.AffectedSOPInstanceUID, value);
            }
        }
    }
}
