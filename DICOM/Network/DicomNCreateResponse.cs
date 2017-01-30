// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNCreateResponse : DicomResponse
    {
        public DicomNCreateResponse(DicomDataset command)
            : base(command)
        {
        }

        public DicomNCreateResponse(DicomNCreateRequest request, DicomStatus status)
            : base(request, status)
        {
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
