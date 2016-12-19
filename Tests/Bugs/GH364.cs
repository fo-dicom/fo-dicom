// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH364
    {
        [Theory]
        [InlineData(@".\Test Data\GH364.dcm")]
        public void DicomFile_Contains_TagBeyond00185020(string fileName)
        {
            var file = DicomFile.Open(fileName);
            var actual = file.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }
    }
}
