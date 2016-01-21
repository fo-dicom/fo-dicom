// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using Xunit;

    [Collection("General")]
    public class GH179
    {
        [Theory]
        [InlineData(@".\Test Data\GH179.dcm")]
        public void DicomFile_Contains_CommentsOnRadiationDoseIncluded(string fileName)
        {
            var file = DicomFile.Open(fileName);
            var actual = file.Dataset.Contains(DicomTag.CommentsOnRadiationDose);
            Assert.True(actual);
        }
    }
}
