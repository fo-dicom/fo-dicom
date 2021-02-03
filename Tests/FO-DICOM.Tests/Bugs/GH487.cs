// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH487
    {
        [Theory]
        [InlineData(@"GH487.dcm")]
        public void DicomFileOpen_LastTagPrivateSequenceEmptyItems_DoesNotThrow(string fileName)
        {
            var exception = Record.Exception(() => DicomFile.Open(TestData.Resolve(fileName)));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("GH487.dcm")]
        public async Task DicomFileOpenAsync_LastTagPrivateSequenceEmptyItems_DoesNotThrow(string fileName)
        {
            var exception = await Record.ExceptionAsync(() => DicomFile.OpenAsync(TestData.Resolve(fileName)));
            Assert.Null(exception);
        }
    }
}
