﻿// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Dicom.IO;

    using Xunit;

    [Collection("General")]
    public class DicomDatasetTest
    {
        #region Unit tests

        [Fact]
        public void Add_OtherDoubleElement_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3.45);
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
        }

        [Fact]
        public void Add_OtherDoubleElementWithMultipleDoubles_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3.45, 6.78, 9.01);
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal(3, dataset.Get<double[]>(tag).Length);
        }

        [Fact]
        public void Add_UnlimitedCharactersElement_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.Get<string>(tag));
        }

        [Fact]
        public void Add_UnlimitedCharactersElementWithMultipleStrings_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("c", dataset.Get<string>(tag, 2));
        }

        [Fact]
        public void Add_UniversalResourceElement_Succeeds()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.Get<string>(tag));
        }

        [Fact]
        public void Add_UniversalResourceElementWithMultipleStrings_OnlyFirstValueIsUsed()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(1, data.Length);
            Assert.Equal("a", data.First());
        }

        [Fact]
        public void Add_PersonName_MultipleNames_YieldsMultipleValues()
        {
            var tag = DicomTag.PerformingPhysicianName;
            var dataset = new DicomDataset();
            dataset.Add(
                tag,
                "Gustafsson^Anders^L",
                "Yates^Ian",
                "Desouky^Hesham",
                "Horn^Chris");

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(4, data.Length);
            Assert.Equal("Desouky^Hesham", data[2]);
        }

        [Fact]
        public void Read_empty_tag()
        {
            var dataset = new DicomDataset();
            Exception ex = null;

            try
            {
                dataset.Get<int>(DicomTag.MetersetRate);
            }
            catch (ArgumentException e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
            Assert.True(ex.Message.Contains("Value is null. Cannot convert"));
        }

        [Theory]
        [MemberData("MultiVMStringTags")]
        public void Add_MultiVMStringTags_YieldsMultipleValues(DicomTag tag, string[] values, Type expectedType)
        {
            var dataset = new DicomDataset();
            dataset.Add(tag, values);

            Assert.IsType(expectedType, dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(values.Length, data.Length);
            Assert.Equal(values.Last(), data.Last());
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> MultiVMStringTags
        {
            get
            {
                yield return
                    new object[]
                        {
                            DicomTag.ReferencedFrameNumber, new[] { "3", "5", "8" },
                            typeof(DicomIntegerString)
                        };
                yield return
                    new object[]
                        {
                            DicomTag.EventElapsedTimes, new[] { "3.2", "5.8", "8.7" },
                            typeof(DicomDecimalString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.PatientTelephoneNumbers, new[] { "0271-22117", "070-669 5073", "0270-11204" },
                            typeof(DicomShortString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.EventTimerNames, new[] { "a", "b", "c", "e", "f" },
                            typeof(DicomLongString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.ConsultingPhysicianName, new[] { "a", "b", "c", "e", "f" },
                            typeof(DicomPersonName)
                        };
                yield return
                new object[]
                        {
                            DicomTag.SOPClassesSupported, new[] { "1.2.3", "4.5.6", "7.8.8.9" },
                            typeof(DicomUniqueIdentifier)
                        };
            }
        }

        #endregion
    }
}