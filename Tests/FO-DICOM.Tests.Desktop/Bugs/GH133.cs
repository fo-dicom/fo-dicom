// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.TestsBugs
{

    [Collection("General")]
    public class GH133
    {
        #region Unit tests

        [Fact]
        public void Open_FileWithEncapsulatedPixelDataInSequenceItem_DoesNotThrow()
        {
            var file = DicomFile.Open(@"Test Data\GH133.dcm");
            var dummy = file.Dataset.Get<string>(new DicomTag(0xeaea, 0x0102));
            Assert.Equal("DCM", dummy);
        }

        #endregion
    }
}
