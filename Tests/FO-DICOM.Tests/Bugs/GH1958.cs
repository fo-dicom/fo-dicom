// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1958
    {
        [Fact]
        public void Open_FileWithoutException()
        {
            DicomSequence contentSequence = null;
            var exception = Record.Exception(() =>
            {
                var dcmFile = DicomFile.Open(TestData.Resolve("GH1958.dcm"));
                // verify that the last real Dicomtag was read correctly
                contentSequence = dcmFile.Dataset.GetSequence(DicomTag.ContentSequence);
            });
            Assert.Null(exception);
            Assert.NotNull(contentSequence);
            Assert.Equal(12, contentSequence.Items.Count);
        }
    }
}
