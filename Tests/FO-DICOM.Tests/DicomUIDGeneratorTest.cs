// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomUIDGeneratorTest
    {
        #region Unit tests

        [Fact]
        public void GenerateDerivedFromUUID_MultipleValues_AllValuesContainOnlyDigitsAndDecimalDots()
        {
            for (var i = 0; i < 1000; ++i)
            {
                var uid = DicomUIDGenerator.GenerateDerivedFromUUID();
                var invalids = Regex.Replace(uid.UID, @"[0-9\.]", "");
                Assert.Equal(0, invalids.Length);
            }
        }

        [Fact]
        public void ConvertGuidToUuidInteger_RoundTripConversion()
        {
            var expected = new Guid("11223344-5566-7788-9900-aabbccddeeff");
            string converted = DicomUIDGenerator.ConvertGuidToUuidInteger(ref expected);
            var actual = ConvertDicomUidToGuid(converted);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertGuidToUuidInteger_RoundTripConversionMaximumValue()
        {
            var expected = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff");
            string converted = DicomUIDGenerator.ConvertGuidToUuidInteger(ref expected);
            var actual = ConvertDicomUidToGuid(converted);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertGuidToUuidInteger_RoundTripConversionEmpty()
        {
            var expected = new Guid();
            string converted = DicomUIDGenerator.ConvertGuidToUuidInteger(ref expected);
            var actual = ConvertDicomUidToGuid(converted);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertGuidToUuidInteger_Iso9834Sample()
        {
            // Sample value of ISO/IEC 9834-8, paragraph 8 and wiki.ihe.net
            var sampleValue = new Guid("f81d4fae-7dec-11d0-a765-00a0c91e6bf6");
            string actual = DicomUIDGenerator.ConvertGuidToUuidInteger(ref sampleValue);
            Assert.Equal("2.25.329800735698586629295641978511506172918", actual);
        }

        [Fact]
        public void Generate_MultipleInParallel_AllValuesUnique()
        {
            const int n = 100000;
            var uids = new string[n];
            Parallel.For(0, n, i => { uids[i] = DicomUIDGenerator.GenerateDerivedFromUUID().UID; });
            Assert.Equal(n, uids.Distinct().Count());
        }

        [Fact]
        public void Generate_SourceUidKnown_ReturnsMappedDestinationUid()
        {
            var source = DicomUIDGenerator.GenerateDerivedFromUUID();

            var generator = new DicomUIDGenerator();
            var expected = generator.Generate(source);
            var actual = generator.Generate(source);

            Assert.Equal(expected, actual);
        }


        private static Guid ConvertDicomUidToGuid(string value)
        {
            // Remove "2.25." OID root prefix
            string valueWithoutRoot = value.Substring(5);

            var bigInteger = BigInteger.Parse(valueWithoutRoot, CultureInfo.InvariantCulture);
            var hex = bigInteger.ToString("x32", CultureInfo.InvariantCulture);
            if (hex.Length > 32)
            {
                // BigInteger will return additional leading 0 when top byte > 7 to indicate positive value, remove it.
                hex = hex.Substring(1);
            }

            return new Guid(hex);
        }

        #endregion
    }
}
