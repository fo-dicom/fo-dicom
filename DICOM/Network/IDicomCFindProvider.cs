// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace Dicom.Network
{
    public interface IDicomCFindProvider
    {
        IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request);
    }
}
