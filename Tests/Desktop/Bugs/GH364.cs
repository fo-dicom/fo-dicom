// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH364
    {
        [Theory]
        [InlineData(@".\Test Data\GH364.dcm")]
        public void DicomFileOpen_Contains_TagBeyond00185020(string fileName)
        {
            var file = DicomFile.Open(fileName);
            var actual = file.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@".\Test Data\GH364.dcm")]
        public async Task DicomFileOpenAsync_Contains_TagBeyond00185020(string fileName)
        {
            var file = await DicomFile.OpenAsync(fileName).ConfigureAwait(false);
            var actual = file.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }
    }
}
