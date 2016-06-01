// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomNGetRequest : DicomRequest
    {
        public DicomNGetRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNGetRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NGetRequest, requestedClassUid)
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
                Command.Add(DicomTag.RequestedSOPInstanceUID, value);
            }
        }

        public DicomTag[] Attributes
        {
            get
            {
                return Command.Get<DicomTag[]>(DicomTag.AttributeIdentifierList);
            }
            private set
            {
                Command.Add(DicomTag.AttributeIdentifierList, value);
            }
        }

        public delegate void ResponseDelegate(DicomNGetRequest request, DicomNGetResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomNGetResponse)response);
            }
            catch
            {
            }
        }
    }
}
