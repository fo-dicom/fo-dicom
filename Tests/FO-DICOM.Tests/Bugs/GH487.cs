// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH487
    {
        [Theory]
        [InlineData(@".\Test Data\GH487.dcm")]
        public void DicomFileOpen_LastTagPrivateSequenceEmptyItems_DoesNotThrow(string fileName)
        {
            var exception = Record.Exception(() => DicomFile.Open(fileName));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(@".\Test Data\GH487.dcm")]
        public async Task DicomFileOpenAsync_LastTagPrivateSequenceEmptyItems_DoesNotThrow(string fileName)
        {
            var exception = await Record.ExceptionAsync(() => DicomFile.OpenAsync(fileName));
            Assert.Null(exception);
        }
    }
}
