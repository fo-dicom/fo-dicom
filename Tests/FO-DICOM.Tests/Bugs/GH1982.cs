// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1982
    {
        [Fact]
        public void ParsingInvlidFileShouldThrow()
        {
            var filename = TestData.Resolve("GH1982.dcm");
            var ex = Record.Exception(() =>
            {
                var dcmFile = DicomFile.Open(filename);
            });
            Assert.NotNull(ex);
            Assert.IsType<DicomFileException>(ex);
            Assert.IsType<DicomDataException>(ex.InnerException);
            // the exception has to show the tag, where the exception had happened
            Assert.Equal(DicomTag.PersonNamesToUseSequence, (ex.InnerException as DicomDataException).Tag);
        }

        [Fact]
        public void ParsingInvlidFileWithCallback()
        {
            var filename = TestData.Resolve("GH1982.dcm");
            DicomFile dcmFile = null;
            var ex = Record.Exception(() =>
            {
                dcmFile = DicomFile.Open(filename, DicomEncoding.Default, stop: (ParseState state) =>
                {
                    if (state.Tag == DicomTag.PersonNamesToUseSequence) return ParseStopStatus.SkipTag;
                    return ParseStopStatus.Continue;
                });
            });
            Assert.Null(ex);
            Assert.NotNull(dcmFile);
            // ensure that all tags after DicomTag.PersonNamesToUseSequence are pared
            Assert.True(dcmFile.Dataset.Contains(DicomTag.PatientBirthDate));
        }

    }
}
