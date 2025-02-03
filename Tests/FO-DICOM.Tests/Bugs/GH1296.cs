// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1296
    {

        [Fact]
        public void OpenFileWithDSNumbersEncodedInCulture()
        {
            // Arrange
            var inputFile = DicomFile.Open(TestData.Resolve("GH1296.dcm"));

            var expectedWindowCenter = 20958;
            var actualWindowCenter = inputFile.Dataset.GetValue<double>(DicomTag.WindowCenter, 0);

            Assert.Equal(expectedWindowCenter, actualWindowCenter, 0.001);
        }

    }
}
