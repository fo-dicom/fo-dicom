// Copyright (c) 2012-2017 fo-dicom contributors.
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

        [Fact]
        public void ToString_ResponseWithoutActionTypeID_DoesNotThrow()
        {
            var command = new DicomDataset(new DicomUnsignedShort(DicomTag.CommandField, (ushort)DicomCommandField.NActionResponse),
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.BasicFilmSessionSOPClass),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, new DicomUID("1.2.3", null, DicomUidType.SOPInstance)),
                new DicomUnsignedShort(DicomTag.MessageIDBeingRespondedTo, 1));
            var response = new DicomNActionResponse(command) { Status = DicomStatus.Success };

            var exception = Record.Exception(() => response.ToString());
            Assert.Null(exception);
        }

        #endregion
    }
}
