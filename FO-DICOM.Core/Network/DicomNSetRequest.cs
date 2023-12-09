// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    public sealed class DicomNSetRequest : DicomRequest
    {
        public DicomNSetRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNSetRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NSetRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
        }

        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValue<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            private set => Command.AddOrUpdate(DicomTag.RequestedSOPInstanceUID, value);
        }

        public delegate void ResponseDelegate(DicomNSetRequest request, DicomNSetResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNSetResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }
    }
}
