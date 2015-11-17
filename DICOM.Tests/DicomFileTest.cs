// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Threading.Tasks;

    using Xunit;

    [Collection("General")]
    public class DicomFileTest
    {
        #region Fields

        private const string MinimumDatasetInstanceUid = "1.2.3";

        private static readonly DicomDataset MinimumDatatset =
            new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

        #endregion

        #region Unit tests

        [Fact]
        public void DicomFile_OpenDefaultEncoding_SwedishCharactersNotMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(MinimumDatatset);
            dataset.Add(new DicomLongString(tag, DicomEncoding.GetEncoding("ISO IR 192"), expected));

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

            var dataset = new DicomDataset(MinimumDatatset);
            dataset.Add(new DicomLongString(tag, DicomEncoding.GetEncoding("ISO IR 192"), expected));

            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream, DicomEncoding.GetEncoding("ISO IR 192"));
            var actual = inFile.Dataset.Get<string>(tag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Save_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public async Task SaveAsync_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            await saveFile.SaveAsync(fileName);
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void BeginSave_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.EndSave(saveFile.BeginSave(fileName, null, null));
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void Open_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = DicomFile.Open(fileName);
            var expected = MinimumDatasetInstanceUid;
            var actual = openFile.Dataset.Get<string>(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OpenAsync_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = await DicomFile.OpenAsync(fileName);
            var expected = MinimumDatasetInstanceUid;
            var actual = openFile.Dataset.Get<string>(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BeginOpen_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = _DicomFile.EndOpen(_DicomFile.BeginOpen(fileName, null, null));
            var expected = MinimumDatasetInstanceUid;
            var actual = openFile.Dataset.Get<string>(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Open_StreamOfMemoryMappedFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(MinimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var file = MemoryMappedFile.CreateFromFile(fileName);
            var openFile = DicomFile.Open(file.CreateViewStream());
            var expected = MinimumDatasetInstanceUid;
            var actual = openFile.Dataset.Get<string>(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HasValidHeader_Part10File_ReturnsTrue()
        {
            var validHeader = DicomFile.HasValidHeader(@".\Test Data\CT1_J2KI");
            Assert.True(validHeader);
        }

        [Fact]
        public void Open_StopAtOperatorsNameTag_OperatorsNameExcluded()
        {
            Func<ParseState, bool> criterion = state => state.Tag.CompareTo(DicomTag.OperatorsName) >= 0;
            var file = DicomFile.Open(@"Test Data\GH064.dcm", DicomEncoding.Default, criterion);
            Assert.False(file.Dataset.Contains(DicomTag.OperatorsName));
        }

        [Fact]
        public void Open_StopAfterOperatorsNameTag_OperatorsNameIncluded()
        {
            Func<ParseState, bool> criterion = state => state.Tag.CompareTo(DicomTag.OperatorsName) > 0;
            var file = DicomFile.Open(@"Test Data\GH064.dcm", DicomEncoding.Default, criterion);
            Assert.True(file.Dataset.Contains(DicomTag.OperatorsName));
        }

        [Fact]
        public void Open_StopAfterInstanceNumberTag_SequenceDepth0InstanceNumberExcluded()
        {
            Func<ParseState, bool> criterion = state => state.Tag.CompareTo(DicomTag.InstanceNumber) > 0;
            var file = DicomFile.Open(@"Test Data\GH064.dcm", DicomEncoding.Default, criterion);
            Assert.False(file.Dataset.Contains(DicomTag.InstanceNumber));
        }

        [Fact]
        public void Open_StopAfterInstanceNumberTagAtDepth0_SequenceDepth0InstanceNumberIncluded()
        {
            Func<ParseState, bool> criterion = state => state.SequenceDepth == 0 && state.Tag.CompareTo(DicomTag.InstanceNumber) > 0;
            var file = DicomFile.Open(@"Test Data\GH064.dcm", DicomEncoding.Default, criterion);
            Assert.True(file.Dataset.Contains(DicomTag.InstanceNumber));
        }

        #endregion
    }
}