// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public sealed class DicomCEchoResponse : DicomResponse
    {
        public DicomCEchoResponse(DicomDataset command)
            : base(command)
        {
        }

        public DicomCEchoResponse(DicomCEchoRequest request, DicomStatus status)
            : base(request, status)
        {
        }
    }
}
