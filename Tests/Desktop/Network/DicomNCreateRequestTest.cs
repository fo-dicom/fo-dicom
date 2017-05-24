// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Network
{
    [Collection("Network"), Trait("Category", "Network")]
    public class DicomNCreateRequestTest
    {
        [Fact]
        public void SOPInstanceUIDGetter_SOPInstanceUIDNotDefinedInConstruction_IsNotDefinedInRequest()
        {
            var command = new DicomDataset();
            command.Add(DicomTag.CommandField, (ushort)DicomCommandField.NCreateRequest);
            command.Add(DicomTag.MessageID, (ushort)1);
            command.Add(DicomTag.AffectedSOPClassUID, "1.2.3");

            var request = new DicomNCreateRequest(command);
            var expected = request.SOPInstanceUID;
            Assert.Null(expected);
        }
    }
}
