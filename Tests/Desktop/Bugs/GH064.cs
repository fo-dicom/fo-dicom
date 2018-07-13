// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Bugs
{
    using System.Linq;

    using Xunit;

    [Collection("General")]
    public class GH064
    {
        [Fact]
        public void DicomFile_ContainingSequenceWithExplicitLengthAndDelimitationItem_CanBeCompletelyParsed()
        {
            var file = DicomFile.Open(@".\Test Data\GH064.dcm");
            var expected = DicomTag.PixelData;
            var actual = file.Dataset.Last().Tag;
            Assert.Equal(expected, actual);
        }
    }
}
