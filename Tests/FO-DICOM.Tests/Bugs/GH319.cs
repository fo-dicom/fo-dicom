// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH319
    {
        [Fact]
        public void Contains_PrivateTag_SufficientlyFound()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomVR.LO, new DicomTag(0x0021, 0x0010, "TEST"), "TEST");
            var found = dataset.Contains(new DicomTag(0x0021, 0x0010, "TEST"));
            Assert.True(found);
        }
    }
}
