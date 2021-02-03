// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH133
    {
        #region Unit tests

        [Fact]
        public void Open_FileWithEncapsulatedPixelDataInSequenceItem_DoesNotThrow()
        {
            var file = DicomFile.Open(TestData.Resolve("GH133.dcm"));
            var dummy = file.Dataset.GetSingleValue<string>(new DicomTag(0xeaea, 0x0102));
            Assert.Equal("DCM", dummy);
        }

        #endregion
    }
}
