﻿// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Writer;
using FellowOakDicom.Tests.Helpers;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomFileTest
    {
        #region Fields

        private const string _minimumDatasetInstanceUid = "1.2.3";

        private static readonly DicomDataset _minimumDatatset =
            new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

        #endregion

        #region Unit tests

        [Fact]
        public void DicomFile_NoDefaultEncoding_SwedishCharactersNotMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(_minimumDatatset)
            {
                new DicomLongString(tag, DicomEncoding.Default, new MemoryByteBuffer(DicomEncoding.GetEncoding("ISO IR 192").GetBytes(expected)))
            };

            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            var actual = inFile.Dataset.GetString(tag);

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void DicomFile_OpenDefaultEncoding_SwedishCharactersMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(_minimumDatatset)
            {
                new DicomLongString(tag, DicomEncoding.Default, new MemoryByteBuffer(DicomEncoding.GetEncoding("ISO IR 192").GetBytes(expected)))
            };

            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream, DicomEncoding.GetEncoding("ISO IR 192"));
            var actual = inFile.Dataset.GetString(tag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomFile_NoEncoding_SwedishCharactersNotMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(_minimumDatatset)
            {
                new DicomLongString(tag, expected)
            };

            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            var actual = inFile.Dataset.GetString(tag);

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void DicomFile_OpenUtf8Encoding_SwedishCharactersMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(_minimumDatatset)
            {
                new DicomLongString(tag, expected),
                { DicomTag.SpecificCharacterSet,"ISO IR 192"  }
            };

            var outFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            outFile.Save(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            var actual = inFile.Dataset.GetString(tag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Save_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(_minimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);
            Assert.True(File.Exists(fileName));
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public async Task SaveAsync_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(_minimumDatatset);
            var fileName = Path.GetTempFileName();
            await saveFile.SaveAsync(fileName);
            Assert.True(File.Exists(fileName));
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void Open_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = DicomFile.Open(fileName);
            var expected = _minimumDatasetInstanceUid;
            var actual = openFile.Dataset.GetString(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public async Task OpenAsync_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = await DicomFile.OpenAsync(fileName);
            var expected = _minimumDatasetInstanceUid;
            var actual = openFile.Dataset.GetString(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void Open_StreamOfMemoryMappedFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDatatset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            using (var file = MemoryMappedFile.CreateFromFile(fileName))
            using (var stream = file.CreateViewStream())
            {
                var openFile = DicomFile.Open(stream);
                var expected = _minimumDatasetInstanceUid;
                var actual = openFile.Dataset.GetString(DicomTag.SOPInstanceUID);
                Assert.Equal(expected, actual);
            }
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void HasValidHeader_Part10File_ReturnsTrue()
        {
            var validHeader = DicomFile.HasValidHeader(TestData.Resolve("CT1_J2KI"));
            Assert.True(validHeader);
        }

        [Fact]
        public void Open_StopAtOperatorsNameTag_OperatorsNameExcluded()
        {
            static bool criterion(ParseState state) => state.Tag.CompareTo(DicomTag.OperatorsName) >= 0;

            var file = DicomFile.Open(TestData.Resolve("GH064.dcm"), DicomEncoding.Default, criterion);
            Assert.False(file.Dataset.Contains(DicomTag.OperatorsName));
        }

        [Fact]
        public void Open_StopAfterOperatorsNameTag_OperatorsNameIncluded()
        {
            static bool criterion(ParseState state) => state.Tag.CompareTo(DicomTag.OperatorsName) > 0;

            var file = DicomFile.Open(TestData.Resolve("GH064.dcm"), DicomEncoding.Default, criterion);
            Assert.True(file.Dataset.Contains(DicomTag.OperatorsName));
        }

        [Fact]
        public void Open_StopAfterInstanceNumberTag_SequenceDepth0InstanceNumberExcluded()
        {
            static bool criterion(ParseState state) => state.Tag.CompareTo(DicomTag.InstanceNumber) > 0;

            var file = DicomFile.Open(TestData.Resolve("GH064.dcm"), DicomEncoding.Default, criterion);
            Assert.False(file.Dataset.Contains(DicomTag.InstanceNumber));
        }

        [Fact]
        public void Open_StopAfterInstanceNumberTagAtDepth0_SequenceDepth0InstanceNumberIncluded()
        {
            static bool criterion(ParseState state) => state.SequenceDepth == 0 && state.Tag.CompareTo(DicomTag.InstanceNumber) > 0;

            var file = DicomFile.Open(TestData.Resolve("GH064.dcm"), DicomEncoding.Default, criterion);
            Assert.True(file.Dataset.Contains(DicomTag.InstanceNumber));
        }

        [Fact]
        public void Save_PixelDataWrittenInManyChunks_EqualsWhenPixelDataWrittenInOneChunk()
        {
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            using var stream1 = new MemoryStream();
            using var stream2 = new MemoryStream();

            var options1 = new DicomWriteOptions { LargeObjectSize = 1024 };
            file.Save(stream1, options1);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            file.Save(stream2, options2);

            Assert.Equal(stream1.ToArray(), stream2.ToArray());
        }

        [Fact]
        public async Task SaveAsync_PixelDataWrittenInManyChunks_EqualsWhenPixelDataWrittenInOneChunk()
        {
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            using var stream1 = new MemoryStream();
            using var stream2 = new MemoryStream();

            var options1 = new DicomWriteOptions { LargeObjectSize = 1024 };
            await file.SaveAsync(stream1, options1).ConfigureAwait(false);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            await file.SaveAsync(stream2, options2).ConfigureAwait(false);

            Assert.Equal(stream1.ToArray(), stream2.ToArray());
        }

        [Fact]
        public void SaveToFile_PixelDataWrittenInManyChunks_EqualsWhenPixelDataWrittenInOneChunk()
        {
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            var options1 = new DicomWriteOptions { LargeObjectSize = 1024 };
            file.Save("saveasynctofile1", options1);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            file.Save("saveasynctofile2", options2);


            var bytes1 = File.ReadAllBytes("saveasynctofile1");
            var bytes2 = File.ReadAllBytes("saveasynctofile2");
            Assert.Equal(bytes1, bytes2);
        }

        [Fact]
        public async Task SaveAsyncToFile_PixelDataWrittenInManyChunks_EqualsWhenPixelDataWrittenInOneChunk()
        {
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            var options1 = new DicomWriteOptions { LargeObjectSize = 1024 };
            await file.SaveAsync("saveasynctofile1", options1).ConfigureAwait(false);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            await file.SaveAsync("saveasynctofile2", options2).ConfigureAwait(false);


            var bytes1 = File.ReadAllBytes("saveasynctofile1");
            var bytes2 = File.ReadAllBytes("saveasynctofile2");
            Assert.Equal(bytes1, bytes2);
        }

        #endregion
    }
}
