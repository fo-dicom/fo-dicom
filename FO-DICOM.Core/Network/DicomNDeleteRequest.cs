// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
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
            get => Command.GetSingleValue<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            private set => Command.AddOrUpdate(DicomTag.RequestedSOPInstanceUID, value);
        }

        public delegate void ResponseDelegate(DicomNDeleteRequest request, DicomNDeleteResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNDeleteResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }
    }
}
