// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNDeleteRequest : DicomRequest
    {
        public DicomNDeleteRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNDeleteRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NDeleteRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
        }

        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            }
            private set
            {
                Command.AddOrUpdate(DicomTag.RequestedSOPInstanceUID, value);
            }
        }

        public delegate void ResponseDelegate(DicomNDeleteRequest request, DicomNDeleteResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomNDeleteResponse)response);
            }
            catch
            {
            }
        }
    }
}
