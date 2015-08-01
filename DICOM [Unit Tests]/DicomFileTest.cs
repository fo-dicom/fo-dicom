// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.IO;

    using Dicom.IO.Buffer;

    using Xunit;

    public class DicomFileTest
    {
        #region Unit tests

        [Fact]
        public void DicomFile_OpenDefaultEncoding_SwedishCharactersNotMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"),
                new DicomLongString(tag, DicomEncoding.GetEncoding("ISO IR 192"), expected));
            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            var actual = inFile.Dataset.Get<string>(tag);

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void DicomFile_OpenUtf8Encoding_SwedishCharactersMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"),
                new DicomLongString(tag, DicomEncoding.GetEncoding("ISO IR 192"), expected));
            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream, DicomEncoding.GetEncoding("ISO IR 192"));
            var actual = inFile.Dataset.Get<string>(tag);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}