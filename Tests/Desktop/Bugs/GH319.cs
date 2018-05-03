// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH319
    {
        [Fact]
        public void Contains_PrivateTag_SufficientlyFound()
        {
            var dataset = new DicomDataset();
            dataset.Add(new DicomTag(0x0021, 0x0010, "TEST"), "TEST");
            var found = dataset.Contains(new DicomTag(0x0021, 0x0010, "TEST"));
            Assert.True(found);
        }
    }
}
