// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomNActionResponseTest
    {
        #region Unit tests

        [Fact]
        public void ActionTypeIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNActionRequest(
                DicomUID.BasicFilmSession,
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
                DicomUID.BasicFilmSession,
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
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.BasicFilmSession),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, new DicomUID("1.2.3", null, DicomUidType.SOPInstance)),
                new DicomUnsignedShort(DicomTag.MessageIDBeingRespondedTo, 1));
            var response = new DicomNActionResponse(command) { Status = DicomStatus.Success };

            var exception = Record.Exception(() => response.ToString());
            Assert.Null(exception);
        }

        [Fact]
        public void StatusSetter_SetWithoutErrorComment_DoesNotAddErrorComment()
        {
            DicomResponse x = new DicomNActionResponse(new DicomDataset())
            {
                Status = DicomStatus.Success
            };
            Assert.False(x.Command.Contains(DicomTag.ErrorComment));
        }

        [Fact]
        public void StatusSetter_ChangesFromStatusWithCommentToWithout_UpdatesErrorComment()
        {
            var comment = "This is a comment";
            DicomResponse x = new DicomNActionResponse(new DicomDataset());
            var status = new DicomStatus(
                             "C303",
                             DicomState.Failure,
                             "Refused: The UPS may only become SCHEDULED via N-CREATE, not N-SET or N-ACTION",
                             comment);
            x.Status = status;
            Assert.Equal(x.Command.GetString(DicomTag.ErrorComment), comment);

            x.Status = DicomStatus.Success;
            Assert.False(x.Command.Contains(DicomTag.ErrorComment));
        }

        #endregion
    }
}
