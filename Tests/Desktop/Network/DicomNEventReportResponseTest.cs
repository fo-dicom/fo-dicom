// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Xunit;

    public class DicomNEventReportResponseTest
    {
        #region Unit tests

        [Fact]
        public void EventTypeIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNEventReportRequest(
                DicomUID.BasicFilmSessionSOPClass,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNEventReportResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.EventTypeID);
            Assert.Null(exception);
        }

        [Fact]
        public void SOPInstanceUIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNEventReportRequest(
                DicomUID.BasicFilmSessionSOPClass,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNEventReportResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.SOPInstanceUID);
            Assert.Null(exception);
        }

        #endregion
    }
}
