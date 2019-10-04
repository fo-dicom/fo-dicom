// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

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
