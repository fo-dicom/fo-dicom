// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests
{
    public class DicomTagsIndexTests
    {
        [Fact]
        public void ShouldFindKnownDicomTags()
        {
            var tagsToTest = new[]
            {
                DicomTag.AccessionNumber,
                DicomTag.PatientID,
            };
            foreach (var tag in tagsToTest)
            {
                var foundTag = DicomTagsIndex.LookupOrCreate(tag.Group, tag.Element);
                Assert.Same(tag, foundTag);
            }
        }
    }
}
