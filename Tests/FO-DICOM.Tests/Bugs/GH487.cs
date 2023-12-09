// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
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
