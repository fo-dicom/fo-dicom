// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH364
    {
        [Theory]
        [InlineData("GH364.dcm")]
        public void DicomFileOpen_Contains_TagBeyond00185020(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName));
            var actual = file.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }

        [Theory]
        [InlineData("GH364.dcm")]
        public async Task DicomFileOpenAsync_Contains_TagBeyond00185020(string fileName)
        {
            var file = await DicomFile.OpenAsync(TestData.Resolve(fileName));
            var actual = file.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }
    }
}
