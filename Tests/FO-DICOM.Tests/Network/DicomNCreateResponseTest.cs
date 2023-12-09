// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomNCreateResponseTest
    {
        [Fact]
        public void Constructor_FromRequestWithoutSopInstanceUid_ShouldNotThrow()
        {
            var request =
                new DicomNCreateRequest(new DicomDataset
                {
                    {DicomTag.CommandField, (ushort) DicomCommandField.NCreateRequest},
                    {DicomTag.MessageID, (ushort) 1},
                    {DicomTag.AffectedSOPClassUID, "1.2.3"}
                });

            DicomNCreateResponse response = null;
            var exception = Record.Exception(() => response = new DicomNCreateResponse(request, DicomStatus.Success));

            Assert.Null(exception);
            Assert.Null(response.SOPInstanceUID);
        }
    }
}
