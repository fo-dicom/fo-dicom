// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Bugs
{

    [Collection("General")]
    public class GH1157
    {

        [Fact]
        public void HandleWrongSequenceTermination()
        {
            var ex = Record.Exception(() =>
            {

                var d = DicomFile.Open(@"Test Data\DIRW0007");
                // Last tag is expected to be pixel data, but will not be present in case the bad private sequence reset prematurely
                // Resulting in a parsing error
                Assert.True(d.Dataset.Contains(DicomTag.PixelData));
            });
            Assert.Null(ex);
        }

    }
}
