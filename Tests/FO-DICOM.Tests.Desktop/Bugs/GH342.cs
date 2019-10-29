// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH342
    {
        #region Unit tests

        [Fact]
        public void Open_EmptyItemsSequence_CanReadBefore()
        {
            var file = DicomFile.Open(@"Test Data\GH342.dcm");
            const string expected = "RIGHT_ON_LEFT";
            var actual = file.Dataset.Get<string>(new DicomTag(0x01f1, 0x1032, "ELSCINT1"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Open_EmptyItemsSequence_CanReadAfter()
        {
            var file = DicomFile.Open(@"Test Data\GH342.dcm");
            const string expected = "3.5";
            var actual = file.Dataset.Get<string>(new DicomTag(0x07a1, 0x1010, "ELSCINT1"));
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
