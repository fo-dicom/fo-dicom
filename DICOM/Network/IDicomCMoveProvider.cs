// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace Dicom.Network
{
    public interface IDicomCMoveProvider
    {
        IEnumerable<DicomCMoveResponse> OnCMoveRequest(DicomCMoveRequest request);
    }
}
