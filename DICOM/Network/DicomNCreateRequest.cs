// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNCreateRequest : DicomRequest
    {
        public DicomNCreateRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNCreateRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NCreateRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
        }

        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID, null);
            }
            private set
            {
                Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
            }
        }

        public delegate void ResponseDelegate(DicomNCreateRequest request, DicomNCreateResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomNCreateResponse)response);
            }
            catch
            {
            }
        }
    }
}
