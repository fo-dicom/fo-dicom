// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public interface IDicomCEchoProvider
    {
        DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request);
    }
}
