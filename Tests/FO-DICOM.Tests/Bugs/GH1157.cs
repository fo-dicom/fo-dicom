// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH1157
    {

        [Fact]
        public void HandleWrongSequenceTermination()
        {
            var ex = Record.Exception(() =>
            {

                var d = DicomFile.Open(TestData.Resolve("DIRW0007"));
                // Last tag is expected to be pixel data, but will not be present in case the bad private sequence reset prematurely
                // Resulting in a parsing error
                Assert.True(d.Dataset.Contains(DicomTag.PixelData));
            });
            Assert.Null(ex);
        }

    }

}
