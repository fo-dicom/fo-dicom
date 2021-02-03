// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH179
    {
        [Theory]
        [InlineData("GH179A.dcm")]
        public void DicomFile_Contains_CommentsOnRadiationDoseIncluded(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName));
            var actual = file.Dataset.Contains(DicomTag.CommentsOnRadiationDose);
            Assert.True(actual);
        }

        [Theory]
        [InlineData("GH179B.dcm")]
        public void DicomFile_Open_PartialDatasetAvailable(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName));
            Assert.True(file.Dataset != null);
            Assert.True(file.IsPartial);
        }
    }
}
