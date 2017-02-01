// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNCreateRequest : DicomRequest
    {
        #region Fields

        private DicomUID _sopInstanceUID;

        #endregion

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
                if (_sopInstanceUID == null
                    && (_sopInstanceUID = Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID, null)) == null)
                {
                    SOPInstanceUID = DicomUIDGenerator.GenerateDerivedFromUUID();
                }

                return _sopInstanceUID;
            }
            private set
            {
                Command.AddOrUpdate(DicomTag.RequestedSOPInstanceUID, value);
                _sopInstanceUID = value;
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
