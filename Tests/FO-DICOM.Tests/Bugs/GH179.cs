// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
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
