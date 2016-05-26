// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Xunit;

    public class DicomNActionResponseTest
    {
        #region Unit tests

        [Fact]
        public void ActionTypeIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNActionRequest(
                DicomUID.BasicFilmSessionSOPClass,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNActionResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.ActionTypeID);
            Assert.Null(exception);
        }

        [Fact]
        public void SOPInstanceUIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNActionRequest(
                DicomUID.BasicFilmSessionSOPClass,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNActionResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.SOPInstanceUID);
            Assert.Null(exception);
        }

        #endregion
    }
}
