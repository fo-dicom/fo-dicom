// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public class DicomNGetResponse : DicomResponse
    {
        public DicomNGetResponse(DicomDataset command)
            : base(command)
        {
        }

        public DicomNGetResponse(DicomNGetRequest request, DicomStatus status)
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
