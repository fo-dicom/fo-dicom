// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomCEchoRequest : DicomRequest
    {
        public DicomCEchoRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomCEchoRequest()
            : base(DicomCommandField.CEchoRequest, DicomUID.Verification)
        {
        }

        public delegate void ResponseDelegate(DicomCEchoRequest request, DicomCEchoResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomCEchoResponse)response);
            }
            catch
            {
            }
        }
    }
}
