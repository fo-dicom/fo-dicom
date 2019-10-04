// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection("Network")]
    [Trait("Category", "Network")]
    public class DicomCStoreRequestTest
    {
        [Fact]
        public void Can_create_request_from_invalid_DicomFile()
        {
            var file = new DicomFile();
            file.Dataset.ValidateItems = false;
            file.Dataset.Add(DicomTag.SOPClassUID, DicomUID.CTImageStorage);
            file.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.04"));
            file.Dataset.ValidateItems = true;


            var request = new DicomCStoreRequest(file);
            Assert.NotNull(request);
        }
    }
}
