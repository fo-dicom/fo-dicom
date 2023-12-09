// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH626
    {
        [Fact]
        public void BadPrivateSequence_Does_Not_Exit_Prematurely()
        {
            var d = DicomFile.Open(TestData.Resolve("GH626.dcm"));
            // Last tag is expected to be pixel data, but will not be present in case the bad private sequence reset prematurely
            // Resulting in a parsing error
            Assert.True(d.Dataset.Contains(DicomTag.PixelData));
        }
    }
}
