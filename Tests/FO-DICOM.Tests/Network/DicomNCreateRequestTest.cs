// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomNCreateRequestTest
    {
        [Fact]
        public void SOPInstanceUIDGetter_SOPInstanceUIDNotDefinedInConstruction_IsNotDefinedInRequest()
        {
            var command = new DicomDataset
            {
                { DicomTag.CommandField, (ushort)DicomCommandField.NCreateRequest },
                { DicomTag.MessageID, (ushort)1 },
                { DicomTag.AffectedSOPClassUID, "1.2.3" }
            };

            var request = new DicomNCreateRequest(command);
            var expected = request.SOPInstanceUID;
            Assert.Null(expected);
        }
    }
}
