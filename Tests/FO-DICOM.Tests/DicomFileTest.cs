// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Writer;
using FellowOakDicom.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests
{
    [Collection(TestCollections.General)]
    public class DicomFileTest
    {
        private class UnseekableStream : MemoryStream
        {
            public override bool CanSeek => false;

            public void Reset()
            {
                base.Seek(0, SeekOrigin.Begin);
            }

            public override long Seek(long offset, SeekOrigin loc)
            {
                throw new NotSupportedException();
            }

            public override long Position
            {
                set => throw new NotSupportedException();
            }
        }

        #region Fields

        private const string _minimumDatasetInstanceUid = "1.2.3";

        private static readonly DicomDataset _minimumDataset =
            new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

        private static readonly DicomDataset _allVrDataset =
            new DicomDataset(_minimumDataset)
            {
                // random tags with all VRs except SQ
                new DicomApplicationEntity(DicomTag.StationAETitle, "MYPACS"),
                new DicomAgeString(DicomTag.PatientAge, "050Y"),
                new DicomAttributeTag(DicomTag.DimensionIndexPointer, new DicomTag(0x0054, 0x0080)),
                new DicomCodeString(DicomTag.SpecificCharacterSet, "ISO IR 192"),
                new DicomDate(DicomTag.InstanceCreationDate, "20200101"),
                new DicomDecimalString(DicomTag.EventElapsedTimes, "1234.5678"),
                new DicomDateTime(DicomTag.AcquisitionDateTime, "20111101082000"),
                new DicomFloatingPointSingle(DicomTag.ExaminedBodyThickness, 123.456f),
                new DicomFloatingPointDouble(DicomTag.OutlineRightVerticalEdge, 123.456789),
                new DicomIntegerString(DicomTag.ReferencedFrameNumber, "25"),
                new DicomLongString(DicomTag.ManufacturerModelName, "ACME Vision"),
                new DicomLongText(DicomTag.AdditionalPatientHistory, "У пациента насморк"),
                new DicomOtherByte(DicomTag.DarkCurrentCounts, new byte[] { 12, 13, 14, 15, 16, 17 }),
                new DicomOtherDouble(DicomTag.DoubleFloatPixelData, new double[] { 12.3, 13.4, 14.5 }),
                new DicomOtherFloat(DicomTag.FloatPixelData, new float[] { 1.2f, 2.3f, 3.4f, 4.5f }),
                new DicomOtherLong(DicomTag.SelectorOLValue, new uint[] { 123456, 789012 }),
                new DicomOtherVeryLong(DicomTag.SelectorOVValue, new ulong[] { 12, 13, 14, 15 }),
                new DicomOtherWord(DicomTag.SelectorOWValue, new ushort[] { 1234, 5678 }),
                new DicomPersonName(DicomTag.OtherPatientNames, "Doe^John\\Doe^Jane"),
                new DicomShortString(DicomTag.AccessionNumber, "ACC-123"),
                new DicomSignedLong(DicomTag.ReferencePixelX0, -12345),
                new DicomSignedShort(DicomTag.TIDOffset, -2030),
                new DicomShortText(DicomTag.SelectorSTValue, "Some short text"),
                new DicomSignedVeryLong(DicomTag.SelectorSVValue, -1234567890),
                new DicomTime(DicomTag.SeriesTime, "113022.45"),
                new DicomUnlimitedCharacters(DicomTag.SelectorUCValue, "Some very long text"),
                new DicomUniqueIdentifier(DicomTag.SelectorUIValue, "123.4.5.89"),
                new DicomUnsignedLong(DicomTag.SimpleFrameList, 12345),
                new DicomUnknown(DicomTag.SelectorUNValue, new byte[] { 1, 2 }),
                new DicomUniversalResource(DicomTag.SelectorURValue, "https://example.com"),
                new DicomUnsignedShort(DicomTag.BitsAllocated, 8),
                new DicomUnlimitedText(DicomTag.SelectorUTValue, "More text..."),
                new DicomUnsignedVeryLong(DicomTag.SelectorUVValue, 1234567890),
            };

        #endregion

        #region Helpers

        #endregion

        #region Unit tests

        [Fact]
        public void DicomFile_NoDefaultEncoding_SwedishCharactersNotMaintained()
        {
            var expected = "Händer Å Fötter";
            var tag = DicomTag.DoseComment;

            var dataset = new DicomDataset(_minimumDataset)
            {
                new DicomLongString(tag, DicomEncoding.DefaultArray,
                    new MemoryByteBuffer(DicomEncoding.GetEncoding("ISO IR 192").GetBytes(expected)))
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

            var dataset = new DicomDataset(_minimumDataset)
            {
                new DicomLongString(tag, DicomEncoding.DefaultArray,
                    new MemoryByteBuffer(DicomEncoding.GetEncoding("ISO IR 192").GetBytes(expected)))
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

            var dataset = new DicomDataset(_minimumDataset)
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

            var dataset = new DicomDataset(_minimumDataset)
            {
                new DicomLongString(tag, expected),
                { DicomTag.SpecificCharacterSet, "ISO IR 192" }
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
        public void Open_TooSmallFile_Raises()
        {
            var stream = new MemoryStream(new byte[20]);
            var exception = Record.Exception(() => DicomFile.Open(stream));
            Assert.IsType<DicomFileException>(exception);
            Assert.StartsWith("Not a valid DICOM file", exception.Message);
        }

        [Fact]
        public void Save_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(_minimumDataset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);
            Assert.True(File.Exists(fileName));
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public async Task SaveAsync_ToFile_FileExistsOnDisk()
        {
            var saveFile = new DicomFile(_minimumDataset);
            var fileName = Path.GetTempFileName();
            await saveFile.SaveAsync(fileName);
            Assert.True(File.Exists(fileName));
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void Open_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDataset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = DicomFile.Open(fileName);
            var expected = _minimumDatasetInstanceUid;
            var actual = openFile.Dataset.GetString(DicomTag.SOPInstanceUID);
            Assert.Equal(expected, actual);
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void Open_FromStream_UsingNoSeek_YieldsValidDicomFile()
        {
            var file = new DicomFile(_allVrDataset);
            var stream = new UnseekableStream();
            file.Save(stream);
            stream.Reset();
            var openFile = DicomFile.Open(stream);
            Assert.True(new DicomDatasetComparer().Equals(file.Dataset, openFile.Dataset));
        }

        [Theory]
        [MemberData(nameof(FileNames))]
        public void Open_FileFromStream_UsingNoSeek_YieldsValidDicomFile(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName), FileReadOption.SkipLargeTags);
            var stream = new UnseekableStream();
            file.Save(stream);
            stream.Reset();
            var openFile = DicomFile.Open(stream, FileReadOption.ReadLargeOnDemand);
            Assert.True(new DicomDatasetComparer().Equals(file.Dataset, openFile.Dataset));
        }

        [Theory]
        [MemberData(nameof(FileNames))]
        public void Open_FileFromStream_YieldsValidDicomFile(string fileName)
        {
            var file = DicomFile.Open(TestData.Resolve(fileName));
            var stream = new MemoryStream();
            file.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var openFile = DicomFile.Open(stream);
            Assert.True(new DicomDatasetComparer().Equals(file.Dataset, openFile.Dataset));
        }

        public static readonly IEnumerable<object[]> FileNames = new[]
        {
            new object[] { "test_SR.dcm" }, // nested sequences
            new object[] { "GH184.dcm" }, // private nested sequences
            new object[] { "GH223.dcm" }, // empty sequence item
            new object[] { "10200904.dcm" }, // RLELossless
            new object[] { "GH227.dcm" }, // Deflated Little Endian Explicit
            new object[] { "genFile.dcm" }, // JPEGBaseline
            new object[] { "GH1261.dcm" }, // JPEGLossless Non-hierarchical 1stOrderPrediction
            new object[] { "GH064.dcm" }, // JPEG2000 Lossless Only
            new object[] { "GH195.dcm" }, // JPEGExtended Process 2+4
            new object[] { "ETIAM_video_002.dcm" }, // MPEG2 Main Profile
            new object[] { "GH177_D_CLUNIE_CT1_IVRLE_BigEndian_undefined_length.dcm" }, // Big Endian
            new object[] { "GH133.dcm" }, // regression test for milestone handling
            new object[] { "multiframe.dcm" } // file with multiple frames and frameoffsettable
        };

        [Fact]
        public void Open_FromStream_YieldsValidDicomFile()
        {
            var file = new DicomFile(_allVrDataset);
            var stream = new MemoryStream();
            file.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var openFile = DicomFile.Open(stream);
            Assert.True(new DicomDatasetComparer().Equals(file.Dataset, openFile.Dataset));
        }

        [Fact]
        public async Task OpenAsync_FromFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDataset);
            var fileName = Path.GetTempFileName();
            saveFile.Save(fileName);

            var openFile = await DicomFile.OpenAsync(fileName);
            Assert.True(new DicomDatasetComparer().Equals(_minimumDataset, openFile.Dataset));
            IOHelper.DeleteIfExists(fileName);
        }

        [Fact]
        public void Open_StreamOfMemoryMappedFile_YieldsValidDicomFile()
        {
            var saveFile = new DicomFile(_minimumDataset);
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
            static bool criterion(ParseState state) =>
                state.SequenceDepth == 0 && state.Tag.CompareTo(DicomTag.InstanceNumber) > 0;

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
            await file.SaveAsync(stream1, options1);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            await file.SaveAsync(stream2, options2);

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
            await file.SaveAsync("saveasynctofile1", options1);
            var options2 = new DicomWriteOptions { LargeObjectSize = 16 * 1024 * 1024 };
            await file.SaveAsync("saveasynctofile2", options2);


            var bytes1 = File.ReadAllBytes("saveasynctofile1");
            var bytes2 = File.ReadAllBytes("saveasynctofile2");
            Assert.Equal(bytes1, bytes2);
        }

        [Fact]
        public void Clone_FromValidDataset_ResultEqualsOriginal()
        {
            var file = new DicomFile(_allVrDataset);
            var clone = file.Clone();
            foreach (DicomItem item in clone.Dataset)
            {
                Assert.Equal(file.Dataset.GetString(item.Tag), clone.Dataset.GetString(item.Tag));
            }
        }

        [Fact]
        public void Clone_FromInValidDataset_ResultEqualsOriginal()
        {
            var file = new DicomFile(_allVrDataset);
            file.Dataset.ValidateItems = false;
            file.Dataset.AddOrUpdate(DicomTag.InstanceCreationDate, "20221313");
            file.Dataset.ValidateItems = true;
            var clone = file.Clone();
            foreach (DicomItem item in clone.Dataset)
            {
                Assert.Equal(file.Dataset.GetString(item.Tag), clone.Dataset.GetString(item.Tag));
            }
        }

        #endregion
    }
}
