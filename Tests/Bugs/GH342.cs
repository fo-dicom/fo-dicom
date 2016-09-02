// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using Xunit;

    [Collection("General")]
    public class GH342
    {
        #region Unit tests

        [Fact]
        public void Open_EmptyItemsSequence_CanReadBefore()
        {
            var file = DicomFile.Open(@"Test Data\GH342.dcm");
            const string expected = "RIGHT_ON_LEFT";
            var actual = file.Dataset.Get<string>(new DicomTag(0x01f1, 0x32, "ELSCINT"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Open_EmptyItemsSequence_CanReadAfter()
        {
            var file = DicomFile.Open(@"Test Data\GH342.dcm");
            const string expected = "3.5";
            var actual = file.Dataset.Get<string>(new DicomTag(0x07a1, 0x10, "ELSCINT"));
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}