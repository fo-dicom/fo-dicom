// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests
{
    [Collection(TestCollections.Logging)]
    public class DicomEncodingTest
    {
        [Fact]
        public void Default_Getter_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.Default.CodePage;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_NonMatchingCharset_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            using var logCollector = NewLogCollector();
            var actual = DicomEncoding.GetEncoding("Invalid").CodePage;
            Assert.Equal(expected, actual);
            Assert.Equal(1, logCollector.NumberOfWarnings);
            var expectedWarning = "'Invalid' is not a valid DICOM encoding - using ASCII encoding instead";
            Assert.Equal(expectedWarning, logCollector.WarningAt(0));
        }

        [Fact]
        public void GetEncoding_WorksWithCommonMisspellings()
        {
            var expected = Encoding.GetEncoding("utf-8");
            // correct spelling
            var actual = DicomEncoding.GetEncoding("ISO_IR 192");
            Assert.Equal(expected, actual);

            // common misspellings
            actual = DicomEncoding.GetEncoding("ISO IR 192");
            Assert.Equal(expected, actual);
            actual = DicomEncoding.GetEncoding("ISO-IR 192");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_GB18030() //https://github.com/fo-dicom/fo-dicom/issues/481
        {
            int codePage = 0;
            var exception = Record.Exception(() => { codePage = DicomEncoding.GetEncoding("GB18030").CodePage; });
            Assert.Null(exception);
            Assert.Equal(54936, codePage);
        }

        [Fact]
        public void GetCharset_GB18030()
        {
            var expected = "GB18030";
            var encoding = DicomEncoding.GetEncoding("GB18030");
            var actual = DicomEncoding.GetCharset(encoding);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FallbackEncoding()
        {
            var ds = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, "1.2.345.67890" }
            };
            ds.AddOrUpdate(new DicomPersonName(DicomTag.PatientName, DicomEncoding.DefaultArray,
                new MemoryByteBuffer(DicomEncoding.GetEncoding("ISO_IR 100").GetBytes("Hölzl^Günther"))));
            var firstFile = new DicomFile(ds);
            var filestream = new MemoryStream();
            firstFile.Save(filestream);

            filestream.Flush();

            using var logCollector = NewLogCollector();
            filestream.Position = 0;
            var secondFile = DicomFile.Open(filestream);

            // the dataset has been read with default ASCII encoding and therefore "ö" and "ü" should not be recognized
            Assert.Equal("H?lzl^G?nther", secondFile.Dataset.GetString(DicomTag.PatientName));

            // a warning is issued on accessing the incorrectly decoded value
            Assert.Equal(1, logCollector.NumberOfWarnings);
            var expectedWarning =
                "Could not decode string 'H?lzl^G?nther' with given encoding, using replacement characters";
            Assert.Equal(1, logCollector.NumberOfWarnings);
            Assert.Equal(expectedWarning, logCollector.WarningAt(0));

            logCollector.Reset();
            filestream.Position = 0;
            var thirdFile = DicomFile.Open(filestream, DicomEncoding.GetEncoding("ISO_IR 100"));

            // if reading with middle european encoding as fallback default the "ö" and "ü" should be recognized
            Assert.Equal("Hölzl^Günther", thirdFile.Dataset.GetString(DicomTag.PatientName));
            Assert.Equal(0, logCollector.NumberOfWarnings);
        }

        [Theory]
        [MemberData(nameof(FileNames))]
        public void PatientNameEncodings(string fileName, string patientName)
        {
            var dataset = DicomFile.Open(TestData.Resolve($"charset/{fileName}.dcm")).Dataset;
            var actualName = dataset.GetSingleValue<string>(DicomTag.PatientName);
            Assert.Equal(patientName, actualName);
        }

        [Theory]
        [MemberData(nameof(EncodingNames))]
        public void GetCharset(string encodingName, string charSetName)
        {
            var encoding = Encoding.GetEncoding(encodingName);
            var actual = DicomEncoding.GetCharset(encoding);
            Assert.Equal(charSetName, actual);
        }


        [Theory]
        [MemberData(nameof(EncodingNamesExtended))]
        public void GetCharsetExtended(string encodingName, string charSetName)
        {
            var encoding = Encoding.GetEncoding(encodingName);
            var actual = DicomEncoding.GetCharset(encoding, extended:true);
            Assert.Equal(charSetName, actual);
        }


        [Theory]
        [MemberData(nameof(EncodedNames))]
        public void SingleByteCodeExtensions(string encodingName, string expectedName, byte[] rawData)
        {
            var ds = new DicomDataset
            {
                // empty first encoding defaults to ASCII
                new DicomCodeString(DicomTag.SpecificCharacterSet, $"\\{encodingName}"),
            };

            // combine ASCII text with text encoded with another single byte encoding
            // and use the respective code extension
            var asciiPart = new byte[] { 0x41, 0x53, 0x43, 0x49, 0x49 }; // "ASCII"
            IByteBuffer buffer = new MemoryByteBuffer(asciiPart.Concat(rawData).ToArray());

            var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
            ds.Add(patientName);
            Assert.Equal("ASCII" + expectedName, ds.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void RegisterNewEncoding()
        {
            DicomEncoding.RegisterEncoding("KOI 8", "koi8-r");
            var ds = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, "KOI 8"),
            };
            // Грозный^Иван encoded in KOI-8
            var koi8Name = new byte[] { 0xe7, 0xd2, 0xcf, 0xda, 0xce, 0xd9, 0xca, 0x5e, 0xe9, 0xd7, 0xc1, 0xce };
            IByteBuffer buffer = new MemoryByteBuffer(koi8Name);
            var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
            ds.Add(patientName);

            Assert.Equal("Грозный^Иван", ds.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void OverwriteRegisteredEncoding()
        {
            var ds = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, "ISO IR 144"),
            };

            try
            {
                // patch the encoding - without this, the name would display as gibberish
                DicomEncoding.RegisterEncoding("ISO IR 144", "koi8-r");

                // Грозный^Иван encoded in KOI-8 instead of iso-8859-5
                var koi8Name = new byte[] { 0xe7, 0xd2, 0xcf, 0xda, 0xce, 0xd9, 0xca, 0x5e, 0xe9, 0xd7, 0xc1, 0xce };
                IByteBuffer buffer = new MemoryByteBuffer(koi8Name);
                var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
                ds.Add(patientName); // patient name would show gibberish
                Assert.Equal("Грозный^Иван", ds.GetString(DicomTag.PatientName));
            }
            finally
            {
                // set back the correct encoding to avoid breaking other tests
                DicomEncoding.RegisterEncoding("ISO IR 144", "iso-8859-5");
            }
        }

        [Fact]
        public void GetNestedCharacterSetInSequence()
        {
            var ds = DicomFile.Open(TestData.Resolve($"charset/chrSQEncoding.dcm")).Dataset;
            var sequence = ds.GetSequence(DicomTag.RequestedProcedureCodeSequence);
            var item = sequence.Items.First();
            var expectedEncodings = new[]
            {
                Encoding.GetEncoding("shift_jis"),
                Encoding.GetEncoding("iso-2022-jp")
            };
            Assert.Equal(expectedEncodings, item.GetEncodingsForSerialization());
            Assert.Equal("ﾔﾏﾀﾞ^ﾀﾛｳ=山田^太郎=やまだ^たろう", item.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void GetInheritedCharacterSetInSequence()
        {
            var ds = DicomFile.Open(TestData.Resolve($"charset/chrSQEncoding1.dcm")).Dataset;
            var sequence = ds.GetSequence(DicomTag.RequestedProcedureCodeSequence);
            var item = sequence.Items.First();
            var expectedEncodings = new[]
            {
                Encoding.GetEncoding("shift_jis"),
                Encoding.GetEncoding("iso-2022-jp")
            };
            Assert.Equal(expectedEncodings, item.GetEncodingsForSerialization());
            Assert.Equal("ﾔﾏﾀﾞ^ﾀﾛｳ=山田^太郎=やまだ^たろう", item.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void ReplacementCharactersUsedForBadEncoding()
        {
            var ds = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, "ISO IR 192"),
            };
            using var logCollector = NewLogCollector();
            // not a valid UTF-8 encoding
            var badName = new byte[] { 0xc4, 0xe9, 0xef, 0xed, 0xf5, 0xf3, 0xe9, 0xef, 0xf2 };
            IByteBuffer buffer = new MemoryByteBuffer(badName);
            var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
            ds.Add(patientName);
            Assert.Equal("���������", ds.GetString(DicomTag.PatientName));
            Assert.Equal(1, logCollector.NumberOfWarnings);
            var expectedMessage =
                "Could not decode string '���������' with given encoding, using replacement characters";
            Assert.Equal(expectedMessage, logCollector.WarningAt(0));
        }

        [Fact]
        public void AsciiUsedIfEncodingIsNotInCharacterSet()
        {
            var ds = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, @"\ISO 2022 IR 100")
            };
            using var logCollector = NewLogCollector();

            // Buc^Jérôme encoded, but with the escape sequence  for the second component
            // pointing to shift_jis instead of Latin-1
            var name = new byte[]
                { 0x1b, 0x2d, 0x41, 0x42, 0x75, 0x63, 0x5e, 0x1b, 0x28, 0x4a, 0x4a, 0xe9, 0x72, 0xf4, 0x6d, 0x65 };
            IByteBuffer buffer = new MemoryByteBuffer(name);
            var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
            ds.Add(patientName);
            Assert.Equal("Buc^J?r?me", ds.GetString(DicomTag.PatientName));
            Assert.Equal(2, logCollector.NumberOfWarnings);
            var expectedMessage =
                "Found escape sequence for 'shift_jis', which is not defined";
            Assert.StartsWith(expectedMessage, logCollector.WarningAt(0));
            expectedMessage =
                "Could not decode string 'J?r?me' with given encoding, using replacement characters";
            Assert.Equal(expectedMessage, logCollector.WarningAt(1));
        }

        [Fact]
        public void AsciiUsedForUnknownEscapeSequence()
        {
            var ds = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, @"\ISO 2022 IR 100")
            };
            using var logCollector = NewLogCollector();

            // Buc^Jérôme encoded, but with an invalid escape sequence instead of Latin-1 for the second component
            var name = new byte[]
                { 0x1b, 0x2d, 0x41, 0x42, 0x75, 0x63, 0x5e, 0x1b, 0x28, 0x4b, 0x4a, 0xe9, 0x72, 0xf4, 0x6d, 0x65 };
            IByteBuffer buffer = new MemoryByteBuffer(name);
            var patientName = new DicomPersonName(DicomTag.PatientName, ds.GetEncodingsForSerialization(), buffer);
            ds.Add(patientName);
            Assert.Equal("Buc^J?r?me", ds.GetString(DicomTag.PatientName));
            Assert.Equal(2, logCollector.NumberOfWarnings);

            var expectedMessage =
                "Unknown escape sequence found in string, using ASCII encoding";
            Assert.Equal(expectedMessage, logCollector.WarningAt(0));
            expectedMessage =
                "Could not decode string 'J?r?me' with given encoding, using replacement characters";
            Assert.Equal(expectedMessage, logCollector.WarningAt(1));
        }

        [Theory]
        [MemberData(nameof(MultiEncodingNames))]
        public void SavePatientNameWithMultiEncoding(string characterSet, string patientName)
        {
            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { DicomTag.SpecificCharacterSet, characterSet },
                { DicomTag.PatientName, patientName}
            };
            var dicomFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            dicomFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            Assert.Equal(patientName, inFile.Dataset.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void SavePatientNameWithWrongEncoding()
        {
            using var logCollector = NewLogCollector();
            var patientName = "Yamada^Tarou=山田^太郎=やまだ^たろう";
            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { DicomTag.SpecificCharacterSet, "ISO_IR 100" },
                { DicomTag.PatientName, patientName}
            };
            var dicomFile = new DicomFile(dataset);
            var stream = new MemoryStream();
            dicomFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var inFile = DicomFile.Open(stream);
            Assert.Equal("Yamada^Tarou=??^??=???^???", inFile.Dataset.GetString(DicomTag.PatientName));
            var expectedWarning = "Could not encode string '山田' with given encodings, " +
                                  "using replacement characters for encoding";
            Assert.Equal(expectedWarning, logCollector.WarningAt(0));
        }

        public static readonly IEnumerable<object[]> FileNames = new[]
        {
            new[] { "chrArab", "قباني^لنزار" },
            new[] { "chrFren", "Buc^Jérôme" },
            new[] { "chrGerm", "Äneas^Rüdiger" },
            new[] { "chrGreek", "Διονυσιος" },
            new[] { "chrH31", "Yamada^Tarou=山田^太郎=やまだ^たろう" },
            new[] { "chrH32", "ﾔﾏﾀﾞ^ﾀﾛｳ=山田^太郎=やまだ^たろう" },
            new[] { "chrHbrw", "שרון^דבורה" },
            new[] { "chrI2", "Hong^Gildong=洪^吉洞=홍^길동" },
            new[] { "chrJapMulti", "やまだ^たろう" },
            new[] { "chrJapMultiExplicitIR6", "やまだ^たろう" },
            new[] { "chrKoreanMulti", "김희중" },
            new[] { "chrRuss", "Люкceмбypг" },
            new[] { "chrX1", "Wang^XiaoDong=王^小東=" },
            new[] { "chrX2", "Wang^XiaoDong=王^小东=" },
            new[] { "chrGB2312", "Zhang^XiaoDong=张^小东=" },
        };

        public static readonly IEnumerable<object[]> EncodedNames = new[]
        {
            new object[] { "ISO 2022 IR 13", "ﾔﾏﾀﾞ^ﾀﾛｳ",
                new byte[] { 0x1b, 0x29, 0x49, 0xd4, 0xcf, 0xc0, 0xde, 0x5e, 0x1b, 0x29, 0x49, 0xc0, 0xdb, 0xb3 } },
            new object[] { "ISO 2022 IR 58", "张^小东",
                new byte[] { 0x1B, 0x24, 0x29, 0x41, 0xD5, 0xC5, 0x5E, 0x1B, 0x24, 0x29, 0x41, 0xD0, 0xA1, 0xB6, 0xAB } },
            new object[] { "ISO 2022 IR 100", "Buc^Jérôme",
                new byte[] { 0x1b, 0x2d, 0x41, 0x42, 0x75, 0x63, 0x5e, 0x1b, 0x2d, 0x41, 0x4a, 0xe9, 0x72, 0xf4, 0x6d, 0x65 } },
            new object[] { "ISO 2022 IR 101", "Wałęsa",
                new byte[] { 0x1b, 0x2d, 0x42, 0x57, 0x61, 0xb3, 0xea, 0x73, 0x61 } },
            new object[] { "ISO 2022 IR 109", "antaŭnomo",
                new byte[] { 0x1b, 0x2d, 0x43, 0x61, 0x6e, 0x74, 0x61, 0xfd, 0x6e, 0x6f, 0x6d, 0x6f } },
            new object[] { "ISO 2022 IR 110", "vārds",
                new byte[] { 0x1b, 0x2d, 0x44, 0x76, 0xe0, 0x72, 0x64, 0x73 } },
            new object[] { "ISO 2022 IR 127", "قباني^لنزار",
                new byte[] { 0x1b, 0x2d, 0x47, 0xe2, 0xc8, 0xc7, 0xe6, 0xea, 0x5e, 0x1b, 0x2d, 0x47, 0xe4, 0xe6, 0xd2, 0xc7, 0xd1 } },
            new object[] { "ISO 2022 IR 126", "Διονυσιος",
                new byte[] { 0x1b, 0x2d, 0x46, 0xc4, 0xe9, 0xef, 0xed, 0xf5, 0xf3, 0xe9, 0xef, 0xf2 } },
            new object[] { "ISO 2022 IR 138", "שרון^דבורה",
                new byte[] { 0x1b, 0x2d, 0x48, 0xf9, 0xf8, 0xe5, 0xef, 0x5e, 0x1b, 0x2d, 0x48, 0xe3, 0xe1, 0xe5, 0xf8, 0xe4 } },
            new object[] { "ISO 2022 IR 144", "Люкceмбypг",
                new byte[] { 0x1b, 0x2d, 0x4c, 0xbb, 0xee, 0xda, 0x63, 0x65, 0xdc, 0xd1, 0x79, 0x70, 0xd3 } },
            new object[] { "ISO 2022 IR 148", "Çavuşoğlu",
                new byte[] { 0x1b, 0x2d, 0x4d, 0xc7, 0x61, 0x76, 0x75, 0xfe, 0x6f, 0xf0, 0x6c, 0x75 } },
            new object[] { "ISO 2022 IR 166", "นามสกุล",
                new byte[] { 0x1b, 0x2d, 0x54, 0xb9, 0xd2, 0xc1, 0xca, 0xa1, 0xd8, 0xc5 } }
        };

        public static IEnumerable<object[]> MultiEncodingNames = new[]
        {
            new object[] { @"\ISO 2022 IR 87", "Yamada^Tarou=山田^太郎=やまだ^たろう" },
            new object[] { @"\ISO 2022 IR 100\ISO 2022 IR 87", "Yamada^Tarou=山田^太郎=やまだ^たろう" },
            new object[] { @"ISO 2022 IR 13\ISO 2022 IR 87", "ﾔﾏﾀﾞ^ﾀﾛｳ=山田^太郎=やまだ^たろう" },
            new object[] { @"\ISO 2022 IR 101", "Wałęsa" },
            new object[] { @"\ISO 2022 IR 109", "antaŭnomo" },
            new object[] { @"\ISO 2022 IR 127", "قباني^لنزار" },
            new object[] { @"\ISO 2022 IR 126", "Διονυσιος" },
            new object[] { @"\ISO 2022 IR 138", "שרון^דבורה" },
            new object[] { @"\ISO 2022 IR 144", "Люкceмбypг" },
            new object[] { @"\ISO 2022 IR 148", "Çavuşoğlu" },
            new object[] { @"\ISO 2022 IR 149", "김희중" },
            new object[] { @"\ISO 2022 IR 166", "นามสก\u0e38ล" }
        };

        public static readonly IEnumerable<object[]> EncodingNames = new[]
        {
            new object[] { "iso-8859-1", "ISO_IR 100" },
            new object[] { "iso-8859-2", "ISO_IR 101" },
            new object[] { "iso-8859-3", "ISO_IR 109" },
            new object[] { "iso-8859-4", "ISO_IR 110" },
            new object[] { "iso-8859-5", "ISO_IR 144" },
            new object[] { "iso-8859-6", "ISO_IR 127" },
            new object[] { "iso-8859-7", "ISO_IR 126" },
            new object[] { "iso-8859-8", "ISO_IR 138" },
            new object[] { "iso-8859-9", "ISO_IR 148" },
            new object[] { "windows-874", "ISO_IR 166" },
            new object[] { "utf-8", "ISO_IR 192" },
        };

        public static readonly IEnumerable<object[]> EncodingNamesExtended = new[]
        {
            new object[] { "iso-8859-1", "ISO 2022 IR 100" },
            new object[] { "iso-8859-2", "ISO 2022 IR 101" },
            new object[] { "iso-8859-3", "ISO 2022 IR 109" },
            new object[] { "iso-8859-4", "ISO 2022 IR 110" },
            new object[] { "iso-8859-5", "ISO 2022 IR 144" },
            new object[] { "iso-8859-6", "ISO 2022 IR 127" },
            new object[] { "iso-8859-7", "ISO 2022 IR 126" },
            new object[] { "iso-8859-8", "ISO 2022 IR 138" },
            new object[] { "iso-8859-9", "ISO 2022 IR 148" },
            new object[] { "windows-874", "ISO 2022 IR 166" },
        };

        private CollectingLoggerSession NewLogCollector()
        {
            var logger = CollectingLoggerProvider.Instance.CollectingLogger;
            return new CollectingLoggerSession(logger);
        }

    }
}
