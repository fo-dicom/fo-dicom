﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Dicom
{
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
        public void Generate_MultipleInParallel_AllValuesUnique()
        {
            const int n = 100;
            var uids = new string[n];
            var generator = new DicomUIDGenerator();
            Parallel.For(0, n, i => { uids[i] = generator.Generate().UID; });
            Assert.Equal(n, uids.Distinct().Count());
        }

        [Fact]
        public void Generate_SourceUidKnown_ReturnsMappedDestinationUid()
        {
            var generator = new DicomUIDGenerator();
            var source = generator.Generate();

            var expected = generator.Generate(source);
            var actual = generator.Generate(source);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
