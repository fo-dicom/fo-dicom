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
