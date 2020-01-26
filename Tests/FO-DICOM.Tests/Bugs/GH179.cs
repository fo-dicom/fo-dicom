// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH179
    {
        [Theory]
        [InlineData(@".\Test Data\GH179A.dcm")]
        public void DicomFile_Contains_CommentsOnRadiationDoseIncluded(string fileName)
        {
            var file = DicomFile.Open(fileName);
            var actual = file.Dataset.Contains(DicomTag.CommentsOnRadiationDose);
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@".\Test Data\GH179B.dcm")]
        public void DicomFile_Open_PartialDatasetAvailable(string fileName)
        {
            var file = DicomFile.Open(fileName);
            Assert.True(file.Dataset != null);
            Assert.True(file.IsPartial);
        }
    }
}
