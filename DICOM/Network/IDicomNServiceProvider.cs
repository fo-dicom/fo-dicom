// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public interface IDicomNServiceProvider
    {
        DicomNActionResponse OnNActionRequest(DicomNActionRequest request);

        DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request);

        DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request);

        DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request);

        DicomNGetResponse OnNGetRequest(DicomNGetRequest request);

        DicomNSetResponse OnNSetRequest(DicomNSetRequest request);
    }
}
